using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityVolumeRendering;

public class VisibilityModeManager : MonoBehaviour
{
    GameObject _defaultModel;
    [SerializeField] GameObject _simpleModel;
    [SerializeField] GameObject _difficultModel;
    GameObject _datasetModel;


    bool simpleModelStateContainer = false;
    bool difficultModelStateContainer = false;
    bool datasetModelStateContainer = false;

    private void Update()
    {
        if(_datasetModel == null)
        {

            try
            {
                _datasetModel = FindObjectOfType<VolumeRenderedObject>().gameObject;
            }
            catch { }
        }
        if(_defaultModel == null)
        {
            try
            {
                _defaultModel = FindObjectOfType<PlacedPhantomMark>().gameObject;
            }
            catch { }
            _difficultModel.SetActive(false);
            _simpleModel.SetActive(false);
            if(_datasetModel != null) _datasetModel.SetActive(false);
        }
        else
        {
            _defaultModel.SetActive(!(_difficultModel.activeSelf || _datasetModel.activeSelf || _simpleModel.activeSelf));
        }




    }

    public void ActivateActiveModes()
    {
        _simpleModel.SetActive(simpleModelStateContainer);
        _difficultModel.SetActive(difficultModelStateContainer);
        _datasetModel.SetActive(datasetModelStateContainer);
    }
    public void SetSimpleModelVisibilityState(bool state)
    {
        _simpleModel.SetActive(state);
        simpleModelStateContainer = state;
    }
    public void SetDifficultModelVisibilityState(bool state)
    {
        _difficultModel.SetActive(state);
        difficultModelStateContainer = state;
    }
    public void SetDatasetModelVisibilityState(bool state)
    {
        _datasetModel.SetActive(state);
        datasetModelStateContainer = state;
    }
}
