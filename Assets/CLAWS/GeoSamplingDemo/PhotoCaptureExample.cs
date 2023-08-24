// Conditional Compilation : no photo services supported through webGL

#if UNITY_WEBGL
using UnityEngine;

[System.Serializable]
public class PhotoCaptureExample : MonoBehaviour
{
    public List<GameObject> outputQuads;
    public string sampleName = "GeoSample";

    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material defaultConfirmMaterial;
    [SerializeField] private GameObject existingProfileView;
    [SerializeField] private GameObject confirmationQuad;
    [SerializeField] private GameObject cameraView;
    
    public void LoadPhotos()
    {
    }
    public void TakePhoto()
    {
    }
    public void ConfirmPhoto()
    {
    }
    public void nextPage()
    {
    }
    public void prevPage()
    {
    }

    // keep these for webgl
    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
    public void CloseOpenView(GameObject panel)
    {
        panel.SetActive(false);
        existingProfileView.SetActive(true);
    }
}
#else
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Windows.WebCam;
using System.IO;

[System.Serializable]
public class PhotoCaptureExample : MonoBehaviour
{
    PhotoCapture photoCaptureObject = null;
    Texture2D targetTexture = null;
    public List<GameObject> outputQuads;

    private int photoCount = 0;
    private int currentPage = 0;

    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material defaultConfirmMaterial;
    [SerializeField] private GameObject existingProfileView;
    [SerializeField] private GameObject confirmationQuad;
    [SerializeField] private GameObject cameraView;

    public string sampleName = "GeoSample";

    void Awake()
    {
        LoadPhotos();
        existingProfileView.SetActive(true);
    }

    public void LoadPhotos()
    {
        if (Directory.Exists(Application.persistentDataPath + "/" + sampleName))
        {
            string folder = Application.persistentDataPath + "/" + sampleName;
            DirectoryInfo d = new DirectoryInfo(folder);
            photoCount = d.GetFiles().Length;
            for (int i = 0; i < outputQuads.Count; ++i)
            {
                if (i + outputQuads.Count * currentPage >= photoCount)
                {
                    Renderer r = outputQuads[i].GetComponent<Renderer>();
                    r.material = defaultMaterial;
                    continue;
                }
                var file = d.GetFiles("*.jpg")[i + outputQuads.Count * currentPage];
                byte[] bytes = File.ReadAllBytes(file.FullName);
                Texture2D texture = new Texture2D(1, 1);
                texture.LoadImage(bytes);
                Renderer quadRenderer = outputQuads[i].GetComponent<Renderer>();
                quadRenderer.material = new Material(Shader.Find("Unlit/Texture"));
                quadRenderer.material.SetTexture("_MainTex", texture);
            }
        }
        else
        {
            photoCount = 0;
            for (int i = 0; i < outputQuads.Count; ++i)
            {
                Renderer r = outputQuads[i].GetComponent<Renderer>();
                r.material = defaultMaterial;
            }
        }
    }
    public void TakePhoto()
    {
        StartPhoto();
    }

    void StartPhoto()
    {
        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
        targetTexture = new Texture2D(cameraResolution.width, cameraResolution.height);

        Renderer r = confirmationQuad.GetComponent<Renderer>();
        r.material = defaultConfirmMaterial;

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
            photoCaptureObject.StartPhotoModeAsync(cameraParameters, delegate (PhotoCapture.PhotoCaptureResult result) {
                // Take a picture
                photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory);
            });
        });
    }

    void Snap()
    {
        // cameraView.SetActive(false);
        cameraView.GetComponent<GeoSampleCollapse>().Toggle(confirmationQuad.gameObject);
        // confirmationQuad.SetActive(true);
    }

    void OnCapturedPhotoToMemory(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame)
    {
        if (result.success)
        {
            //StartCoroutine(Snap());
            Snap();

            // Copy the raw image data into the target texture
            photoCaptureFrame.UploadImageDataToTexture(targetTexture);

            Renderer c_quadRenderer = confirmationQuad.GetComponent<Renderer>();
            c_quadRenderer.material = new Material(Shader.Find("Unlit/Texture"));
            c_quadRenderer.material.SetTexture("_MainTex", targetTexture);
        }
        // Deactivate the camera
        photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
    }

    public void ConfirmPhoto()
    {
        if (photoCount < outputQuads.Count * (currentPage + 1))
        {
            Renderer quadRenderer = outputQuads[photoCount % outputQuads.Count].GetComponent<Renderer>();
            quadRenderer.material = new Material(Shader.Find("Unlit/Texture"));
            quadRenderer.material.SetTexture("_MainTex", targetTexture);
        }

        photoCount++;
        if (!Directory.Exists(Application.persistentDataPath + "/" + sampleName))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/" + sampleName);
        }

        string filename = string.Format(@"{0}/CapturedImage{1}_n.jpg", sampleName, photoCount);
        string filePath = Path.Combine(Application.persistentDataPath, filename);
        File.WriteAllBytes(filePath, targetTexture.EncodeToJPG());
    }

    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        // Shutdown the photo capture resource
        photoCaptureObject.Dispose();
        photoCaptureObject = null;
    }

    public void nextPage()
    {
        if (photoCount <= (currentPage+1) * outputQuads.Count)
        {
            return;
        }
        currentPage++;
        LoadPhotos();
    }

    public void prevPage()
    {
        if(currentPage == 0)
        {
            return;
        }
        currentPage--;
        LoadPhotos();
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
    public void CloseOpenView(GameObject panel)
    {
        panel.SetActive(false);
        existingProfileView.SetActive(true);
    }
}
#endif
