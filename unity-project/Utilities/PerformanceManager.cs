using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class PerformanceManager : MonoBehaviour
{

    public int targetFPS = 72;
    public GameObject TField;

    void Start()
    {

        if (Application.isPlaying)
        {

            updateFPS();
        }
    }

    void Update()
    {
        
        if (Application.isPlaying)
        {

            updateFPS();
        }
    }

    void updateFPS()
    {

        Application.targetFrameRate = targetFPS;
    }

    public void getFPSLimitFromField()
    {
        
        if (int.Parse(TField.GetComponent<TMP_InputField>().text) > 15)
        {
            targetFPS = int.Parse(TField.GetComponent<TMP_InputField>().text);
        }
        Application.targetFrameRate = targetFPS;
    }
}
