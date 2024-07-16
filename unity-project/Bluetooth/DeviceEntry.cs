using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mauznemo.LinuxBluetooth;

public class DeviceEntry : MonoBehaviour
{
    [SerializeField] private Mauznemo.LinuxBluetooth.BluetoothDevice device;

    [SerializeField] private Button connectButton;
    [SerializeField] private Button pairButton;
    [SerializeField] private Button trustButton;
    [SerializeField] private TMP_Text deviceNameText;
    [SerializeField] private TMP_Text deviceMacAddressText;
    [SerializeField] private TMP_Text connectButtonText;
    [SerializeField] private TMP_Text pairButtonText;
    [SerializeField] private TMP_Text trustedButtonText;
    [SerializeField] private TMP_Text DebugText;

    [SerializeField] private bool connected;
    [SerializeField] private bool paired;
    [SerializeField] private bool trusted;

    public bool activeConnection = false;

    private void Awake()
    {
        connectButton.onClick.AddListener(() =>
        {
            if (connected)
                BluetoothManager.Instance.DisconnectToDevice(device.macAddress);
            else
                BluetoothManager.Instance.ConnectToDevice(device.macAddress);
        });
        pairButton.onClick.AddListener(() =>
        {
            if (paired)
                BluetoothManager.Instance.RemoveDevice(device.macAddress);
            else
                BluetoothManager.Instance.PairDevice(device.macAddress);
        });

        trustButton.onClick.AddListener(() =>
        {
            if (trusted)
                BluetoothManager.Instance.UntrustDevice(device.macAddress);
            else
                BluetoothManager.Instance.TrustDevice(device.macAddress);
        });
    }

    public void Init(BluetoothDevice newDevice)
    {
        device = newDevice;

        deviceNameText.text = device.name;
        deviceMacAddressText.text = device.macAddress;
        UpdateStats();

        BluetoothManager.OnUpdateUI += UpdateStats;
        BluetoothManager.OnRemoveDevice += OnRemoveDevice;
    }

    private void OnDestroy()
    {
        BluetoothManager.OnUpdateUI -= UpdateStats;
        BluetoothManager.OnRemoveDevice -= OnRemoveDevice;
    }

    private void OnRemoveDevice(string obj)
    {
        if (obj == device.macAddress)
        {
            BluetoothManager.Instance.RemoveDeviceFromList(obj);
            Debug.Log("Removed: " + obj);
            Destroy(gameObject);
        }
    }

    public void UpdateStats()
    {
        BluetoothDeviceInfo info = BluetoothManager.Instance.GetDeviceInfo(device.macAddress);
        DebugText.text = $"Paired: {info.paired}, Trusted: {info.trusted}, Bonded: {info.bonded}, Connected: {info.connected}, Battery: {info.batteryPercentage}";
        paired = info.paired;
        connected = info.connected;
        trusted = info.trusted;

        connectButton.interactable = paired;

        connectButtonText.text = connected ? "Disconnect" : "Connect";
        pairButtonText.text = paired ? "Remove" : "Pair";
        trustedButtonText.text = trusted ? "Untrust" : "Trust";

        activeConnection = connected ? true : false;
    }
}