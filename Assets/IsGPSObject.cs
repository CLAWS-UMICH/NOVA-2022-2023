using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGPSObject : MonoBehaviour
{
    public GPSCoords coords;

    void Start()
    {
        EventBus.Subscribe<UpdatedGPSOriginEvent>(UpdatePosition);
    }

    void UpdatePosition(UpdatedGPSOriginEvent e)
    {
        Debug.Log("Updating Position");
        Vector3 newPos = GPSUtils.GPSCoordsToAppPosition(coords);
        transform.localPosition = newPos;
    }
}
