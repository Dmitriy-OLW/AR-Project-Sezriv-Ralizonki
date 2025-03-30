using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BodySystemButtonSliderPair : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _systemName;
    [SerializeField] TogglePartButton _button;
    [SerializeField] TransparencySlider _slider;

    public void SetBodySystem(BodySystem bodySystem)
    {
        _systemName.text = bodySystem.SystemName;
        _button.partToToggle = bodySystem.SystemObject;
        _slider.targetRenderer = bodySystem.SystemObject.GetComponent<MeshRenderer>();
    }
}
