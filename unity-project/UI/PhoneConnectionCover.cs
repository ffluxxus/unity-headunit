using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;
using System;
using System.Diagnostics.Eventing.Reader;
using Nova;
//using VoltstroStudios.UnityWebBrowser;
//using VoltstroStudios.UnityWebBrowser.Core;

public class PhoneConnectionCover : MonoBehaviour
{
    public GameObject blurObject;
    public UIBlock2D blur;
    public GameObject aaToggleB;
    public GameObject cpToggleB;
    public GameObject sToggleB;
    public GameObject Browser;
    private Toggle aaToggle;
    private Toggle cpToggle;
    private Toggle spotifyToggle;
    public float fadeSpeed = 1.0f; // Adjust this value to control fade speed (lower = slower)
    private RawImage browser;
    public bool isFirstRun = true;
    private bool aaState = false;
    private bool cpState = false;
    private bool spotifyState = false;
    public string androidAutoPath = ""; // replace with your android auto developer headunit path
    public string spotifyPath = "https://open.spotify.com";
    public string carplayPath = ""; // replace with your react app url once created. its set rn to carplay image
    //[SerializeField] private BaseUwbClientManager webClientManager;
    //private WebBrowserClient webBrowserClient;

    void Start()
    {
        blur.Color = new Color(blur.Color.r, blur.Color.g, blur.Color.b, 0f);

        aaToggle = aaToggleB.GetComponent<Toggle>();
        spotifyToggle = sToggleB.GetComponent<Toggle>();
        aaState = aaToggle.isOn;
        cpToggle = cpToggleB.GetComponent<Toggle>();
        browser = Browser.GetComponent<RawImage>();
        browser.color = new Color(browser.color.r, browser.color.g, browser.color.b, 0f);
        cpState = cpToggle.isOn;
        blurObject.SetActive(true);
        //webBrowserClient = webClientManager.browserClient;
    }

    public void toggleCover()
    {
        spotifyToggle = sToggleB.GetComponent<Toggle>();
        cpToggle = cpToggleB.GetComponent<Toggle>();
        spotifyState = spotifyToggle.GetComponent<Toggle>();
        blurObject.SetActive(true);
        aaToggle = aaToggleB.GetComponent<Toggle>();
        browser = Browser.GetComponent<RawImage>();
        aaState = aaToggle.isOn;
        cpState = cpToggle.isOn;
        spotifyState = spotifyToggle.isOn;

        if (aaState || cpState)
        {
            if (aaState)
            {

                if (spotifyState)
                {
                    //webBrowserClient.LoadUrl(spotifyPath);
                }
                StartCoroutine(FadeInCoverAA());
            }
            else
            {

                //webBrowserClient.LoadUrl(carplayPath);
                StartCoroutine(FadeInCover());

            }
        }
        else
        {
            if (aaState)
            {

                StartCoroutine(FadeOutCoverAA());
            }
            else
            {
                {
                    StartCoroutine(FadeOutCover());
                }
            }

            if (aaState)
            {

                if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
                {

                }
                else
                {

                    // linux addition

                }
            }
            else if (!aaState)
            {

                if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
                {

                    Process[] processes = Process.GetProcessesByName("desktop_head_unit.exe");
                    if (processes.Length > 0)
                    {
                        processes[0].Kill();
                    }
                }
                else
                {

                    // linux implement

                }
            }
        }
    }

    IEnumerator FadeOutCoverAA()
    {
        browser.color = new Color(browser.color.r, browser.color.g, browser.color.b, 0f);
        float alpha = blur.Color.a; // Get the current alpha value

        while (alpha > 0f)
        {
            alpha -= fadeSpeed * Time.deltaTime; // Decrease alpha by fadeSpeed every frame
            blur.Color = new Color(blur.Color.r, blur.Color.g, blur.Color.b, alpha);
            yield return null; // Wait for next frame before continuing
        }

        // Set inactive only after the fade is complete
        blurObject.SetActive(false);
        Browser.SetActive(false);
    }

    IEnumerator FadeInCoverAA()
    {

        browser.color = new Color(browser.color.r, browser.color.g, browser.color.b, 0f);
        blurObject.SetActive(true);

        float alpha = blur.Color.a; // Get the current alpha value

        while (alpha < 1f) // Loop until alpha reaches 1 (fully opaque)
        {
            alpha += fadeSpeed * Time.deltaTime; // Increase alpha by fadeSpeed every frame
            blur.Color = new Color(blur.Color.r, blur.Color.g, blur.Color.b, alpha);
            //if (spotifyState)
            //{

                //browser.color = new Color(browser.color.r, browser.color.g, browser.color.b, alpha);
            //}
            yield return null; // Wait for next frame before continuing
        }
    }

    IEnumerator FadeOutCover()
    {
        float alpha = blur.Color.a; // Get the current alpha value

        while (alpha > 0f)
        {
            alpha -= fadeSpeed * Time.deltaTime; // Decrease alpha by fadeSpeed every frame
            blur.Color = new Color(blur.Color.r, blur.Color.g, blur.Color.b, alpha);
            browser.color = new Color(browser.color.r, browser.color.g, browser.color.b, alpha);
            yield return null; // Wait for next frame before continuing
        }

        // Set inactive only after the fade is complete
        blurObject.SetActive(false);
        Browser.SetActive(false);
    }

    IEnumerator FadeInCover()
    {
        blurObject.SetActive(true);
        Browser.SetActive(true);

        float alpha = blur.Color.a; // Get the current alpha value

        while (alpha < 1f) // Loop until alpha reaches 1 (fully opaque)
        {
            alpha += fadeSpeed * Time.deltaTime; // Increase alpha by fadeSpeed every frame
            blur.Color = new Color(blur.Color.r, blur.Color.g, blur.Color.b, alpha);
            browser.color = new Color(browser.color.r, browser.color.g, browser.color.b, alpha);
            yield return null; // Wait for next frame before continuing
        }
    }
}
