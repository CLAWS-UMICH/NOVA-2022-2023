using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuiToggle : MonoBehaviour
{
    public void toggleActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
