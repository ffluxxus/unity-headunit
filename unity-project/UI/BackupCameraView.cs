using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackupCameraView : MonoBehaviour
{

    private bool camAvailable;
    private WebCamTexture backCamera;
    public int camId = 1;
    public Texture fallback; // fallback texture

    public RawImage background;
    public AspectRatioFitter fit;
    public GameObject cover;
    private Image im;

    private bool initializeCamera;


    private void Start()
    {

        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {

            // no cameras
            Debug.LogError("[BackupCameraView.cs] Camera missing!");
            camAvailable = false;
            return;
        }

        backCamera = new WebCamTexture(devices[camId].name, Screen.width, Screen.height);

        if (backCamera == null)
        {
            Debug.LogError("[BackupCameraView.cs] Failed to connect to camera!");
            return;
        }
        background.texture = backCamera;

        camAvailable = true;

        im = cover.GetComponent<Image>();
        StartCoroutine(initCam());
    }

    private void Update()
    {
        if (!camAvailable)
            return;


        im = cover.GetComponent<Image>();

        if (im.color.a > 0f)
            backCamera.Play();
        else
            backCamera.Pause();

        float ratio = (float)backCamera.width / (float)backCamera.height;
        fit.aspectRatio = ratio;

        float scaleY = backCamera.videoVerticallyMirrored ? -1f: 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -backCamera.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }

    IEnumerator initCam ()
    {
        
        if (camAvailable)
        {

            backCamera.Play();
            yield return new WaitForSeconds(5.0f);
            Debug.Log("[BackupCameraView.cs] Allowed 5 seconds for Backup Camera to Initialize");
        }
    }
}
