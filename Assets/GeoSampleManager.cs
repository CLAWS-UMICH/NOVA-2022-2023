using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TSS.Msgs;

public class GeoSampleManager : MonoBehaviour
{
    int id = 1;
    // Start is called before the first frame update
    void Start()
    {
        EventBus.Subscribe<GeoSpecRecievedEvent>(CreateGeoSample);
        EventBus.Subscribe<ScrollEvent>(OnScroll);
        EventBus.Subscribe<CloseEvent>(CloseGeo);
        EventBus.Subscribe<BackEvent>(BackGeo);
    }

    private void CreateGeoSample(GeoSpecRecievedEvent e) {

        SpecMsg s = Simulation.User.GEO;
        string rockType = getRockType(s);
        string coordinate = "42.1234 N, 24.1234 E";
        Simulation.User.AstronautGeoSamples.geoSampleList.Insert(0, new GeoSample(id, rockType, System.DateTime.Now.ToString(), coordinate, "23940329", 'n', "", s));
        EventBus.Publish<GeoSampleUpdatedEvent>(new GeoSampleUpdatedEvent(0));
        PopUpManager.MakePopup("New Geo Sample Added.");
        id++;

    }
    private void OnScroll(ScrollEvent e) {
        if(e.screen == Screens.Geosampling || e.screen == Screens.Geosample_Expanded) {
            if(e.direction == Direction.down) {
                gameObject.GetComponent<GeoSampleVegaController>().scrollDown();
            }
            else if(e.direction == Direction.up) {
                gameObject.GetComponent<GeoSampleVegaController>().scrollUp();
            }
        }
    }
    private void CloseGeo(CloseEvent e) {
        if(e.screen == Screens.Geosampling || e.screen == Screens.Geosample_Expanded 
        || e.screen == Screens.Geosample_Description|| e.screen == Screens.Geosample_Gallery
        || e.screen == Screens.Geosample_Camera || e.screen == Screens.Geosample_Confirm) {
                gameObject.GetComponent<GeoSampleVegaController>().close();
        }
    }
    private void BackGeo(BackEvent e) {
        if(e.screen == Screens.Geosampling || e.screen == Screens.Geosample_Expanded 
        || e.screen == Screens.Geosample_Description|| e.screen == Screens.Geosample_Gallery
        || e.screen == Screens.Geosample_Camera || e.screen == Screens.Geosample_Confirm) {
                gameObject.GetComponent<GeoSampleVegaController>().back();
        }
    }
    private string getRockType(SpecMsg s) {
        //Mare Basalt
        if(sigma(s.SiO2, 40.58) && sigma(s.TiO2, 12.83) && sigma(s.Al2O3, 10.91) && sigma(s.FeO, 13.18) && sigma(s.MnO, 0.19) && sigma(s.MgO, 6.7) && sigma(s.CaO, 10.64) && sigma(s.K2O, -0.11) && sigma(s.P2O3, 0.34)) {
            return "Mare Basalt";
        }
        else if(sigma(s.SiO2, 36.89) && sigma(s.TiO2, 2.44) && sigma(s.Al2O3, 9.6) && sigma(s.FeO, 14.52) && sigma(s.MnO, 0.24) && sigma(s.MgO, 5.3) && sigma(s.CaO, 8.22) && sigma(s.K2O, -0.13) && sigma(s.P2O3, 0.29)) {
            return "Vesicular Basalt";
        }
        else if(sigma(s.SiO2, 41.62) && sigma(s.TiO2, 2.44) && sigma(s.Al2O3, 9.52) && sigma(s.FeO, 18.12) && sigma(s.MnO, 0.27) && sigma(s.MgO, 11.1) && sigma(s.CaO, 8.12) && sigma(s.K2O, -0.12) && sigma(s.P2O3, 0.28)) {
            return "Olivine Basalt";
        }
        else if(sigma(s.SiO2, 46.72) && sigma(s.TiO2, 1.1) && sigma(s.Al2O3, 19.01) && sigma(s.FeO, 7.21) && sigma(s.MnO, 0.14) && sigma(s.MgO, 7.83) && sigma(s.CaO, 14.22) && sigma(s.K2O, 0.43) && sigma(s.P2O3, 0.65)) {
            return "Feldspathic Basalt";
        }
        else if(sigma(s.SiO2, 46.53) && sigma(s.TiO2, 3.4) && sigma(s.Al2O3, 11.68) && sigma(s.FeO, 16.56) && sigma(s.MnO, 0.24) && sigma(s.MgO, 6.98) && sigma(s.CaO, 11.11) && sigma(s.K2O, -0.02) && sigma(s.P2O3, 0.38)) {
            return "Pigeonite Basalt";
        }
        else if(sigma(s.SiO2, 42.45) && sigma(s.TiO2, 1.56) && sigma(s.Al2O3, 11.44) && sigma(s.FeO, 17.91) && sigma(s.MnO, 0.27) && sigma(s.MgO, 10.45) && sigma(s.CaO, 9.37) && sigma(s.K2O, -0.08) && sigma(s.P2O3, 0.34)) {
            return "Olivine Basalt";
        }
        else if(sigma(s.SiO2, 42.56) && sigma(s.TiO2, 9.38) && sigma(s.Al2O3, 12.03) && sigma(s.FeO, 11.27) && sigma(s.MnO, 0.17) && sigma(s.MgO, 9.7) && sigma(s.CaO, 10.52) && sigma(s.K2O, 0.28) && sigma(s.P2O3, 0.44)) {
            return "Ilmenite Basalt";
        }
        else {
            return "IDK :/";
        }

        // else if(sigma(s.SiO2, ), sigma(s.TiO2, ), sigma(s.Al2O3, ), sigma(s.FeO, ), sigma(s.MnO, ), sigma(s.MgO, ), sigma(s.CaO, ), sigma(s.K2O, ), sigma(s.P2O3, ));
        // else if(sigma(s.SiO2, ), sigma(s.TiO2, ), sigma(s.Al2O3, ), sigma(s.FeO, ), sigma(s.MnO, ), sigma(s.MgO, ), sigma(s.CaO, ), sigma(s.K2O, ), sigma(s.P2O3, ));
        // else if(sigma(s.SiO2, ), sigma(s.TiO2, ), sigma(s.Al2O3, ), sigma(s.FeO, ), sigma(s.MnO, ), sigma(s.MgO, ), sigma(s.CaO, ), sigma(s.K2O, ), sigma(s.P2O3, ));
        // else if(sigma(s.SiO2, ), sigma(s.TiO2, ), sigma(s.Al2O3, ), sigma(s.FeO, ), sigma(s.MnO, ), sigma(s.MgO, ), sigma(s.CaO, ), sigma(s.K2O, ), sigma(s.P2O3, ));
        // else if(sigma(s.SiO2, ), sigma(s.TiO2, ), sigma(s.Al2O3, ), sigma(s.FeO, ), sigma(s.MnO, ), sigma(s.MgO, ), sigma(s.CaO, ), sigma(s.K2O, ), sigma(s.P2O3, ));
        // else if(sigma(s.SiO2, ), sigma(s.TiO2, ), sigma(s.Al2O3, ), sigma(s.FeO, ), sigma(s.MnO, ), sigma(s.MgO, ), sigma(s.CaO, ), sigma(s.K2O, ), sigma(s.P2O3, ));
    } 
    private bool sigma(float spec, double compare) {
        float sigma = (float).5;
        float comp = (float)compare;
        return (spec < comp + sigma) && (spec > comp - sigma);
    }
}
