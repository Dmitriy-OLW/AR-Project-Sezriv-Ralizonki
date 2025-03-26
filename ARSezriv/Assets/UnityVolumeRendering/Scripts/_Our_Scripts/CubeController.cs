using UnityEngine;
using UnityEngine.UI;
using UnityVolumeRendering;

public class CubeController : MonoBehaviour
{
    [Header("Cube Reference")]
    [SerializeField] private Transform targetCube;

    [Header("Movement Settings")]
    [SerializeField] private float maxMoveDistance = 1f;
    [SerializeField] private float maxScale = 2.0f;
    [SerializeField] private float minScale = 0.001f;
    [SerializeField] private Vector3 defaultScale = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] private float sliceHeight = 0.003f;

    [Header("UI Sliders - Rotation")]
    [SerializeField] private Slider rotateXSlider;
    [SerializeField] private Slider rotateYSlider;
    [SerializeField] private Slider rotateZSlider;

    [Header("UI Sliders - Position")]
    [SerializeField] private Slider positionXSlider;
    [SerializeField] private Slider positionYSlider;
    [SerializeField] private Slider positionZSlider;

    [Header("UI Sliders - Scale")]
    [SerializeField] private Slider scaleXSlider;
    [SerializeField] private Slider scaleYSlider;
    [SerializeField] private Slider scaleZSlider;
    [SerializeField] private Slider scaleUniformSlider;

    [Header("UI Buttons - Special Functions")]
    [SerializeField] private Toggle sliceButton;
    [SerializeField] private Toggle toggleSliceButton;

    [Header("UI Buttons - Reset")]
    [SerializeField] private Toggle resetPosition;
    [SerializeField] private Toggle resetRotation;
    [SerializeField] private Toggle resetScale;
    [SerializeField] private Toggle resetAll;

    private bool isSliced = false;
    private Vector3 initialRotation = new Vector3(0, 0, 0);

    private void Start()
    {
        // Сохраняем начальный поворот
        if (targetCube != null)
        {
            initialRotation = targetCube.eulerAngles;
        }

        
        // Настройка слайдеров вращения
        rotateXSlider.onValueChanged.AddListener((value) => RotateCube(Vector3.right, value));
        rotateYSlider.onValueChanged.AddListener((value) => RotateCube(Vector3.up, value));
        rotateZSlider.onValueChanged.AddListener((value) => RotateCube(Vector3.forward, value));

        // Настройка слайдеров позиции
        positionXSlider.minValue = -maxMoveDistance;
        positionXSlider.maxValue = maxMoveDistance;
        positionYSlider.minValue = -maxMoveDistance;
        positionYSlider.maxValue = maxMoveDistance;
        positionZSlider.minValue = -maxMoveDistance;
        positionZSlider.maxValue = maxMoveDistance;
        
        positionXSlider.onValueChanged.AddListener((value) => SetPosition(Vector3.right, value));
        positionYSlider.onValueChanged.AddListener((value) => SetPosition(Vector3.up, value));
        positionZSlider.onValueChanged.AddListener((value) => SetPosition(Vector3.forward, value));

        // Настройка слайдеров масштабирования
        scaleXSlider.minValue = minScale;
        scaleXSlider.maxValue = maxScale;
        scaleYSlider.minValue = minScale;
        scaleYSlider.maxValue = maxScale;
        scaleZSlider.minValue = minScale;
        scaleZSlider.maxValue = maxScale;
        scaleUniformSlider.minValue = minScale;
        scaleUniformSlider.maxValue = maxScale;
        
        scaleXSlider.onValueChanged.AddListener((value) => SetScale(Vector3.right, value));
        scaleYSlider.onValueChanged.AddListener((value) => SetScale(Vector3.up, value));
        scaleZSlider.onValueChanged.AddListener((value) => SetScale(Vector3.forward, value));
        scaleUniformSlider.onValueChanged.AddListener((value) => SetUniformScale(value));

        // Настройка кнопок сброса
        resetPosition.onValueChanged.AddListener((isOn) => { if(isOn) ResetPosition(); });
        resetRotation.onValueChanged.AddListener((isOn) => { if(isOn) ResetRotation(); });
        resetScale.onValueChanged.AddListener((isOn) => { if(isOn) ResetScale(); });
        resetAll.onValueChanged.AddListener((isOn) => { if(isOn) ResetAll(); });

        // Настройка специальных кнопок
        sliceButton.onValueChanged.AddListener((isOn) => { if(isOn) SliceObject(); });
        toggleSliceButton.onValueChanged.AddListener((isOn) => OffSlice(isOn));
    }

    private void SetPosition(Vector3 axis, float value)
    {
        if (targetCube != null)
        {
            Vector3 newPosition = targetCube.position;
            
            if (axis == Vector3.right) // X axis
                newPosition.x = value;
            else if (axis == Vector3.up) // Y axis
                newPosition.y = value;
            else if (axis == Vector3.forward) // Z axis
                newPosition.z = value;
                
            targetCube.position = newPosition;
        }
    }

    private void RotateCube(Vector3 axis, float angle)
    {
        if (targetCube != null)
        {
            Vector3 currentRotation = targetCube.eulerAngles;
            
            if (axis == Vector3.right) // X axis
            {
                targetCube.rotation = Quaternion.Euler(initialRotation.x + angle, currentRotation.y, currentRotation.z);
            }
            else if (axis == Vector3.up) // Y axis
            {
                targetCube.rotation = Quaternion.Euler(currentRotation.x, initialRotation.y + angle, currentRotation.z);
            }
            else if (axis == Vector3.forward) // Z axis
            {
                targetCube.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, initialRotation.z + angle);
            }
        }
    }

    private void SetScale(Vector3 axis, float value)
    {
        if (targetCube == null) return;

        Vector3 newScale = targetCube.localScale;
        
        if (axis == Vector3.right) // X axis
            newScale.x = value;
        else if (axis == Vector3.up) // Y axis
            newScale.y = value;
        else if (axis == Vector3.forward) // Z axis
            newScale.z = value;
            
        targetCube.localScale = newScale;
    }

    private void SetUniformScale(float value)
    {
        if (targetCube == null) return;
        targetCube.localScale = new Vector3(value, value, value);
    }

    private void SliceObject()
    {
        if (targetCube == null) return;

        Vector3 newScale = targetCube.localScale;
        newScale.y = sliceHeight;
        targetCube.localScale = newScale;
        isSliced = true;
        scaleYSlider.value = sliceHeight;
    }

    public void OffSlice(bool isOn)
    {
        if (!isOn) return;
        
        Debug.Log("Отключение среза или разреза");
        ResetAll();
        targetCube.position = new Vector3(10, 0, 0);
        targetCube.localScale = new Vector3(0.6751387f, 0.244536f, 0.8415973f);
        CutoutBox crossSectionCube_SC = targetCube.gameObject.GetComponent<UnityVolumeRendering.CutoutBox>();
        if (crossSectionCube_SC != null)
        {
            crossSectionCube_SC.targetObject = null;
        }
    }

    private void ResetPosition()
    {
        if (targetCube != null)
        {
            targetCube.position = Vector3.zero;
            positionXSlider.value = 0;
            positionYSlider.value = 0;
            positionZSlider.value = 0;
        }
    }

    private void ResetRotation()
    {
        if (targetCube != null)
        {
            targetCube.rotation = Quaternion.Euler(initialRotation);
            rotateXSlider.value = 0;
            rotateYSlider.value = 0;
            rotateZSlider.value = 0;
        }
    }

    private void ResetScale()
    {
        if (targetCube != null)
        {
            targetCube.localScale = defaultScale;
            scaleXSlider.value = defaultScale.x;
            scaleYSlider.value = defaultScale.y;
            scaleZSlider.value = defaultScale.z;
            scaleUniformSlider.value = defaultScale.x;
        }
    }

    private void ResetAll()
    {
        ResetPosition();
        ResetRotation();
        ResetScale();
    }
}