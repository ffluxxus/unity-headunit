using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject[] cameras;
    public GameObject[] cameraTrackers;
    private int numCameras = 0;   
    private int lastCameraSelected = 0;
    
    public void Start()
    {
        if (cameras.Length != cameraTrackers.Length) {
            Debug.LogError("[CameraController.cs] Cameras and camera trackers arrays must be equal. They depend on having the same array lengths.");
        }
        numCameras = cameras.Length;
    }

    void Update()
    {
        
    }

    public void SwitchCamera() {
        
        foreach (GameObject cam in cameras)
        {
            cam.SetActive(false);
        }
        foreach (GameObject camTracker in cameraTrackers)
        {
            camTracker.SetActive(false);
        }
        
        if (lastCameraSelected == cameras.Length) // doing a transition camera or other camera
        {

            lastCameraSelected = 0;
        } else if (lastCameraSelected == cameras.Length - 1) {
            lastCameraSelected = 0;            
        } else {
            lastCameraSelected += 1;          
        }

        cameras[lastCameraSelected].SetActive(true);
        cameraTrackers[lastCameraSelected].SetActive(true);

    }
}
