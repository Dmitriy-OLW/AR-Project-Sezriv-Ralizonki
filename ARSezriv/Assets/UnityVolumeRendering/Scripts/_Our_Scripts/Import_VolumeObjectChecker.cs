using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityVolumeRendering;
using UnityEngine.UI;

public class Import_VolumeObjectChecker : MonoBehaviour
{
    //[Header("Сюда объект, к которому притягивается датасет")]
    public GameObject targetObject { get; private set; } // Целевой объект в сцене
    public float disableDelay = 10f; // Задержка перед отключением (10 сек)
    public GameObject founddataset;
    
    
    [Header("Дополнительное смещение и поворот")]
    public Vector3 positionOffset = Vector3.zero;
    public Vector3 rotationOffset = Vector3.zero;
    
    [SerializeField] private Toggle toggleComponentToggle;
    [SerializeField] private Vector3 Usi_scale = new Vector3(0.1f, 0.1f, 0.1f);
    
    [Header("В скрипте добавлять компонет для захвата датасета руками")]
    private bool isActive = true;
    private float timer = 0f;
    private bool timerRunning = false;
    private bool objectFound = false;
    private VolumeRenderedObject currentVolumeObject; // Текущий обрабатываемый объект
    private bool ItIsUSI = false;
    public int RamkaorCube = 0;
    public GameObject[] Buttons_off_edith;
    public Slider[] dataset_slider_rotation;
    public Slider[] dataset_slider_position;
    


    private void Start()
    {
        foreach (GameObject obg in Buttons_off_edith)
        {
            obg.SetActive(false);
        }
        foreach (Slider slider in dataset_slider_rotation)
        {
            slider.minValue = 0;
            slider.minValue = 360;
        }
        foreach (Slider slider in dataset_slider_position)
        {
            slider.minValue = -0.3f;
            slider.maxValue = 0.3f;
        }

        dataset_slider_rotation[0].value = Math.Abs(rotationOffset.x);
        dataset_slider_rotation[1].value = Math.Abs(rotationOffset.y);
        dataset_slider_rotation[2].value = Math.Abs(rotationOffset.z);
        dataset_slider_position[0].value = positionOffset.x;
        dataset_slider_position[1].value = positionOffset.y;
        dataset_slider_position[2].value = positionOffset.z;
        toggleComponentToggle.onValueChanged.AddListener(ToggleMeshRenderer);
    }


    void FixedUpdate()
    {

        rotationOffset.x = dataset_slider_rotation[0].value;
        rotationOffset.y = dataset_slider_rotation[1].value;
        rotationOffset.z = dataset_slider_rotation[2].value;
        positionOffset.x = dataset_slider_position[0].value;
        positionOffset.y = dataset_slider_position[1].value;
        positionOffset.z = dataset_slider_position[2].value;
        
        if (targetObject == null)
        { 
            try
            {
                targetObject = GameObject.FindObjectOfType<PlacedPhantomMark>().gameObject;
            }catch { }
        }
        else
        {
            if ((founddataset != null) && !isActive)
            {
                // Перемещаем объект с учетом смещения
                founddataset.transform.position = targetObject.transform.position + positionOffset;

                // Поворачиваем объект с учетом смещения
                founddataset.transform.rotation = targetObject.transform.rotation * Quaternion.Euler(rotationOffset);
            }
        }

        
        if (!isActive) return;

        // Если таймер запущен, отсчитываем время
        if (timerRunning)
        {
            timer += Time.deltaTime;
            if (timer >= disableDelay)
            {
                OnTimerEnd(); 
                _SetActive(false);
                return;
            }
        }

        VolumeRenderedObject[] volumeObjects = FindObjectsOfType<VolumeRenderedObject>(true);
        
        foreach (VolumeRenderedObject volObj in volumeObjects)
        {
            if (volObj.gameObject != targetObject && volObj.gameObject != this.gameObject)
            {
                currentVolumeObject = volObj;
                
                // Перемещаем объект с учетом смещения
                volObj.transform.position = targetObject.transform.position + positionOffset;
                
                // Поворачиваем объект с учетом смещения
                volObj.transform.rotation = targetObject.transform.rotation * Quaternion.Euler(rotationOffset);
                
                Debug.Log($"Объект {volObj.name} перемещен к {targetObject.name}");

                if (!objectFound)
                {
                    founddataset = volObj.gameObject;
                    OnObjectFound(volObj.gameObject);
                }
                
                timerRunning = true;
                return;
            }
        }
    }
    
    // Метод для обновления смещения в реальном времени
    public void UpdateOffset(Vector3 newPositionOffset, Vector3 newRotationOffset)
    {
        positionOffset = newPositionOffset;
        rotationOffset = newRotationOffset;
        
        if (currentVolumeObject != null)
        {
            currentVolumeObject.transform.position = targetObject.transform.position + positionOffset;
            currentVolumeObject.transform.rotation = targetObject.transform.rotation * Quaternion.Euler(rotationOffset);
        }
    }
    
    // Пустой метод, вызываемый при первом обнаружении объекта
    private void OnObjectFound(GameObject volObj)
    {
        //добавление необходимых компонентов
        //volObj.AddComponent<MetaSDK_Compounent>(); Напищи свои компонеты
        
        
        
        // Этот метод сработает только один раз при первом обнаружении объекта
        Debug.Log("Здесь писать добавление компонета для захвата датасета руками");
        objectFound = true;

    }
    
    // Пустой метод, вызываемый при завершении таймера
    private void OnTimerEnd()
    {

        GameObject.FindObjectOfType<CreateTFFile>().LoadFile();
        if (ItIsUSI)
        {
            founddataset.transform.localScale = Usi_scale;
            GameObject.FindObjectOfType<CrossSection_box>().CreateSlicer_for_YSI();
            GameObject.FindObjectOfType<CubeController>().ApplyCustomScale();
        }
        foreach (GameObject obg in Buttons_off_edith)
        {
            obg.SetActive(true);
        }
    }

    // Метод для ручного включения
    public void _SetActive(bool active)
    {
        objectFound = false;
        isActive = active;
        //enabled = active;
        timerRunning = false;
        timer = 0f;
        
        if (active)
        {
            Debug.Log("Скрипт активирован");
        }
        else
        {
            Debug.Log("Скрипт деактивирован");
        }
    }

    // Метод для временной активации с автоотключением
    public void ActivateTemporarily(float duration)
    {
        disableDelay = duration;
        _SetActive(true);
    }
    
    public void ToggleMeshRenderer(bool isOn)
    {
        if (founddataset == null)
        {
            Debug.LogWarning("No dataset found to toggle MeshRenderer");
            return;
        }

        // Ищем MeshRenderer в дочерних объектах
        MeshRenderer meshRenderer = founddataset.GetComponentInChildren<MeshRenderer>(true);
        
        if (meshRenderer != null)
        {
            meshRenderer.enabled = isOn;
            if (isOn == true)
            {
                if (RamkaorCube == 1)
                {
                    GameObject.FindObjectOfType<CrossSectionCreator>().CreateSlicer();
                }
                if (RamkaorCube == 2)
                {
                    GameObject.FindObjectOfType<CrossSection_box>().CreateSlicer_for_YSI();
                }
            }

            if (isOn == false)
            {
                GameObject.FindObjectOfType<CrossSectionCreator>().off_slicer();
                GameObject.FindObjectOfType<CrossSection_box>().off_slicer();
            }
            
            Debug.Log($"MeshRenderer {(isOn ? "enabled" : "disabled")}");
        }
        else
        {
            Debug.LogWarning("MeshRenderer not found in dataset children");
        }
    }

    public void Usi_Bool(bool value)
    {
        ItIsUSI = value;
    }


}