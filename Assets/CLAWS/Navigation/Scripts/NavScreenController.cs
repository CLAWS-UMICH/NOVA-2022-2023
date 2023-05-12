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

    [SerializeField] GameObject mainCam;
    [SerializeField] GameObject openNavMenuButton;
    [SerializeField] GameObject gameManager;

    [SerializeField] GameObject geoTag;
    [SerializeField] GameObject missionTag;
    [SerializeField] GameObject obstacleTag;
    [SerializeField] GameObject UIWaypoint;

    string globalWaypointTextTitle;
    string globalWaypointType;

    Transform previousEndGoal = null;

    Transform currentEndPosition = null;


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
        currentEndPosition = null;
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
        currentEndPosition = null;
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

    List<GameObject> missionList = new List<GameObject>();
    List<GameObject> geoList = new List<GameObject>();
    List<GameObject> roverList = new List<GameObject>();
    public List<Waypoint> allWaypoints = new List<Waypoint>();

    public void OpenGeoConfirmationScreenTest()
    {
        string type = "geosample"; // Test type

        string title = "Test geo title"; // Test title

        // CALLS THE FUNCTION TO MAKE
        OpenConfirmationScreen(type, title);
    }

    public void OpenRegConfirmationScreenTest()
    {
        string type = "regular"; // Test type

        string title = "Test regular title"; // Test title

        // CALLS THE FUNCTION TO MAKE
        OpenConfirmationScreen(type, title);
    }

    public void OpenDangerConfirmationScreenTest()
    {
        string type = "danger"; // Test type

        string title = "Test regular title"; // Test title

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
        int count = 0;
        Vector3 position;
        float yOffset;

        // Create a waypoint based on the type
        CreateWaypoints way = gameManager.GetComponent<CreateWaypoints>();
        CloseConfirmation();
        Waypoint newWaypoint = way.CreateWaypoint(globalWaypointType, globalWaypointTextTitle);
        allWaypoints.Add(newWaypoint);

        // Get the title and letter of the waypoint
        string title = newWaypoint.GetTitle();
        string letter = newWaypoint.GetLetter();

        // Add the UI button element based on the type
        switch (globalWaypointType)
        {
            case "geosample":
                count++;
                // Gets the position of the GeoButtons parent gameobject
                // This is where the buttons will be instaniated
                Transform geoButtons = geoScreen.transform.Find("GeoButtons");
                position = geoButtons.position;

                // Find the yOffset so that the new buttons are below each other
                yOffset = -0.04f * geoList.Count;
                position.y += yOffset;

                // Create the buttons and set their title and letter
                GameObject newGeoPrefab = Instantiate(UIWaypoint, position, Quaternion.identity, geoButtons);
                newGeoPrefab.transform.Find("Text/Title").GetComponent<TextMeshPro>().text = title;
                newGeoPrefab.transform.Find("Text/Letter").GetComponent<TextMeshPro>().text = letter;

                geoList.Add(newGeoPrefab);
                break;
            case "regular":
                count++;
                // Gets the position of the GeoButtons parent gameobject
                // This is where the buttons will be instaniated
                Transform missionButtons = missionScreen.transform.Find("MissionButtons");
                position = missionButtons.position;

                // Find the yOffset so that the new buttons are below each other
                yOffset = -0.04f * missionList.Count;
                position.y += yOffset;

                // Create the buttons and set their title and letter
                GameObject newMissionPrefab = Instantiate(UIWaypoint, position, Quaternion.identity, missionButtons);
                newMissionPrefab.transform.Find("Text/Title").GetComponent<TextMeshPro>().text = title;
                newMissionPrefab.transform.Find("Text/Letter").GetComponent<TextMeshPro>().text = letter;

                missionList.Add(newMissionPrefab);
                break;
            default:
                Debug.Log("Unknown waypoint type");
                break;
        }

        if (count > 0)
        {
            // The same as above but every waypoint is added to the rover buttons
            Transform roverButtons = roverScreen.transform.Find("RoverButtons");
            Vector3 roverPositionUI = roverButtons.position;
            float roveryOffset = -0.04f * roverList.Count;
            roverPositionUI.y += roveryOffset;
            GameObject newRoverUIPrefab = Instantiate(UIWaypoint, roverPositionUI, Quaternion.identity, roverButtons);
            newRoverUIPrefab.transform.Find("Text/Title").GetComponent<TextMeshPro>().text = title;
            newRoverUIPrefab.transform.Find("Text/Letter").GetComponent<TextMeshPro>().text = letter;

            roverList.Add(newRoverUIPrefab);
        }
 


    }

    public void updateCurrentEnd(Transform end)
    {
        currentEndPosition = end;
    }

    public void StartNav()
    {
        if (currentEndPosition != null)
        {
            Transform playerPosition = mainCam.transform;


            //if (ToggleFinalDestinationForCorrectEndTarget(endPosition))
            //{
            gameManager.GetComponent<Pathfinding>().startPathFinding(playerPosition, currentEndPosition);

            previousEndGoal = currentEndPosition;
            //}
            CloseAll();
        }



    }

    private bool ToggleFinalDestinationForCorrectEndTarget(Transform endPosition)
    {
        NavigatableObject endNavigation = endPosition.gameObject.GetComponent<NavigatableObject>();

        if (endNavigation == null)
        {
            Debug.LogError("Object must contain the 'Navigatable Object' script in order to be considered a valid end position");
            return false;
        }
        else
        {
            if (previousEndGoal != null)
            {
                NavigatableObject prevNav = previousEndGoal.GetComponent<NavigatableObject>();
                if (prevNav == null)
                {
                    return false;
                }
                else
                {
                    prevNav.ToggleFinalDestination();
                }

            }

            RemoveOldPath(endNavigation);

            endNavigation.ToggleFinalDestination();
        }

        return true;
    }

    private void RemoveOldPath(NavigatableObject endNav)
    {
        if (previousEndGoal != null)
        {
            endNav.DestroyAllBreadCrumbs();
        }
    }

}
