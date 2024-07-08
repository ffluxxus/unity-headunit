using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class BaseArduinoCommunication : MonoBehaviour
{
    protected SerialPort serialPort;
    [SerializeField] protected string portName;
    [SerializeField] protected int baudRate = 9600;

    protected void Init()
    {
        if (string.IsNullOrEmpty(portName))
        {
            Debug.LogError($"Port for Arduino is null");
            return;
        }

        serialPort = new SerialPort(portName, baudRate);
        serialPort.Open();
    }

    public bool IsConnected()
    {
        return serialPort != null && serialPort.IsOpen;
    }

    protected void ReadMessage()
    {
        if (serialPort != null && serialPort.IsOpen && serialPort.BytesToRead > 0)
        {
            string message = serialPort.ReadLine();
            char[] charsToTrim = { ' ', '\n', '\r', '\t' };
            string trimmed = message.Trim(charsToTrim);
            Received(trimmed);
        }
    }

    protected void ReadLatestMessage()
    {
        if (serialPort != null && serialPort.IsOpen && serialPort.BytesToRead > 0)
        {
            string message = serialPort.ReadLine();
            if (message.Contains("\n"))
            {
                serialPort.DiscardOutBuffer();
                serialPort.DiscardInBuffer();
            }

            char[] charsToTrim = { ' ', '\n', '\r', '\t' };
            string trimmed = message.Trim(charsToTrim);

            if (string.IsNullOrEmpty(trimmed))
            {
                return;
            }

            Received(trimmed);
        }
    }

    public void Send(string message)
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Write(message + "\n");
        }
        else
        {
            Debug.LogError($"Arduino on {portName} not connected!");
        }
    }

    public virtual void Received(string message)
    {

    }

    void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
