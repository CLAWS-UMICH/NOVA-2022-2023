using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

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
    [SerializeField] Camera mapCam;
    [SerializeField] Camera mainCam;
    [SerializeField] GameObject navObject;

    [SerializeField] GameObject roverObject;

    Transform previousEndGoal = null;


    // Start is called before the first frame update
    void Start()
    {
        mainScreen.SetActive(false);
        crewScreen.SetActive(false);
        missionScreen.SetActive(false);
        CloseNavButtons();
        openMapButton.SetActive(true);
        SetAllCullingToCamera();

    }

    public void CloseScreen(GameObject Screen)
    {
        StartCoroutine(_CloseScreen(Screen));
    }
    IEnumerator _CloseScreen(GameObject Screen)
    {
        yield return new WaitForSeconds(1f);
        Screen.SetActive(false);
    }

    public void OpenMainScreen()
    {
        openMapButton.SetActive(false);
        mainScreen.SetActive(true);
    }

    public void CloseAll()
    {
        StartCoroutine(_CloseAllScreens());
    }

    IEnumerator _CloseAllScreens()
    {
        yield return new WaitForSeconds(1f);

        mainScreen.SetActive(false);
        crewScreen.SetActive(false);
        missionScreen.SetActive(false);
        confirmCreationScreen.SetActive(false);
        CloseNavButtons();
        openMapButton.SetActive(true);
        SetAllCullingToCamera();
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
        StartCoroutine(_OpenCrewScreen());
    }

    IEnumerator _OpenCrewScreen()
    {
        yield return new WaitForSeconds(1f);
        CloseNavButtons();
        mainScreen.SetActive(false);
        missionScreen.SetActive(false);
        crewScreen.SetActive(true);
        ShowOnlyCrewIcons();
    }

    public void OpenMissionScreen()
    {
        StartCoroutine(_OpenMissionScreen());
    }

    IEnumerator _OpenMissionScreen()
    {
        yield return new WaitForSeconds(1f);
        CloseNavButtons();
        mainScreen.SetActive(false);
        crewScreen.SetActive(false);
        missionScreen.SetActive(true);
        ShowOnlyMissionIcons();
    }

    public void HoverOnRover()
    {
        StartCoroutine(_HoverOnRover());
    }

    IEnumerator _HoverOnRover()
    {
        yield return new WaitForSeconds(1f);
        crewScreen.SetActive(false);
        missionScreen.SetActive(false);
        CloseNavButtons();
        OpenMainScreen();


        startNav2.SetActive(false);
        startNav3.SetActive(false);
        startNav4.SetActive(false);
        startNav1.SetActive(true);
        ShowOnlyRoverIcons();
    }

    public void HoverOnLander()
    {
        StartCoroutine(_HoverOnLander());
    }

    IEnumerator _HoverOnLander()
    {
        yield return new WaitForSeconds(1f);
        crewScreen.SetActive(false);
        missionScreen.SetActive(false);
        CloseNavButtons();
        OpenMainScreen();

        startNav1.SetActive(false);
        startNav3.SetActive(false);
        startNav4.SetActive(false);
        startNav2.SetActive(true);
        ShowOnlyLanderIcons();
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
        mapCam.cullingMask |= 1 << LayerMask.NameToLayer("CrewLayer");
        mapCam.cullingMask |= 1 << LayerMask.NameToLayer("MissionLayer");
        mapCam.cullingMask |= 1 << LayerMask.NameToLayer("RoverLayer");
        mapCam.cullingMask |= 1 << LayerMask.NameToLayer("LanderLayer");
    }

    void RemoveAllCullingToCamera()
    {
        mapCam.cullingMask &= ~(1 << LayerMask.NameToLayer("CrewLayer"));
        mapCam.cullingMask &= ~(1 << LayerMask.NameToLayer("MissionLayer"));
        mapCam.cullingMask &= ~(1 << LayerMask.NameToLayer("RoverLayer"));
        mapCam.cullingMask &= ~(1 << LayerMask.NameToLayer("LanderLayer"));

    }
    void ShowOnlyCrewIcons()
    {
        RemoveAllCullingToCamera();
        mapCam.cullingMask |= 1 << LayerMask.NameToLayer("CrewLayer");
    }

    void ShowOnlyMissionIcons()
    {
        RemoveAllCullingToCamera();
        mapCam.cullingMask |= 1 << LayerMask.NameToLayer("MissionLayer");
    }

    void ShowOnlyRoverIcons()
    {
        RemoveAllCullingToCamera();
        mapCam.cullingMask |= 1 << LayerMask.NameToLayer("RoverLayer");
    }

    void ShowOnlyLanderIcons()
    {
        RemoveAllCullingToCamera();
        mapCam.cullingMask |= 1 << LayerMask.NameToLayer("LanderLayer");
    }

    // Functions that start the said navigation
    public void StartRoverNavigation()
    {
        Transform end = roverObject.transform;

        StartNav(end);

        CloseAll();
    }

    public void StartLanderNavigation()
    {
        Transform end = roverObject.transform;

        StartNav(end);

        CloseAll();
    }

    public void StartCrewNavigation()
    {
        Transform end = roverObject.transform;

        StartNav(end);

        CloseAll();
    }

    public void StartMissionNavigation()
    {
        Transform end = roverObject.transform;

        StartNav(end);

        CloseAll();
    }

    void StartNav(Transform endPosition)
    {
        Transform playerPosition = mainCam.transform;


        if (ToggleFinalDestinationForCorrectEndTarget(endPosition))
        {
            navObject.GetComponent<Pathfinding>().startPathFinding(playerPosition, endPosition);

            previousEndGoal = endPosition;
        }

        
    }

    private bool ToggleFinalDestinationForCorrectEndTarget(Transform endPosition)
    {
        NavigatableObject endNavigation = endPosition.gameObject.GetComponent<NavigatableObject>();

        if (endNavigation == null)
        {
            Debug.LogError("Object must contain the 'Navigatable Object' script in order to be considered a valid end position");
            return false;
        } else
        {
            if (previousEndGoal != null)
            {
                NavigatableObject prevNav = previousEndGoal.GetComponent<NavigatableObject>();
                if (prevNav == null)
                {
                    return false;
                } else
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


    // WAYPOINTS
    void OpenConfirmationScreen(string s)
    {
        Transform textTransform = confirmCreationScreen.transform.Find("Text");
        if (textTransform != null)
        {
            Transform typeTransform = textTransform.Find("Type");
            if (typeTransform != null)
            {
                TextMeshPro typeText = typeTransform.GetComponent<TextMeshPro>();
                if (typeText != null)
                {
                    typeText.text = s;
                }
            }
        }
        confirmCreationScreen.SetActive(true);
    }

    public void OpenWaypoint(string tag, string title)
    {
        //console.log("Tag " + tag + " Title: " + title);
    }

    public void OpenRegWaypoint(string type, string title)
    {
        OpenConfirmationScreen("Regular Waypoint");
    }

    public void OpenCreateGeo(string type, string title)
    {
        OpenConfirmationScreen("Geo Sample Waypoint");
    }

    public void OpenCreateDanger(string type, string title)
    {
        OpenConfirmationScreen("Danger Waypoint");
    }

    public void CloseConfirmation(string type, string title)
    {
        confirmCreationScreen.SetActive(false);
    }

    public void CreateAPoint()
    {
        string type = "";
        Transform textTransform = confirmCreationScreen.transform.Find("Text");
        if (textTransform != null)
        {
            Transform typeTransform = textTransform.Find("Type");
            if (typeTransform != null)
            {
                TextMeshPro typeText = typeTransform.GetComponent<TextMeshPro>();
                if (typeText != null)
                {
                    type = typeText.text;
                }
            }
        }

        CreateWaypoints way = GetComponent<CreateWaypoints>();
        way.CreateWaypoint(type);
        CloseConfirmation();
    }

 
}
