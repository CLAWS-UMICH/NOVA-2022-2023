using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UINavButton : MonoBehaviour
{
    NavScreenController navScreenController;
    string letterOfObject;
    GameObject lightBlueBorder;

    void Awake()
    {
        letterOfObject = gameObject.transform.Find("Text/Letter").GetComponent<TextMeshPro>().text;
        lightBlueBorder = gameObject.transform.Find("BackPlate/LightBlue").gameObject;
        navScreenController = FindObjectOfType<NavScreenController>();
        lightBlueBorder.SetActive(false);
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

    public void ButtonSelected()
    {
        lightBlueBorder.SetActive(true);
        navScreenController.turnOffPastButtonLightBlue();
        navScreenController.updateCurrentSelectedButton(lightBlueBorder);
    }

    public string GetLetter()
    {
        return letterOfObject;
    }
}
