using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GeoSampleVegaControllerDerek : MonoBehaviour
{
    [SerializeField]
    GameObject ListController;
    [SerializeField]
    GameObject ExpandedListController;
    [SerializeField]
    GameObject DescriptionController;
    [SerializeField]
    GameObject GalleryController;

    //Keep track of whats in focus
    // "list", "expand", "description", "gallery", "none"
    string currentFocus = "";
    //Serialized Objects- ListController, ExpandedListController, DescriptionController, Gallery, NotOpen

    public void updateCurrentFocus(string NewFocus) {

    }

    public void scrollDown() {
        //Dummy function
        // VegaErrorSound();
    }
    void scrollUp() {

    }
    void expand() {

    }
    //Kriti
    void recordNote() {

    }
    void openButton1() {

    }
    void openButton2() {

    }
    void openButton3() {

    }
    //closes all geosample windows
    void close() {

    }
    //same as edit note
    //Kriti
    void retry_note() {

    }
    //Kriti - Adhav can do if not enough time
    //confirms for photo taking or description writing
    void confirm() {

    }
    void open_gallery() {

    }
    void take_photo() {

    }
}
