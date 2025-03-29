using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityVolumeRendering;

public class CrossSection_box : MonoBehaviour
{
    public GameObject Cross_Section_Cube;
    [SerializeField] private Transform Parent;

    public VolumeRenderedObject volumeObject;

    public GameObject Gen_Object;

    public CrossSectionManager crossSectionManager;

   // public GameObject duplicatedObject;

    public CutoutBox crossSectionCube_SC;
    
    public Vector3 positionOffset;
    public void Start()
    {

        Cross_Section_Cube.SetActive(false);

    }
    IEnumerator WaitToEnableKostil()
    {
        Cross_Section_Cube.SetActive(false);
        yield return new WaitForEndOfFrame();
        Cross_Section_Cube.SetActive(true);
        yield break;
    }

    public void CreateSlicer_for_YSI()
    {
        GameObject.FindObjectOfType<CrossSectionCreator>().off_slicer();
        //GameObject.FindObjectOfType<PlaneController>().ToggleComponent();
        Cross_Section_Cube.SetActive(true);
        GameObject.FindObjectOfType<Import_VolumeObjectChecker>().RamkaorCube = 2;

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
        crossSectionCube_SC = Cross_Section_Cube.GetComponent<UnityVolumeRendering.CutoutBox>();

        crossSectionCube_SC.targetObject = volumeObject;
        Parent.position = Gen_Object.transform.position + positionOffset;
        Parent.rotation = Gen_Object.transform.rotation;
        StartCoroutine(WaitToEnableKostil());

    }
    private void Update()
    {
        if (Cross_Section_Cube.activeInHierarchy == true && Gen_Object != null)
        {
            Parent.position = Gen_Object.transform.position;
            Parent.rotation = Gen_Object.transform.rotation;
        }

        if (Gen_Object == null)
        {
            Cross_Section_Cube.SetActive(false);
        }
    }

    public void off_slicer()
    {
        Cross_Section_Cube.SetActive(false);
    }
}