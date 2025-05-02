using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class gameManager : MonoBehaviour
{
    public enum gameState { menu, onboadring, playable, pause, fin }
    public gameState states;
    public float gameTimer, tabTimer;
    playerScript playScript;
    public collisionManager collisionManager;
    public TMP_Text timerText;
    public int level, playerLives;
    [SerializeField] int levelIndex;
    public GameObject dummy2, dummy3;
    public AudioSource src;
    [SerializeField] GameObject panel, startPanel, control, lives, topRight, endPanel;
    [SerializeField] TextMeshProUGUI pauseText, livesText;
    spawnerScript spawner;
    bool isPaused;
    public Coroutine beforeRoutine;
    // Start is called before the fi
    // rst frame update
    bool hasStarted;
    void Start()
    {
        playScript = GameObject.Find("player").GetComponent<playerScript>();
        spawner = GameObject.Find("dummy1").GetComponent<spawnerScript>();
        src = GetComponent<AudioSource>();
        Application.targetFrameRate = 60;
        gameTimer = 300;
        level = 1;
        playerLives = 3;
        src.volume = .6f;

        states = gameState.menu;
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) TogglePauseState();
        if (states == gameState.menu) Time.timeScale = 1;
        if (Input.GetKeyDown(KeyCode.Space) && states == gameState.menu) 
        {
            states = gameState.playable; startPanel.SetActive(false);
            control.SetActive(true); lives.SetActive(true); topRight.SetActive(true);
            if (!hasStarted)
            {
                StartCoroutine(spawner.beforeGame());
                hasStarted = true;
            }
        }

        if (playerLives <= 0) states = gameState.fin;
       // if (states == gameState.fin) SceneManager.LoadScene("Lose Scene");
       if (states == gameState.fin)
        {
            endPanel.SetActive(true);
            Time.timeScale = 0;
            if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene("SampleScene");
            if (Input.GetKeyDown(KeyCode.Q)) Application.Quit();
        }
        if (states == gameState.pause) pauseText.text = "'ESC' Escape";
        if (states == gameState.playable) pauseText.text = "'ESC' Pause";

        livesText.text = $"{playerLives}";
    }
    public void TogglePauseState()
    {
        isPaused = !isPaused;
        states = isPaused ? gameState.pause : gameState.playable;

        Time.timeScale = isPaused ? 0 : 1;
        panel.SetActive(isPaused);
    }
    public void mainMenu()
    {
        states = gameState.menu;
        SceneManager.LoadScene("SampleScene");
    }
    public void QuitApplication()
    {
        Application.Quit();
    }
}
