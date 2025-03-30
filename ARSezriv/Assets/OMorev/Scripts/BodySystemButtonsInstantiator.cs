using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySystemButtonsInstantiator : MonoBehaviour
{
    [SerializeField] BodySystems.TypeOfBody _bodyType;
    [SerializeField] GameObject _bodySystemButtonAndSliderPrefab;


    List<GameObject> _currentButtons = new List<GameObject>();
    private void OnEnable()
    {
        InstantiateAllBodySystemButtons(FindBodySystems(_bodyType));
    }
    private void OnDisable()
    {
        DestroyAllBodySystemButtons();
    }

    void InstantiateAllBodySystemButtons(BodySystems body)
    {
        foreach(BodySystem system in body.BodySystemsList)
        {
            GameObject b = Instantiate(_bodySystemButtonAndSliderPrefab, transform);
            _currentButtons.Add(b);
            b.GetComponent<BodySystemButtonSliderPair>().SetBodySystem(system);
        }
    }
    void DestroyAllBodySystemButtons()
    {
        foreach(GameObject button in _currentButtons)
        {
            Destroy(button);
        }
        _currentButtons.Clear();
    }
    BodySystems FindBodySystems(BodySystems.TypeOfBody bodyType)
    {
        foreach(BodySystems body in FindObjectsOfType<BodySystems>())
        {
            if (body.BodyType == bodyType) return body;
        }
        return null;
    }
}
