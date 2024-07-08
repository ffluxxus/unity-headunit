using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.TestTools;
using UnityEditor;
using System.Diagnostics.Eventing.Reader;

/*
    NEEDS IMPLEMENTATION OF ENGINE DATA
*/

public class QuestsMenuController : MonoBehaviour
{

    public GameObject QuestsToggle;
    public GameObject Menu;
    public GameObject dMenu;
    public GameObject bMenu;
    public GameObject[] quests;
    public GameObject[] questCheckmarks;
    public GameObject[] questBackgrounds;
    public GameObject[] questLabels;

    private Color incompleteC = new Color(85, 85, 85, 1f);
    private Color completeC = new Color(7, 170, 0, 1f);
    private string[] currentQuests;
    private bool status = false;

    [SerializeField]
    public string[] allQuests =
    {
        "Achieve 45 MPH", 
        "Careful Driver", // drive for 10 minutes without quickly / harshly rotating gyro sensor
        "Achieve 60 MPH",
        "Baby Your Car", // drive like careful driver quest + no harsh acceleration / braking
        "Click it", // click your seatbelt
        "No Intel", // keep radar detector off for entire drive
        "Definitely Distracted", // do 5 or more actions on the headunit while moving 15+ mph
        "Sippin' that Drank", // have the movement tracker catch you taking a sip of a drink
        "Perfect Parking", // have the parking sensor (if enabled), confirm your in the lines with equal room per side
        "Maintenance Day", // have the GPS register you at a auto shop / body shop / etc.
        "Reckless Passenger", // have the passenger not click their seatbelt once moving 20+ mph
        "Backseat Driver", // have the microphone pickup talking about driver movements on the road
        "Fill Her Up", // have your gas tank fill from 10% (<1/4) to 95%(>3/4) or higher 
        "Moving In Style", // play music at 50% volume or higher for 20 minutes
        "Nighttime Driver" // start driving in the day and continue to the night
    };

    private void Start()
    {

        Menu.SetActive(false);

        /*
        if (quests.Length == 0 || quests.Length != questCheckmarks.Length || allQuests.Length == 0 || quests.Length != questLabels.Length || quests.Length != questBackgrounds.Length)
        {

            Debug.LogError("[QuestsMenuController.cs] An object was left empty or an important array. Failed to initialize quests system!");
        } else
        {

            status = true;
            resetAllQuests();
        }
        */

        status = true;
        resetAllQuests();
    }

    private void Update()
    {
        
    }

    public void OnClick()
    {

        if (QuestsToggle.GetComponent<Toggle>().isOn)
        {

            if (dMenu.activeInHierarchy)
            {

                dMenu.SetActive(false);
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

    public void resetAllQuests()
    {

        currentQuests = giveQuests();

        for (int i = 0; i < currentQuests.Length; i++)
        {

            questLabels[i].GetComponent<TMP_Text>().text = (1+i)+". "+currentQuests[i];
        }

        for (int i = 0; i < questCheckmarks.Length; i++)
        {

            questCheckmarks[i].GetComponent<Toggle>().enabled = false;
            questBackgrounds[i].GetComponent<Image>().color = incompleteC;
        }

    }

    public string[] giveQuests()
    {

        string[] result = new string[quests.Length];
        int value = -1;

        for (int i = 0; i < quests.Length; i++)
        {

            value = Random.Range(0, allQuests.Length);
            while (result.Contains(allQuests[value]))
            {

                value = Random.Range(0, allQuests.Length);
            }

            result[i] = allQuests[value];
        }

        for (int i = 0; i < result.Length; i++)
        {

            if (result[i] == null) { 
                for (int j = 0; j < quests.Length; j++)
                {

                    result[j] = "Quest Request Failed";
                }
                return result;
            }
        }

        return result;
    }

    public bool isComplete (GameObject quest)
    {

        if (quests.Contains(quest))
        {

            int index = -1;

            for (int i = 0; i < quests.Length; i++)
            {

                if (quests[i] == quest)
                {

                    index = i; break;
                }
            }

            if (index != -1)
            {

                Toggle tog = questCheckmarks[index].GetComponent<Toggle>();
                
                if (tog.enabled)
                {

                    return true;
                }
            }
        }

        return false;
    }

    public void complete (GameObject quest)
    {

        if (quests.Contains(quest) && !isComplete(quest))
        {

            int index = -1;

            for (int i = 0; i < quests.Length; i++)
            {

                if (quests[i] == quest)
                {

                    index = i;
                }
            }

            questCheckmarks[index].GetComponent<Toggle>().enabled = true;
            questBackgrounds[index].GetComponent<Image>().color = completeC;
        }
    }
}
