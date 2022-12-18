using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manipulate : MonoBehaviour
{

    [SerializeField] static bool mode = true;

    public void manipulateStart() {
        mode = false;
        Debug.Log(mode);
    }

    public void manipulateEnd() {
        mode = true;
        // Debug.Log(mode);
    }

    public bool getMode() {
        return mode;
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
