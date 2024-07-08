using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Threading;


public class SpotifyMediaController : MonoBehaviour
{
    [SerializeField] private string serverHost = "127.0.0.1";
    [SerializeField] private int serverPort = 5000;
    [SerializeField] private float updateInterval = 0.0f;
    [SerializeField] private float startDelay = 2.0f;
    [SerializeField] private float reconnectInterval = 5f;

    public GameObject songName;
    public GameObject songArtist;
    public GameObject songAlbumArt;
    public GameObject bluetoothModeToggle;
    public Sprite fallback;

    public TMP_Text nullAlbumText;
    public TMP_Text playtime;
    private TMP_Text nameT;
    private TMP_Text artistT;

    private Socket socket;
    private bool isConnected = false;
    private bool isBluetoothMode = false;

    private float nextUpdateTime = 0.0f;
    private float nextReconnectTime = 0.0f;

    void Start()
    {

        StartCoroutine(ConnectToServer());

        nameT = songName.GetComponent<TMP_Text>();
        artistT = songArtist.GetComponent<TMP_Text>();

        isBluetoothMode = bluetoothModeToggle.GetComponent<Toggle>().isOn;
    }

    void Update()
    {

        isBluetoothMode = bluetoothModeToggle.GetComponent<Toggle>().isOn;

        if (isBluetoothMode)
        {

            nameT = songName.GetComponent<TMP_Text>();
            artistT = songArtist.GetComponent<TMP_Text>();
            nameT.SetText("...");
            artistT.SetText("...");
            playtime.SetText("...");
            nullAlbumText.SetText("?");
            StartCoroutine(DownloadImage(""));

        } else if (!isConnected)
        {

            // StartCoroutine(ConnectToServer());
        }

        if (isConnected && Time.time >= nextUpdateTime && !isBluetoothMode)
        {
            CheckForUpdates();
            nextUpdateTime = Time.time + updateInterval;
        }
    }

    public void PlayPause()
    {

        if (!isConnected || isBluetoothMode)
            StartCoroutine(ConnectToServer());
    }

    public void Reverse()
    {

        if (!isConnected || isBluetoothMode)
            StartCoroutine(ConnectToServer());

    }

    public void Forward()
    {

        if (!isConnected || isBluetoothMode)
            StartCoroutine(ConnectToServer());

    }

    IEnumerator DownloadImage(string MediaUrl)
    {
        if (string.IsNullOrEmpty(MediaUrl) || isBluetoothMode) // Check for empty or null string
        {
            songAlbumArt.GetComponent<Image>().overrideSprite = fallback; // Set fallback texture directly
            yield break; // Exit the coroutine if empty string is encountered
        }

        if (!isBluetoothMode)
        {

            UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError("[SpotifyMediaController.cs] Failed to apply texture/render to album art image" + request.error);
            }
            else
            {
                Texture2D tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
                songAlbumArt.GetComponent<Image>().overrideSprite = sprite;
            }
        }
    }


    private bool Connect()
    {
        if (!isBluetoothMode)
        {

            try
            {
                // Create a TCP socket
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Connect to the server
                socket.Connect(new IPEndPoint(IPAddress.Parse(serverHost), serverPort));
                return true;
            }
            catch (SocketException ex)
            {
                Debug.LogError($"[SpotifyMediaController.cs] Error connecting to server: {ex.Message}");
                return false;
            }
        } else
        {

            return false;
        }
    }


    IEnumerator ConnectToServer()
    {
        if (!isBluetoothMode)
        {

            if (Connect())
            {
                isConnected = true;
                Debug.Log("[MediaController.cs] Connected to server.");
            }
        }


        yield return null;
    }




    private void CheckForUpdates()
    {
        if (socket == null || !isConnected)
        {
            return;
        }

        try
        {
            // Receive data from the server
            byte[] buffer = new byte[1024];
            int bytesReceived = socket.Receive(buffer);

            if (bytesReceived > 0)
            {
                // Decode received data
                string jsonString = Encoding.UTF8.GetString(buffer, 0, bytesReceived);

                //Debug.Log($"[MediaController.cs] Received JSON data: {jsonString}");

                // Parse JSON data with error handling
                try
                {

                    nameT = songName.GetComponent<TMP_Text>();
                    artistT = songArtist.GetComponent<TMP_Text>();

                    var node = JSON.Parse(jsonString);
                    string title = node[0]["title"];
                    string artist = node[0]["artist"];
                    string img_url = node[0]["image"];
                    int start = node[0]["start"];
                    int end = node[0]["end"];
                    string playing = node[0]["playing"];

                    if (playing == "True")
                    {

                        TimeSpan spt = TimeSpan.FromMilliseconds(start);
                        TimeSpan ept = TimeSpan.FromMilliseconds(end);

                        string StartTime = spt.ToString(@"mm\:ss");
                        string EndTime = ept.ToString(@"mm\:ss");

                        playtime.SetText(StartTime + " / " + EndTime);

                        nameT.SetText(title);
                        artistT.SetText(artist);

                        if (img_url.Contains("https://"))
                        {

                            StartCoroutine(DownloadImage(img_url));
                            nullAlbumText.SetText("");
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    Debug.LogError($"[MediaController.cs] Error parsing JSON data: {ex.Message}");
                    nameT = songName.GetComponent<TMP_Text>();
                    artistT = songArtist.GetComponent<TMP_Text>();
                    nameT.SetText("Nothing playing");
                    artistT.SetText("");
                    playtime.SetText("");
                    nullAlbumText.SetText("?");
                    StartCoroutine(DownloadImage(""));
                }
            }
            else
            {
                // Handle case where no data is received (e.g., server might not be sending anything)
                Debug.LogWarning("[MediaController.cs] No data received from server.");
                nameT = songName.GetComponent<TMP_Text>();
                artistT = songArtist.GetComponent<TMP_Text>();
                nameT.SetText("Nothing playing");
                artistT.SetText("");
                playtime.SetText("");
                nullAlbumText.SetText("?");
                StartCoroutine(DownloadImage(""));
            }
        }
        catch (SocketException ex)
        {
            Debug.LogWarning($"[MediaController.cs] Error receiving data from server: {ex.Message}");
            nameT = songName.GetComponent<TMP_Text>();
            artistT = songArtist.GetComponent<TMP_Text>();
            nameT.SetText("...");
            artistT.SetText("...");
            playtime.SetText("...");
            nullAlbumText.SetText("?");
            StartCoroutine(DownloadImage(""));
            isConnected = false; // Handle disconnection
        }
    }

    void OnApplicationQuit()
    {
        if (socket != null && isConnected)
        {
            socket.Close();
        }
    }
}