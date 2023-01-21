using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class OpenScreens : MonoBehaviour
{
    [SerializeField]
    GameObject bigScreen;

    [SerializeField]
    GameObject dropDown;

    [SerializeField]
    GameObject closeButton;


    public void openScreen()
    {
        bigScreen.SetActive(true);
        dropDown.SetActive(true);
        closeButton.SetActive(true);
    }

    public void closeScreen()
    {
        bigScreen.SetActive(false);
        dropDown.SetActive(false);
        closeButton.SetActive(false);
    }
}