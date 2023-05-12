using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEgressSwitchControl : MonoBehaviour
{
    [SerializeField] GameObject yellowSquare;

    public void SetFlashing(bool f)
    {
        yellowSquare.GetComponent<YellowFlash>().SetFlashing(f);
    }
}
