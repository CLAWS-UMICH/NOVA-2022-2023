using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Windows.WebCam;

[System.Serializable]
public class PhotoCaptureExample : MonoBehaviour
{

    [SerializeField]
    GameObject screen;
    PhotoCapture photoCaptureObject = null;
    Texture2D targetTexture = null;

    bool isTakingPhoto = false;
    List<GameObject> outputQuads;

    public void TakePhoto()
    {
        //if (isTakingPhoto)
        //{
        //    return;
        //}
        //isTakingPhoto = true;

        //GameObject activeNote = GetComponent<NoteController>().getCurrActiveNote();
        //GameObject recentSample = activeNote.GetComponent<SampleController>().getRecentSample();
        //if (recentSample)
        //{
        //    outputQuads = recentSample.GetComponent<VoiceController>().getAvalibleQuads();
        //    StopAllCoroutines();
            StartCoroutine(ReadyCamera());

        //}
        //else
        //{
        //    isTakingPhoto = false;
        //}
    }

    IEnumerator ReadyCamera()
    {
        yield return new WaitForSeconds(2f);
        StartPhoto();
    }

    void StartPhoto()
    {
        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
        targetTexture = new Texture2D(cameraResolution.width, cameraResolution.height);

        // Create a PhotoCapture object
        PhotoCapture.CreateAsync(false, delegate (PhotoCapture captureObject)
        {
            photoCaptureObject = captureObject;
            CameraParameters cameraParameters = new CameraParameters();
            cameraParameters.hologramOpacity = 0.0f;
            cameraParameters.cameraResolutionWidth = cameraResolution.width;
            cameraParameters.cameraResolutionHeight = cameraResolution.height;
            cameraParameters.pixelFormat = CapturePixelFormat.BGRA32;

            // Activate the camera
            photoCaptureObject.StartPhotoModeAsync(cameraParameters, delegate (PhotoCapture.PhotoCaptureResult result)
            {
                // Take a picture
                photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory);
            });
        });
    }

    void OnCapturedPhotoToMemory(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame)
    {
        // Copy the raw image data into the target texture
        photoCaptureFrame.UploadImageDataToTexture(targetTexture);

        // Create a GameObject to which the texture can be applied
        //GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        foreach(GameObject quad in outputQuads)
        {
            Renderer quadRenderer = quad.GetComponent<Renderer>() as Renderer;
            quadRenderer.material = new Material(Shader.Find("Mixed Reality Toolkit/NoteBackplate"));

            //quad.transform.parent = this.transform;
            //quad.transform.localPosition = new Vector3(0.0f, 0.0f, 3.0f);

            quadRenderer.material.SetTexture("_MainTex", targetTexture);
        }

        // Deactivate the camera
        photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
    }

    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        // Shutdown the photo capture resource
        photoCaptureObject.Dispose();
        photoCaptureObject = null;
        isTakingPhoto = false;
    }
}