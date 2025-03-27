using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityVolumeRendering;

public class CrossSectionCreator : MonoBehaviour
{
    public GameObject Cross_Section_Plane;
    [SerializeField] private Transform Parent;

    public VolumeRenderedObject volumeObject;

    public GameObject Gen_Object;

    public CrossSectionManager crossSectionManager;

    public GameObject duplicatedObject;

    public CrossSectionPlane crossSectionPlane_SC;
    public void Start()
    {

        Cross_Section_Plane.SetActive(false);

    }
    IEnumerator WaitToEnableKostil()
    {
        Cross_Section_Plane.SetActive(false);
        yield return new WaitForEndOfFrame();
        Cross_Section_Plane.SetActive(true);
        yield break;
    }

    public void CreateSlicer()
    {
        Cross_Section_Plane.SetActive(true);

        //GameObject.FindObjectOfType<CubeController>().OffSlice(true);

        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name.StartsWith("VolumeRenderedObject"))
            {
                Debug.Log("Найден подходящий объект: " + obj.name);
                Gen_Object = obj;
                volumeObject = obj.GetComponent<UnityVolumeRendering.VolumeRenderedObject>();
                break;
            }
        }
        if (volumeObject == null)
        {
            Debug.LogError("Не найден объект с компонентом VolumeRenderedObject!"); return;
        }

        try
        {
            Destroy(Gen_Object.GetComponent<UnityVolumeRendering.CrossSectionManager>());
        }
        catch{ }

        crossSectionManager = Gen_Object.AddComponent<UnityVolumeRendering.CrossSectionManager>();
        crossSectionPlane_SC = Cross_Section_Plane.GetComponent<UnityVolumeRendering.CrossSectionPlane>();
        Parent.position = Gen_Object.transform.position;
        crossSectionPlane_SC.targetObject = volumeObject;
        StartCoroutine(WaitToEnableKostil());

    }
}