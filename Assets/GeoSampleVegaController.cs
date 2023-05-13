using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GeoSampleVegaController : MonoBehaviour
{
    [SerializeField]
    GameObject ListController;
    [SerializeField]
    GameObject ExpandedListController;
    [SerializeField]
    GameObject DescriptionController;
    [SerializeField]
    GameObject GalleryController;

    [SerializeField] GameObject GalleryCameraView;
    [SerializeField] GameObject GalleryConfirmationView;
    [SerializeField] GameObject GalleryView;

    //Keep track of whats in focus
    // "list", "expand", "description", "gallery", "none",
    // gallery - "confirm", "camera"
    string currentFocus = "";
    //Serialized Objects- ListController, ExpandedListController, DescriptionController, Gallery, NotOpen

    public void updateCurrentFocus(string NewFocus) {
        currentFocus = NewFocus;
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

    //gallery part
    public void enable_camera()
    {
        if (currentFocus != "gallery")
        {
            //vega error sound
            Debug.Log("cannot perform this command");
            return;
        }
        updateCurrentFocus("camera");
        GalleryView.SetActive(false);
        GalleryCameraView.SetActive(true);
    }
    public void take_photo()
    {
        if (currentFocus != "camera")
        {
            //vega error sound
            Debug.Log("cannot perform this command");
            return;
        }
        updateCurrentFocus("confirm");
        GalleryController.GetComponent<PhotoCaptureExample>().TakePhoto();
    }
    public void open_gallery()
    {
        if(currentFocus != "description")
        {
            //vega error sound
            Debug.Log("cannot perform this command");
            return;
        }
        updateCurrentFocus("gallery");
        GalleryController.SetActive(true);
        GalleryController.GetComponent<PhotoCaptureExample>().sampleName = DescriptionController.GetComponent<GeoSampleDescriptionMenuController>().GetSample().sampleID.ToString();
        GalleryController.GetComponent<PhotoCaptureExample>().LoadPhotos();
        DescriptionController.SetActive(false);
    }
    public void confirm_photo() //hardcoded
    {
        if (currentFocus != "confirm")
        {
            //vega error sound
            Debug.Log("cannot perform this command");
            return;
        }
        updateCurrentFocus("gallery");
        GalleryController.GetComponent<PhotoCaptureExample>().ConfirmPhoto();
        GalleryView.SetActive(true);
        GalleryConfirmationView.SetActive(false);
    }
    public void retake_photo() //hardcoded
    {
        if (currentFocus != "confirm")
        {
            //vega error sound
            Debug.Log("cannot perform this command");
            return;
        }
        updateCurrentFocus("camera");
        GalleryCameraView.SetActive(true);
        GalleryConfirmationView.SetActive(false);
    }
    public void page_right()
    {
        if (currentFocus != "gallery")
        {
            //vega error sound
            Debug.Log("cannot perform this command");
            return;
        }
        GalleryController.GetComponent<PhotoCaptureExample>().nextPage();

    }
    public void page_left()
    {
        if (currentFocus != "gallery")
        {
            //vega error sound
            Debug.Log("cannot perform this command");
            return;
        }
        GalleryController.GetComponent<PhotoCaptureExample>().prevPage();

    }
    public void close_gallery()
    {
        if (currentFocus != "gallery")
        {
            //vega error sound
            Debug.Log("cannot perform this command");
            return;
        }
        updateCurrentFocus("description");
        GalleryController.SetActive(false);
        DescriptionController.SetActive(true);
    }

    public void cancel_photo()
    {
        if (currentFocus != "camera" && currentFocus != "confirm")
        {
            //vega error sound
            Debug.Log("cannot perform this command");
            return;
        }
        updateCurrentFocus("gallery");
        GalleryView.SetActive(true);
        GalleryCameraView.SetActive(false);
        GalleryConfirmationView.SetActive(false);
    }
}
