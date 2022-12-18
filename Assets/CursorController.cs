using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CursorController : MonoBehaviour
{
    [SerializeField] GameObject g;
    [SerializeField] GameObject camera;
    Vector3 NorthDir;
    Vector3 waypoint;

    // Update is called once per frame
    void Update()
    {
        NorthDir.z = camera.transform.eulerAngles.y;
        g.transform.localRotation =Quaternion.Euler(NorthDir.z + 120, 90, -90);
        g.transform.localPosition = new Vector3((float)0.01*camera.transform.position[0], (float)0.01*camera.transform.position[2], (float)-.001);
    }
}
