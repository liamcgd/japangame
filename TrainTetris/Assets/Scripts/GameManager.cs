using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stationText;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private CanvasGroup pauseMenu;
    // Feel free to move functionality out if wanted
    [SerializeField] public static Color[] stopColours;
    [SerializeField] public static Color[] stopBorderColours;
    private List<Station> stations;
    private int stationNumber = 0;

    private float depTimer = 30;
    public static int score = 0;
    public static event Action nextStopEvent;

    void Start()
    {
        InvokeRepeating(nameof(UpdateTimer), 0, 1);
        // Colours to fill each person by stop
        stopColours = new Color[] {
            new Color(250f, 135f, 127f),
            new Color(242f, 203f, 124f),
            new Color(131f, 181f, 130f),
        };
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
            // Cancel input
            // Move to next station
            if (stationNumber >= stations.Count)
            {
                // Game Over
            }
            else
            {
                ChangeStation();
            }
        }
    }

    public void ChangeStation()
    {
        stationNumber++;
        stationText.text = stations[stationNumber].StationName;
        if (nextStopEvent != null)
        {
            nextStopEvent();
        }
    }

    private void UpdateTimer()
    {
        timerText.text = "00:" + Mathf.Ceil(depTimer).ToString();
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
