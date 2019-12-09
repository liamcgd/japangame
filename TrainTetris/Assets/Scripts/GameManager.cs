using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    [SerializeField] private TextMeshProUGUI stationText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private CanvasGroup pauseMenu;
    // Feel free to move functionality out if wanted
    [SerializeField] public Color[] stopColours;
    [SerializeField] public Color[] stopBorderColours;

    private List<Station> stations;
    private int stationNumber = 0;

    private float depTimer = 30;
    public int score = 0;
    public event Action nextStopEvent;
    public string spawnSide;

    private void Awake()
    {
        // Singleton
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        //stopColours = new Color[] {
        //    new Color(250f, 135f, 127f),
        //    new Color(242f, 203f, 124f),
        //    new Color(131f, 181f, 130f),
        //};
        // Colour to fill each person border by stop
        stopBorderColours = new Color[] {
            new Color(191f, 40f, 68f),
            new Color(230f, 151f, 117f),
            new Color(102f, 117f, 76f),
        };
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
        InvokeRepeating(nameof(UpdateTimer), 0, 1);
        // Colours to fill each person by stop

        stationText.text = stations[stationNumber].StationName;
    }

    void Update()
    {
        if (depTimer > 0)
        {
            depTimer -= Time.deltaTime;
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
        stationNumber++;
        stationText.text = stations[stationNumber].StationName;
        spawnSide = stations[stationNumber].EntryDirection;
        if (nextStopEvent != null)
        {
            nextStopEvent();
        }
    }

    private void UpdateTimer()
    {
        timerText.text = "00:" + Mathf.Ceil(depTimer).ToString("00");
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
