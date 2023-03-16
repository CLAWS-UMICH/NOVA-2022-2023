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
        prevCrumbPosition = camera.transform.position;
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

        while (distanceMoved >= crumbDistance)
        {
            PlaceBreadcrumb();
            prevCrumbPosition = currentPosition;
            distanceMoved = Vector3.Distance(currentPosition, prevCrumbPosition);
        }
    }
    private void PlaceBreadcrumb()
    {
        Vector3 direction = currentPosition - prevCrumbPosition;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up) * Quaternion.Euler(90f, 0f, 0f);
        GameObject instantiated = Instantiate(breadcrumb, prevCrumbPosition, rotation);
        crumbPositions.Add(prevCrumbPosition); //List of positions for whenever needed
        instantiated.transform.SetParent(allBread.transform);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("crumb"))
        {
            crumbPositions.Remove(other.gameObject.transform.position);
            Destroy(other.gameObject);
        }
    }
}

