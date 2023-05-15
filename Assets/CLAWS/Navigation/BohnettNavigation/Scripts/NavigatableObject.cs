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

    static public void DestroyAllBreadCrumbs()
    {
        GameObject allBreadCrumbsObject = GameObject.Find("NavExtras/Breadcrumbs");
        GameObject allPlacedBreadCrumbsObject = GameObject.Find("ParentBreadCrumbs");

        foreach (Transform child in allBreadCrumbsObject.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in allPlacedBreadCrumbsObject.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
