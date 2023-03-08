using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breadcrumbs : MonoBehaviour
{
    [SerializeField] private GameObject breadcrumb;
    [SerializeField] private GameObject allBread;
    [SerializeField] private GameObject camera;
    private Vector3 currentPosition;
    private Vector3 prevCrumbPosition;
    bool backtracingMode = false;
    private List<Vector3> crumbPositions = new();
    [SerializeField] float crumbDistance = 5f;

    void Start()
    {
        prevCrumbPosition = transform.position;
    }

    void Update()
    {
        if (backtracingMode)
        {
            foreach (Transform crumbTransform in allBread.transform)
            {
                crumbTransform.Rotate(new Vector3(0f, 180f, 0f));
            }
        }
        currentPosition = camera.transform.position;
        float distanceMoved = Vector3.Distance(currentPosition, prevCrumbPosition);

        if (distanceMoved >= crumbDistance)
        {
            PlaceBreadcrumb();
            prevCrumbPosition = currentPosition;
        }
    }

    void PlaceBreadcrumb()
    {
        Quaternion rotation = Quaternion.Euler(new Vector3(90f, 0f, currentPosition.z));
        Vector3 position = new Vector3(prevCrumbPosition.x, -1.25f, prevCrumbPosition.z);
        GameObject instantiated = Instantiate(breadcrumb, position, rotation);
        crumbPositions.Add(position); //List of positions for whenever needed
        instantiated.transform.SetParent(allBread.transform);
    }
}
