using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Screens
{
    Home,

    UIA,

    Vitals,

    Geosampling,
    Geosample_Expanded,
    Geosample_Description,
    Geosample_Gallery,
    Geosample_Camera,
    Geosample_Confirm,

    Messaging,
    Messaging_MCC,
    Messaging_Jane,
    Messaging_Neil,

    Navigation,
    Navigation_Crew,
    Navigation_Geo,
    Navigation_Mission,
    Navigation_Rover,
    Navigation_Lander,
    Navigation_Rover_Confirm,
    Navigation_Waypoint_Confirm,

    TaskList,
    TaskList_CurrentTask
}
[System.Serializable]
public enum LUNAState
{
    left, right, center
}

public class ScreenManager : MonoBehaviour
{
    public GameObject UIAPanel;
    public List<GameObject> VitalsPanel;
    public GeoSampleVegaController GeosamplingPanel;
    public GameObject MessagingPanel;
    public NavScreenController NavigationPanel;
    public LUNAFOVManager lunafov;

    // STATE MACHINE
    public static Screens CurrScreen = Screens.Home;
    public static LUNAState LUNA = LUNAState.center;

    private void Start()
    {
        lunafov = GetComponent<LUNAFOVManager>();
        EventBus.Subscribe<ScreenChangedEvent>(SwitchScreen);
    }

    public void SwitchScreen(ScreenChangedEvent e)
    {
        Debug.Log(CurrScreen.ToString() + " -> " + e.screen.ToString());

        CurrScreen = e.screen;
        LUNA = e.luna;

        // handle luna
        if (LUNA == LUNAState.center)
        {
            lunafov.LUNACenter();
        }
        else if (LUNA == LUNAState.left)
        {
            lunafov.LUNALeft();
        }
        else if (LUNA == LUNAState.right)
        {
            lunafov.LUNARight();
        }
    }
    
    public void CloseScreen()
    {
        EventBus.Publish<CloseEvent>(new CloseEvent(CurrScreen));
        CurrScreen = Screens.Home;
    }

    public static void ScrollUp()
    {
        if (LUNA == LUNAState.center)
        {
            EventBus.Publish<ScrollEvent>(new ScrollEvent(CurrScreen, Direction.up));
        }
        else if (LUNA == LUNAState.right)
        {
            EventBus.Publish<ScrollEvent>(new ScrollEvent(Screens.TaskList, Direction.up));
        }
        else if (LUNA == LUNAState.left)
        {
            EventBus.Publish<ScrollEvent>(new ScrollEvent(Screens.Navigation, Direction.up));
        }

    }

    public static void ScrollDown()
    {
        if (LUNA == LUNAState.center)
        {
            EventBus.Publish<ScrollEvent>(new ScrollEvent(CurrScreen, Direction.down));
        }
        else if (LUNA == LUNAState.right)
        {
            EventBus.Publish<ScrollEvent>(new ScrollEvent(Screens.TaskList, Direction.down));
        }
    }

    // RANDOM STUFF
    public void CloseScreen(GameObject Screen)
    {
        StartCoroutine(_CloseScreen(Screen));
    }
    IEnumerator _CloseScreen(GameObject Screen)
    {
        yield return new WaitForSeconds(1f);
        Screen.SetActive(false);
    }
}
