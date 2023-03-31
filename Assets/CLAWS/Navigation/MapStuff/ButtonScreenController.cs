using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScreenController : MonoBehaviour
{

    [SerializeField] GameObject mainScreen;
    [SerializeField] GameObject crewScreen;
    [SerializeField] GameObject missionScreen;
    [SerializeField] GameObject openMapButton;
    [SerializeField] GameObject confirmCreationScreen;
    [SerializeField] GameObject startNav1; // Rover
    [SerializeField] GameObject startNav2; // Lander
    [SerializeField] GameObject startNav3; // Crew
    [SerializeField] GameObject startNav4; // Mission
    [SerializeField] Camera cam;
    [SerializeField] GameObject navObject;

    [SerializeField] GameObject roverObject;



    // Start is called before the first frame update
    void Start()
    {
        CloseAll();

    }

    public void OpenMainScreen()
    {
        openMapButton.SetActive(false);
        mainScreen.SetActive(true);
    }

    public void CloseAll()
    {
        mainScreen.SetActive(false);
        crewScreen.SetActive(false);
        missionScreen.SetActive(false);
        confirmCreationScreen.SetActive(false);
        CloseNavButtons();
        SetAllCullingToCamera();
        openMapButton.SetActive(true);
    }

    public void CloseNavButtons()
    {
        startNav1.SetActive(false);
        startNav2.SetActive(false);
        startNav3.SetActive(false);
        startNav4.SetActive(false);
    }

    public void OpenCrewScreen()
    {
        CloseNavButtons();
        mainScreen.SetActive(false);
        missionScreen.SetActive(false);

        ShowOnlyCrewIcons();
        crewScreen.SetActive(true);
    }

    public void OpenMissionScreen()
    {
        CloseNavButtons();
        mainScreen.SetActive(false);
        crewScreen.SetActive(false);

        ShowOnlyMissionIcons();
        missionScreen.SetActive(true);
    }

    public void HoverOnRover()
    {
        crewScreen.SetActive(false);
        missionScreen.SetActive(false);
        CloseNavButtons();
        OpenMainScreen();


        startNav2.SetActive(false);
        startNav3.SetActive(false);
        startNav4.SetActive(false);

        ShowOnlyRoverIcons();
        startNav1.SetActive(true);
    }

    public void HoverOnLander()
    {
        crewScreen.SetActive(false);
        missionScreen.SetActive(false);
        CloseNavButtons();
        OpenMainScreen();

        startNav1.SetActive(false);
        startNav3.SetActive(false);
        startNav4.SetActive(false);

        ShowOnlyLanderIcons();
        startNav2.SetActive(true);
    }

    public void HoverOnCrew()
    {
        startNav1.SetActive(false);
        startNav2.SetActive(false);
        startNav4.SetActive(false);
        startNav3.SetActive(true);
    }

    public void HoverOnMission()
    {
        startNav1.SetActive(false);
        startNav2.SetActive(false);
        startNav3.SetActive(false);
        startNav4.SetActive(true);
    }

    // Icon Stuff
    // cam.cullingMask |= 1 << 3; adds the index 3 layer to the culling mask
    // cam.cullingMask &= ~(1 << 3); removes the index 3 layer to the culling mask
    // Instead of index of 3 you can use: cam.cullingMask |= 1 << LayerMask.NameToLayer("MyLayer");

    void SetAllCullingToCamera()
    {
        cam.cullingMask |= 1 << LayerMask.NameToLayer("CrewLayer");
        cam.cullingMask |= 1 << LayerMask.NameToLayer("MissionLayer");
        cam.cullingMask |= 1 << LayerMask.NameToLayer("RoverLayer");
        cam.cullingMask |= 1 << LayerMask.NameToLayer("LanderLayer");
    }

    void RemoveAllCullingToCamera()
    {
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("CrewLayer"));
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("MissionLayer"));
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("RoverLayer"));
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("LanderLayer"));
    }
    void ShowOnlyCrewIcons()
    {
        RemoveAllCullingToCamera();
        cam.cullingMask |= 1 << LayerMask.NameToLayer("CrewLayer");
    }

    void ShowOnlyMissionIcons()
    {
        RemoveAllCullingToCamera();
        cam.cullingMask |= 1 << LayerMask.NameToLayer("MissionLayer");
    }

    void ShowOnlyRoverIcons()
    {
        RemoveAllCullingToCamera();
        cam.cullingMask |= 1 << LayerMask.NameToLayer("RoverLayer");
    }

    void ShowOnlyLanderIcons()
    {
        RemoveAllCullingToCamera();
        cam.cullingMask |= 1 << LayerMask.NameToLayer("LanderLayer");
    }

    // Functions that start the said navigation
    public void StartRoverNavigation()
    {
        Transform end = roverObject.transform;

        StartNav(end);
    }

    public void StartLanderNavigation()
    {

    }

    public void StartCrewNavigation()
    {

    }

    public void StartMissionNavigation()
    {

    }

    void StartNav(Transform endPosition)
    {
        Transform playerPosition = cam.transform;

        navObject.GetComponent<Pathfinding>().startPathFinding(playerPosition, endPosition);
    }


    // WAYPOINTS
    public void OpenConfirmation()
    {
        confirmCreationScreen.SetActive(true);
    }

    public void ConfirmCreation()
    {
        // Create waypoint based on tag text.


        CloseConfirmation();
    }

    public void CloseConfirmation()
    {
        confirmCreationScreen.SetActive(false);
    }

    // Create certain types of waypoints
    public void CreateWaypoint()
    {
        CreateWaypoints way = GetComponent<CreateWaypoints>();
        way.CreateWaypoint();
    }

    public void CreateGeo()
    {

    }

    public void CreateDanger()
    {

    }
}
