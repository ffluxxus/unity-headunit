using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.UIElements;
using System;
using UnityEngine.UI;

// PDF Documentation of saving variables: file:///E:/Unity/Local%20Projects/CarDash/Assets/BayatGames/SaveGameFree/Documentation/Save%20Game%20Free%20Documentation%20-%20PDF%20Version.pdf

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private GameObject FramerateInput;
    [SerializeField] private GameObject AAStartupToggle;
    [SerializeField] private GameObject CPStartupToggle;
    [SerializeField] private GameObject AASpotifySwapToggle;
    [SerializeField] private GameObject EngineStatisticsSystem;
    [SerializeField] private int FallbackFPS = 72;
    [SerializeField] private bool EditorLoadingAndSaving = false;
    private int FPSLimit;
    private bool AAonStartup;
    private bool CPonStartup;
    private bool AASpotifySwap;
    private bool vGearbox;

    void Start()
    {

        if (Application.isEditor && EditorLoadingAndSaving)
        {

            CheckSaveFile();
            LoadFromSaveFile();
        } else if (!Application.isEditor)
        {

            CheckSaveFile();
            LoadFromSaveFile();
        }
    }


    void Update()
    {

        
    }
    
    public void LoadFromSaveFile ()
    {

        FPSLimit = SaveGame.Load<int>("fps_limit");
        AAonStartup = SaveGame.Load<bool>("aa_on_startup");
        CPonStartup = SaveGame.Load<bool>("cp_on_startup");
        AASpotifySwap = SaveGame.Load<bool>("aa_spotify_swap");
        vGearbox = SaveGame.Load<bool>("virtual_gearbox");

        AAStartupToggle.GetComponent<Toggle>().isOn = AAonStartup;
        CPStartupToggle.GetComponent <Toggle>().isOn = CPonStartup;
        AASpotifySwapToggle.GetComponent<Toggle>().isOn = AASpotifySwap;
    }

    public void CheckSaveFile ()
    {

        if (!SaveGame.Exists("has_ran"))
        {

            try
            {

                FPSLimit = int.Parse(FramerateInput.GetComponent<TMP_InputField>().text);
            } catch (FormatException e)
            {

                FPSLimit = FallbackFPS;
            }
            AAonStartup = AAStartupToggle.GetComponent<Toggle>().isOn;
            CPonStartup = CPStartupToggle.GetComponent<Toggle>().isOn;
            AASpotifySwap = AASpotifySwapToggle.GetComponent <Toggle>().isOn;

            SaveGame.Save<bool>("has_ran", true);
            if (FPSLimit != -1 || FPSLimit! < 15)
            {

                SaveGame.Save<int>("fps_limit", FPSLimit);
            }
            else
            {

                SaveGame.Save<int>("fps_limit", FallbackFPS);
            }

            SaveGame.Save<bool>("aa_on_startup", AAonStartup);
            SaveGame.Save<bool>("cp_on_startup", CPonStartup);
            SaveGame.Save<bool>("aa_spotify_swap", AASpotifySwap);
            SaveGame.Save<bool>("virtual_gearbox", EngineStatisticsSystem.GetComponent<EngineStatisticsController>().virtualGearboxStatus());
        }
    }

    public void SaveFPSLimit()
    {

        if (Application.isEditor && !EditorLoadingAndSaving)
        {

            return;
        } 
        else if (!SaveGame.Exists("has_ran"))
        {

            CheckSaveFile();
        }

        int temp = FPSLimit;
        try
        {

            FPSLimit = int.Parse(FramerateInput.GetComponent<TMP_InputField>().text);
        }
        catch (FormatException e)
        {

            FPSLimit = FallbackFPS;
        }

        if (FPSLimit != -1 || FPSLimit !< 15)
        {

            SaveGame.Save<int>("fps_limit", FPSLimit);
        } else
        {

            FPSLimit = temp;
        }
    }

    public void SaveAAonStartup()
    {

        if (Application.isEditor && !EditorLoadingAndSaving)
        {

            return;
        }
        else if (!SaveGame.Exists("has_ran"))
        {

            CheckSaveFile();
        }

        if (AAonStartup != AAStartupToggle.GetComponent<Toggle>().isOn) // user changed variable
        {
            AAonStartup = AAStartupToggle.GetComponent<Toggle>().isOn;
            SaveGame.Save<bool>("aa_on_startup", AAonStartup);

            if (CPonStartup | SaveGame.Load<bool>("cp_on_startup") && AAonStartup)
            {

                SaveGame.Save<bool>("cp_on_startup", false);
                CPonStartup = SaveGame.Load<bool>("cp_on_startup");
            }
        }
    }

    public void SaveCPonStartup()
    {

        if (Application.isEditor && !EditorLoadingAndSaving)
        {

            return;
        }
        else if (!SaveGame.Exists("has_ran"))
        {

            CheckSaveFile();
        }

        if (CPonStartup != CPStartupToggle.GetComponent<Toggle>().isOn) // user changed variable
        {
            CPonStartup = CPStartupToggle.GetComponent<Toggle>().isOn;
            SaveGame.Save<bool>("cp_on_startup", AAonStartup);

            if (AAonStartup | SaveGame.Load<bool>("aa_on_startup") && CPonStartup)
            {

                SaveGame.Save<bool>("aa_on_startup", false);
                AAonStartup = SaveGame.Load<bool>("aa_on_startup");
            }
        }
    }

    public void SaveAASpotifySwap()
    {

        if (Application.isEditor && !EditorLoadingAndSaving)
        {

            return;
        }
        else if (!SaveGame.Exists("has_ran"))
        {

            CheckSaveFile();
        }

        if (CPonStartup != CPStartupToggle.GetComponent<Toggle>().isOn) // user changed variable
        {
            CPonStartup = CPStartupToggle.GetComponent<Toggle>().isOn;
            SaveGame.Save<bool>("aa_spotify_swap", AASpotifySwap);

            if (AAonStartup | SaveGame.Load<bool>("aa_on_startup") && AASpotifySwap)
            {

                SaveGame.Save<bool>("aa_on_startup", false);
                AAonStartup = SaveGame.Load<bool>("aa_on_startup");
            }
        }
    }
}
