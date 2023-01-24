using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PSIFill : MonoBehaviour
{
    // For now, just let the update function make the value change
    // Eventually let whatever grabs data call 'this' function
    [SerializeField] float psiValue = 11.2f;
    [SerializeField] TMP_Text psiText = null;

    private void Update()
    {
        FillPSI();
    }

    // This function will take in a PSI value to assign to the string
    // For UI/UX presentation purposes, this will be omitted
    private void FillPSI()
    {
        psiText.text = psiValue.ToString() + " psi";
    }
}
