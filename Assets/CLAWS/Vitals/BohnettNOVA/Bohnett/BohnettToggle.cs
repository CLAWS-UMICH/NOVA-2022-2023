using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BohnettToggle : MonoBehaviour
{
    public void ToggleScreen()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
