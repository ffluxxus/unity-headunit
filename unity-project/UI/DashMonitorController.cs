using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashMonitorController : MonoBehaviour
{

    public Image gasIcon;
    public Image tireIcon;
    public Image batteryIcon;
    public Image engineIcon;
    public Image oilIcon;
    public Image seatbeltIcon;
    public Image tractionIcon;

    public bool gasToggle = false;
    public bool tireToggle = false;
    public bool batteryToggle = false;
    public bool engineToggle = false;
    public bool oilToggle = false;
    public bool seatToggle = false;
    public bool tractionToggle = false;

    public float disabledAlpha = 0.1f;
    public float enabledAlpha = 1.0f;


    void Start()
    {
        
        gasIcon.color = new Color(gasIcon.color.r, gasIcon.color.g, gasIcon.color.b, disabledAlpha);
        tireIcon.color = new Color(tireIcon.color.r, tireIcon.color.g, tireIcon.color.b, disabledAlpha);
        batteryIcon.color = new Color(batteryIcon.color.r, batteryIcon.color.g, batteryIcon.color.b, disabledAlpha);
        engineIcon.color = new Color(engineIcon.color.r, engineIcon.color.g, engineIcon.color.b, disabledAlpha);
        oilIcon.color = new Color(oilIcon.color.r, oilIcon.color.g, oilIcon.color.b, disabledAlpha);
        seatbeltIcon.color = new Color(seatbeltIcon.color.r, seatbeltIcon.color.g, seatbeltIcon.color.b, disabledAlpha);
        tractionIcon.color = new Color(tractionIcon.color.r, tractionIcon.color.g, tractionIcon.color.b, disabledAlpha);
    }

    public void toggleLowFuel()
    {

        if (gasToggle)
        {
            gasToggle = false;
            gasIcon.color = new Color(gasIcon.color.r, gasIcon.color.g, gasIcon.color.b, disabledAlpha);
        } else
        {
            gasToggle = true;
            gasIcon.color = new Color(gasIcon.color.r, gasIcon.color.g, gasIcon.color.b, enabledAlpha);
        }
    }

    public void toggleLowTirePSI()
    {

        if (tireToggle)
        {

            tireToggle = false;
            tireIcon.color = new Color(tireIcon.color.r, tireIcon.color.g, tireIcon.color.b, disabledAlpha);
        } else
        {

            tireToggle = true;
            tireIcon.color = new Color(tireIcon.color.r, tireIcon.color.g, tireIcon.color.b, enabledAlpha);
        }
    }

    public void toggleLowBattery()
    {

        if (batteryToggle)
        {

            batteryToggle = false;
            batteryIcon.color = new Color(batteryIcon.color.r, batteryIcon.color.g, batteryIcon.color.b, disabledAlpha);
        } else
        {

            batteryToggle = true;
            batteryIcon.color = new Color(batteryIcon.color.r, batteryIcon.color.g, batteryIcon.color.b, enabledAlpha);
        }
    }

    public void toggleEngineTemp()
    {

        if (engineToggle)
        {

            engineToggle = false;
            engineIcon.color = new Color(engineIcon.color.r, engineIcon.color.g, engineIcon.color.b, disabledAlpha);
        } else
        {

            engineToggle = true;
            engineIcon.color = new Color(engineIcon.color.r, engineIcon.color.g, engineIcon.color.b, enabledAlpha);
        }
    }

    public void toggleOilTemp ()
    {

        if (oilToggle)
        {

            oilToggle = false;
            oilIcon.color = new Color(oilIcon.color.r, oilIcon.color.g, oilIcon.color.b, disabledAlpha);
        } else
        {

            oilToggle = true;
            oilIcon.color = new Color(oilIcon.color.r, oilIcon.color.g, oilIcon.color.b, enabledAlpha);
        }
    }

    public void toggleSeatbelt()
    {

        if (seatToggle)
        {

            seatToggle = false;
            seatbeltIcon.color = new Color(seatbeltIcon.color.r, seatbeltIcon.color.g, seatbeltIcon.color.b, disabledAlpha);
        } else
        {

            seatToggle = true;
            seatbeltIcon.color = new Color(seatbeltIcon.color.r, seatbeltIcon.color.g, seatbeltIcon.color.b, enabledAlpha);
        }
    }

    public void toggleTraction()
    {

        if (tractionToggle)
        {

            tractionIcon.color = new Color(tractionIcon.color.r, tractionIcon.color.g, tractionIcon.color.b, disabledAlpha);
            tractionToggle = false;
        } else
        {

            tractionIcon.color = new Color(tractionIcon.color.r, tractionIcon.color.g, tractionIcon.color.b, enabledAlpha);
            tractionToggle = true;
        }
    }
}
