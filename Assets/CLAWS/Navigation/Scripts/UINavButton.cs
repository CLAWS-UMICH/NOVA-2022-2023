using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UINavButton : MonoBehaviour
{
    NavScreenController navScreenController;
    string letterOfObject;
    void Awake()
    {
        letterOfObject = gameObject.transform.Find("Text/Letter").GetComponent<TextMeshPro>().text;
        navScreenController = FindObjectOfType<NavScreenController>();
    }
    public void ButtonClicked()
    {
        foreach (Waypoint waypoint in navScreenController.allWaypoints)
        {
            if (waypoint.GetLetter() == letterOfObject)
            {
                navScreenController.updateCurrentEnd(waypoint.GetPosition());

            }
        }
    }
}
