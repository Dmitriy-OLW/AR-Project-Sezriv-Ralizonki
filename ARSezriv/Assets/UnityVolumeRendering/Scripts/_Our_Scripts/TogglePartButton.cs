using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Скрипт для кнопок, которые включают/выключают составные объекты
public class TogglePartButton : MonoBehaviour
{
    public GameObject partToToggle;

    private void Start()
    {
        GetComponent<Toggle>().onValueChanged.AddListener(TogglePart);
    }

    private void TogglePart(bool isOn)
    {
        if (partToToggle != null)
        {
            partToToggle.SetActive(isOn);
        }
    }
}