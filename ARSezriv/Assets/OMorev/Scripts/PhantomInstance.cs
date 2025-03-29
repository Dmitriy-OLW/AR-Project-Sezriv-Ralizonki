using Meta.XR.BuildingBlocks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomInstance : MonoBehaviour
{
    AnchoringManager anchoringManager;
    private void Start()
    {
        anchoringManager = FindObjectOfType<AnchoringManager>();
        if(transform.parent == null)
        {
            anchoringManager.spatialAnchorSpawnerBuildingBlock.gameObject.SetActive(false);
            anchoringManager.buttonsMapper.gameObject.SetActive(false);
            foreach(PhantomInstance phantom in FindObjectsOfType<PhantomInstance>())
            {
                if(phantom != this)
                {
                    anchoringManager.SetOnHandAnchor(phantom);
                }
            }
            PlacedPhantomMark mark = gameObject.AddComponent<PlacedPhantomMark>();
        }
    }


    
}
