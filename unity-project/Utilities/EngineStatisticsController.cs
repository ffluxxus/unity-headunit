using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EngineStatisticsController : MonoBehaviour
{

    [SerializeField] private TMP_Text ParkGear;
    [SerializeField] private TMP_Text ReverseGear;
    [SerializeField] private TMP_Text NeutralGear;
    [SerializeField] private TMP_Text DriveGear;

    [SerializeField] private TMP_Text FirstGear;
    [SerializeField] private TMP_Text SecondGear;
    [SerializeField] private TMP_Text ThirdGear;
    [SerializeField] private TMP_Text FourthGear;
    [SerializeField] private TMP_Text FifthGear;
    [SerializeField] private TMP_Text SixthGear;

    [SerializeField] private TMP_Text Speed;
    [SerializeField] private TMP_Text Mileage;
    [SerializeField] private string gearbox = "Manual"; // Manual o Automatic
    [SerializeField] private string defaultGear = "P";

    [SerializeField] private GameObject SaveSystem;
    [SerializeField] private bool displayMode = false;

    [SerializeField] private Color disabledAlpha = new Color(0.3764706f, 0.3764706f, 0.3764706f, 1.0f);
    [SerializeField] private Color enabledAlpha = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    private bool activatedDM;
    private string currentGearbox = "Manual";

    void Start()
    {

        currentGearbox = gearbox;

        ParkGear.enabled = false;
        ReverseGear.enabled = false;
        NeutralGear.enabled = false;
        DriveGear.enabled = false;

        FirstGear.enabled = false;
        SecondGear.enabled = false;
        ThirdGear.enabled = false;
        FourthGear.enabled = false;
        FifthGear.enabled = false;
        SixthGear.enabled = false;

        switch (defaultGear) {

            case "P":
                ParkGear.color = enabledAlpha;
                ReverseGear.color = disabledAlpha;
                NeutralGear.color = disabledAlpha;
                DriveGear.color = disabledAlpha;
                break;

            case "R":
                ParkGear.color = disabledAlpha;
                ReverseGear.color = enabledAlpha;
                NeutralGear.color = disabledAlpha;
                DriveGear.color = disabledAlpha;
                break;

            case "N":
                ParkGear.color = disabledAlpha;
                ReverseGear.color = disabledAlpha;
                NeutralGear.color = enabledAlpha;
                DriveGear.color = disabledAlpha;
                break;

            case "D":
                ParkGear.color = disabledAlpha;
                ReverseGear.color = disabledAlpha;
                NeutralGear.color = disabledAlpha;
                DriveGear.color = enabledAlpha;
                break;

            default:
                ParkGear.color = enabledAlpha;
                ReverseGear.color = disabledAlpha;
                NeutralGear.color = disabledAlpha;
                DriveGear.color = disabledAlpha;
                break;

        }
    }

    
    void Update()
    {


        if (gearbox == "Manual" && gearbox != currentGearbox)
        {

            DriveGear.enabled = false;
        }
        else if (gearbox == "Automatic" && gearbox != currentGearbox)
        {

            DriveGear.enabled = true;
        }

        if (displayMode && !activatedDM)
        {

            Speed.text = "19";
            Mileage.text = "315 mi";
            if (ParkGear.enabled)
            {

                SwapMode();
            }

        }
        else
        {

            Speed.text = "0";
            Mileage.text = "0 mi";
            if (!ParkGear.enabled)
            {

                SwapMode();
            }
        }
    }

    public void SwapMode()
    {

        if (gearbox == "Automatic")
        {

            Debug.LogError("[EngineStatisticsController.cs] Swap Manual Mode method cannot be ran in Automatic Gearbox mode.");
        } else
        {

            if (ParkGear.enabled)
            {

                ParkGear.enabled = false;
                ReverseGear.enabled = false;
                NeutralGear.enabled = false;
                DriveGear.enabled = false;

                FirstGear.enabled = true;
                SecondGear.enabled = true;
                ThirdGear.enabled = true;
                FourthGear.enabled = true;
                FifthGear.enabled = true;
                SixthGear.enabled = true;
            } else
            {

                ParkGear.enabled = true;
                ReverseGear.enabled = true;
                NeutralGear.enabled = true;
                DriveGear.enabled = false;

                FirstGear.enabled = false;
                SecondGear.enabled = false;
                ThirdGear.enabled = false;
                FourthGear.enabled = false;
                FifthGear.enabled = false;
                SixthGear.enabled = false;
            }
        }
    }
}
