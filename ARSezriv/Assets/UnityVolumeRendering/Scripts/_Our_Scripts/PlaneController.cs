using UnityEngine;
using UnityEngine.UI;
using UnityVolumeRendering;

public class PlaneController : MonoBehaviour
{
    [Header("Plane Reference")]
    [SerializeField] private Transform targetPlane;
    [SerializeField] private Transform Prostavka;
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

    [Header("Reset Control")]
    [SerializeField] private Toggle resetAllToggle;

    [Header("Component Control")]
    [SerializeField] private Toggle toggleComponentToggle;

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

        // Set initial slider values to zero (relative to parent)
        ResetSlidersToZero();

        // Reset toggle
        resetAllToggle.onValueChanged.AddListener(OnResetAllToggled);

        // Component control toggle
        toggleComponentToggle.onValueChanged.AddListener(ToggleComponent);
    }

    private void OnPositionXChanged(float value)
    {
        if (targetPlane != null && parent != null)
        {
            targetPlane.localPosition = Prostavka.localPosition + new Vector3(value, targetPlane.localPosition.y, targetPlane.localPosition.z);
        }
    }

    private void OnPositionYChanged(float value)
    {
        if (targetPlane != null && parent != null)
        {
            targetPlane.localPosition = Prostavka.localPosition + new Vector3(targetPlane.localPosition.x, value, targetPlane.localPosition.z);
        }
    }

    private void OnPositionZChanged(float value)
    {
        if (targetPlane != null && parent != null)
        {
            targetPlane.localPosition = Prostavka.localPosition + new Vector3(targetPlane.localPosition.x, targetPlane.localPosition.y, value);
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

        // Reset sliders to zero
        ResetSlidersToZero();
    }

    private void ResetSlidersToZero()
    {
        positionXSlider.value = 0;
        positionYSlider.value = 0;
        positionZSlider.value = 0;

        rotationXSlider.value = 0;
        rotationYSlider.value = 0;
        rotationZSlider.value = 0;
    }

    private void ToggleComponent(bool isOn)
    {
        if (targetPlane != null)
        {
            targetPlane.gameObject.SetActive(false);
            GameObject.FindObjectOfType<Import_VolumeObjectChecker>().RamkaorCube = 0;
        }
    }
}