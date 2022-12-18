using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class enableMouseControl : MonoBehaviour
{

    GameObject g;
    controledByMouse script;
    


    public void click() {
        
         script.enabled = true;
         Debug.Log("true, on");
    }
    // Start is called before the first frame update
    void Start()
    {
        script = g.GetComponent<controledByMouse>();
        script.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
