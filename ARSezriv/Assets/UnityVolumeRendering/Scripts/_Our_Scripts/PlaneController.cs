using UnityEngine;
using UnityEngine.UI;

public class PlaneController : MonoBehaviour
{
    [Header("Plane Reference")]
    [SerializeField] private Transform targetPlane;

    [Header("Movement Settings")]
    [SerializeField] private float moveStep = 0.5f;
    [SerializeField] private float scaleStep = 0.1f;
    [SerializeField] private float uniformScaleStep = 0.1f;
    [SerializeField] private Vector3 defaultScale = new Vector3(1f, 1f, 1f);
    [SerializeField] private float minScale = 0.001f;

    [Header("Rotation Sliders")]
    [SerializeField] private Slider rotationXSlider;
    [SerializeField] private Slider rotationYSlider;
    [SerializeField] private Slider rotationZSlider;
    [SerializeField] private float maxRotationAngle = 360f;

    [Header("UI Buttons - Movement")]
    [SerializeField] private Button moveXPlus;
    [SerializeField] private Button moveXMinus;
    [SerializeField] private Button moveYPlus;
    [SerializeField] private Button moveYMinus;
    [SerializeField] private Button moveZPlus;
    [SerializeField] private Button moveZMinus;

    [Header("UI Buttons - Scale - Not Use")]
    [SerializeField] private Button scaleUniformPlus;
    [SerializeField] private Button scaleUniformMinus;

    [Header("UI Buttons - Reset")]
    [SerializeField] private Button resetPosition;
    [SerializeField] private Button resetRotation;
    [SerializeField] private Button resetScale;
    [SerializeField] private Button resetAll;

    [Header("Component Control")]
    [SerializeField] private Button toggleComponentButton;

    private Vector3 initialRotation = new Vector3(0, 0, 0);

    private void Start()
    {
        // Сохраняем начальный поворот объекта
        if (targetPlane != null)
        {
            initialRotation = targetPlane.eulerAngles;
        }

        // Инициализация слайдеров
        rotationXSlider.minValue = 0;
        rotationXSlider.maxValue = maxRotationAngle;
        rotationYSlider.minValue = 0;
        rotationYSlider.maxValue = maxRotationAngle;
        rotationZSlider.minValue = 0;
        rotationZSlider.maxValue = maxRotationAngle;

        rotationXSlider.onValueChanged.AddListener(OnRotationXChanged);
        rotationYSlider.onValueChanged.AddListener(OnRotationYChanged);
        rotationZSlider.onValueChanged.AddListener(OnRotationZChanged);

        // Установка начальных значений слайдеров
        if (targetPlane != null)
        {
            rotationXSlider.value = initialRotation.x;
            rotationYSlider.value = initialRotation.y;
            rotationZSlider.value = initialRotation.z;
        }

        // Movement buttons
        moveXPlus.onClick.AddListener(() => MovePlane(Vector3.right));
        moveXMinus.onClick.AddListener(() => MovePlane(Vector3.left));
        moveYPlus.onClick.AddListener(() => MovePlane(Vector3.up));
        moveYMinus.onClick.AddListener(() => MovePlane(Vector3.down));
        moveZPlus.onClick.AddListener(() => MovePlane(Vector3.forward));
        moveZMinus.onClick.AddListener(() => MovePlane(Vector3.back));


        scaleUniformPlus.onClick.AddListener(() => ScalePlane(Vector3.one, true));
        scaleUniformMinus.onClick.AddListener(() => ScalePlane(-Vector3.one, true));

        // Reset buttons
        resetPosition.onClick.AddListener(ResetPosition);
        resetRotation.onClick.AddListener(ResetRotation);
        resetScale.onClick.AddListener(ResetScale);
        resetAll.onClick.AddListener(ResetAll);

        // Component control button
        toggleComponentButton.onClick.AddListener(ToggleComponent);
    }

    private void OnRotationXChanged(float value)
    {
        if (targetPlane != null)
        {
            Vector3 currentRotation = targetPlane.eulerAngles;
            currentRotation.x = value;
            targetPlane.eulerAngles = currentRotation;
        }
    }

    private void OnRotationYChanged(float value)
    {
        if (targetPlane != null)
        {
            Vector3 currentRotation = targetPlane.eulerAngles;
            currentRotation.y = value;
            targetPlane.eulerAngles = currentRotation;
        }
    }

    private void OnRotationZChanged(float value)
    {
        if (targetPlane != null)
        {
            Vector3 currentRotation = targetPlane.eulerAngles;
            currentRotation.z = value;
            targetPlane.eulerAngles = currentRotation;
        }
    }

    private void MovePlane(Vector3 direction)
    {
        if (targetPlane != null)
        {
            targetPlane.Translate(direction * moveStep, Space.World);
        }
    }

    private void ScalePlane(Vector3 axis, bool isUniform)
    {
        if (targetPlane == null) return;

        Vector3 scaleChange = isUniform ? axis * uniformScaleStep : axis * scaleStep;
        Vector3 newScale = targetPlane.localScale + scaleChange;

        newScale.x = Mathf.Max(newScale.x, minScale);
        newScale.y = Mathf.Max(newScale.y, minScale);
        newScale.z = Mathf.Max(newScale.z, minScale);

        targetPlane.localScale = newScale;
    }

    private void ResetPosition()
    {
        if (targetPlane != null)
        {
            targetPlane.position = Vector3.zero;
        }
    }

    private void ResetRotation()
    {
        if (targetPlane != null)
        {
            targetPlane.rotation = Quaternion.identity;
            rotationXSlider.value = 0;
            rotationYSlider.value = 0;
            rotationZSlider.value = 0;
        }
    }

    private void ResetScale()
    {
        if (targetPlane != null)
        {
            targetPlane.localScale = defaultScale;
        }
    }

    private void ResetAll()
    {
        ResetPosition();
        ResetRotation();
        ResetScale();
    }

    private void ToggleComponent()
    {
        Debug.Log("Отключение среза или разреза");
        // В будущем здесь будет:
        // 1. Получение компонента (например: targetPlane.GetComponent<SliceComponent>())
        // 2. Управление компонентом (включение/выключение)
        // 3. Сброс параметров компонента при необходимости
    }
}