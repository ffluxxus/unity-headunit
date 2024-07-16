using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConnectionIconController : MonoBehaviour
{

    [SerializeField] private DeviceEntry de;

    [SerializeField] private Image btIcon;
    [SerializeField] private TMP_Text cellLabel;
    [SerializeField] private Image cellBar1;
    [SerializeField] private Image cellBar2;
    [SerializeField] private Image cellBar3;
    [SerializeField] private Image cellBar4;

    [SerializeField] private bool displayMode = false;

    private bool activatedDM;

    void Start()
    {

        cellLabel.text = "";
        btIcon.enabled = false;
        cellBar1.enabled = false;
        cellBar2.enabled = false;
        cellBar3.enabled = false;
        cellBar4.enabled = false;
    }

    
    void Update()
    {

        if (displayMode && !activatedDM)
        {

            cellLabel.text = "5G";
            btIcon.enabled = true;
            cellBar1.enabled = true;
            cellBar2.enabled = true;
            cellBar3.enabled = true;
            cellBar4.enabled = true;
        }
        else
        {

            cellLabel.text = "";
            btIcon.enabled = false;
            cellBar1.enabled = false;
            cellBar2.enabled = false;
            cellBar3.enabled = false;
            cellBar4.enabled = false;
        }
    }
}
