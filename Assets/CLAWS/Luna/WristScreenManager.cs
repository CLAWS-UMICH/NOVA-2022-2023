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
        Messaging_MCC = transform.Find("Messaging_Chat").gameObject;
        //Messaging_Jane = transform.Find("Messaging_Chat").gameObject;
        //Messaging_Neil = transform.Find("Messaging_Chat").gameObject;

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
        pastScreenOpen = Home;
    }

    public void ChangeWristScreen(ScreenChangedEvent e)
    {
        foreach(Transform child in gameObject.transform)
            {
                if(child.gameObject.activeSelf)
                {
                    child.gameObject.SetActive(false);
                }
            }
        switch(e.screen)
        {
            
            case Screens.Home:
                Home.SetActive(true);
                break;
            case Screens.Vitals:
                Vitals.SetActive(true);
                break;
            case Screens.Geosampling:
                Geosampling.SetActive(true);
                break;
            case Screens.Geosample_Expanded:
                Geosample_Expanded.SetActive(true);
                break;
            case Screens.Geosample_Description:
                Geosample_Description.SetActive(true);
                break;
            case Screens.Geosample_Gallery:
                Geosample_Gallery.SetActive(true);
                break;
            case Screens.Geosample_Camera:
                Geosample_Camera.SetActive(true);
                break;
            case Screens.Geosample_Confirm:
                Geosample_Confirm.SetActive(true);
                currentScreenOpen = Geosample_Confirm;
                break;
            case Screens.Messaging:
                Messaging.SetActive(true);
                currentScreenOpen = Messaging;
                break;
            case Screens.Messaging_MCC:
                Messaging_MCC.SetActive(true);
                currentScreenOpen = Messaging_MCC;
                break;
            case Screens.Messaging_Jane:
                Messaging_Jane.SetActive(true);
                currentScreenOpen = Messaging_Jane;
                break;
            case Screens.Messaging_Neil:
                Messaging_Neil.SetActive(true);
                currentScreenOpen = Messaging_Neil;
                break;
            case Screens.Navigation:
                Navigation.SetActive(true);
                currentScreenOpen = Navigation;
                break;
            case Screens.Navigation_Crew:
                Navigation_Crew.SetActive(true);
                currentScreenOpen = Navigation_Crew;
                break;
            case Screens.Navigation_Geo:
                Navigation_Geo.SetActive(true);
                currentScreenOpen = Navigation_Geo;
                break;
            case Screens.Navigation_Mission:
                Navigation_Mission.SetActive(true);
                currentScreenOpen = Navigation_Mission;
                break;
            case Screens.Navigation_Rover:
                Navigation_Rover.SetActive(true);
                currentScreenOpen = Navigation_Rover;
                break;
            case Screens.Navigation_Lander:
                Navigation_Lander.SetActive(true);
                currentScreenOpen = Navigation_Lander;
                break;
            case Screens.Navigation_Rover_Confirm:
                Navigation_Rover_Confirm.SetActive(true);
                currentScreenOpen = Navigation_Rover_Confirm;
                break;
            case Screens.Navigation_Waypoint_Confirm:
                Navigation_Waypoint_Confirm.SetActive(true);
                currentScreenOpen = Navigation_Waypoint_Confirm;
                break;
            case Screens.TaskList:
                TaskList.SetActive(true);
                currentScreenOpen = TaskList;
                break;
            case Screens.TaskList_CurrentTask:
                TaskList_CurrentTask.SetActive(true);
                currentScreenOpen = TaskList_CurrentTask;
                break;
            default:
                Debug.Log("No screen matched");
                break;
        }

        
        // pastScreenOpen.SetActive(false);
        pastScreenOpen = currentScreenOpen;
    }
}
