using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class gameManager : MonoBehaviour
{
    public enum gameState { menu, onboadring, playable, win, lose }
    public gameState states;
    public float gameTimer, tabTimer;
    playerScript playScript;
    public collisionManager collisionManager;
    public TMP_Text timerText;
    public int level, playerLives;
    [SerializeField] int levelIndex;
    public GameObject dummy2, dummy3;
    // Start is called before the first frame update
    void Start()
    {
        playScript = GameObject.Find("player").GetComponent<playerScript>();
        Application.targetFrameRate = 60;
        gameTimer = 300;
        level = 1;
        playerLives = 5;

        states = gameState.onboadring;
        //dummy2.SetActive(false);
        //dummy3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) SceneManager.LoadScene("SampleScene");
        //if (states == gameState.playable)
        //{
        //    dummy2.SetActive(true);
        //    dummy3.SetActive(true);
        //}
        if (gameTimer > 300) gameTimer = 300;
        if (playScript.score >= 3) gameTimer -= Time.deltaTime;
        timerText.text = string.Format("{0:D2}:{1:D2}", (int)gameTimer / 60, (int)gameTimer % 60);

        //if (gameTimer <= 0)
        //{
        //    states = gameState.lose;
        //    gameTimer = 0;
        //}

        if (playerLives <= 0) states = gameState.lose;
        //else if (gameTimer > 0 && playScript.score >= targetLevel())
        //{
        //    playScript.score = 0;
        //    levelIndex++;
        //}
        if (states == gameState.win) SceneManager.LoadScene("Win Scene");
        if (states == gameState.lose) SceneManager.LoadScene("Lose Scene");

        int targetLevel()
        {
            switch (levelIndex)
            {
                case 0: return 3;
                case 1: return 4;
                case 2: return 5;
                case 3: return 6;
                case 4: return 7;
                case 5: return 8;
                default: return 8;
            }
        }
    }
}
