using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    [SerializeField] private TextMeshProUGUI stationText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private CanvasGroup pauseMenu;
    [SerializeField] private AudioClip[] stationJingles;
    [SerializeField] private AudioClip[] departureJingles;
    [SerializeField, FormerlySerializedAs("audio")] private AudioSource audioSource;

    private List<Station> stations;
    private int stationNumber = 0;
    private float timer;

    public Color[] stopColours;
    public float depTimer = 30;
    public int score = 0;
    public event Action nextStopEvent;
    public string spawnSide;

    private void Awake()
    {
        // Singleton
        if (_instance != null && _instance != this)
            Destroy(this);
        else
            _instance = this;

        stations = new List<Station>()
        {
            new Station("Tokyo", "right"),
            new Station("Shinagawa", "left"),
            new Station("Shibuya", "right"),
            new Station("Shinjuku", "left"),
            new Station("Ikebukuro", "right"),
            new Station("Ueno", "left"),
        };
        scoreText.text = "Score: " + score.ToString();
        spawnSide = stations[stationNumber].EntryDirection;
    }

    void Start()
    {
        timer = depTimer;
        InvokeRepeating(nameof(UpdateTimer), 0, 1);
        // Colours to fill each person by stop

        stationText.text = stations[stationNumber].StationName;
        audioSource.PlayOneShot(stationJingles[stationNumber]);
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (IsInvoking(nameof(UpdateTimer)))
        {
            CancelInvoke(nameof(UpdateTimer));
            timerText.text = "00:00";
            // Cancel input?
            // Move to next station
            if (stationNumber >= stations.Count)
            {
                // Game Over
                QuitGame();
            }
            else
            {
                ChangeStation();
            }
        }
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score.ToString();
    }

    public void ChangeStation()
    {
        StartCoroutine(PlayDepJingle());
    }

    private IEnumerator PlayDepJingle()
    {
        audioSource.PlayOneShot(departureJingles[Random.Range(0, departureJingles.Length - 1)]);
        while (audioSource.isPlaying)
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;//0.01f;
            }
            yield return null;
        }
        Time.timeScale = 1;

        stationNumber++;
        stationText.text = stations[stationNumber].StationName;
        spawnSide = stations[stationNumber].EntryDirection;
        audioSource.PlayOneShot(stationJingles[stationNumber]);
        nextStopEvent?.Invoke();
        timer = depTimer;
        InvokeRepeating(nameof(UpdateTimer), 0, 1);
    }

    private void UpdateTimer()
    {
        timerText.text = "00:" + Mathf.Ceil(timer).ToString("00");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.alpha = 1;
        pauseMenu.blocksRaycasts = true;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        pauseMenu.alpha = 0;
        pauseMenu.blocksRaycasts = false;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
