using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Скрипт для слайдера прозрачности
public class TransparencySlider : MonoBehaviour
{
    public Renderer targetRenderer;
    private Material material;
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(UpdateTransparency);

        // Создаем копию материала, чтобы не изменять оригинальный
        if (targetRenderer != null)
        {
            material = new Material(targetRenderer.material);
            targetRenderer.material = material;
        }
    }

    private void UpdateTransparency(float value)
    {
        if (material != null)
        {
            Color color = material.color;
            color.a = value;
            material.color = color;
        }
    }
}