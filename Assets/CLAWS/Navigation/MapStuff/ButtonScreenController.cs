using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScreenController : MonoBehaviour
{

    [SerializeField] GameObject mainScreen;
    [SerializeField] GameObject crewScreen;
    [SerializeField] GameObject missionScreen;
    [SerializeField] GameObject openMapButton;
    [SerializeField] GameObject startNav1; // Rover
    [SerializeField] GameObject startNav2; // Lander
    [SerializeField] GameObject startNav3; // Crew
    [SerializeField] GameObject startNav4; // Mission
    [SerializeField] Camera mapCam;
    [SerializeField] Camera mainCam;
    [SerializeField] GameObject navObject;

    [SerializeField] GameObject roverObject;


    // Start is called before the first frame update
    void Start()
    {
        mainScreen.SetActive(false);
        crewScreen.SetActive(false);
        missionScreen.SetActive(false);
        CloseNavButtons();
        SetAllCullingToCamera();
        openMapButton.SetActive(true);

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
        StartCoroutine(_OpenCrewScreen());
    }

    IEnumerator _OpenCrewScreen()
    {
        yield return new WaitForSeconds(1f);
        CloseNavButtons();
        mainScreen.SetActive(false);
        missionScreen.SetActive(false);

        ShowOnlyCrewIcons();
        crewScreen.SetActive(true);
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

        ShowOnlyMissionIcons();
        missionScreen.SetActive(true);
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

        ShowOnlyRoverIcons();
        startNav1.SetActive(true);
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
        CloseAll();
    }

    public void StartCrewNavigation()
    {
        CloseAll();
    }

    public void StartMissionNavigation()
    {
        CloseAll();
    }

    void StartNav(Transform endPosition)
    {
        Transform playerPosition = mainCam.transform;

        navObject.GetComponent<Pathfinding>().startPathFinding(playerPosition, endPosition);
    }
}
