using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Screens
{
    UIA,
    Vitals,
    Geosampling,
    Messaging,
    Navigation, 
    TaskList
}
[System.Serializable]
public enum LUNAState
{
    left, right, center
}

public class ScreenManager : MonoBehaviour
{

    GameObject UIAPanel;
    GameObject VitalsPanel;
    GameObject GeosamplingPanel;
    GameObject MessagingPanel;
    GameObject NavigationPanel;

    // STATE MACHINE
    public static Screens CurrScreen = Screens.UIA;
    public static LUNAState LUNA = LUNAState.center;

    public void SwitchScreen(string ScreenToShow)
    {
        switch (ScreenToShow)
        {
            case "UIA":
                UIAPanel.SetActive(true);
                VitalsPanel.SetActive(false);
                GeosamplingPanel.SetActive(false);
                MessagingPanel.SetActive(false);
                NavigationPanel.SetActive(false);
                CurrScreen = Screens.UIA;
                break;
            case "Vitals":
                UIAPanel.SetActive(false);
                VitalsPanel.SetActive(true);
                GeosamplingPanel.SetActive(false);
                MessagingPanel.SetActive(false);
                NavigationPanel.SetActive(false);
                CurrScreen = Screens.Vitals;
                break;
            case "Geosampling":
                UIAPanel.SetActive(false);
                VitalsPanel.SetActive(false);
                GeosamplingPanel.SetActive(true);
                MessagingPanel.SetActive(false);
                NavigationPanel.SetActive(false);
                CurrScreen = Screens.Geosampling;
                break;
            case "Messaging":
                UIAPanel.SetActive(false);
                VitalsPanel.SetActive(false);
                GeosamplingPanel.SetActive(false);
                MessagingPanel.SetActive(true);
                NavigationPanel.SetActive(false);
                CurrScreen = Screens.Messaging;
                break;
            case "Navigation":
                UIAPanel.SetActive(false);
                VitalsPanel.SetActive(false);
                GeosamplingPanel.SetActive(false);
                MessagingPanel.SetActive(false);
                NavigationPanel.SetActive(true);
                CurrScreen = Screens.Navigation;
                break;
            default:
                break;
        }
        Debug.Log("Curr Screen = " + ScreenToShow.ToString());
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
