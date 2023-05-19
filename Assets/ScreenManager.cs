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
    Navigation
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

    public void SwitchScreen(Screens ScreenToShow)
    {
        switch (ScreenToShow)
        {
            case Screens.UIA:
                UIAPanel.SetActive(true);
                VitalsPanel.SetActive(false);
                GeosamplingPanel.SetActive(false);
                MessagingPanel.SetActive(false);
                NavigationPanel.SetActive(false);
                break;
            case Screens.Vitals:
                UIAPanel.SetActive(false);
                VitalsPanel.SetActive(true);
                GeosamplingPanel.SetActive(false);
                MessagingPanel.SetActive(false);
                NavigationPanel.SetActive(false);
                break;
            case Screens.Geosampling:
                UIAPanel.SetActive(false);
                VitalsPanel.SetActive(false);
                GeosamplingPanel.SetActive(true);
                MessagingPanel.SetActive(false);
                NavigationPanel.SetActive(false);
                break;
            case Screens.Messaging:
                UIAPanel.SetActive(false);
                VitalsPanel.SetActive(false);
                GeosamplingPanel.SetActive(false);
                MessagingPanel.SetActive(true);
                NavigationPanel.SetActive(false);
                break;
            case Screens.Navigation:
                UIAPanel.SetActive(false);
                VitalsPanel.SetActive(false);
                GeosamplingPanel.SetActive(false);
                MessagingPanel.SetActive(false);
                NavigationPanel.SetActive(true);
                break;
            default:
                break;
        }
        CurrScreen = ScreenToShow;
        Debug.Log("Curr Screen = " + ScreenToShow.ToString());
    }
    
    public static void ScrollUp()
    {
        EventBus.Publish<ScrollEvent>(new ScrollEvent(CurrScreen, Direction.up));
    }

    public static void ScrollDown()
    {
        EventBus.Publish<ScrollEvent>(new ScrollEvent(CurrScreen, Direction.down));
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
