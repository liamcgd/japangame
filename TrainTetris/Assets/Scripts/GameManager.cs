using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private CanvasGroup pauseMenu;

    private float depTimer = 30;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(UpdateTimer), 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (depTimer > 0)
        {
            depTimer -= Time.deltaTime;
        }
        else if (IsInvoking(nameof(UpdateTimer)))
        {
            CancelInvoke(nameof(UpdateTimer));
            timerText.text = "Departure: 0";
        }
    }

    private void UpdateTimer()
    {
        timerText.text = "Departure: " + Mathf.Ceil(depTimer).ToString();
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
}
