using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class UIEgressSwitchControl : MonoBehaviour
{
    [SerializeField] GameObject yellowSquare;
    Interactable toggle;

    private void Start()
    {
        toggle = transform.GetComponentInChildren<Interactable>();
    }

    public void SetToggleState(bool state)
    {
        toggle.IsToggled = state;
    }

    public void SetFlashing(bool f)
    {
        yellowSquare.GetComponent<YellowFlash>().SetFlashing(f);
    }
}
