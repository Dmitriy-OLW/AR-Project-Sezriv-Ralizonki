using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityVolumeRendering;
using UnityEngine.UI;

public class CrossSection_for_USI : MonoBehaviour
{
    //Not usr this script
    
    
    public GameObject Cross_Section_Cube;
    public VolumeRenderedObject volumeObject;
    public GameObject Gen_Object;
    public CrossSectionManager crossSectionManager;
    public CutoutBox crossSectionCube_SC;
    public Vector3 positionOffset;
    
    public List<GameObject> USI_Dataset_Mas = new List<GameObject>();
    public List<GameObject> USI_CrosSection_Mas = new List<GameObject>();

    // Default scale values
    private Vector3 defaultScale = new Vector3(0.6751387f, 0.244536f, 0.8415973f);
    private const float minScale = 0.001f;

    // UI Elements
    [Header("UI Controls")]
    public Slider scaleXSlider;
    public Slider scaleYSlider;
    public Slider scaleZSlider;
    public Slider rotationXSlider;
    public Slider rotationYSlider;
    public Slider rotationZSlider;
    public Toggle resetButton;
    public Toggle sliceButton;
    public Toggle OffSliceButton;
    public Toggle uniformScaleToggle;

    private void Start()
    {
        // Initialize UI listeners
        if (scaleXSlider != null) scaleXSlider.onValueChanged.AddListener(OnScaleXChanged);
        if (scaleYSlider != null) scaleYSlider.onValueChanged.AddListener(OnScaleYChanged);
        if (scaleZSlider != null) scaleZSlider.onValueChanged.AddListener(OnScaleZChanged);
        if (rotationXSlider != null) rotationXSlider.onValueChanged.AddListener(OnRotationXChanged);
        if (rotationYSlider != null) rotationYSlider.onValueChanged.AddListener(OnRotationYChanged);
        if (rotationZSlider != null) rotationZSlider.onValueChanged.AddListener(OnRotationZChanged);
        if (resetButton != null) resetButton.onValueChanged.AddListener((isOn) => ResetAll());
        if (sliceButton != null) sliceButton.onValueChanged.AddListener((isOn) => ApplySlice());
        if (OffSliceButton != null) OffSliceButton.onValueChanged.AddListener((isOn) => OffSliceComponent());
    }

    IEnumerator WaitToEnableKostil()
    {
        Cross_Section_Cube.SetActive(false);
        yield return new WaitForEndOfFrame();
        Cross_Section_Cube.SetActive(true);
        yield break;
    }

    public void CreateSlicer_for_YSI()
    {
        //!!!!! Незабыть включить
        //GameObject.FindObjectOfType<PlaneController>().ToggleComponent();
        //GameObject.FindObjectOfType<CubeController>().OffSlice();

        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name.StartsWith("VolumeRenderedObject"))
            {
                Debug.Log("Найден подходящий объект: " + obj.name);
                Gen_Object = obj;
                volumeObject = obj.GetComponent<UnityVolumeRendering.VolumeRenderedObject>();
                
                if (volumeObject == null)
                {
                    Debug.LogError("Не найден объект с компонентом VolumeRenderedObject!"); return;
                }
                try
                {
                    Destroy(Gen_Object.GetComponent<UnityVolumeRendering.CrossSectionManager>());
                }
                catch{ }

                crossSectionManager = Gen_Object.AddComponent<UnityVolumeRendering.CrossSectionManager>();
                GameObject instans_CrossSection = Instantiate(Cross_Section_Cube, Gen_Object.transform.position, Quaternion.Euler(0,0,0), Gen_Object.transform);
                
                crossSectionCube_SC = instans_CrossSection.GetComponent<UnityVolumeRendering.CutoutBox>();
                crossSectionCube_SC.targetObject = volumeObject;
                instans_CrossSection.transform.position = instans_CrossSection.transform.position + positionOffset;
                instans_CrossSection.transform.localScale = defaultScale;
                
                StartCoroutine(WaitToEnableKostil());
                
                USI_Dataset_Mas.Add(Gen_Object);
                USI_CrosSection_Mas.Add(instans_CrossSection);
                    
                // Initialize sliders with current values
                UpdateSlidersWithCurrentValues();
            }
        }
    }

    private void UpdateSlidersWithCurrentValues()
    {
        if (USI_CrosSection_Mas.Count > 0)
        {
            Transform firstTransform = USI_CrosSection_Mas[0].transform;
            if (scaleXSlider != null) scaleXSlider.value = firstTransform.localScale.x;
            if (scaleYSlider != null) scaleYSlider.value = firstTransform.localScale.y;
            if (scaleZSlider != null) scaleZSlider.value = firstTransform.localScale.z;
            if (rotationXSlider != null) rotationXSlider.value = firstTransform.localEulerAngles.x;
            if (rotationYSlider != null) rotationYSlider.value = firstTransform.localEulerAngles.y;
            if (rotationZSlider != null) rotationZSlider.value = firstTransform.localEulerAngles.z;
        }
    }

    // Scale control methods
    public void OnScaleXChanged(float value)
    {
        float scaleValue = Mathf.Max(value, minScale);
        if (uniformScaleToggle != null && uniformScaleToggle.isOn)
        {
            SetUniformScale(new Vector3(scaleValue, scaleValue, scaleValue));
        }
        else
        {
            SetScaleForAll(new Vector3(scaleValue, -1, -1));
        }
    }

    public void OnScaleYChanged(float value)
    {
        float scaleValue = Mathf.Max(value, minScale);
        if (uniformScaleToggle != null && uniformScaleToggle.isOn)
        {
            SetUniformScale(new Vector3(scaleValue, scaleValue, scaleValue));
        }
        else
        {
            SetScaleForAll(new Vector3(-1, scaleValue, -1));
        }
    }

    public void OnScaleZChanged(float value)
    {
        float scaleValue = Mathf.Max(value, minScale);
        if (uniformScaleToggle != null && uniformScaleToggle.isOn)
        {
            SetUniformScale(new Vector3(scaleValue, scaleValue, scaleValue));
        }
        else
        {
            SetScaleForAll(new Vector3(-1, -1, scaleValue));
        }
    }

    private void SetScaleForAll(Vector3 newScale)
    {
        foreach (GameObject obj in USI_CrosSection_Mas)
        {
            if (obj != null)
            {
                Vector3 currentScale = obj.transform.localScale;
                if (newScale.x >= 0) currentScale.x = Mathf.Max(newScale.x, minScale);
                if (newScale.y >= 0) currentScale.y = Mathf.Max(newScale.y, minScale);
                if (newScale.z >= 0) currentScale.z = Mathf.Max(newScale.z, minScale);
                obj.transform.localScale = currentScale;
            }
        }
    }

    private void SetUniformScale(Vector3 newScale)
    {
        foreach (GameObject obj in USI_CrosSection_Mas)
        {
            if (obj != null)
            {
                obj.transform.localScale = newScale;
            }
        }
    }

    // Rotation control methods
    public void OnRotationXChanged(float value)
    {
        SetRotationForAll(new Vector3(value, -1, -1));
    }

    public void OnRotationYChanged(float value)
    {
        SetRotationForAll(new Vector3(-1, value, -1));
    }

    public void OnRotationZChanged(float value)
    {
        SetRotationForAll(new Vector3(-1, -1, value));
    }

    private void SetRotationForAll(Vector3 newRotation)
    {
        foreach (GameObject obj in USI_CrosSection_Mas)
        {
            if (obj != null)
            {
                Vector3 currentRotation = obj.transform.localEulerAngles;
                if (newRotation.x >= 0) currentRotation.x = newRotation.x;
                if (newRotation.y >= 0) currentRotation.y = newRotation.y;
                if (newRotation.z >= 0) currentRotation.z = newRotation.z;
                obj.transform.localEulerAngles = currentRotation;
            }
        }
    }

    // Reset function
    public void ResetAll()
    {
        foreach (GameObject obj in USI_CrosSection_Mas)
        {
            if (obj != null)
            {
                obj.transform.localPosition = positionOffset;
                obj.transform.localRotation = Quaternion.identity;
                obj.transform.localScale = defaultScale;
            }
        }
        UpdateSlidersWithCurrentValues();
    }

    // Slice function
    public void ApplySlice()
    {
        foreach (GameObject obj in USI_CrosSection_Mas)
        {
            if (obj != null)
            {
                Vector3 scale = obj.transform.localScale;
                scale.y = 0.003f;
                obj.transform.localScale = scale;
            }
        }
        if (scaleYSlider != null) scaleYSlider.value = 0.003f;
    }

    // Toggle slice component function
    public void OffSliceComponent()
    {
        Debug.Log("отключение среза или разреза");
        Debug.Log("Удаление всех объектов сечения и очистка списков");
    
        // Удаляем все объекты сечения со сцены
        foreach (GameObject crossSectionObj in USI_CrosSection_Mas)
        {
            if (crossSectionObj != null)
            {
                Destroy(crossSectionObj);
            }
        }
    
        // Очищаем список объектов сечения
        USI_CrosSection_Mas.Clear();
    
        // Очищаем список датасетов
        USI_Dataset_Mas.Clear();
    
        Debug.Log("Все объекты сечения удалены, списки очищены");
    }
}