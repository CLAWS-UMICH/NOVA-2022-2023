using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TSS.Msgs;

//Geosample 
[System.Serializable]
public class GeoSample 
{
    public int sampleID;
    public string rockType;
    public string lunarTime;
    // Coordinates
    public string location;
    public SpecMsg specMsg;
    public string RFID;
    public char taskType;
    public string description;
    public GeoSample() {
        sampleID = -1;
        rockType = "";
        lunarTime = "";
        location = "";
        RFID = "";
        taskType = '\0';
        description = "";
    }
    public GeoSample(int sampleID_in, string rockType_in, string lunarTime_in,
    string location_in, string RFID_in, char taskType_in, string description_in, SpecMsg s) {
        sampleID = sampleID_in;
        rockType = rockType_in;
        lunarTime = lunarTime_in;
        location = location_in;
        RFID = RFID_in;
        taskType = taskType_in;
        description = description_in;
        specMsg = s;
    }
}
