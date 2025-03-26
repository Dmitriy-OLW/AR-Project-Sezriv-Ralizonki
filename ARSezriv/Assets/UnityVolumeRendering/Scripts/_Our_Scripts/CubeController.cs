using UnityEngine;
using UnityEngine.UI;
using UnityVolumeRendering;

public class CubeController : MonoBehaviour
{
    [Header("Cube Reference")]
    [SerializeField] private Transform targetCube;

    [Header("Movement Settings")]
    [SerializeField] private float moveStep = 0.5f;
    [SerializeField] private float scaleStep = 0.1f;
    [SerializeField] private float uniformScaleStep = 0.1f;
    [SerializeField] private Vector3 defaultScale = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] private float minScale = 0.001f;
    [SerializeField] private float sliceHeight = 0.003f;

    [Header("UI Sliders - Rotation")]
    [SerializeField] private Slider rotateXSlider;
    [SerializeField] private Slider rotateYSlider;
    [SerializeField] private Slider rotateZSlider;

    [Header("UI Buttons - Movement")]
    [SerializeField] private Toggle moveXPlus;
    [SerializeField] private Toggle moveXMinus;
    [SerializeField] private Toggle moveYPlus;
    [SerializeField] private Toggle moveYMinus;
    [SerializeField] private Toggle moveZPlus;
    [SerializeField] private Toggle moveZMinus;

    [Header("UI Buttons - Scale")]
    [SerializeField] private Toggle scaleXPlus;
    [SerializeField] private Toggle scaleXMinus;
    [SerializeField] private Toggle scaleYPlus;
    [SerializeField] private Toggle scaleYMinus;
    [SerializeField] private Toggle scaleZPlus;
    [SerializeField] private Toggle scaleZMinus;
    [SerializeField] private Toggle scaleUniformPlus;
    [SerializeField] private Toggle scaleUniformMinus;

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
        
        
        // Настройка кнопок перемещения
         moveXPlus.onValueChanged.AddListener((isOn) => { if(isOn) MoveCube(Vector3.right); });
        moveXMinus.onValueChanged.AddListener((isOn) => { if(isOn) MoveCube(Vector3.left); });
        moveYPlus.onValueChanged.AddListener((isOn) => { if(isOn) MoveCube(Vector3.up); });
        moveYMinus.onValueChanged.AddListener((isOn) => { if(isOn) MoveCube(Vector3.down); });
        moveZPlus.onValueChanged.AddListener((isOn) => { if(isOn) MoveCube(Vector3.forward); });
        moveZMinus.onValueChanged.AddListener((isOn) => { if(isOn) MoveCube(Vector3.back); });

        // Настройка кнопок масштабирования
        scaleXPlus.onValueChanged.AddListener((isOn) => { if(isOn) ScaleCube(Vector3.right, false); });
        scaleXMinus.onValueChanged.AddListener((isOn) => { if(isOn) ScaleCube(Vector3.left, false); });
        scaleYPlus.onValueChanged.AddListener((isOn) => { if(isOn) ScaleCube(Vector3.up, false); });
        scaleYMinus.onValueChanged.AddListener((isOn) => { if(isOn) ScaleCube(Vector3.down, false); });
        scaleZPlus.onValueChanged.AddListener((isOn) => { if(isOn) ScaleCube(Vector3.forward, false); });
        scaleZMinus.onValueChanged.AddListener((isOn) => { if(isOn) ScaleCube(Vector3.back, false); });
        scaleUniformPlus.onValueChanged.AddListener((isOn) => { if(isOn) ScaleCube(Vector3.one, true); });
        scaleUniformMinus.onValueChanged.AddListener((isOn) => { if(isOn) ScaleCube(-Vector3.one, true); });

        // Настройка кнопок сброса
        resetPosition.onValueChanged.AddListener((isOn) => { if(isOn) ResetPosition(); });
        resetRotation.onValueChanged.AddListener((isOn) => { if(isOn) ResetRotation(); });
        resetScale.onValueChanged.AddListener((isOn) => { if(isOn) ResetScale(); });
        resetAll.onValueChanged.AddListener((isOn) => { if(isOn) ResetAll(); });

        // Настройка специальных кнопок
        sliceButton.onValueChanged.AddListener((isOn) => { if(isOn) SliceObject(); });
        toggleSliceButton.onValueChanged.AddListener((isOn) => OffSlice(isOn));
    }

    private void MoveCube(Vector3 direction)
    {
        if (targetCube != null)
        {
            targetCube.Translate(direction * moveStep, Space.World);
        }
    }

    private void RotateCube(Vector3 axis, float angle)
    {
        if (targetCube != null)
        {
            // Вычисляем разницу между текущим углом и начальным
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

    private void ScaleCube(Vector3 axis, bool isUniform)
    {
        if (targetCube == null) return;

        Vector3 scaleChange = isUniform ? axis * uniformScaleStep : axis * scaleStep;
        Vector3 newScale = targetCube.localScale + scaleChange;

        // Ограничение минимального масштаба
        newScale.x = Mathf.Max(newScale.x, minScale);
        newScale.y = Mathf.Max(newScale.y, minScale);
        newScale.z = Mathf.Max(newScale.z, minScale);

        targetCube.localScale = newScale;
    }

    private void SliceObject()
    {
        if (targetCube == null) return;

        Vector3 newScale = targetCube.localScale;
        newScale.y = sliceHeight;
        targetCube.localScale = newScale;
        isSliced = true;
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
        }
    }

    private void ResetRotation()
    {
        if (targetCube != null)
        {
            targetCube.rotation = Quaternion.Euler(initialRotation);
            // Сбрасываем слайдеры
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
        }
    }

    private void ResetAll()
    {
        ResetPosition();
        ResetRotation();
        ResetScale();
    }
}