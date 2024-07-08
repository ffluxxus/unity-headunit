using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import TextMesh Pro namespace

public class StatusTextController : MonoBehaviour
{
    public TextMeshProUGUI statusText; // Reference to the TextMesh Pro object
    public float dotAnimationSpeed = 1f;// Speed of the dot animation (adjust for desired speed)

    private string[] tips =
    {
        "Checking vehicle status", "Waiting on your vehicle", "Booting up", "Checking seatbelts", "Activating airbags"
    };
    private string baseText = "";
    private int dotCount = 0;

    void Start()
    {
        baseText = tips[Random.Range(0, 4)];

        statusText.text = baseText; // Set initial text
    }

    void Update()
    {
        dotCount = (int)(Time.time * dotAnimationSpeed) % 4; // Calculate dot count based on time and speed

        string currentText = baseText;

        // Add dots based on the dot count
        switch (dotCount)
        {
            case 0:
                currentText += "";
                break;
            case 1:
                currentText += ".";
                break;
            case 2:
                currentText += "..";
                break;
            case 3:
                currentText += "...";
                break;
        }

        statusText.text = currentText; // Update the text with dots
    }
}
