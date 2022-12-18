using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableDragging : MonoBehaviour
{

    GameObject g;
    // private static enableDragging instance = null;
    private bool draggingMode = false;
    // private static readonly object padlock = new object();

    // enableDragging()
    // {
    // }

    // public static enableDragging getInstance ()
    // {
 
    //         if (instance == null)
    //         {
    //             lock (padlock)
    //             {
    //                 if (instance == null)
    //                 {
    //                     instance = new enableDragging();
    //                 }
    //             }
    //         }
    //         return instance;
        
    // }

    public bool getMode () {
        return draggingMode;
    }

    public void click() {
        draggingMode = !draggingMode;
        Debug.Log(draggingMode);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
