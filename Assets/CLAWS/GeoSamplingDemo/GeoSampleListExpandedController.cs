using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class GeoSampleListExpandedController : MonoBehaviour
{
    [SerializeField]
    GameObject[] sampleObjects;
    //The list of 3 geosample objects that will be displayed to the astronaut
    GeoSample[] holdingContainer = new GeoSample[3];
    public int currentIndex = 0;

    void Start()
    {
        EventBus.Subscribe<GeoSampleUpdatedEvent>(RecieveNewList);
        refresh();
        Debug.Log("Poggers");
    }

    public void refresh()
    {
        UpdateHoldingContainer();
        Render();
    }

    private void RecieveNewList(GeoSampleUpdatedEvent e)
    {
        //Update current_index
        currentIndex = e.index;
        UpdateHoldingContainer();
        Render();
    }
    //Increments or decrements the task list view as the astronaut scrolls
    public void changeCurrentIndex(int incr)
    {
        if ((incr < 0 && currentIndex > 0) || (incr > 0 && currentIndex < Simulation.User.AstronautGeoSamples.geoSampleList.Count - 1))
        {
            currentIndex += incr;
            UpdateHoldingContainer();
            Render();
        }
    }
    private void UpdateHoldingContainer()
    {
        int size = Simulation.User.AstronautGeoSamples.geoSampleList.Count;
        for (int i = currentIndex; i < currentIndex + 3; i++)
        {
            if (i < size)
            {
                holdingContainer[i - currentIndex] = Simulation.User.AstronautGeoSamples.geoSampleList[i];
            }
            else
            {
                holdingContainer[i - currentIndex] = new GeoSample();
            }
        }
    }
    private void Render()
    {
        for (int i = 0; i < 3; i++)
        {
            if (holdingContainer[i].taskType == '\0')
            {
                sampleObjects[i].SetActive(false);
            }
            else
            {
                renderHelper(i);
            }
        }
    }

    private void renderHelper(int index)
    {
        sampleObjects[index].SetActive(true);
        //Change Title text
        sampleObjects[index].transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = "Sample " + (holdingContainer[index].sampleID) + ": " + holdingContainer[index].rockType;
        //Change Subtitle text
        sampleObjects[index].transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = holdingContainer[index].lunarTime;
        sampleObjects[index].transform.GetChild(3).gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = holdingContainer[index].location;
    }
}
