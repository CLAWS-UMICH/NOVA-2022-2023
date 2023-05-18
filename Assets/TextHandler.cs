using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshPro tmp;
    void Start()
    {
        
    }

    public void SetText(string message){
        tmp.text = message;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
