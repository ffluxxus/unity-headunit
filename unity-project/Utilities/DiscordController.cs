using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Discord;
using TMPro;
using UnityEngine.UI;

public class DiscordController : MonoBehaviour
{

    public Discord.Discord discord;
    public long applicationId = 000000000000000000; // replace with your discord application id
    public GameObject cpToggle;
    public GameObject aaToggle;
    public bool debugMode = false;
    private bool isUsingCarplay;
    private bool isUsingAndroidAuto;
    private bool enabledRPC;

    void Start()
    {

        enabledRPC = !Application.isEditor || debugMode;

        if (enabledRPC)
        {

            discord = new Discord.Discord(applicationId, (System.UInt64)Discord.CreateFlags.Default);
            var activityManager = discord.GetActivityManager();
            var activity = new Discord.Activity
            {

                Details = "Currently driving...",
                State = "",
                Assets = {

                LargeImage = "carplayx",
                LargeText = "v"+Application.version,
            }
            };

            // clear old activities
            activityManager.ClearActivity((result) =>
            {
                if (result == Discord.Result.Ok)
                {
                    Debug.Log("[DiscordController.cs] All old presences cleared");
                }
                else
                {
                    Debug.Log("[DiscordController.cs] Failed to clear old presences or discord process not found");
                }
            });

            activityManager.UpdateActivity(activity, (res) => {
                if (res == Discord.Result.Ok)
                {

                    Debug.Log("[DiscordController.cs] Default presence set and connected successfully");
                }
                else
                {

                    Debug.LogError("[DiscordController.cs] Discord process not found, failed to connect");
                }
            });
        }
    }

    void OnApplicationQuit ()
    {
        discord = new Discord.Discord(applicationId, (System.UInt64)Discord.CreateFlags.Default);
        var activityManager = discord.GetActivityManager();
        activityManager.ClearActivity((result) =>
        {
            if (result == Discord.Result.Ok)
            {
                Debug.Log("[DiscordController.cs] All old presences cleared");
            }
            else
            {
                Debug.Log("[DiscordController.cs] Failed to clear old presences or discord process not found");
            }
        });
    }

    void Update()
    {

        enabledRPC = !Application.isEditor || debugMode;
        
        if (enabledRPC)
        {

            discord.RunCallbacks();
            isUsingCarplay = cpToggle.GetComponent<Toggle>().isOn;
            isUsingAndroidAuto = cpToggle.GetComponent<Toggle>().isOn;
        }
    }

    void doActivity(string state, string small) {

        var activityManager = discord.GetActivityManager();
        var activity = new Discord.Activity {

            Details = "Currently driving...",
            State = state,
            Assets = {

                LargeImage = "carplayx",
                LargeText = "v"+Application.version,
                SmallImage = small,
            }
        };

        activityManager.UpdateActivity(activity, (res) => {
            if (res == Discord.Result.Ok) {

                Debug.Log("[DiscordController.cs] Presence Connected Successfully");
            } else {

                Debug.LogError("[DiscordController.cs] Presence not found, Discord process missing perhaps?");
            }
        });
    }

    public void toggleCarplay() {

        isUsingCarplay = cpToggle.GetComponent<Toggle>().isOn;

        if (enabledRPC) {

            if (isUsingCarplay)
            {

                if (enabled)
                {

                    doActivity("Using Carplay", "carplay");
                    Debug.Log("[DiscordController.cs] Carplay Discord Status set to " + isUsingCarplay);
                }
            }
            else
            {

                if (enabled)
                {

                    Debug.Log("[DiscordController.cs] Carplay Discord Status set to " + isUsingCarplay);
                    doActivity("", "");
                }
            }
        }

    }

    public void toggleAndroidAuto() {

        isUsingAndroidAuto = aaToggle.GetComponent<Toggle>().isOn;

        if (enabledRPC)
        {

            if (isUsingAndroidAuto)
            {

                if (enabled)
                {

                    Debug.Log("[DiscordController.cs] Android Auto Discord Status set to " + isUsingAndroidAuto);
                    doActivity("Using Android Auto", "aa");
                }
            }
            else
            {

                if (enabled)
                {

                    Debug.Log("[DiscordController.cs] Android Auto Discord Status set to " + isUsingAndroidAuto);
                    doActivity("", "");
                }
            }
        }
    }
}
