using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

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
    [SerializeField] GameObject geoObject;
    [SerializeField] GameObject mission1Object;
    [SerializeField] GameObject mission3Object;

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
        
    }
    // Start is called before the first frame update

 

    void Start()
    {
        roverObjectStartLocation = roverObject;
        titleLetter = "";
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
        SetAllCullingToCamera();


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
        mapCam.cullingMask |= 1 << LayerMask.NameToLayer("LanderLayer");
        mapCam.cullingMask |= 1 << LayerMask.NameToLayer("RoverLayer");
        mapCam.cullingMask &= ~(1 << LayerMask.NameToLayer("CrewLayer"));
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

        Waypoint newCrew = new Waypoint(crewObject.transform, "Patrick", (Type)System.Enum.Parse(typeof(Type), "crew"));
        crewObject.transform.Find("Icons/Letter/LetterText").GetComponent<TextMeshPro>().text = newCrew.GetLetter();
        crewList.Add(newCrew);
        allWaypoints.Add(newCrew);

        Waypoint newGeo = new Waypoint(geoObject.transform, "MRS-001", (Type)System.Enum.Parse(typeof(Type), "geosample"));
        geoObject.transform.Find("Icons/Letter/LetterText").GetComponent<TextMeshPro>().text = newGeo.GetLetter();
        createGeoButton(newGeo.GetTitle(), newGeo.GetLetter());
        createRoverButtons(newGeo.GetTitle(), newGeo.GetLetter());
        geoList.Add(newGeo);
        roverList.Add(newGeo);
        allWaypoints.Add(newGeo);

        Waypoint newMission1 = new Waypoint(mission1Object.transform, "Lunar Hiking", (Type)System.Enum.Parse(typeof(Type), "regular"));
        mission1Object.transform.Find("Icons/Letter/LetterText").GetComponent<TextMeshPro>().text = newMission1.GetLetter();
        createRegularButton(newMission1.GetTitle(), newMission1.GetLetter());
        createRoverButtons(newMission1.GetTitle(), newMission1.GetLetter());
        missionList.Add(newMission1);
        roverList.Add(newMission1);
        allWaypoints.Add(newMission1);


        Waypoint newMission3 = new Waypoint(mission3Object.transform, "Lunar Mapping", (Type)System.Enum.Parse(typeof(Type), "regular"));
        mission3Object.transform.Find("Icons/Letter/LetterText").GetComponent<TextMeshPro>().text = newMission3.GetLetter();
        createRegularButton(newMission3.GetTitle(), newMission3.GetLetter());
        createRoverButtons(newMission3.GetTitle(), newMission3.GetLetter());
        missionList.Add(newMission3);
        roverList.Add(newMission3);
        allWaypoints.Add(newMission3);
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
        titleText.text = "Set title with VEGA";


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
        TextMeshPro coordText = waypointConfirmationScreen.transform.Find("Text/CoordText").GetComponent<TextMeshPro>();
        coordText.text = "Set Latitude with VEGA";

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
                Debug.Log("latitute: " + globalLat);
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

    public void SetWaypointLat(string lat)
    {
        globalLat = lat;
        TextMeshPro coordText = waypointConfirmationScreen.transform.Find("Text/CoordText").GetComponent<TextMeshPro>();
        coordText.text = "Set Longitude with VEGA";
    }

    public void SetWaypointLong(string longt)
    {
        globalLong = longt;
        TextMeshPro coordText = waypointConfirmationScreen.transform.Find("Text/CoordText").GetComponent<TextMeshPro>();
        coordText.text = $"{globalLat}, {globalLong}"; //set # of float values shown ?
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
        Transform roverButton = roverScreen.transform.Find("WaypointButtons");
        Vector3 roverPositionUI = roverButton.position;
        float roveryOffset = -0.04f * roverList.Count;
        roverPositionUI.y += roveryOffset;
        roverPositionUI.z += -0.55f / 100f;
        roverPositionUI.x += .8f / 100f;
        GameObject newRoverUIPrefab = Instantiate(UIWaypoint, roverPositionUI, Quaternion.identity, roverButton);
        newRoverUIPrefab.transform.rotation = Quaternion.identity;
        newRoverUIPrefab.transform.Find("Text/Title").GetComponent<TextMeshPro>().text = title;
        newRoverUIPrefab.transform.Find("Text/Letter").GetComponent<TextMeshPro>().text = letter;
        roverButtons.Add(newRoverUIPrefab);
        testButtons.Add(newRoverUIPrefab);
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
        Transform missionButton = missionScreen.transform.Find("MissionButtons");
        Vector3 position = missionButton.position;

        // Find the yOffset so that the new buttons are below each other
        float yOffset = -0.04f * missionList.Count;
        position.y += yOffset;
        position.z += -0.55f / 100f;
        position.x += .8f / 100f;

        // Create the buttons and set their title and letter
        GameObject newMissionPrefab = Instantiate(UIWaypoint, position, Quaternion.identity, missionButton);
        newMissionPrefab.transform.rotation = Quaternion.identity;
        newMissionPrefab.transform.Find("Text/Title").GetComponent<TextMeshPro>().text = title;
        newMissionPrefab.transform.Find("Text/Letter").GetComponent<TextMeshPro>().text = letter;
        missionButtons.Add(newMissionPrefab);
        testButtons.Add(newMissionPrefab);
    }


    public void CrewTestNav()
    {
        updateCurrentEnd(crewObject.transform, "Patrick A");
    }

    public void LanderTestNav()
    {
        updateCurrentEnd(landerObject.transform, "Lander");
    }

    public void updateCurrentSelectedButton(GameObject current)
    {
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
        yield return new WaitForSeconds(10f);
        roverNavScreen.SetActive(false);
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

            updateRoverLocation();
            StartCoroutine(OpenRoverNavScreen());

        }
        else if (currentEndPosition != null)
        {
            Transform playerPosition = mainCam.transform;


            //if (ToggleFinalDestinationForCorrectEndTarget(endPosition))
            //{
            gameManager.GetComponent<Pathfinding>().startPathFinding(playerPosition, currentEndPosition);
            StartCheckingDistance(currentEndPosition);
            previousEndGoal = currentEndPosition;
            //}
            CloseAll();
        }



    }
    bool roverThere = false;
    bool recalled = false;
    // Every second update the rover location on the map
    IEnumerator _updateRoverLocation(float totalDis)
    {
        while (!roverThere || recalled)
        {
            yield return new WaitForSeconds(1f);
            roverObject.transform.position = roverObject.transform.position; // Update where it is from NASA TSS
            updateRoverProgress(totalDis);
        }
    }

    public void updateRoverProgress(float totalDis)
    {
        float percentageDone = Vector3.Distance(roverObject.transform.position, roverEndLocation.position) / totalDis * 100;

        // Update UI based on the percentage

    }

    public void RecallRover()
    {
        recalled = true;

        // GIVE TO NASA
        // Give this position to bring rover back to beginning
        // roverObjectStartLocation.transform.position;
    }

    private void updateRoverLocation()
    {
        float totalRoverDistance = Vector3.Distance(roverEndLocation.position, roverObject.transform.position);
        StartCoroutine(_updateRoverLocation(totalRoverDistance));
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

}
