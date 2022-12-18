using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleZedong : MonoBehaviour
{

    public GameObject screen;
    
    // Start is called before the first frame update
    public void toggle() {
        Debug.Log("screen");
        screen.SetActive(!screen.activeSelf);
    }
}
