using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickPerson : MonoBehaviour
{
    [SerializeField] TextMeshPro name;
    [SerializeField] Vector3 sizeOfDiff = new Vector3(0.1f, 0.1f, 0);
    [SerializeField] bool selected = false;


    public void click() {
        Transform t = transform;
        if (!selected) t.localScale += sizeOfDiff;
        else t.localScale -= sizeOfDiff;
        selected = !selected; 
        Debug.Log(selected);

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
