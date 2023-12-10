using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gamePlayPanel;
    public GameObject gameOverPanel;
    public GameObject gameWinPanel;
    public GameObject pausePanel;
    public int timeLimit;
    public GameObject timePanel;
    public TextMeshProUGUI timeText;
    private int enemyCount;
    public int score;

    public bool isGameActive = true;

    private void Awake()
    {
        instance = this;

        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    private void Start()
    {
        StartCoroutine(TimeCount());

        timePanel.SetActive(true);

        gamePlayPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        gameWinPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        PauseGame();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gamePlayPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        isGameActive = false;
    }

    private void GameWin()
    {
        gamePlayPanel.SetActive(false);
        gameWinPanel.SetActive(true);
        isGameActive = false;
    }

    IEnumerator TimeCount()
    {
        while (timeLimit > 0)
        {
            yield return new WaitForSeconds(1f);
            timeLimit--;

            timeText.text = timeLimit.ToString();

            if (timeLimit == 0)
            {
                GameOver();
            }
        }
    }

    public void EnemyKilled()
    {
        enemyCount--;
        if (enemyCount == 0)
        {
            GameWin();
        }
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGameActive)
            {
                Time.timeScale = 0;
                gamePlayPanel.SetActive(false);
                pausePanel.SetActive(true);
                isGameActive = false;
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void ResumeGame()
    {
        gamePlayPanel.SetActive(true);
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        isGameActive = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
