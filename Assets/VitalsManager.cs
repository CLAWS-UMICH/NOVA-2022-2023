using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitalsManager : MonoBehaviour
{
    public List<GameObject> close_screens;
    public bool is_opened;    

    private void Start()
    {
        EventBus.Subscribe<CloseEvent>(Callback_closeVitals);
        EventBus.Subscribe<BackEvent>(Callback_backVitals);
        is_opened = false;
    }

    public void ToggleVitals()
    {
        if (!is_opened)
        {
            OpenVitals();
        }
        else
        {
            CloseVitals();
        }
        is_opened = !is_opened;
    }

    public void OpenVitals()
    {
        EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.Vitals, LUNAState.center));
        foreach (GameObject g in close_screens)
        {
            g.SetActive(true);
        }
    }

    public void CloseVitals()
    {
        EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.Home, LUNAState.center));
        foreach (GameObject g in close_screens)
        {
            StartCoroutine(closeAfterDelay(g));
        }
    }

    void Callback_closeVitals(CloseEvent e)
    {
        if (e.screen == Screens.Vitals && StateMachineNOVA.LUNA == LUNAState.center)
        {
            CloseVitals();
            foreach (GameObject g in close_screens)
            {
                g.SetActive(false);
            }
        }
    }

    void Callback_backVitals(BackEvent e)
    {
        if (e.screen == Screens.Vitals && StateMachineNOVA.LUNA == LUNAState.center)
        {
            CloseVitals();
            foreach (GameObject g in close_screens)
            {
                g.SetActive(false);
            }
        }
    }

    IEnumerator closeAfterDelay(GameObject g) 
    {
        yield return new WaitForSeconds(1);
        g.SetActive(false);
    }
}
