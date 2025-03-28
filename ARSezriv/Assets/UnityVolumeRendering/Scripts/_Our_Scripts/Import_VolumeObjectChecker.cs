using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityVolumeRendering;

public class Import_VolumeObjectChecker : MonoBehaviour
{
    [Header("Сюда объект, к которому притягивается датасет")]
    public GameObject targetObject; // Целевой объект в сцене
    public float disableDelay = 10f; // Задержка перед отключением (10 сек)
    
    [Header("Дополнительное смещение и поворот")]
    public Vector3 positionOffset = Vector3.zero;
    public Vector3 rotationOffset = Vector3.zero;
    
    [Header("В скрипте добавлять компонет для захвата датасета руками")]
    private bool isActive = true;
    private float timer = 0f;
    private bool timerRunning = false;
    private bool objectFound = false;
    private VolumeRenderedObject currentVolumeObject; // Текущий обрабатываемый объект

    void FixedUpdate()
    {
        if (!isActive) return;

        // Если таймер запущен, отсчитываем время
        if (timerRunning)
        {
            timer += Time.deltaTime;
            if (timer >= disableDelay)
            {
                OnTimerEnd();
                SetActive(false);
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
                    OnObjectFound();
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
    private void OnObjectFound()
    {
        // Этот метод сработает только один раз при первом обнаружении объекта
        Debug.Log("Здесь писать добавление компонета для захвата датасета руками");
        objectFound = true;
        
        // Здесь можно добавить свою логику, которая должна выполниться один раз
    }
    
    // Пустой метод, вызываемый при завершении таймера
    private void OnTimerEnd()
    {
        
        GameObject.FindObjectOfType<CreateTFFile>().LoadFile();
    }

    // Метод для ручного включения
    public void SetActive(bool active)
    {
        objectFound = false;
        isActive = active;
        enabled = active;
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
        SetActive(true);
    }
}