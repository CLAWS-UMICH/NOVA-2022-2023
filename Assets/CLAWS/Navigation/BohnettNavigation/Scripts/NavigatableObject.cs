using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigatableObject : MonoBehaviour
{
    [SerializeField] GameObject allBreadCrumbs;
    [SerializeField] GameObject allPlacedBreadCrumbs;

    bool isFinalDestination = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isFinalDestination && other.gameObject.CompareTag("MainCamera"))
        {
            DestroyAllBreadCrumbs();
        }
        
    }

    public void ToggleFinalDestination()
    {
        isFinalDestination = !isFinalDestination;
    }

    public void DestroyAllBreadCrumbs()
    {
        foreach (Transform child in allBreadCrumbs.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in allPlacedBreadCrumbs.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
