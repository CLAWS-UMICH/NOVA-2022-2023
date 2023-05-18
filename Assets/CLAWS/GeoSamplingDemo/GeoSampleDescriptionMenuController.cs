using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class GeoSampleDescriptionMenuController : MonoBehaviour
{
    //ListView
    [SerializeField] GeoSampleListController controller;
    //ExpandedView
    [SerializeField] GeoSampleListController descriptionController;
    [SerializeField] GameObject photoController;
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] TMP_Text lunarTimeText;
    [SerializeField] TMP_Text coordinateText;

    [SerializeField] GameObject GeoSampleController;
    public GeoSample sample;
    bool fromExpanded = false;

    //private void Start()
    //{
    //    Simulation.User.AstronautGeoSamples.geoSampleList.Add(new GeoSample() { sampleID = 0, rockType = "cool", lunarTime = "5", location = "12 N, 13 E", description = "this rock is cool" });
    //    Simulation.User.AstronautGeoSamples.geoSampleList.Add(new GeoSample() { sampleID = 1, rockType = "raw", lunarTime = "7", location = "7 N, 15 E", description = "hello, this is a dummy" });
    //    Simulation.User.AstronautGeoSamples.geoSampleList.Add(new GeoSample() { sampleID = 2, rockType = "lame", lunarTime = "12", location = "2 N, 1 E", description = "i am describing a lame rock" });
    //}
    public void closed() {
        if(fromExpanded == true) {
            gameObject.GetComponent<GeoSampleCollapse>().Toggle(descriptionController.gameObject);
            // descriptionController.gameObject.SetActive(true);
        }
        else {
            // controller.gameObject.SetActive(true);
            gameObject.GetComponent<GeoSampleCollapse>().Toggle(controller.gameObject);
        }
    }
    public void UpdateDescriptionMenuList(int button)
    {
        fromExpanded = false;
        int indexOfGeoSample = button + controller.currentIndex;
        Debug.Log(indexOfGeoSample);
        sample = Simulation.User.AstronautGeoSamples.geoSampleList[indexOfGeoSample];
        Debug.Log(sample.description);

        titleText.text = "Sample " + sample.sampleID;
        descriptionText.text = sample.description;
        lunarTimeText.text = sample.lunarTime + " Lunar Time";
        coordinateText.text = sample.location;
    }
    public void UpdateDescriptionMenuDescription(int button)
    {
        fromExpanded = true;
        int indexOfGeoSample = button + descriptionController.currentIndex;
        sample = Simulation.User.AstronautGeoSamples.geoSampleList[indexOfGeoSample];

        titleText.text = "Sample " + sample.sampleID;
        descriptionText.text = sample.description;
        lunarTimeText.text = sample.lunarTime + " Lunar Time";
        coordinateText.text = sample.location;
    }
    public void activateDescription() {
        gameObject.SetActive(true);
    }
    public void openGallery() {
        photoController.GetComponent<PhotoCaptureExample>().sampleName = sample.sampleID.ToString();
        photoController.GetComponent<PhotoCaptureExample>().LoadPhotos();
        photoController.SetActive(true);
        gameObject.SetActive(false);
    }
    public void closeGallery() {
        photoController.SetActive(false);
        gameObject.SetActive(true);
    }
    public GeoSample GetSample()
    {
        return sample;
    }
    public void UpdateCurrentFocus()
    {
        if (fromExpanded)
        {
            GeoSampleController.GetComponent<GeoSampleVegaController>().updateCurrentFocus("expand");
        }
        else
        {
            GeoSampleController.GetComponent<GeoSampleVegaController>().updateCurrentFocus("list");

        }
    }
}

