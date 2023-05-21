using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Text.RegularExpressions;
using Microsoft.MixedReality.Toolkit.Utilities;
using TSS;
using TSS.Msgs;

public class NavScreenController : MonoBehaviour
{
    GameObject test;

    GameObject mainNavScreen;
    GameObject crewScreen;
    GameObject geoScreen;
    GameObject missionScreen;
    GameObject roverScreen;
    GameObject landerScreen;
    GameObject roverNavScreen;
    GameObject waypointConfirmationScreen;
    GameObject slider;
    GameObject slider2;
    GameObject roverSmallScreen;

    [SerializeField] GameObject telemMan;
    [SerializeField] GameObject mainCam;
    [SerializeField] Camera mapCam;
    [SerializeField] GameObject openNavMenuButton;
    [SerializeField] GameObject gameManager;

    [SerializeField] GameObject geoTag;
    [SerializeField] GameObject missionTag;
    [SerializeField] GameObject obstacleTag;
    [SerializeField] GameObject UIWaypoint;

    // Predetermed Objects
    [SerializeField] GameObject landerObject;
    [SerializeField] GameObject crewObject;
    [SerializeField] GameObject missionA;
    [SerializeField] GameObject missionB;
    [SerializeField] GameObject missionC;
    [SerializeField] GameObject missionD;
    [SerializeField] GameObject missionE;
    [SerializeField] GameObject missionF;
    [SerializeField] GameObject missionG;
    [SerializeField] GameObject missionH;
    [SerializeField] GameObject missionI;


    [SerializeField] GameObject roverObject;
    GameObject roverObjectStartLocation;


    string globalWaypointTextTitle;
    string globalWaypointType;
    string globalLat;
    string globalLong;
    string currentScreenOpen;

    Transform previousEndGoal = null;

    Transform currentEndPosition = null;

    private GameObject currentSelectedButton = null;

    public GameObject speech;
    string message;
    private IEnumerator coroutine;



    private void Awake()
    {
        mainNavScreen = transform.Find("MainNavScreen").gameObject;
        crewScreen = transform.Find("CrewScreen").gameObject;
        geoScreen = transform.Find("GeoScreen").gameObject;
        missionScreen = transform.Find("MissionScreen").gameObject;
        roverScreen = transform.Find("RoverScreen").gameObject;
        landerScreen = transform.Find("LanderScreen").gameObject;
        roverNavScreen = transform.Find("RoverUpdateScreen").gameObject;
        waypointConfirmationScreen = transform.Find("WaypointScreen").gameObject;
        slider = transform.Find("RoverUpdateScreen/Canvas/Slider").gameObject;

        roverSmallScreen = transform.Find("RoverSmallUpdate").gameObject;
        slider2 = transform.Find("RoverSmallUpdate/Canvas/Slider").gameObject;

    }
    // Start is called before the first frame update

 

    void Start()
    {
        EventBus.Subscribe<CloseEvent>(CloseNavigation);

        roverObjectStartLocation = roverObject;
        titleLetter = "";
        titleOfCurrentWaypoint = "";
        currentSelectedButton = null;
        playerWithinDistance = false;
        createPreDeterminedPoints();
        currentEndPosition = null;
        currentScreenOpen = "";
        openNavMenuButton.SetActive(true);

        mainNavScreen.SetActive(false);
        crewScreen.SetActive(false);
        geoScreen.SetActive(false);
        missionScreen.SetActive(false);
        roverScreen.SetActive(false);
        landerScreen.SetActive(false);
        roverNavScreen.SetActive(false);
        waypointConfirmationScreen.SetActive(false);
        roverSmallScreen.SetActive(false);
        SetAllCullingToCamera();




    }

    // Close all screens when clicking close screen
    private void CloseNavigation(CloseEvent e)
    {
        if (e.screen == Screens.Navigation || e.screen == Screens.Navigation_Crew || e.screen == Screens.Navigation_Geo
            || e.screen == Screens.Navigation_Lander || e.screen == Screens.Navigation_Lander || e.screen == Screens.Navigation_Mission
            || e.screen == Screens.Navigation_Rover || e.screen == Screens.Navigation_Rover_Confirm|| e.screen == Screens.Navigation_Waypoint_Confirm)
        {
            CloseAll();
        }
    }

    public void CloseAll()
    {
        playerWithinDistance = false;
        currentScreenOpen = "";
        currentEndPosition = null;
        StartCoroutine(_CloseScreen());
        SetAllCullingToCamera();
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
        roverNavScreen.SetActive(false);
        turnOffPastButtonLightBlue();
        currentSelectedButton = null;
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
        roverNavScreen.SetActive(false);
        turnOffPastButtonLightBlue();
        currentSelectedButton = null;
    }

    public void OpenNavMainMenu()
    {
        fixRotationOfButtons();
        currentScreenOpen = "Menu";
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
        roverNavScreen.SetActive(false);
        turnOffPastButtonLightBlue();
        currentSelectedButton = null;
        ShowOnlyCrewIcons();
    }

    public void OpenCrewScreen()
    {
        currentScreenOpen = "Crew";
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
        roverNavScreen.SetActive(false);
        turnOffPastButtonLightBlue();
        currentSelectedButton = null;
        ShowOnlyGeoIcons();
    }

    public void OpenGeoScreen()
    {
        currentScreenOpen = "Geo";
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
        roverNavScreen.SetActive(false);
        turnOffPastButtonLightBlue();
        currentSelectedButton = null;
        ShowOnlyMissionIcons();
    }

    public void OpenMissionScreen()
    {
        currentScreenOpen = "Mission";
        OpenMission();
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
        roverNavScreen.SetActive(false);
        turnOffPastButtonLightBlue();
        currentSelectedButton = null;
        ShowOnlyRoverIcons();
    }

    public void OpenRoverScreen()
    {
        currentScreenOpen = "Rover";
        OpenRover();
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
        roverNavScreen.SetActive(false);
        turnOffPastButtonLightBlue();
        currentSelectedButton = null;
        ShowOnlyLanderIcons();
    }

    public void OpenLanderScreen()
    {
        currentEndPosition = landerObject.transform;
        playerWithinDistance = false;
        currentScreenOpen = "Lander";
        StartCoroutine(_OpenLanderScreen());
    }

    // Icon Stuff
    // cam.cullingMask |= 1 << 3; adds the index 3 layer to the culling mask
    // cam.cullingMask &= ~(1 << 3); removes the index 3 layer to the culling mask
    // Instead of index of 3 you can use: cam.cullingMask |= 1 << LayerMask.NameToLayer("MyLayer");

    void SetAllCullingToCamera()
    {
        mapCam.cullingMask |= 1 << LayerMask.NameToLayer("CrewLayer");
        mapCam.cullingMask |= 1 << LayerMask.NameToLayer("MissionLayer");
        mapCam.cullingMask |= 1 << LayerMask.NameToLayer("GeoLayer");
        mapCam.cullingMask |= 1 << LayerMask.NameToLayer("LanderLayer");
        mapCam.cullingMask |= 1 << LayerMask.NameToLayer("RoverLayer");
    }

    void ShowOnlyCrewIcons()
    {
        mapCam.cullingMask &= ~(1 << LayerMask.NameToLayer("MissionLayer"));
        mapCam.cullingMask &= ~(1 << LayerMask.NameToLayer("GeoLayer"));
        mapCam.cullingMask &= ~(1 << LayerMask.NameToLayer("LanderLayer"));
        mapCam.cullingMask &= ~(1 << LayerMask.NameToLayer("RoverLayer"));
        mapCam.cullingMask |= 1 << LayerMask.NameToLayer("CrewLayer");
    }

    void ShowOnlyMissionIcons()
    {
        mapCam.cullingMask &= ~(1 << LayerMask.NameToLayer("GeoLayer"));
        mapCam.cullingMask &= ~(1 << LayerMask.NameToLayer("RoverLayer"));
        mapCam.cullingMask &= ~(1 << LayerMask.NameToLayer("CrewLayer"));
        mapCam.cullingMask &= ~(1 << LayerMask.NameToLayer("LanderLayer"));
        mapCam.cullingMask |= 1 << LayerMask.NameToLayer("MissionLayer");
    }

    void ShowOnlyLanderIcons()
    {
        mapCam.cullingMask &= ~(1 << LayerMask.NameToLayer("MissionLayer"));
        mapCam.cullingMask &= ~(1 << LayerMask.NameToLayer("GeoLayer"));
        mapCam.cullingMask &= ~(1 << LayerMask.NameToLayer("RoverLayer"));
        mapCam.cullingMask &= ~(1 << LayerMask.NameToLayer("CrewLayer"));
        mapCam.cullingMask |= 1 << LayerMask.NameToLayer("LanderLayer");
    }

    void ShowOnlyGeoIcons()
    {
        mapCam.cullingMask &= ~(1 << LayerMask.NameToLayer("MissionLayer"));
        mapCam.cullingMask &= ~(1 << LayerMask.NameToLayer("GeoLayer"));
        mapCam.cullingMask &= ~(1 << LayerMask.NameToLayer("RoverLayer"));
        mapCam.cullingMask &= ~(1 << LayerMask.NameToLayer("CrewLayer"));
        mapCam.cullingMask &= ~(1 << LayerMask.NameToLayer("LanderLayer"));
        mapCam.cullingMask |= 1 << LayerMask.NameToLayer("GeoLayer");
    }

    void ShowOnlyRoverIcons()
    {
        mapCam.cullingMask |= 1 << LayerMask.NameToLayer("MissionLayer");
        mapCam.cullingMask |= 1 << LayerMask.NameToLayer("GeoLayer");
        mapCam.cullingMask |= 1 << LayerMask.NameToLayer("RoverLayer");
        mapCam.cullingMask &= ~(1 << LayerMask.NameToLayer("CrewLayer"));
        mapCam.cullingMask &= ~(1 << LayerMask.NameToLayer("LanderLayer"));
    }





    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    // WAYPOINTS
    // FOR TESTING ONLY SO YOU CAN SEE IF THE CONFIRMATION SCREEN OPENS USE THIS FUNCTIOn:

    List<Waypoint> missionList = new List<Waypoint>();
    List<Waypoint> geoList = new List<Waypoint>();
    List<Waypoint> roverList = new List<Waypoint>();
    List<Waypoint> crewList = new List<Waypoint>();
    public List<Waypoint> allWaypoints = new List<Waypoint>();

    List<GameObject> missionButtons = new List<GameObject>();
    List<GameObject> geoButtons = new List<GameObject>();
    List<GameObject> roverButtons = new List<GameObject>();
    List<GameObject> crewButtons = new List<GameObject>();

    List<GameObject> testButtons = new List<GameObject>();
    int firstMessage = 0;

    private void fixRotationOfButtons()
    {
        while (testButtons.Count > 0)
        {
            // Get the first button in the list
            GameObject button = testButtons[0];

            // Set the rotation of the button to (0,0,0)
            button.transform.localRotation = Quaternion.identity;

            // Remove the button from the list
            testButtons.RemoveAt(0);
        }
    }

    private void createPreDeterminedPoints()
    {

        List<string> regNames = new List<string>();
        regNames.Add("Rock A1");
        regNames.Add("Sand Land");
        regNames.Add("Meteor C1");
        regNames.Add("Rock A2");
        regNames.Add("Walkway");
        regNames.Add("Trailer");
        regNames.Add("Meteor Edge");
        regNames.Add("Hill");
        regNames.Add("Yard Edge");

        GameObject[] missionArray = {missionA, missionB, missionC, missionD, missionE, missionF, missionG, missionH, missionI};


        for (int i = 0; i < 9; i++)
        {
            Waypoint newMissionA = new Waypoint(missionArray[i].transform, regNames[i], (Type)System.Enum.Parse(typeof(Type), "regular"));
            missionArray[i].transform.Find("Icons/Letter/LetterText").GetComponent<TextMeshPro>().text = newMissionA.GetLetter();
            missionArray[i].transform.Find("WaypointSign/Plate/Backplate/IconAndText/Letter").GetComponent<TextMeshPro>().text = newMissionA.GetLetter();
            createRegularButton(newMissionA.GetTitle(), newMissionA.GetLetter());
            createRoverButtons(newMissionA.GetTitle(), newMissionA.GetLetter());
            missionList.Add(newMissionA);
            roverList.Add(newMissionA);
            allWaypoints.Add(newMissionA);
        }

        OpenRover();
        OpenMission();





        Waypoint newCrew = new Waypoint(crewObject.transform, "Patrick", (Type)System.Enum.Parse(typeof(Type), "crew"));
        crewObject.transform.Find("Letter/LetterText").GetComponent<TextMeshPro>().text = newCrew.GetLetter();
        crewList.Add(newCrew);
        allWaypoints.Add(newCrew);

    }

    private void OpenMission()
    {
        firstMessageM = 0;
        Transform missionButton = missionScreen.transform.Find("ButtonStuffMission");
        for (int i = 5; i < missionButtons.Count; i++)
        {
            missionButtons[i].SetActive(false);
        }
        missionButton.GetComponent<GridObjectCollection>().UpdateCollection();
    }

    private void OpenRover()
    {
        firstMessage = 0;
        Transform roverButton = roverScreen.transform.Find("ButtonStuff");
        for (int i = 5; i < roverButtons.Count; i++)
        {
            roverButtons[i].SetActive(false);
        }
        roverButton.GetComponent<GridObjectCollection>().UpdateCollection();
    }

    int firstMessageM = 0;

    public void ScrollDown()
    {
        Debug.Log(roverButtons.Count);
        GameObject roverButton = roverScreen.transform.Find("ButtonStuff").gameObject;

        int len = roverButtons.Count;
        if (firstMessage + 4 < len && firstMessage >= 0)
        {
            if (firstMessage != 0)
            {
                Debug.Log("firstMessage");
                roverButtons[firstMessage - 1].SetActive(true);
                roverButtons[firstMessage + 4].SetActive(false);
                firstMessage--;
                roverButton.GetComponent<GridObjectCollection>().UpdateCollection();
                StartCoroutine(updateCollection(roverButton));
            }
        }


    }

    public void ScrollDownMission()
    {
        Debug.Log(missionButtons.Count);
        GameObject missionButton = missionScreen.transform.Find("ButtonStuffMission").gameObject;

        int len = missionButtons.Count;
        if (firstMessageM + 4 < len && firstMessageM >= 0)
        {
            if (firstMessageM != 0)
            {
                Debug.Log("firstMessage: " + firstMessageM);

                missionButtons[firstMessageM - 1].SetActive(true);
                missionButtons[firstMessageM + 4].SetActive(false);
                firstMessageM--;
                missionButton.GetComponent<GridObjectCollection>().UpdateCollection();
                StartCoroutine(updateCollection(missionButton));
            }
        }


    }

    public void ScrollUp()
    {
        GameObject roverButton = roverScreen.transform.Find("ButtonStuff").gameObject;


        int len = roverButtons.Count;
        if (firstMessage + 5 < len && firstMessage >= 0)
        {
            roverButtons[firstMessage].SetActive(false);
            roverButtons[firstMessage + 5].SetActive(true);
            firstMessage++;
            roverButton.GetComponent<GridObjectCollection>().UpdateCollection();
            StartCoroutine(updateCollection(roverButton));
        }


    }

    IEnumerator updateCollection(GameObject buttonType)
    {
        yield return new WaitForSeconds(0f);
        buttonType.GetComponent<GridObjectCollection>().UpdateCollection();
    }

    public void ScrollUpMission()
    {
        GameObject missionButton = missionScreen.transform.Find("ButtonStuffMission").gameObject;


        int len = missionButtons.Count;
        if (firstMessageM + 5 < len && firstMessageM >= 0)
        {
            Debug.Log("firstMessage: " + firstMessageM);
            missionButtons[firstMessageM].SetActive(false);
            missionButtons[firstMessageM + 5].SetActive(true);
            firstMessageM++;
            missionButton.GetComponent<GridObjectCollection>().UpdateCollection();
            StartCoroutine(updateCollection(missionButton));
        }

    }


    public void OpenGeoConfirmationScreenTest()
    {
        string type = "geosample"; // Test type


        // CALLS THE FUNCTION TO MAKE
        OpenConfirmationScreen(type);
    }

    public void OpenRegConfirmationScreenTest()
    {
        string type = "regular"; // Test type


        // CALLS THE FUNCTION TO MAKE
        OpenConfirmationScreen(type);
    }

    public void OpenDangerConfirmationScreenTest()
    {
        string type = "danger"; // Test type


        // CALLS THE FUNCTION TO MAKE
        OpenConfirmationScreen(type);
    }

    void OpenConfirmationScreen(string type)
    {

        // Add the type (Tag look at figma for the different tags. There can be "geosample, danger, regular"
        // Add the title of the waypoint in text given the title parameter

        TextMeshPro titleText = waypointConfirmationScreen.transform.Find("Text/TitleText").GetComponent<TextMeshPro>();
        titleText.text = "\"Set Title\" with VEGA";


        waypointConfirmationScreen.SetActive(true);

        switch (type)
        {
            case "danger":
                geoTag.SetActive(false);
                missionTag.SetActive(false);
                obstacleTag.SetActive(true);
                globalWaypointType = type;
                globalWaypointTextTitle = "Danger";
                break;
            case "geosample":
                missionTag.SetActive(false);
                obstacleTag.SetActive(false);
                geoTag.SetActive(true);
                globalWaypointType = type;
                globalWaypointTextTitle = "GeoSample";
                break;
            case "regular":
                obstacleTag.SetActive(false);
                geoTag.SetActive(false);
                missionTag.SetActive(true);
                globalWaypointType = type;
                globalWaypointTextTitle = "Mission";
                break;
            default:
                Debug.Log("Unknown waypoint type");
                break;
        }

    }

    public void SetWaypointTitle(string title)
    {

        globalWaypointTextTitle = title;
        TextMeshPro titleText = waypointConfirmationScreen.transform.Find("Text/TitleText").GetComponent<TextMeshPro>();
        titleText.text = title;

    }

    public void recordLat() {
        speech.SetActive(true);
        message = speech.GetComponent<SpeechManager>().GetMessage();
        bool active = true;
        coroutine = StartListeningLat(active);
        StartCoroutine(coroutine);
        speech.SetActive(false);
    }

    public void recordCoordLong() {
        speech.SetActive(true);
        message = speech.GetComponent<SpeechManager>().GetMessage();
        bool active = true;
        coroutine = StartListeningLong(active);
        StartCoroutine(coroutine);
        speech.SetActive(false);
    }

    public void recordTitle() {
        speech.SetActive(true);
        message = speech.GetComponent<SpeechManager>().GetMessage();
        bool active = true;
        coroutine = StartListeningTitle(active);
        StartCoroutine(coroutine);
        speech.SetActive(false);
    }

    IEnumerator StartListeningLat(bool active){
        int i = 0;
        string prevMessage = message;
        bool speaking = false;
        while(active){
            yield return new WaitForSeconds(0.8f);
            message = speech.GetComponent<SpeechManager>().GetMessage();
            i++;
            if(message!=prevMessage){
                speaking = true;
                //add something here to update text box with message text
            }
            if(i==3 && speaking){
                i = 0;
                speaking = false;
            }
            else if(i==3 && !speaking){
                i = 0;
                speaking = false;
                SetWaypointLat(message);
                Debug.Log("lattitude: " + globalLat);
                //finished speaking so stop recording. store message as description
                active = false;
                
            }
            prevMessage = message;
        }  
    }

    IEnumerator StartListeningLong(bool active){
        int i = 0;
        string prevMessage = message;
        bool speaking = false;
        while(active){
            yield return new WaitForSeconds(0.8f);
            message = speech.GetComponent<SpeechManager>().GetMessage();
            i++;
            if(message!=prevMessage){
                speaking = true;
                //add something here to update text box with message text
            }
            if(i==3 && speaking){
                i = 0;
                speaking = false;
            }
            else if(i==3 && !speaking){
                i = 0;
                speaking = false;
                SetWaypointLong(message);
                Debug.Log("longitude: " + globalLong);
                //finished speaking so stop recording. store message as description
                active = false;
                
            }
            prevMessage = message;
        }  
    }

    IEnumerator StartListeningTitle(bool active){
        int i = 0;
        string prevMessage = message;
        bool speaking = false;
        while(active){
            yield return new WaitForSeconds(0.8f);
            message = speech.GetComponent<SpeechManager>().GetMessage();
            i++;
            if(message!=prevMessage){
                speaking = true;
                //add something here to update text box with message text
            }
            if(i==3 && speaking){
                i = 0;
                speaking = false;
            }
            else if(i==3 && !speaking){
                i = 0;
                speaking = false;
                SetWaypointTitle(message);
                Debug.Log("title: " + globalWaypointTextTitle);
                //finished speaking so stop recording. store message as description
                active = false;
                
            }
            prevMessage = message;
        }  
    }

    public void SetWaypointLat(string lat)
    {
        // Remove "set latitude" using Replace
        string result = lat.Replace("set latitude", string.Empty);

        // Remove spaces using Replace
        result = result.Replace(" ", string.Empty);
        globalLat = result;
        TextMeshPro coordText = waypointConfirmationScreen.transform.Find("Text/LatText").GetComponent<TextMeshPro>();
        coordText.text = "Latitude: " + result;
    }

    public void SetWaypointLong(string longt)
    {
        string result = longt.Replace("set longitude", string.Empty);

        // Remove spaces using Replace
        result = result.Replace(" ", string.Empty);
        globalLong = result;
        TextMeshPro coordText = waypointConfirmationScreen.transform.Find("Text/LongText").GetComponent<TextMeshPro>();
        coordText.text = "Longitude: " + result; //set # of float values shown ?
    }

    // THIS FUNCTION IS CALLED BY VEGA
    public void OpenWaypoint(string type)
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

        OpenConfirmationScreen(type);
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
                createGeoButton(title, letter);

                geoList.Add(newWaypoint);
                break;
            case "regular":
                count++;
                createRegularButton(title, letter);

                missionList.Add(newWaypoint);
                break;
            default:
                Debug.Log("Unknown waypoint type");
                break;
        }

        if (count > 0)
        {
            // The same as above but every waypoint is added to the rover buttons
            createRoverButtons(title, letter);

            roverList.Add(newWaypoint);
        }
 


    }

    private void createRoverButtons(string title, string letter) //need to reintegrate into unity scene
    {
        Transform roverButton = roverScreen.transform.Find("ButtonStuff");
        GameObject newRoverUIPrefab = Instantiate(UIWaypoint, roverButton);
        newRoverUIPrefab.transform.localScale = new Vector3(2.86298656f, 2.71759987f, 0.201269999f);
        newRoverUIPrefab.transform.rotation = Quaternion.identity;
        newRoverUIPrefab.transform.Find("Text/Title").GetComponent<TextMeshPro>().text = title;
        newRoverUIPrefab.transform.Find("Text/Letter").GetComponent<TextMeshPro>().text = letter;
        roverButtons.Add(newRoverUIPrefab);
        firstMessage = 0;
        testButtons.Add(newRoverUIPrefab);
        roverButton.GetComponent<GridObjectCollection>().UpdateCollection();
    }

    private void createGeoButton(string title, string letter)
    {
        // Gets the position of the GeoButtons parent gameobject
        // This is where the buttons will be instaniated
        Transform geoButton = geoScreen.transform.Find("GeoButtons");
        Vector3 position = geoButton.position;

        // Find the yOffset so that the new buttons are below each other
        float yOffset = -0.04f * geoList.Count;
        position.y += yOffset;
        position.z += -0.55f / 100f;
        position.x += .8f / 100f;
        // Create the buttons and set their title and letter
        GameObject newGeoPrefab = Instantiate(UIWaypoint, position, Quaternion.identity, geoButton);
        newGeoPrefab.transform.rotation = Quaternion.identity;
        newGeoPrefab.transform.Find("Text/Title").GetComponent<TextMeshPro>().text = title;
        newGeoPrefab.transform.Find("Text/Letter").GetComponent<TextMeshPro>().text = letter;
        geoButtons.Add(newGeoPrefab);
        testButtons.Add(newGeoPrefab);
    }

    private void createRegularButton(string title, string letter)
    {
        // Gets the position of the GeoButtons parent gameobject
        // This is where the buttons will be instaniated
        Transform missionButton = missionScreen.transform.Find("ButtonStuffMission");
        GameObject newMissionPrefab = Instantiate(UIWaypoint, missionButton);
        newMissionPrefab.transform.localScale = new Vector3(2.86298656f, 2.71759987f, 0.201269999f);
        newMissionPrefab.transform.rotation = Quaternion.identity;
        newMissionPrefab.transform.Find("Text/Title").GetComponent<TextMeshPro>().text = title;
        newMissionPrefab.transform.Find("Text/Letter").GetComponent<TextMeshPro>().text = letter;
        missionButtons.Add(newMissionPrefab);
        firstMessageM = 0;
        testButtons.Add(newMissionPrefab);
        missionButton.GetComponent<GridObjectCollection>().UpdateCollection();
    }


    public void CrewTestNav()
    {
        updateCurrentEnd(crewObject.transform, "Patrick A");
    }

    public void LanderTestNav()
    {
        updateCurrentEnd(landerObject.transform, "Lander");
    }

    string titleOfCurrentWaypoint = "";

    public void updateCurrentSelectedButton(GameObject current, string titleOfObject)
    {
        titleOfCurrentWaypoint = titleOfObject;
        currentSelectedButton = current;
    }

    public void turnOffPastButtonLightBlue()
    {
        if (currentSelectedButton != null)
        {
            currentSelectedButton.SetActive(false);
        }
    }

    string titleLetter = "";


    public void updateCurrentEnd(Transform end, string str)
    {
        playerWithinDistance = false;
        currentEndPosition = end;
        titleLetter = str;
    }

    // Mission E
    public void SelectWaypointLetter(string letter)
    {
        List<GameObject> buttons = new List<GameObject>();

        switch (currentScreenOpen)
        {
            case "Crew":
                buttons = crewButtons;
                break;
            case "Geo":
                buttons = geoButtons;
                break;
            case "Mission":
                buttons = missionButtons;
                break;
            case "Rover":
                buttons = roverButtons;
                break;
            default:
                Debug.Log("No Screen Open For Navigation");
                break;
        }

        // Loop through the list and get the script on the UINavButton game object
        foreach (GameObject button in buttons)
        {
            UINavButton uiScriptButton = button.GetComponent<UINavButton>();
            if (uiScriptButton.GetLetter() == letter)
            {
                uiScriptButton.ButtonSelected();
                break;
            }
        }

    }
    bool playerWithinDistance = false;

    public void StartCheckingDistance(Transform end)
    {
        playerWithinDistance = false;
        StartCoroutine(CheckPlayerDistanceCoroutine(end));
    }

    IEnumerator CheckPlayerDistanceCoroutine(Transform end)
    {
        while (!playerWithinDistance)
        {
            CheckPlayerDistance(end);
            yield return new WaitForSeconds(1f);
        }
    }

    private void CheckPlayerDistance(Transform end)
    {

        // Check the distance between the player and this gameobject
        float distanceToPlayer = Vector3.Distance(mainCam.transform.position, end.position);

        // If the distance is within the threshold, set the bool variable to true and stop looping
        if (distanceToPlayer <= 2.5f)
        {
            playerWithinDistance = true;
            CancelInvoke("CheckPlayerDistance");

            // Destroy breadcrumbs here
            Debug.Log("Cancel Pathfding");
            NavigatableObject.DestroyAllBreadCrumbs();
        }
    }

    IEnumerator OpenRoverNavScreen()
    {
        yield return new WaitForSeconds(1f);
        roverNavScreen.SetActive(true);
        yield return new WaitForSeconds(6.9f);
        roverNavScreen.SetActive(false);
        roverSmallScreen.SetActive(true);
    }
    Transform roverEndLocation = null;
    
    public void StartNav()
    {
        if (currentScreenOpen == "Rover")
        {
            roverThere = false;
            recalled = false;
            roverEndLocation = currentEndPosition;
            roverObjectStartLocation = roverObject;
            CloseAll();
            roverNavScreen.transform.Find("Text/waypointText").GetComponent<TextMeshPro>().text = titleLetter;

            // Give this to NASA
            // Current End Position of where the rover should go: roverEndLocation
            // Start Position: roverObjectStartLocation.transform.position;
            double startLat = roverObject.GetComponent<IsGPSObject>().coords.latitude;
            double startLong = roverObject.GetComponent<IsGPSObject>().coords.longitude;

            //.GetComponent<TSSConnection>().SendRoverNavigateCommand((float)startLat, (float)startLong);
            telemMan.GetComponent<TelemetryServerManager>().tss.SendRoverNavigateCommand((float)startLat, (float)startLong);
            Debug.Log("Start ROver Navigation");
            
            _updateRoverObjectCoords();
            updateRoverLocation();
            StartCoroutine(OpenRoverNavScreen());

        }
        else if (currentEndPosition != null)
        {

            NavigatableObject.DestroyAllBreadCrumbs();
            Transform playerPosition = mainCam.transform;


            //if (ToggleFinalDestinationForCorrectEndTarget(endPosition))
            //{
            gameManager.GetComponent<Pathfinding>().startPathFinding(playerPosition, currentEndPosition);
            StartCheckingDistance(currentEndPosition);
            previousEndGoal = currentEndPosition;
            //}
            CloseAll();

            PopUpManager.MakePopup("Starting navigation to " + titleOfCurrentWaypoint);
        }



    }

    bool roverThere = false;
    bool recalled = false;
    // Every second update the rover location on the map
    IEnumerator _updateRoverLocation(float totalDis)
    {
        while (!roverThere && !recalled)
        {
            yield return new WaitForSeconds(1f);
            roverObject.transform.position = roverObject.transform.position; // Update where it is from NASA TSS
            updateRoverProgress(totalDis);
        }
    }

    private void RoverMadeIt()
    {
        roverThere = true;
        PopUpManager.MakePopup("Rover arrived at its destination!");
        roverSmallScreen.SetActive(false);
        roverNavScreen.SetActive(false);

    }

    public void updateRoverProgress(float totalDis)
    {
        float percentageDone = (totalDis - Vector3.Distance(roverObject.transform.position, roverEndLocation.position)) / totalDis * 100;

        // Update UI based on the percentage


        slider.GetComponent<RoverProgressHandler>().UpdateProgressBar(percentageDone);
        slider2.GetComponent<RoverProgressHandler>().UpdateProgressBar(percentageDone);
        string prog = Simulation.User.ROVER.navigation_status;
        transform.Find("RoverUpdateScreen/Text/StatusText").GetComponent<TextMeshPro>().text = "Status: " + prog;

        if (percentageDone >= 95)
        {
            RoverMadeIt();
        }

    }

    public void RecallRover()
    {
        recalled = true;

        // GIVE TO NASA
        // Give this position to bring rover back to beginning
        // roverObjectStartLocation.transform.position;
        telemMan.GetComponent<TelemetryServerManager>().tss.SendRoverRecallCommand();

        // Turn off 
        StartCoroutine(_recallRover());
    }

    IEnumerator _recallRover()
    {
        yield return new WaitForSeconds(1f);
        roverSmallScreen.SetActive(false);
    }

    public void closeRoverConfirm ()
    {
        StartCoroutine(_closeRoverScreen());
    }

    IEnumerator _closeRoverScreen()
    {
        yield return new WaitForSeconds(1f);
        roverNavScreen.SetActive(false);
        roverSmallScreen.SetActive(true);
    }

    private void updateRoverLocation()
    {
        float totalRoverDistance = Vector3.Distance(roverEndLocation.position, roverObject.transform.position);
        StartCoroutine(_updateRoverLocation(totalRoverDistance));
    }

    IEnumerator _updateRoverObjectCoords()
    {
        while (!roverThere && !recalled)
        {
            yield return new WaitForSeconds(1f);
            GPSCoords coords = new GPSCoords(Simulation.User.ROVER.lat, Simulation.User.ROVER.lon);
            Vector3 location = GPSUtils.GPSCoordsToAppPosition(coords);
            roverObject.transform.position = location;
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
            NavigatableObject.DestroyAllBreadCrumbs();
        }
    }

    public void EndNavigation()
    {
        NavigatableObject.DestroyAllBreadCrumbs();
    }

}
