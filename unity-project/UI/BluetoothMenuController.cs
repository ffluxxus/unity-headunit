using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BluetoothMenuController : MonoBehaviour
{

    public GameObject BTToggle;
    public GameObject Menu;
    public GameObject qMenu;
    public GameObject dMenu;

    private void Start()
    {

        Menu.SetActive(false);

    }

    public void OnClick()
    {

        if (BTToggle.GetComponent<Toggle>().isOn)
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

            if (dMenu.activeInHierarchy)
            {

                dMenu.SetActive(false);
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
}
