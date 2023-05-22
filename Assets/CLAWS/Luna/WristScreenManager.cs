using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristScreenManager : MonoBehaviour
{
    GameObject Home;

    GameObject Vitals;

    GameObject Geosampling;
    GameObject Geosample_Expanded;
    GameObject Geosample_Description;
    GameObject Geosample_Gallery;
    GameObject Geosample_Camera;
    GameObject Geosample_Confirm;

    GameObject Messaging;
    GameObject Messaging_MCC;
    GameObject Messaging_Jane;
    GameObject Messaging_Neil;

    GameObject Navigation;
    GameObject Navigation_Crew;
    GameObject Navigation_Geo;
    GameObject Navigation_Mission;
    GameObject Navigation_Rover;
    GameObject Navigation_Lander;
    GameObject Navigation_Rover_Confirm;
    GameObject Navigation_Waypoint_Confirm;

    GameObject TaskList;
    GameObject TaskList_CurrentTask;

    private GameObject currentScreenOpen;
    private GameObject pastScreenOpen = null;

    // Start is called before the first frame update
    void Start()
    {
        GameObject currentScreenOpen = Home;
        GameObject pastScreenOpen = null;
        EventBus.Subscribe<ScreenChangedEvent>(ChangeWristScreen);

        Home = transform.Find("Home").gameObject;

        Vitals = transform.Find("Vitals").gameObject;

        Geosampling = transform.Find("Geosampling").gameObject;
        Geosample_Expanded = transform.Find("Geosample_Expanded").gameObject;
        Geosample_Description = transform.Find("Geosample_Description").gameObject;
        Geosample_Gallery = transform.Find("Geosample_Gallery").gameObject;
        Geosample_Camera = transform.Find("Geosample_Camera").gameObject;
        Geosample_Confirm = transform.Find("Geosample_Confirm").gameObject;

        Messaging = transform.Find("Messaging").gameObject;
        Messaging_MCC = transform.Find("Messaging_MCC").gameObject;
        Messaging_Jane = transform.Find("Messaging_Jane").gameObject;
        Messaging_Neil = transform.Find("Messaging_Neil").gameObject;

        Navigation = transform.Find("Navigation").gameObject;
        Navigation_Crew = transform.Find("Navigation_Crew").gameObject;
        Navigation_Geo = transform.Find("Navigation_Geo").gameObject;
        Navigation_Mission = transform.Find("Navigation_Mission").gameObject;
        Navigation_Rover = transform.Find("Navigation_Rover").gameObject;
        Navigation_Lander = transform.Find("Navigation_Lander").gameObject;
        Navigation_Rover_Confirm = transform.Find("Navigation_Rover_Confirm").gameObject;
        Navigation_Waypoint_Confirm = transform.Find("Navigation_Waypoint_Confirm").gameObject;

        TaskList = transform.Find("TaskList").gameObject;
        TaskList_CurrentTask = transform.Find("TaskList_CurrentTask").gameObject;
    }

    public void ChangeWristScreen(ScreenChangedEvent e)
    {
        pastScreenOpen = currentScreenOpen;

        switch(e.screen)
        {
            case Screens.Home:
                currentScreenOpen = Home;
                break;
            case Screens.Vitals:
                currentScreenOpen = Vitals;
                break;
            case Screens.Geosampling:
                currentScreenOpen = Geosampling;
                break;
            case Screens.Geosample_Expanded:
                currentScreenOpen = Geosample_Expanded;
                break;
            case Screens.Geosample_Description:
                currentScreenOpen = Geosample_Description;
                break;
            case Screens.Geosample_Gallery:
                currentScreenOpen = Geosample_Gallery;
                break;
            case Screens.Geosample_Camera:
                currentScreenOpen = Geosample_Camera;
                break;
            case Screens.Geosample_Confirm:
                currentScreenOpen = Geosample_Confirm;
                break;
            case Screens.Messaging:
                currentScreenOpen = Messaging;
                break;
            case Screens.Messaging_MCC:
                currentScreenOpen = Messaging_MCC;
                break;
            case Screens.Messaging_Jane:
                currentScreenOpen = Messaging_Jane;
                break;
            case Screens.Messaging_Neil:
                currentScreenOpen = Messaging_Neil;
                break;
            case Screens.Navigation:
                currentScreenOpen = Navigation;
                break;
            case Screens.Navigation_Crew:
                currentScreenOpen = Navigation_Crew;
                break;
            case Screens.Navigation_Geo:
                currentScreenOpen = Navigation_Geo;
                break;
            case Screens.Navigation_Mission:
                currentScreenOpen = Navigation_Mission;
                break;
            case Screens.Navigation_Rover:
                currentScreenOpen = Navigation_Rover;
                break;
            case Screens.Navigation_Lander:
                currentScreenOpen = Navigation_Lander;
                break;
            case Screens.Navigation_Rover_Confirm:
                currentScreenOpen = Navigation_Rover_Confirm;
                break;
            case Screens.Navigation_Waypoint_Confirm:
                currentScreenOpen = Navigation_Waypoint_Confirm;
                break;
            case Screens.TaskList:
                currentScreenOpen = Navigation_Rover_Confirm;
                break;
            case Screens.TaskList_CurrentTask:
                currentScreenOpen = Navigation_Rover_Confirm;
                break;
            default:
                Debug.Log("No screen matched");
                break;
        }

        pastScreenOpen.SetActive(false);
        currentScreenOpen.SetActive(true);
    }
}
