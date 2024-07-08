using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackupCameraCover : MonoBehaviour
{
    public GameObject cover;
    public GameObject cameraa;
    public GameObject warning;
    public GameObject toggleButton;
    public Image backupLines;
    public float fadeSpeed = 0.8f; // Adjust this value to control fade speed (lower = slower)
    private Image coverImage;
    private RawImage cameraImage;
    private Toggle toggle;
    private TMP_Text msg;
    private bool state;

    void Start()
    {
        backupLines.color = new Color(backupLines.color.r, backupLines.color.g, backupLines.color.b, 0f);
        coverImage = cover.GetComponent<Image>(); // Get the Image component of the cover object
        coverImage.color = new Color(coverImage.color.r, coverImage.color.g, coverImage.color.b, 0f);
        cameraImage = cameraa.GetComponent<RawImage>(); // Get the Image component of the cover object
        cameraImage.color = new Color(cameraImage.color.r, cameraImage.color.g, cameraImage.color.b, 0f);
        msg = warning.GetComponent<TMP_Text>();
        msg.color = new Color(msg.color.r, msg.color.g, msg.color.b, 0f);
        toggle = toggleButton.GetComponent<Toggle>();
        state = toggle.isOn;
        cover.SetActive(false);
        cameraa.SetActive(false);
        warning.SetActive(false);

    }


    public void toggleCover()
    {
        toggle = toggleButton.GetComponent<Toggle>();
        cameraImage = cameraa.GetComponent<RawImage>();
        msg = warning.GetComponent<TMP_Text>();
        state = toggle.isOn;
        coverImage = cover.GetComponent<Image>();

        // Check alpha as usual for subsequent clicks
        if (state)
        {
                StartCoroutine(FadeInCover());
        }
        else
        {
                StartCoroutine(FadeOutCover());
        }
    }

    IEnumerator FadeOutCover()
    {
        float alpha = coverImage.color.a; // Get the current alpha value
        float alpha1 = cameraImage.color.a;
        float alpha2 = msg.color.a;

        while (alpha > 0f)
        {
            alpha -= fadeSpeed * Time.deltaTime; // Decrease alpha by fadeSpeed every frame
            alpha1 -= fadeSpeed * Time.deltaTime;
            alpha2 -= fadeSpeed * Time.deltaTime;
            coverImage.color = new Color(coverImage.color.r, coverImage.color.g, coverImage.color.b, alpha);
            cameraImage.color = new Color(cameraImage.color.r, cameraImage.color.g, cameraImage.color.b, alpha1);
            backupLines.color = new Color(backupLines.color.r, backupLines.color.g, backupLines.color.b, alpha2);
            msg.color = new Color(msg.color.r, msg.color.g, msg.color.b, alpha2);
            yield return null; // Wait for next frame before continuing
        }

        // Set inactive only after the fade is complete
        cover.SetActive(false);
        cameraa.SetActive(false);
        warning.SetActive(false);
    }

    IEnumerator FadeInCover()
    {
        cover.SetActive(true);
        cameraa.SetActive(true);
        warning.SetActive(true);

        float alpha = coverImage.color.a; // Get the current alpha value
        float alpha1 = cameraImage.color.a;
        float alpha2 = msg.color.a;

        while (alpha < 1f) // Loop until alpha reaches 1 (fully opaque)
        {
            alpha += fadeSpeed * Time.deltaTime; // Increase alpha by fadeSpeed every frame
            alpha1 += fadeSpeed * Time.deltaTime;
            alpha2 += fadeSpeed * Time.deltaTime;
            coverImage.color = new Color(coverImage.color.r, coverImage.color.g, coverImage.color.b, alpha);
            cameraImage.color = new Color(cameraImage.color.r, cameraImage.color.g, cameraImage.color.b, alpha1);
            backupLines.color = new Color(backupLines.color.r, backupLines.color.g, backupLines.color.b, alpha2);
            msg.color = new Color(msg.color.r, msg.color.g, msg.color.b, alpha2);
            yield return null; // Wait for next frame before continuing
        }
    }
}
