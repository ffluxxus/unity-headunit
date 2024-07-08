using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenuController : MonoBehaviour
{

    public GameObject DebugToggle;
    public GameObject Menu;
    public GameObject MetricsT;
    public GameObject PerfMetrics;
    public GameObject qMenu;
    public GameObject bMenu;
    
    void Start()
    {

        Menu.SetActive(false);
    }

    public void OnClick()
    {

        if (DebugToggle.GetComponent<Toggle>().isOn)
        {

            if (qMenu.activeInHierarchy)
            {

                qMenu.SetActive(false);
                Menu.SetActive(true);
            }
            else
            {

                Menu.SetActive(true);
            }

            if (bMenu.activeInHierarchy)
            {

                bMenu.SetActive(false);
                Menu.SetActive(true);
            }
            else
            {

                Menu.SetActive(true);
            }
        }
        else
        {

            Menu.SetActive(false);
        }
    }

    public void onMetrics()
    {

        if (MetricsT.GetComponent<Toggle>().isOn)
        {

            PerfMetrics.SetActive(true);
        }
        else
        {

            PerfMetrics.SetActive(false);
        }
    }
}
