using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Text.RegularExpressions;
using Microsoft.MixedReality.Toolkit.Utilities;
using TSS;
using TSS.Msgs;
using UnityEngine.InputSystem;

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
    [SerializeField] GameObject janeObject;
    [SerializeField] GameObject neilObject;
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
        //EventBus.Subscribe<ScrollEvent>(ScrollProperScreen);
        EventBus.Subscribe<CloseEvent>(CloseNavigation);
        EventBus.Subscribe<ScrollEvent>(ScrollManager);

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
            EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.Home, LUNAState.center));
        }

    }

    private void ScrollManager(ScrollEvent e)
    {
        if (e.direction == Direction.up) 
        {
            if (e.screen == Screens.Navigation_Mission)
            {
                ScrollUpMission();
            }

            if (e.screen == Screens.Navigation_Rover)
            {
                ScrollUp();
            }
        }
        else if (e.direction == Direction.down)
        {
            if (e.screen == Screens.Navigation_Mission)
            {
                ScrollDownMission();
            }

            if (e.screen == Screens.Navigation_Rover)
            {
                ScrollDown();
            }
        }
    }

    public void CloseAll()
    {
        EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.Home, LUNAState.center));
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
        EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.Navigation, LUNAState.center));

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
        EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.Navigation_Crew, LUNAState.center));
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
        EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.Navigation_Geo, LUNAState.center));
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
        EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.Navigation_Mission, LUNAState.center));
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
        EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.Navigation_Rover, LUNAState.center));
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
        EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.Navigation_Lander, LUNAState.center));
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





        Waypoint newCrew = new Waypoint(janeObject.transform, "Jane", (Type)System.Enum.Parse(typeof(Type), "crew"));
        janeObject.transform.Find("Letter/LetterText").GetComponent<TextMeshPro>().text = newCrew.GetLetter();
        crewList.Add(newCrew);
        allWaypoints.Add(newCrew);

        Waypoint newCrew2 = new Waypoint(neilObject.transform, "Neil", (Type)System.Enum.Parse(typeof(Type), "crew"));
        neilObject.transform.Find("Letter/LetterText").GetComponent<TextMeshPro>().text = newCrew2.GetLetter();
        
        crewList.Add(newCrew2);
        allWaypoints.Add(newCrew2);

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

    int firstMessageM = 0; // Keeps track of the top option in our gridCollections

    
    // Scrolls Rover Up
    public void ScrollUp()
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
    

    // Scrolls Mission Up
    public void ScrollUpMission()
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
    

    // Scroll Rover Down
    public void ScrollDown()
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


    // Scroll Down Mission
    public void ScrollDownMission()
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



    // ================== NEW SCROLL EVENT METHODS ==========================
    // Added the ability to respond to ScrollEvents to move the navigation lists

    /*
    private void ScrollProperScreen(ScrollEvent e)
    {
        
        switch(e.screen)
        {
            case Screens.Navigation_Crew:
                if (e.direction == Direction.up)
                {
                    ScrollUp(crewScreen, crewButtons);
                } else
                {
                    ScrollDown(crewScreen, crewButtons);
                }
                break;

            case Screens.Navigation_Geo:
                if (e.direction == Direction.up)
                {
                    ScrollUp(geoScreen, geoButtons);
                }
                else
                {
                    ScrollDown(geoScreen, geoButtons);
                }
                break;

            case Screens.Navigation_Mission:
                //Debug.Log("01");
                if (e.direction == Direction.up)
                {
                    ScrollUp(missionScreen, missionButtons);
                }
                else
                {
                    ScrollDown(missionScreen, missionButtons);
                }
                break;


            case Screens.Navigation_Rover:
                if (e.direction == Direction.up)
                {
                    ScrollUp(roverScreen, roverButtons);
                }
                else
                {
                    ScrollDown(roverScreen, roverButtons);
                }
                break;

            default:
                Debug.Log(":(");
                return;
        }
    }



    // General Scroll Up function for navigation windows
    private void ScrollUp(GameObject screen, List<GameObject> buttons)
    {
        //Debug.Log("Going Up");
        GameObject button = GiveAppropriateGameObjectFromChildren(screen);

        if (button == null)
        {
            return;
        }

        int len = buttons.Count;
        if (firstMessage + 5 < len && firstMessage >= 0)
        {
            buttons[firstMessage].SetActive(false);
            buttons[firstMessage + 5].SetActive(true);
            firstMessage++;
            button.GetComponent<GridObjectCollection>().UpdateCollection();
            StartCoroutine(updateCollection(button));
        }
    }

    // General Scroll Down function for navigation windows
    private void ScrollDown(GameObject screen, List<GameObject> buttons)
    {
        //Debug.Log("Going Down");
        GameObject button = GiveAppropriateGameObjectFromChildren(screen);

        if (button == null)
        {
            return;
        }

        int len = buttons.Count;
        if (firstMessage + 4 < len && firstMessage >= 0)
        {
            if (firstMessage != 0)
            {
                Debug.Log("firstMessage: " + firstMessage);

                buttons[firstMessage - 1].SetActive(true);
                buttons[firstMessage + 4].SetActive(false);
                firstMessage--;
                button.GetComponent<GridObjectCollection>().UpdateCollection();
                StartCoroutine(updateCollection(button));
            }
        }
    }

    // Gets the button from the child of the screen gameObject
    private GameObject GiveAppropriateGameObjectFromChildren(GameObject screen)
    {
        GameObject button;
        if (screen == missionScreen)
        {
            button = screen.transform.Find("ButtonStuffMission").gameObject;
        }
        else
        {
            button = screen.transform.Find("ButtonStuff").gameObject;
        }

        return button;
    }

    

    
    // Debug methods to simulate events getting sent
    public void RaiseScrollEventUp()
    {

        // TODO fix please samuel

        Debug.Log("Scroll current screen up");
       // ScrollProperScreen(new ScrollEvent(StateMachineNOVA.CurrScreen, Direction.up));
    }

    public void RaiseScrollEventDown()
    {

        // TODO fix please samuel

        Debug.Log("Scroll current screen down");
       // ScrollProperScreen(new ScrollEvent(StateMachineNOVA.CurrScreen, Direction.down));
    }
    
    // =======================================================================
    */


    IEnumerator updateCollection(GameObject buttonType)
    {
        yield return new WaitForSeconds(0f);
        buttonType.GetComponent<GridObjectCollection>().UpdateCollection();
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
        EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.Navigation_Waypoint_Confirm, LUNAState.center));
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
        EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.Home, LUNAState.center));
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


    public void CrewTestNavJane()
    {
        updateCurrentEnd(janeObject.transform, "Jane J", "");
    }

    public void CrewTestNavNeil()
    {
        updateCurrentEnd(neilObject.transform, "Neil K", "");
    }

    public void LanderTestNav()
    {
        updateCurrentEnd(landerObject.transform, "Lander", "");
    }

    string titleOfCurrentWaypoint = "";

    public void updateCurrentSelectedButton(GameObject current, string titleOfObject, string letter)
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
    string roverLetter = "";

    public void updateCurrentEnd(Transform end, string str, string l)
    {
        playerWithinDistance = false;
        currentEndPosition = end;
        titleLetter = str;
        roverLetter = l;
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
        EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.Navigation_Rover_Confirm, LUNAState.center));
        yield return new WaitForSeconds(1f);
        roverNavScreen.SetActive(true);
        yield return new WaitForSeconds(6.9f);

        if (!recalled)
        {
            roverNavScreen.SetActive(false);
            roverSmallScreen.SetActive(true);
            EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.Home, LUNAState.center));
        }
    }

    IEnumerator RoverFromMenu()
    {
        yield return new WaitForSeconds(1f);
        roverNavScreen.SetActive(true);
        roverSmallScreen.SetActive(false);
    }

    public void OpenRoverNavScreenFromMenu()
    {
        StartCoroutine(RoverFromMenu());
        EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.Navigation_Rover_Confirm, LUNAState.center));

    }
    Transform roverEndLocation = null;
    
    public void StartNav()
    {
        if (currentScreenOpen == "Rover")
        {
            GameObject roverEndGameObject = missionA;

            switch (roverLetter)
            {
                case "A":
                    roverEndGameObject = missionA;
                    break;
                case "B":
                    roverEndGameObject = missionB;
                    break;
                case "C":
                    roverEndGameObject = missionC;
                    break;
                case "D":
                    roverEndGameObject = missionD;
                    break;
                case "E":
                    roverEndGameObject = missionE;
                    break;
                case "F":
                    roverEndGameObject = missionF;
                    break;
                case "G":
                    roverEndGameObject = missionG;
                    break;
                case "H":
                    roverEndGameObject = missionH;
                    break;
                case "I":
                    roverEndGameObject = missionI;
                    break;
                default:
                    Debug.Log("No waypoint");
                    break;
            }

            
            roverThere = false;
            recalled = false;
            roverEndLocation = currentEndPosition;
            roverObjectStartLocation = roverObject;
            CloseAll();
            roverNavScreen.transform.Find("Text/waypointText").GetComponent<TextMeshPro>().text = titleLetter;

            // Give this to NASA
            // Current End Position of where the rover should go: roverEndLocation
            // Start Position: roverObjectStartLocation.transform.position;

            // TODO fix send the waypoint location rather than rover location
            double startLat = roverEndGameObject.GetComponent<IsGPSObject>().coords.latitude;
            double startLong = roverEndGameObject.GetComponent<IsGPSObject>().coords.longitude;


            //.GetComponent<TSSConnection>().SendRoverNavigateCommand((float)startLat, (float)startLong);
            telemMan.GetComponent<TelemetryServerManager>().tss.SendRoverNavigateCommand((float)startLat, (float)startLong);
            
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

    }

    public void updateRoverProgress(float totalDis)
    {
        float percentageDone = (totalDis - Vector3.Distance(roverObject.transform.position, roverEndLocation.position)) / totalDis * 100;

        // Update UI based on the percentage


        slider.GetComponent<RoverProgressHandler>().UpdateProgressBar(percentageDone, roverLetter);
        slider2.GetComponent<RoverProgressHandler>().UpdateProgressBar(percentageDone, roverLetter);
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
        roverNavScreen.SetActive(true);
        EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.Navigation_Rover_Confirm, LUNAState.center));

    }

    public void closeRoverConfirm ()
    {
        StartCoroutine(_closeRoverScreen());
        EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.Home, LUNAState.center));
    }

    IEnumerator _closeRoverScreen()
    {
        yield return new WaitForSeconds(1f);
        roverNavScreen.SetActive(false);
        if (!recalled)
        {
            roverSmallScreen.SetActive(true);
        }
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
