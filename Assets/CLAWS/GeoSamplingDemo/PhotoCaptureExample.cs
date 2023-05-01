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
    [SerializeField] private GameObject existingProfileView;
    [SerializeField] private GameObject newProfileView;

    void Awake()
    {
        LoadPhotos();
        if (photoCount == 0)
        {
            existingProfileView.SetActive(false);
            newProfileView.SetActive(true);
        }
        else
        {
            existingProfileView.SetActive(true);
            newProfileView.SetActive(false);
        }
    }

    void LoadPhotos()
    {
        if (Directory.Exists(Application.persistentDataPath + "/GeoSample"))
        {
            string folder = Application.persistentDataPath + "/GeoSample";
            DirectoryInfo d = new DirectoryInfo(folder);
            photoCount = d.GetFiles().Length;
            for (int i = 0; i < outputQuads.Count; ++i)
            {
                if(i + outputQuads.Count * currentPage >= photoCount)
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
    }

    public void TakePhoto()
    {
        StartCoroutine(ReadyCamera());
        //StartPhoto();
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
            photoCaptureObject.StartPhotoModeAsync(cameraParameters, delegate (PhotoCapture.PhotoCaptureResult result) {
                // Take a picture
                photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory);
            });
        });
    }

    void OnCapturedPhotoToMemory(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame)
    {
        if (result.success)
        {
            // Copy the raw image data into the target texture
            photoCaptureFrame.UploadImageDataToTexture(targetTexture);

            if (photoCount < outputQuads.Count * (currentPage + 1))
            {
                Renderer quadRenderer = outputQuads[photoCount % outputQuads.Count].GetComponent<Renderer>();
                quadRenderer.material = new Material(Shader.Find("Unlit/Texture"));
                quadRenderer.material.SetTexture("_MainTex", targetTexture);
            }
            photoCount++;
            existingProfileView.SetActive(true);
            newProfileView.SetActive(false);
            if (!Directory.Exists(Application.persistentDataPath + "/GeoSample"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/GeoSample");
            }

            string filename = string.Format(@"GeoSample/CapturedImage{0}_n.jpg", photoCount);
            string filePath = Path.Combine(Application.persistentDataPath, filename);
            File.WriteAllBytes(filePath, targetTexture.EncodeToJPG());
        }


        // Deactivate the camera
        photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
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
}