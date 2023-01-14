using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDownMenuHandler : MonoBehaviour
{
    [SerializeField] GameObject gb;
    void Start()
    {
        gb.SetActive(false);
    }

    public void onClick() {
        if (gb.activeSelf) {
            gb.SetActive(false);
        }
        else {
            gb.SetActive(true);
        }
    }
}
