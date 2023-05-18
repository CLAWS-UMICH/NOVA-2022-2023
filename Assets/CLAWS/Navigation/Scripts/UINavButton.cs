using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UINavButton : MonoBehaviour
{
    NavScreenController navScreenController;
    string letterOfObject;
    string titleOfObject;
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
                titleOfObject = waypoint.GetTitle();
                navScreenController.updateCurrentEnd(waypoint.GetPosition(), waypoint.GetTitle() + " " + waypoint.GetLetter());

            }
        }
    }

    public void ButtonSelected()
    {
        lightBlueBorder.SetActive(true);
        navScreenController.turnOffPastButtonLightBlue();
        navScreenController.updateCurrentSelectedButton(lightBlueBorder, titleOfObject);
    }

    public string GetLetter()
    {
        return letterOfObject;
    }
}
