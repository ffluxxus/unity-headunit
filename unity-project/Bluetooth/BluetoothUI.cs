using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mauznemo.LinuxBluetooth;

public class BluetoothUI : MonoBehaviour
{
    [SerializeField] private Button scanButton;
    [SerializeField] private Button lockButton;
    [SerializeField] private TMP_Text lockButtonText;
    [SerializeField] private TMP_Text scanButtonText;

    [SerializeField] private DeviceEntry deviceEntryPrefab;
    [SerializeField] private Transform deviceEntryParent;
    [SerializeField] private Transform transformPaired;
    [SerializeField] private Transform transformFound;

    private bool blocked;

    private void Start()
    {
        blocked = BluetoothManager.Instance.IsSoftBlocked();
        lockButtonText.text = blocked ? "Bluetooth: Off" : "Bluetooth: On";

        scanButton.onClick.AddListener(() =>
        {
            BluetoothManager.Instance.SetScan(true);
            scanButtonText.text = "Scanning...";
        });
        lockButton.onClick.AddListener(() =>
        {
            blocked = !blocked;
            BluetoothManager.Instance.SetBluetoothBlock(blocked);
            lockButtonText.text = blocked ? "Bluetooth: Off" : "Bluetooth: On";
        });

        BluetoothManager.OnDeviceFound += HandleDeviceFound;
        BluetoothManager.OnConfirmPasskey += HandleConfirmPasskey;

        ShowPairedDevices();
    }

    private void HandleConfirmPasskey(string obj)
    {
        ModalWindow.Create().Init("Passkey", $"Confirm passkey: {obj}", ModalWindow.ModalType.YesNo, () =>
        {
            BluetoothManager.Instance.ConfirmPasskey();
        }, () => { });
    }

    private void HandleDeviceFound((string macAddress, string name, bool paired) device)
    {
        int index;
        if (device.paired)
        {
            index = transformPaired.GetSiblingIndex();
        }
        else
        {
            index = transformFound.GetSiblingIndex();
        }

        DeviceEntry deviceEntry = Instantiate(deviceEntryPrefab, deviceEntryParent);
        deviceEntry.gameObject.SetActive(true);
        deviceEntry.transform.SetSiblingIndex(index + 1);
        deviceEntry.Init(new BluetoothDevice
        {
            name = device.name,
            macAddress = device.macAddress
        });
    }

    private void ShowPairedDevices()
    {
        var devices = BluetoothManager.Instance.ListPairedDevices();
        foreach (var device in devices)
        {
            BluetoothManager.Instance.HandleNewDevice(device.name, device.macAddress, true);
        }
    }
}