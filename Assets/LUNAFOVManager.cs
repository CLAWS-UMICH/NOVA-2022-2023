using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LUNAFOVManager : MonoBehaviour
{
    SketchYawOffset sketchYawOffset;

    private void Start()
    {
        sketchYawOffset = GameObject.Find("LunaTransform").GetComponent<SketchYawOffset>();
    }

    public void LUNALeft()
    {
        sketchYawOffset.offset = 20;
    }
    public void LUNACenter()
    {
        sketchYawOffset.offset = 0;
    }
    public void LUNARight()
    {
        sketchYawOffset.offset = -20;
    }
}
