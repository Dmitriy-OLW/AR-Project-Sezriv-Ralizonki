using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityVolumeRendering;

public class VisibilityModeManager : MonoBehaviour
{
    [SerializeField] Toggle _simpleModelToggle;
    [SerializeField] Toggle _difficultModelToggle;
    [SerializeField] Toggle _datasetToggle;



    GameObject _defaultModel;
    GameObject _simpleModel;
    GameObject _difficultModel;
    GameObject _datasetModel;


    bool simpleModelStateContainer = false;
    bool difficultModelStateContainer = false;
    bool datasetModelStateContainer = false;


    private void Start()
    {
        _simpleModelToggle.isOn = false;
        _difficultModelToggle.isOn = true;
        _datasetToggle.isOn = true;

        _simpleModelToggle.onValueChanged.AddListener((isOn) =>
        {
            SetSimpleModelVisibilityState(isOn);
        });
        _difficultModelToggle.onValueChanged.AddListener((isOn) =>
        {
            SetDifficultModelVisibilityState(isOn);
        });
        _datasetToggle.onValueChanged.AddListener((isOn) =>
        {
            SetDatasetModelVisibilityState(isOn);
        });
    }
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
                _difficultModel = _defaultModel.GetComponent<PhantomInstance>()._difficultModel;
                _simpleModel = _defaultModel.GetComponent<PhantomInstance>()._simpleModel;
            }
            catch { }
            if(_datasetModel != null) _datasetModel.SetActive(false);
        }
        //else
        //{
        //    _defaultModel.GetComponent<MeshRenderer>().enabled = (!(_difficultModel.activeSelf || _datasetModel.activeSelf || _simpleModel.activeSelf));
        //}
        if (_simpleModel.activeInHierarchy == true || _difficultModel.activeInHierarchy == true || (_datasetModel == null ? false : (_datasetModel.activeInHierarchy == true)))
        {
            _defaultModel.GetComponent<MeshRenderer>().enabled = false;
        }
        if (_simpleModel.activeInHierarchy == false && _difficultModel.activeInHierarchy == false && (_datasetModel == null ? true : (_datasetModel.activeInHierarchy == false)))
        {
            _defaultModel.GetComponent<MeshRenderer>().enabled = true;
        }



    }

    public void ActivateActiveModes()
    {
        _simpleModel.SetActive(simpleModelStateContainer);
        _difficultModel.SetActive(difficultModelStateContainer);
        _datasetModel.SetActive(datasetModelStateContainer);
    }
    void SetSimpleModelVisibilityState(bool state)
    {
        _simpleModel.SetActive(state);
        simpleModelStateContainer = state;
    }
    void SetDifficultModelVisibilityState(bool state)
    {
        _difficultModel.SetActive(state);
        difficultModelStateContainer = state;
    }
    void SetDatasetModelVisibilityState(bool state)
    {
        _datasetModel.SetActive(state);
        datasetModelStateContainer = state;
    }



}
