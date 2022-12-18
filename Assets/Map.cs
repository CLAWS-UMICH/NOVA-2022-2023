using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private static Map instance;

    private void Awake() {
        if(instance == null) {
            instance = this;
            t = gameObject.transform;
        }
    }
    public static Transform t {get; private set;} 

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(t);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
