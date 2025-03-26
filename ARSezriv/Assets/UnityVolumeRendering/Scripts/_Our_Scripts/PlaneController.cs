using UnityEngine;
using UnityEngine.UI;
using UnityVolumeRendering;

public class PlaneController : MonoBehaviour
{
    [Header("Plane Reference")]
    [SerializeField] private Transform targetPlane;

    [Header("Movement Settings")]
    [SerializeField] private float maxPositionOffset = 1f;
    [SerializeField] private Vector3 defaultPosition = Vector3.zero;
    [SerializeField] private Vector3 defaultScale = new Vector3(1f, 1f, 1f);
    [SerializeField] private float minScale = 0.001f;
    [SerializeField] private float maxScale = 5f;

    [Header("Position Sliders")]
    [SerializeField] private Slider positionXSlider;
    [SerializeField] private Slider positionYSlider;
    [SerializeField] private Slider positionZSlider;

    [Header("Rotation Sliders")]
    [SerializeField] private Slider rotationXSlider;
    [SerializeField] private Slider rotationYSlider;
    [SerializeField] private Slider rotationZSlider;
    [SerializeField] private float maxRotationAngle = 360f;

    [Header("Scale Sliders")]
    [SerializeField] private Slider scaleXSlider;
    [SerializeField] private Slider scaleYSlider;
    [SerializeField] private Slider scaleZSlider;
    [SerializeField] private Slider scaleUniformSlider;

    [Header("Reset Buttons")]
    [SerializeField] private Toggle resetPosition;
    [SerializeField] private Toggle resetRotation;
    [SerializeField] private Toggle resetScale;
    [SerializeField] private Toggle resetAll;

    [Header("Component Control")]
    [SerializeField] private Toggle toggleComponentButton;

    private Vector3 initialRotation;

    private void Start()
    {
        if (targetPlane != null)
        {
            initialRotation = targetPlane.eulerAngles;
        }

        // Initialize position sliders
        positionXSlider.minValue = -maxPositionOffset;
        positionXSlider.maxValue = maxPositionOffset;
        positionYSlider.minValue = -maxPositionOffset;
        positionYSlider.maxValue = maxPositionOffset;
        positionZSlider.minValue = -maxPositionOffset;
        positionZSlider.maxValue = maxPositionOffset;

        positionXSlider.onValueChanged.AddListener(OnPositionXChanged);
        positionYSlider.onValueChanged.AddListener(OnPositionYChanged);
        positionZSlider.onValueChanged.AddListener(OnPositionZChanged);

        // Initialize rotation sliders
        rotationXSlider.minValue = 0;
        rotationXSlider.maxValue = maxRotationAngle;
        rotationYSlider.minValue = 0;
        rotationYSlider.maxValue = maxRotationAngle;
        rotationZSlider.minValue = 0;
        rotationZSlider.maxValue = maxRotationAngle;

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
        scaleUniformSlider.minValue = minScale;
        scaleUniformSlider.maxValue = maxScale;

        scaleXSlider.onValueChanged.AddListener(OnScaleXChanged);
        scaleYSlider.onValueChanged.AddListener(OnScaleYChanged);
        scaleZSlider.onValueChanged.AddListener(OnScaleZChanged);
        scaleUniformSlider.onValueChanged.AddListener(OnUniformScaleChanged);

        // Set initial slider values
        if (targetPlane != null)
        {
            positionXSlider.value = targetPlane.position.x;
            positionYSlider.value = targetPlane.position.y;
            positionZSlider.value = targetPlane.position.z;

            rotationXSlider.value = initialRotation.x;
            rotationYSlider.value = initialRotation.y;
            rotationZSlider.value = initialRotation.z;

            scaleXSlider.value = targetPlane.localScale.x;
            scaleYSlider.value = targetPlane.localScale.y;
            scaleZSlider.value = targetPlane.localScale.z;
            scaleUniformSlider.value = (targetPlane.localScale.x + targetPlane.localScale.y + targetPlane.localScale.z) / 3f;
        }

        // Reset buttons
        resetPosition.onValueChanged.AddListener((isOn) => { if(isOn) ResetPosition(); });
        resetRotation.onValueChanged.AddListener((isOn) => { if(isOn) ResetRotation(); });
        resetScale.onValueChanged.AddListener((isOn) => { if(isOn) ResetScale(); });
        resetAll.onValueChanged.AddListener((isOn) => { if(isOn) ResetAll(); });

        // Component control button
        toggleComponentButton.onValueChanged.AddListener((isOn) => ToggleComponent());
    }

    private void OnPositionXChanged(float value)
    {
        if (targetPlane != null)
        {
            Vector3 position = targetPlane.position;
            position.x = value;
            targetPlane.position = position;
        }
    }

    private void OnPositionYChanged(float value)
    {
        if (targetPlane != null)
        {
            Vector3 position = targetPlane.position;
            position.y = value;
            targetPlane.position = position;
        }
    }

    private void OnPositionZChanged(float value)
    {
        if (targetPlane != null)
        {
            Vector3 position = targetPlane.position;
            position.z = value;
            targetPlane.position = position;
        }
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

    private void OnScaleXChanged(float value)
    {
        if (targetPlane != null)
        {
            Vector3 scale = targetPlane.localScale;
            scale.x = value;
            targetPlane.localScale = scale;
            scaleUniformSlider.value = (scale.x + scale.y + scale.z) / 3f;
        }
    }

    private void OnScaleYChanged(float value)
    {
        if (targetPlane != null)
        {
            Vector3 scale = targetPlane.localScale;
            scale.y = value;
            targetPlane.localScale = scale;
            scaleUniformSlider.value = (scale.x + scale.y + scale.z) / 3f;
        }
    }

    private void OnScaleZChanged(float value)
    {
        if (targetPlane != null)
        {
            Vector3 scale = targetPlane.localScale;
            scale.z = value;
            targetPlane.localScale = scale;
            scaleUniformSlider.value = (scale.x + scale.y + scale.z) / 3f;
        }
    }

    private void OnUniformScaleChanged(float value)
    {
        if (targetPlane != null)
        {
            targetPlane.localScale = new Vector3(value, value, value);
            scaleXSlider.value = value;
            scaleYSlider.value = value;
            scaleZSlider.value = value;
        }
    }

    private void ResetPosition()
    {
        if (targetPlane != null)
        {
            targetPlane.position = defaultPosition;
            positionXSlider.value = defaultPosition.x;
            positionYSlider.value = defaultPosition.y;
            positionZSlider.value = defaultPosition.z;
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
            scaleXSlider.value = defaultScale.x;
            scaleYSlider.value = defaultScale.y;
            scaleZSlider.value = defaultScale.z;
            scaleUniformSlider.value = (defaultScale.x + defaultScale.y + defaultScale.z) / 3f;
        }
    }

    private void ResetAll()
    {
        ResetPosition();
        ResetRotation();
        ResetScale();
    }

    public void ToggleComponent()
    {
        Debug.Log("Отключение среза или разреза");
        ResetAll();
        targetPlane.position = new Vector3(11, 0, 0);
        CrossSectionPlane crossSectionPlane_SC = targetPlane.gameObject.GetComponent<UnityVolumeRendering.CrossSectionPlane>();
        if (crossSectionPlane_SC != null)
        {
            crossSectionPlane_SC.targetObject = null;
        }
    }
}