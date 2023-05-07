using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class GeoSampleDescriptionMenuController : MonoBehaviour
{
    [SerializeField]
    GameObject[] geosampleButtons = new GameObject[3];
    [SerializeField] GeoSampleListController controller;
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] TMP_Text lunarTimeText;
    [SerializeField] TMP_Text coordinateText;

    //private void Start()
    //{
    //    Simulation.User.AstronautGeoSamples.geoSampleList.Add(new GeoSample() { sampleID = 0, rockType = "cool", lunarTime = "5", location = "12 N, 13 E", description = "this rock is cool" });
    //    Simulation.User.AstronautGeoSamples.geoSampleList.Add(new GeoSample() { sampleID = 1, rockType = "raw", lunarTime = "7", location = "7 N, 15 E", description = "hello, this is a dummy" });
    //    Simulation.User.AstronautGeoSamples.geoSampleList.Add(new GeoSample() { sampleID = 2, rockType = "lame", lunarTime = "12", location = "2 N, 1 E", description = "i am describing a lame rock" });
    //}
    public void UpdateDescriptionMenu(GameObject sampleButton)
    {
        int button = 0;
        for (int i = 0; i < 3; i++)
        {
            if (sampleButton == geosampleButtons[i])
            {
                button = i;
            }
        }

        int indexOfGeoSample = button + controller.currentIndex;
        GeoSample sample = Simulation.User.AstronautGeoSamples.geoSampleList[indexOfGeoSample];

        titleText.text = "Sample " + (sample.sampleID + 1).ToString() + ": " + sample.rockType;
        descriptionText.text = sample.description;
        lunarTimeText.text = sample.lunarTime + " Lunar Time";
        coordinateText.text = sample.location;
    }
}
