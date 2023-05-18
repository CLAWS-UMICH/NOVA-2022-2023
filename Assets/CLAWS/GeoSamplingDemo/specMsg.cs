using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class specMsg 
{
    public float SiO2;
    public float TiO2;
    public float Al2O3;
    public float FeO;
    public float MnO;
    public float MgO;
    public float CaO;
    public float K2O;
    public float P2O3;
    public specMsg() {
        SiO2 = 0;
        TiO2= 0;
        Al2O3= 0;
        FeO= 0;
        MnO= 0;
        MgO= 0;
        CaO= 0;
        K2O= 0;
        P2O3= 0;
    }
    public specMsg(float SiO2_in, float TiO2_in, float Al2O3_in, float FeO_in, float MnO_in, float MgO_in, float CaO_in, float K2O_in, float P2O3_in) {
        SiO2 = SiO2_in;
        TiO2= TiO2_in;
        Al2O3= Al2O3_in;
        FeO= FeO_in;
        MnO= MnO_in;
        MgO= MgO_in;
        CaO= CaO_in;
        K2O= K2O_in;
        P2O3= P2O3_in;
    }
}
