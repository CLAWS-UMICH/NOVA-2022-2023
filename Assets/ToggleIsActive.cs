using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleIsActive : MonoBehaviour
{
    bool setIsActive=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleActive() {
        setIsActive = !setIsActive;
        gameObject.SetActive(setIsActive);
       
    }
}
