using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraController : MonoBehaviour
{
    [SerializeField] GameObject following;
    private Vector3 targetPosition;

    // Update is called once per frame
    void Update()
    {
        targetPosition = new Vector3(following.transform.position.x, following.transform.position.y + 2, following.transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.time);

        
    }
}
