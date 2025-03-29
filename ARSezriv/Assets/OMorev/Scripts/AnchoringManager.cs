using Meta.XR.BuildingBlocks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchoringManager : MonoBehaviour
{
    public SpatialAnchorSpawnerBuildingBlock spatialAnchorSpawnerBuildingBlock;
    public ControllerButtonsMapper buttonsMapper;
    public SpatialAnchorLoaderBuildingBlock loader;
    private PhantomInstance OnHandAnchor;
    public void SetNewAnchor()
    {
        FindObjectOfType<SpatialAnchorCoreBuildingBlock>().EraseAllAnchors();
        spatialAnchorSpawnerBuildingBlock.gameObject.SetActive(true);
        buttonsMapper.gameObject.SetActive(true);
        if(OnHandAnchor != null) OnHandAnchor.gameObject.SetActive(true);
    }
    public void LoadAnchor()
    {
        loader.LoadAnchorsFromDefaultLocalStorage();
    }

    public void SetOnHandAnchor(PhantomInstance onHand)
    {
        OnHandAnchor = onHand;
        OnHandAnchor.gameObject.SetActive(false);
    }

    public void ReturnPlacedAnchorToDefaultPosition()
    {
        FindObjectOfType<PlacedPhantomMark>().transform.position = FindObjectOfType<PlacedPhantomMark>().positionContainer;
        FindObjectOfType<PlacedPhantomMark>().transform.rotation = FindObjectOfType<PlacedPhantomMark>().rotationContainer;
    }
}
