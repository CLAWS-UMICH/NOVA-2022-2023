using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.__XR__.WSA.WebCam;

[System.Serializable]
public class PhotoCaptureExample : MonoBehaviour {
    
    [SerializeField]
    GameObject screen;
    UnityEngine.Windows.WebCam.PhotoCapture photoCaptureObject = null;
    Texture2D targetTexture = null;

    // Use this for initialization
    void Start() {
        Resolution cameraResolution = UnityEngine.Windows.WebCam.PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
        targetTexture = new Texture2D(cameraResolution.width, cameraResolution.height);

        // Create a PhotoCapture object
        UnityEngine.Windows.WebCam.PhotoCapture.CreateAsync(false, delegate (UnityEngine.Windows.WebCam.PhotoCapture captureObject) {
            photoCaptureObject = captureObject;
            UnityEngine.Windows.WebCam.CameraParameters cameraParameters = new UnityEngine.Windows.WebCam.CameraParameters();
            cameraParameters.hologramOpacity = 0.0f;
            cameraParameters.cameraResolutionWidth = cameraResolution.width;
            cameraParameters.cameraResolutionHeight = cameraResolution.height;
            cameraParameters.pixelFormat = UnityEngine.Windows.WebCam.CapturePixelFormat.BGRA32;

        });
    }

    void takePicture() {
        // Activate the camera
        photoCaptureObject.StartPhotoModeAsync(cameraParameters, delegate (UnityEngine.Windows.WebCam.PhotoCapture.PhotoCaptureResult result) {
            // Take a picture
            photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory);
        });
        OnStoppedPhotoMode();
    }

    void OnCapturedPhotoToMemory(UnityEngine.Windows.WebCam.PhotoCapture.PhotoCaptureResult result, UnityEngine.Windows.WebCam.PhotoCaptureFrame photoCaptureFrame) {
        // Copy the raw image data into the target texture
        photoCaptureFrame.UploadImageDataToTexture(targetTexture);

        // Create a GameObject to which the texture can be applied
        GameObject __quad__ = GameObject.CreatePrimitive(PrimitiveType.Quad);
        Renderer quadRenderer = quad.GetComponent<Renderer>() as Renderer;
        quadRenderer.material = new Material(Shader.Find("Custom/Unlit/UnlitTexture"));

        quad.transform.parent = this.transform;
        quad.transform.localPosition = new Vector3(0.0f, 0.0f, 3.0f);

        quadRenderer.material.SetTexture("_MainTex", targetTexture);

        // Deactivate the camera
        photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
    }

    void OnStoppedPhotoMode(UnityEngine.Windows.WebCam.PhotoCapture.PhotoCaptureResult result) {
        // Shutdown the photo capture resource
        photoCaptureObject.Dispose();
        photoCaptureObject = null;
    }
}