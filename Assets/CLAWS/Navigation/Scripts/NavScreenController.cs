using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class NavScreenController : MonoBehaviour
{
    GameObject mainNavScreen;
    GameObject crewScreen;
    GameObject geoScreen;
    GameObject missionScreen;
    GameObject roverScreen;
    GameObject landerScreen;
    GameObject waypointConfirmationScreen;

    [SerializeField] GameObject openNavMenuButton;
    [SerializeField] GameObject gameManager;

    [SerializeField] GameObject geoTag;
    [SerializeField] GameObject missionTag;
    [SerializeField] GameObject obstacleTag;

    string globalWaypointTextTitle;
    string globalWaypointType;


    private void Awake()
    {
        mainNavScreen = transform.Find("MainNavScreen").gameObject;
        crewScreen = transform.Find("CrewScreen").gameObject;
        geoScreen = transform.Find("GeoScreen").gameObject;
        missionScreen = transform.Find("MissionScreen").gameObject;
        roverScreen = transform.Find("RoverScreen").gameObject;
        landerScreen = transform.Find("LanderScreen").gameObject;
        waypointConfirmationScreen = transform.Find("WaypointScreen").gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {

        openNavMenuButton.SetActive(true);
        mainNavScreen.SetActive(false);
        crewScreen.SetActive(false);
        geoScreen.SetActive(false);
        missionScreen.SetActive(false);
        roverScreen.SetActive(false);
        landerScreen.SetActive(false);
        waypointConfirmationScreen.SetActive(false);

    }

    public void CloseAll()
    {
        StartCoroutine(_CloseScreen());
    }

    IEnumerator _CloseScreen()
    {
        yield return new WaitForSeconds(1f);
        openNavMenuButton.SetActive(true);
        mainNavScreen.SetActive(false);
        crewScreen.SetActive(false);
        geoScreen.SetActive(false);
        missionScreen.SetActive(false);
        roverScreen.SetActive(false);
        landerScreen.SetActive(false);
        waypointConfirmationScreen.SetActive(false);
    }

    IEnumerator _OpenNavMainMenu()
    {
        yield return new WaitForSeconds(1f);
        openNavMenuButton.SetActive(false);
        mainNavScreen.SetActive(true);
        crewScreen.SetActive(false);
        geoScreen.SetActive(false);
        missionScreen.SetActive(false);
        roverScreen.SetActive(false);
        landerScreen.SetActive(false);
    }

    public void OpenNavMainMenu()
    {
        StartCoroutine(_OpenNavMainMenu());
    }

    IEnumerator _OpenCrewScreen()
    {
        yield return new WaitForSeconds(1f);
        openNavMenuButton.SetActive(false);
        mainNavScreen.SetActive(false);
        crewScreen.SetActive(true);
        geoScreen.SetActive(false);
        missionScreen.SetActive(false);
        roverScreen.SetActive(false);
        landerScreen.SetActive(false);
    }

    public void OpenCrewScreen()
    {
        StartCoroutine(_OpenCrewScreen());
    }

    IEnumerator _OpenGeoScreen()
    {
        yield return new WaitForSeconds(1f);
        openNavMenuButton.SetActive(false);
        mainNavScreen.SetActive(false);
        crewScreen.SetActive(false);
        geoScreen.SetActive(true);
        missionScreen.SetActive(false);
        roverScreen.SetActive(false);
        landerScreen.SetActive(false);
    }

    public void OpenGeoScreen()
    {
        StartCoroutine(_OpenGeoScreen());
    }

    IEnumerator _OpenMissionScreen()
    {
        yield return new WaitForSeconds(1f);
        openNavMenuButton.SetActive(false);
        mainNavScreen.SetActive(false);
        crewScreen.SetActive(false);
        geoScreen.SetActive(false);
        missionScreen.SetActive(true);
        roverScreen.SetActive(false);
        landerScreen.SetActive(false);
    }

    public void OpenMissionScreen()
    {
        StartCoroutine(_OpenMissionScreen());
    }

    IEnumerator _OpenRoverScreen()
    {
        yield return new WaitForSeconds(1f);
        openNavMenuButton.SetActive(false);
        mainNavScreen.SetActive(false);
        crewScreen.SetActive(false);
        geoScreen.SetActive(false);
        missionScreen.SetActive(false);
        roverScreen.SetActive(true);
        landerScreen.SetActive(false);
    }

    public void OpenRoverScreen()
    {
        StartCoroutine(_OpenRoverScreen());
    }

    IEnumerator _OpenLanderScreen()
    {
        yield return new WaitForSeconds(1f);
        openNavMenuButton.SetActive(false);
        mainNavScreen.SetActive(false);
        crewScreen.SetActive(false);
        geoScreen.SetActive(false);
        missionScreen.SetActive(false);
        roverScreen.SetActive(false);
        landerScreen.SetActive(true);
    }

    public void OpenLanderScreen()
    {
        StartCoroutine(_OpenLanderScreen());
    }







    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    // WAYPOINTS
    // FOR TESTING ONLY SO YOU CAN SEE IF THE CONFIRMATION SCREEN OPENS USE THIS FUNCTIOn:

    public void OpenConfirmationScreenTest()
    {
        string type = "danger"; // Test type

        string title = "Test danger title"; // Test title

        // CALLS THE FUNCTION TO MAKE
        OpenConfirmationScreen(type, title);
    }

    void OpenConfirmationScreen(string type, string title)
    {
        // Add the type (Tag look at figma for the different tags. There can be "geosample, danger, regular"
        // Add the title of the waypoint in text given the title parameter

        TextMeshPro titleText = waypointConfirmationScreen.transform.Find("Text/TitleText").GetComponent<TextMeshPro>();
        titleText.text = title;


        waypointConfirmationScreen.SetActive(true);

        switch (type)
        {
            case "danger":
                geoTag.SetActive(false);
                missionTag.SetActive(false);
                obstacleTag.SetActive(true);
                globalWaypointType = type;
                globalWaypointTextTitle = title;
                break;
            case "geosample":
                missionTag.SetActive(false);
                obstacleTag.SetActive(false);
                geoTag.SetActive(true);
                globalWaypointType = type;
                globalWaypointTextTitle = title;
                break;
            case "regular":
                obstacleTag.SetActive(false);
                geoTag.SetActive(false);
                missionTag.SetActive(true);
                globalWaypointType = type;
                globalWaypointTextTitle = title;
                break;
            default:
                Debug.Log("Unknown waypoint type");
                break;
        }

    }

    // THIS FUNCTION IS CALLED BY VEGA
    public void OpenWaypoint(string type, string title)
    {
        // ERROR HANDLING ON IF THE TYPE IS NOT 1 of the 3 WAYPOINT TYPES
        switch (type)
        {
            case "danger":
                break;
            case "geosample":
                break;
            case "regular":
                break;
            default:
                Debug.Log("Unknown waypoint type");
                break;
        }

        OpenConfirmationScreen(type, title);
    }


    // CLOSES THE CONFIRMATION SCREEN
    IEnumerator _CloseConfirmation()
    {
        yield return new WaitForSeconds(1f);
        waypointConfirmationScreen.SetActive(false);
    }

    public void CloseConfirmation()
    {
        StartCoroutine(_CloseConfirmation());
    }

    // CALL THIS WHEN THE BUTTON FOR CONFIRMING THE CREATION OF A WAYPOINT IS MADE
    public void CreateAPoint()
    {
        // This is where you create the point or confirm you want to create one

        CreateWaypoints way = gameManager.GetComponent<CreateWaypoints>();
        CloseConfirmation();
        way.CreateWaypoint(globalWaypointType, globalWaypointTextTitle);
    }

}
