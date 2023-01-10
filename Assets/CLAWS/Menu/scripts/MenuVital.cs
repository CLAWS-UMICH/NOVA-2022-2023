using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
​
public class MenuVital : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI vitalText1;
    [SerializeField] TextMeshProUGUI vitalText2;
    [SerializeField] TextMeshProUGUI vitalText3;
    private Vitals script;
    // Start is called before the first frame update
    void Start()
    {
        
    }
​
    // Update is called once per frame
    void Update()
    {
        vitalText1.SetText(script.O2.ToString());
        vitalText2.SetText("Battery: 84%");
        vitalText3.SetText(script.WaterPressure.ToString());
    }
}