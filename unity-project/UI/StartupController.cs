using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.TestTools;
using System.Diagnostics;

public class StartupController : MonoBehaviour

{
    public GameObject sUI;
    public GameObject UI;
    public Image background;
    public Image logo;
    public RawImage loading;
    public float delayTime = 3f; // Adjust this value to change the delay
    public float fadeSpeed = 0.8f; // Adjust this value to control fade speed (lower = slower)
    public string MediaServerPath = "E://Unity//Local Projects//CarDash_BUILD//CarCommunication.py"; // replace with your path to spotify media server (if using) or just comment out code using it

    void Start() 
    {
        /*
        if (!string.IsNullOrEmpty(MediaServerPath))
            Application.OpenURL("file://" + MediaServerPath);
        */
        loading.color = new Color(loading.color.r, loading.color.g, loading.color.b, 1f);
        UnityEngine.Debug.Log("[StartupController.cs] Startup Screen enabled, waiting for "+delayTime+"s");
        UI.SetActive(false);
        sUI.SetActive(true);
    }

    void Update()
    {
        if (delayTime > 0)
        {
            delayTime -= Time.deltaTime;
            if (delayTime <= 0)
            {
                UnityEngine.Debug.Log("[StartupController.cs] Startup Screen faded out and disabled");
                StartCoroutine(FadeOutCover());
            }
        }
    }

    IEnumerator FadeOutCover()
    {
        float alpha = background.color.a; // Get the current alpha value
        float alpha1 = logo.color.a;
        loading.color = new Color(loading.color.r, loading.color.g, loading.color.b, 0f);

        while (alpha > 0f)
        {
            alpha -= fadeSpeed * Time.deltaTime; // Decrease alpha by fadeSpeed every frame
            alpha1 -= fadeSpeed * Time.deltaTime; // Decrease alpha by fadeSpeed every frame
            background.color = new Color(background.color.r, background.color.g, background.color.b, alpha);
            logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, alpha1);
            yield return null; // Wait for next frame before continuing
        }

        // Set inactive only after the fade is complete
        sUI.gameObject.SetActive(false);
        UI.gameObject.SetActive(true);
    }

}
