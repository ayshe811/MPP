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
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI pauseText, livesText;
    bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        playScript = GameObject.Find("player").GetComponent<playerScript>();
        src = GetComponent<AudioSource>();
        Application.targetFrameRate = 60;
        gameTimer = 300;
        level = 1;
        playerLives = 3;
        src.volume = .6f;

        states = gameState.playable;
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) TogglePauseState();

        if (playerLives <= 0) states = gameState.fin;
        if (states == gameState.fin) SceneManager.LoadScene("Lose Scene");
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
    public void QuitApplication()
    {
        Application.Quit();
    }
}
