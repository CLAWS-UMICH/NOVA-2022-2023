using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    [SerializeField] GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Closing(){
        //if(panel.activeSelf){
            panel.SetActive(false);
        //}
        Debug.Log("hi");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
