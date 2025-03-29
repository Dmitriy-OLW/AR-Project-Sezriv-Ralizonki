using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedPhantomMark : MonoBehaviour
{
    public Vector3 positionContainer { get; private set; }
    public Quaternion rotationContainer { get; private set; }
    public void WriteMarkedPosition()
    {
        positionContainer = transform.position;
        rotationContainer = transform.rotation;
    }
}
