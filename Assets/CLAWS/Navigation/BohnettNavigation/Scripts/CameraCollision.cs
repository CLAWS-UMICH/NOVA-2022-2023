using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Event for the camera to fire when it collides with a breadCrumb
// This allows the gameManager to keep the script and nothing needs to moved around
public class BreadCrumbCollisionEvent
{
    public GameObject breadCrumb;

    public BreadCrumbCollisionEvent(GameObject crumb)
    {
        breadCrumb = crumb;
    }
}


public class CameraCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("crumb"))
        {
            EventBus.Publish<BreadCrumbCollisionEvent>(new BreadCrumbCollisionEvent(other.gameObject));
        }
    }
}
