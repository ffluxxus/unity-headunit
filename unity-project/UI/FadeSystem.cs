using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FadeSystem : MonoBehaviour
{

    public float fadeSpeed = 1.0f;

    IEnumerator FadeOut(GameObject componentWithImageComponent)
    {
        float alpha = componentWithImageComponent.GetComponent<Image>().color.a; // Get the current alpha value

        while (alpha > 0f)
        {
            alpha -= fadeSpeed * Time.deltaTime; // Decrease alpha by fadeSpeed every frame
            componentWithImageComponent.GetComponent<Image>().color = new Color(componentWithImageComponent.GetComponent<Image>().color.r, componentWithImageComponent.GetComponent<Image>().color.g, componentWithImageComponent.GetComponent<Image>().color.b, alpha);
            yield return null; // Wait for next frame before continuing
        }

        // Set inactive only after the fade is complete
        componentWithImageComponent.SetActive(false);
    }

    IEnumerator FadeIn(GameObject componentWithImageComponent)
    {
        componentWithImageComponent.SetActive(true);

        float alpha = componentWithImageComponent.GetComponent<Image>().color.a; // Get the current alpha value

        while (alpha < 1f) // Loop until alpha reaches 1 (fully opaque)
        {
            alpha += fadeSpeed * Time.deltaTime; // Increase alpha by fadeSpeed every frame
            componentWithImageComponent.GetComponent<Image>().color = new Color(componentWithImageComponent.GetComponent<Image>().color.r, componentWithImageComponent.GetComponent<Image>().color.g, componentWithImageComponent.GetComponent<Image>().color.b, alpha);
            yield return null; // Wait for next frame before continuing
        }
    }
}
