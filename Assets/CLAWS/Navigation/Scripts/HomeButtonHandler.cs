using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeButtonHandler : MonoBehaviour
{

    [SerializeField] GameObject navScreen;
    [SerializeField] GameObject crewScreen;
    // Start is called before the first frame update
    void Start()
    {
        crewScreen.SetActive(false);
    }

    public void CrewButton()
    {
        navScreen.SetActive(false);
        crewScreen.SetActive(true);
    }
    
    public void CloseButton()
    {
        navScreen.SetActive(false);
        crewScreen.SetActive(false);
    }

    public void BackButton()
    {
        crewScreen.SetActive(false);
        navScreen.SetActive(true);
    }


}
