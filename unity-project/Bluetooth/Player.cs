using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mauznemo.LinuxBluetooth;

public class Player : MonoBehaviour
{
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button playButton;
    [SerializeField] private TMP_Text playText;

    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text artistText;

    private bool playing = false;

    private void Awake()
    {
        previousButton.onClick.AddListener(() => {
            BluetoothManager.Instance.PlayerPrevious();
        });
        nextButton.onClick.AddListener(() => {
            BluetoothManager.Instance.PlayerNext();
        });
        playButton.onClick.AddListener(() => {
            if (playing)
            {
                BluetoothManager.Instance.PlayerPause();
            }
            else
            {
                BluetoothManager.Instance.PlayerPlay();
            }
        });
    }

    private void Start()
    {
        BluetoothManager.OnPlayerPlaying += OnPlayerPlaying;
        BluetoothManager.OnPlayerPaused += OnPlayerPaused;
        BluetoothManager.OnPlayerTitleChanged += OnPlayerTitleChanged;
        BluetoothManager.OnPlayerArtistChanged += OnPlayerArtistChanged;
        BluetoothManager.OnPlayerStopped += OnPlayerStopped;

        Invoke(nameof(GetPlayer), 0.5f);
    }

    private void OnPlayerStopped()
    {
        gameObject.SetActive(false);
    }

    private void GetPlayer()
    {
        BluetoothManager.Instance.SendPlayerCommand("show");
    }

    private void OnPlayerArtistChanged(string obj)
    {
        artistText.text = obj;
    }

    private void OnPlayerTitleChanged(string obj)
    {
        titleText.text = obj;
    }

    private void OnPlayerPaused()
    {
        gameObject.SetActive(true);
        playText.text = "Play";
        playing = false;
    }

    private void OnPlayerPlaying()
    {
        playText.text = "Pause";
        playing = true;
    }
}