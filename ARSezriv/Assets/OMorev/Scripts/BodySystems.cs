using System;
using System.Collections.Generic;
using UnityEngine;

public class BodySystems : MonoBehaviour
{
    [SerializeField] List<BodySystem> _bodySystemsList;

    public List<BodySystem> BodySystemsList
    {
        get => _bodySystemsList;
    }


    [SerializeField] TypeOfBody typeOfBody;

    public TypeOfBody BodyType
    {
        get => typeOfBody;
    }
    public enum TypeOfBody
    {
        simple, difficult
    }

}


[Serializable]
public class BodySystem
{
    [SerializeField] string systemName;
    [SerializeField] GameObject systemObject;

    public string SystemName
    {
        get => systemName;
    }
    public GameObject SystemObject
    {
        get => systemObject;
    }
}
