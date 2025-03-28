using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransparencySlider : MonoBehaviour
{
    public Renderer targetRenderer;
    private Material material;
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.minValue = 0f;
        slider.maxValue = 1.2f;
        slider.value = 1.15f; // Начальное значение 1.1 (Opaque)
        slider.onValueChanged.AddListener(UpdateTransparency);

        if (targetRenderer != null)
        {
            material = new Material(targetRenderer.material);
            targetRenderer.material = material;
            
            // Инициализируем начальное состояние (Opaque, так как slider.value = 1.1)
            UpdateMaterialSettings(slider.value);
        }
    }

    private void UpdateTransparency(float value)
    {
        if (material != null)
        {
            Color color = material.color;
            color.a = Mathf.Clamp(value, 0f, 1f); // Альфа всегда между 0 и 1
            material.color = color;
            
            UpdateMaterialSettings(value);
        }
    }

    private void UpdateMaterialSettings(float sliderValue)
    {
        bool shouldBeTransparent = sliderValue < 1f;
        
        // Устанавливаем Surface Type
        material.SetFloat("_Surface", shouldBeTransparent ? 1 : 0); // 1 = Transparent, 0 = Opaque
        
        if (shouldBeTransparent)
        {
            // Настройки для прозрачного материала
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        }
        else
        {
            // Настройки для непрозрачного материала
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            material.SetInt("_ZWrite", 1);
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
        }
    }
}