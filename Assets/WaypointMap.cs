using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaypointMap : MonoBehaviour
{
    Transform mapParent;
    [SerializeField] GameObject waypointMapPrefab;
    GameObject waypointObject;
    // Start is called before the first frame update
    void Start()
    {
        mapParent = Map.t;
        waypointObject = Instantiate(waypointMapPrefab, mapParent);
    }

    // Update is called once per frame
    void Update()
    {
        waypointObject.transform.localPosition = new Vector3((float)0.01*gameObject.transform.position[0], (float)0.01*gameObject.transform.position[2], (float)-.001);
    }
}
