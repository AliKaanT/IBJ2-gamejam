using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

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
    public int enemyCount;
    public int score;

    public bool isGameActive = true;

    public Image watch;

    private List<RecordedState> recordedStates = new List<RecordedState>();
    private Rigidbody2D rb;
    [SerializeField] private float _shakeDuration = 1f;
    [SerializeField] private float _shakeStrength = 1f;
    [SerializeField] private int _shakeVibrato = 10;



    [SerializeField] private GameObject player;
    private bool rewinding = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
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
        if (rewinding == false)
        {
            RecordState();
        }
        else
        {
            RewindTime();
        }
    }

    public void GameOver()
    {
        rewinding = true;

        gamePlayPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        isGameActive = false;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }
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
                if (watch != null)
                {
                    watch.transform.DORotate(new Vector3(0, 0, 180), 1f, RotateMode.FastBeyond360).SetEase(Ease.Linear);
                }
            }
        }
    }

    public void EnemyKilled()
    {
        enemyCount--;
        Debug.Log(enemyCount);

        if (enemyCount == 0)
        {
            GameWin();
        }
    }

    public void PauseGame()
    {
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     if (isGameActive)
        //     {
        //         Time.timeScale = 0;
        //         gamePlayPanel.SetActive(false);
        //         pausePanel.SetActive(true);
        //         isGameActive = false;
        //     }
        //     else
        //     {
        //         ResumeGame();
        //     }
        // }
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

    private void RewindTime()
    {
        if (recordedStates.Count > 0)
        {
            Debug.Log(recordedStates.Count);

            RecordedState recordedState = recordedStates[0];
            player.transform.position = recordedState.position;
            rb.velocity = recordedState.velocity;
            player.transform.rotation = recordedState.rotation;

            for (int i = 0; i < 7; i++)
            {
                if (recordedStates.Count > 0)
                    recordedStates.RemoveAt(0);
            }
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    private void RecordState()
    {
        recordedStates.Insert(0, new RecordedState(player.transform.position, rb.velocity, player.transform.rotation, Time.time));
    }
}

