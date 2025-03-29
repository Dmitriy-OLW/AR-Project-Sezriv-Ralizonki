using UnityEngine;
using UnityEngine.UI;
using UnityVolumeRendering;

public class CubeController : MonoBehaviour
{
    [Header("Plane Reference")]
    [SerializeField] private Transform targetPlane;
    [SerializeField] private Transform parent;

    [Header("Movement Settings")]
    [SerializeField] private float positionOffset = 1f;

    [Header("Position Sliders")]
    [SerializeField] private Slider positionXSlider;
    [SerializeField] private Slider positionYSlider;
    [SerializeField] private Slider positionZSlider;

    [Header("Rotation Sliders")]
    [SerializeField] private Slider rotationXSlider;
    [SerializeField] private Slider rotationYSlider;
    [SerializeField] private Slider rotationZSlider;

    [Header("Scale Sliders")]
    [SerializeField] private Slider scaleXSlider;
    [SerializeField] private Slider scaleYSlider;
    [SerializeField] private Slider scaleZSlider;
    [SerializeField] private Slider uniformScaleSlider;

    [Header("Reset Control")]
    [SerializeField] private Toggle resetAllToggle;

    [Header("Component Control")]
    [SerializeField] private Toggle toggleComponentToggle;
    [SerializeField] private Toggle smallScaleToggle;

    private Vector3 initialScale = new Vector3(0.5f, 0.5f, 0.5f);
    private Vector3 smallScale = new Vector3(0.5f, 0.003f, 0.5f);
    private const float minScale = 0.001f;
    private const float maxScale = 1f;
    
    [Header("Custom Scale Settings")]
    [SerializeField] private Vector3 customScale = new Vector3(0.6751387f, 0.244536f, 0.8415973f);
    [SerializeField] private float scaleFactor = 0.1f;

    private void Start()
    {
        // Initialize position sliders
        positionXSlider.minValue = -positionOffset;
        positionXSlider.maxValue = positionOffset;
        positionYSlider.minValue = -positionOffset;
        positionYSlider.maxValue = positionOffset;
        positionZSlider.minValue = -positionOffset;
        positionZSlider.maxValue = positionOffset;

        positionXSlider.onValueChanged.AddListener(OnPositionXChanged);
        positionYSlider.onValueChanged.AddListener(OnPositionYChanged);
        positionZSlider.onValueChanged.AddListener(OnPositionZChanged);

        // Initialize rotation sliders
        rotationXSlider.minValue = 0;
        rotationXSlider.maxValue = 360f;
        rotationYSlider.minValue = 0;
        rotationYSlider.maxValue = 360f;
        rotationZSlider.minValue = 0;
        rotationZSlider.maxValue = 360f;

        rotationXSlider.onValueChanged.AddListener(OnRotationXChanged);
        rotationYSlider.onValueChanged.AddListener(OnRotationYChanged);
        rotationZSlider.onValueChanged.AddListener(OnRotationZChanged);

        // Initialize scale sliders
        scaleXSlider.minValue = minScale;
        scaleXSlider.maxValue = maxScale;
        scaleYSlider.minValue = minScale;
        scaleYSlider.maxValue = maxScale;
        scaleZSlider.minValue = minScale;
        scaleZSlider.maxValue = maxScale;
        uniformScaleSlider.minValue = minScale;
        uniformScaleSlider.maxValue = maxScale;

        scaleXSlider.onValueChanged.AddListener(OnScaleXChanged);
        scaleYSlider.onValueChanged.AddListener(OnScaleYChanged);
        scaleZSlider.onValueChanged.AddListener(OnScaleZChanged);
        uniformScaleSlider.onValueChanged.AddListener(OnUniformScaleChanged);

        // Reset toggle
        resetAllToggle.onValueChanged.AddListener(OnResetAllToggled);

        // Component control toggle
        toggleComponentToggle.onValueChanged.AddListener(ToggleComponent);

        // Small scale toggle
        smallScaleToggle.onValueChanged.AddListener(OnSmallScaleToggled);

        // Set initial slider values to zero (relative to parent)
        ResetSlidersToZero();
    }

    private void OnPositionXChanged(float value)
    {
        if (targetPlane != null && parent != null)
        {
            targetPlane.position = parent.position + new Vector3(value, targetPlane.localPosition.y, targetPlane.localPosition.z);
        }
    }

    private void OnPositionYChanged(float value)
    {
        if (targetPlane != null && parent != null)
        {
            targetPlane.position = parent.position + new Vector3(targetPlane.localPosition.x, value, targetPlane.localPosition.z);
        }
    }

    private void OnPositionZChanged(float value)
    {
        if (targetPlane != null && parent != null)
        {
            targetPlane.position = parent.position + new Vector3(targetPlane.localPosition.x, targetPlane.localPosition.y, value);
        }
    }

    private void OnRotationXChanged(float value)
    {
        if (targetPlane != null)
        {
            if (targetPlane.localEulerAngles.y == 180 || targetPlane.localEulerAngles.z == 180)
            {
                targetPlane.localEulerAngles = new Vector3(value, 0, 0);
            }
            else
            {
                targetPlane.localEulerAngles = new Vector3(value, targetPlane.localEulerAngles.y, targetPlane.localEulerAngles.z);
            }
        }
    }

    private void OnRotationYChanged(float value)
    {
        if (targetPlane != null)
        {
            targetPlane.localEulerAngles = new Vector3(targetPlane.localEulerAngles.x, value, targetPlane.localEulerAngles.z);
        }
    }

    private void OnRotationZChanged(float value)
    {
        if (targetPlane != null)
        {
            targetPlane.localEulerAngles = new Vector3(targetPlane.localEulerAngles.x, targetPlane.localEulerAngles.y, value);
        }
    }

    private void OnScaleXChanged(float value)
    {
        if (targetPlane != null)
        {
            targetPlane.localScale = new Vector3(value, targetPlane.localScale.y, targetPlane.localScale.z);
            UpdateUniformScaleSlider();
        }
    }

    private void OnScaleYChanged(float value)
    {
        if (targetPlane != null)
        {
            targetPlane.localScale = new Vector3(targetPlane.localScale.x, value, targetPlane.localScale.z);
            UpdateUniformScaleSlider();
        }
    }

    private void OnScaleZChanged(float value)
    {
        if (targetPlane != null)
        {
            targetPlane.localScale = new Vector3(targetPlane.localScale.x, targetPlane.localScale.y, value);
            UpdateUniformScaleSlider();
        }
    }

    private void OnUniformScaleChanged(float value)
{
    if (targetPlane != null)
    {
        // Сохраняем текущие пропорции
        Vector3 originalScale = targetPlane.localScale;
        
        // Находим максимальный компонент текущего масштаба
        float maxComponent = Mathf.Max(originalScale.x, originalScale.y, originalScale.z);
        
        // Если все компоненты масштаба равны (уже uniform), просто применяем новое значение
        if (Mathf.Approximately(originalScale.x, originalScale.y) && 
            Mathf.Approximately(originalScale.y, originalScale.z))
        {
            Vector3 newScale = new Vector3(value, value, value);
            targetPlane.localScale = newScale;
        }
        else
        {
            // Рассчитываем коэффициенты пропорций
            float xRatio = originalScale.x / maxComponent;
            float yRatio = originalScale.y / maxComponent;
            float zRatio = originalScale.z / maxComponent;
            
            // Применяем новое значение с сохранением пропорций
            Vector3 newScale = new Vector3(
                Mathf.Clamp(value * xRatio, minScale, maxScale),
                Mathf.Clamp(value * yRatio, minScale, maxScale),
                Mathf.Clamp(value * zRatio, minScale, maxScale)
            );
            
            targetPlane.localScale = newScale;
            
            // Если какой-то компонент достиг предела, корректируем другие
            if (newScale.x >= maxScale || newScale.x <= minScale ||
                newScale.y >= maxScale || newScale.y <= minScale ||
                newScale.z >= maxScale || newScale.z <= minScale)
            {
                // Находим самый "проблемный" компонент (который первым достиг предела)
                float limitingFactor = 1f;
                if (newScale.x >= maxScale) limitingFactor = Mathf.Min(limitingFactor, maxScale / (value * xRatio));
                if (newScale.y >= maxScale) limitingFactor = Mathf.Min(limitingFactor, maxScale / (value * yRatio));
                if (newScale.z >= maxScale) limitingFactor = Mathf.Min(limitingFactor, maxScale / (value * zRatio));
                if (newScale.x <= minScale) limitingFactor = Mathf.Min(limitingFactor, minScale / (value * xRatio));
                if (newScale.y <= minScale) limitingFactor = Mathf.Min(limitingFactor, minScale / (value * yRatio));
                if (newScale.z <= minScale) limitingFactor = Mathf.Min(limitingFactor, minScale / (value * zRatio));
                
                // Применяем корректировку
                newScale = new Vector3(
                    value * xRatio * limitingFactor,
                    value * yRatio * limitingFactor,
                    value * zRatio * limitingFactor
                );
                targetPlane.localScale = newScale;
            }
        }
        
        // Обновляем слайдеры
        scaleXSlider.SetValueWithoutNotify(targetPlane.localScale.x);
        scaleYSlider.SetValueWithoutNotify(targetPlane.localScale.y);
        scaleZSlider.SetValueWithoutNotify(targetPlane.localScale.z);
    }
}

    private void UpdateUniformScaleSlider()
    {
        // Only update uniform scale slider if all scales are equal
        if (Mathf.Approximately(targetPlane.localScale.x, targetPlane.localScale.y) && 
            Mathf.Approximately(targetPlane.localScale.y, targetPlane.localScale.z))
        {
            uniformScaleSlider.SetValueWithoutNotify(targetPlane.localScale.x);
        }
    }

    private void OnResetAllToggled(bool isOn)
    {
        if (isOn)
        {
            ResetAll();
            resetAllToggle.isOn = false; // Automatically un-toggle after reset
        }
    }

    private void ResetAll()
    {
        if (targetPlane == null || parent == null)
            return;

        // Reset position and rotation to parent's values
        targetPlane.position = parent.position;
        targetPlane.rotation = parent.rotation;
        
        // Reset scale to initial value
        targetPlane.localScale = initialScale;
        
        // Reset sliders to zero/initial values
        ResetSlidersToZero();
        
        // Reset toggles
        smallScaleToggle.isOn = false;
    }

    private void ResetSlidersToZero()
    {
        positionXSlider.value = 0;
        positionYSlider.value = 0;
        positionZSlider.value = 0;
        
        rotationXSlider.value = 0;
        rotationYSlider.value = 0;
        rotationZSlider.value = 0;
        
        scaleXSlider.value = initialScale.x;
        scaleYSlider.value = initialScale.y;
        scaleZSlider.value = initialScale.z;
        uniformScaleSlider.value = initialScale.x;
    }

    private void ToggleComponent(bool isOn)
    {
        if (targetPlane != null)
        {
            targetPlane.gameObject.SetActive(false);
            GameObject.FindObjectOfType<Import_VolumeObjectChecker>().RamkaorCube = 0;
        }
    }

    private void OnSmallScaleToggled(bool isOn)
    {
        if (targetPlane != null)
        {
            targetPlane.localScale = isOn ? smallScale : initialScale;
            
            // Update sliders
            if (isOn)
            {
                scaleXSlider.SetValueWithoutNotify(smallScale.x);
                scaleYSlider.SetValueWithoutNotify(smallScale.y);
                scaleZSlider.SetValueWithoutNotify(smallScale.z);
                uniformScaleSlider.SetValueWithoutNotify(smallScale.x);
            }
            else
            {
                scaleXSlider.SetValueWithoutNotify(initialScale.x);
                scaleYSlider.SetValueWithoutNotify(initialScale.y);
                scaleZSlider.SetValueWithoutNotify(initialScale.z);
                uniformScaleSlider.SetValueWithoutNotify(initialScale.x);
            }
        }
    }

    public void ApplyCustomScale()
    {
        //targetPlane.localPosition = parent.localPosition + new Vector3(0.0073f, 0, -0.0048f);
        if (targetPlane != null)
        {
            Vector3 newScale = new Vector3(
                customScale.x * scaleFactor,
                customScale.y * scaleFactor,
                customScale.z * scaleFactor
            );
            
            targetPlane.localScale = newScale;
            
            // Update sliders to reflect the new scale
            scaleXSlider.SetValueWithoutNotify(newScale.x);
            scaleYSlider.SetValueWithoutNotify(newScale.y);
            scaleZSlider.SetValueWithoutNotify(newScale.z);
            uniformScaleSlider.SetValueWithoutNotify(newScale.x); // Assuming uniform scale should show X value

            positionXSlider.value = 0.009f;
            positionZSlider.value = -0.007f;
        }
    }
    
    public void SetCustomScale(Vector3 newScale, float newFactor)
    {
        customScale = newScale;
        scaleFactor = newFactor;
        ApplyCustomScale();
    }
}