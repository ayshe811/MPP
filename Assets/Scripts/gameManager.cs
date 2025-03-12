using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class gameManager : MonoBehaviour
{
    public enum gameState { menu, playable, win, lose }
    public gameState states;
    public float gameTimer, tabTimer;
    playerScript playScript;
    public TMP_Text timerText;
    // Start is called before the first frame update
    void Start()
    {
        playScript = GameObject.Find("player").GetComponent<playerScript>();
        Application.targetFrameRate = 60;
        gameTimer = 300;

        states = gameState.playable;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameTimer > 300) gameTimer = 300;
        if (playScript.score >= 3) gameTimer -= Time.deltaTime;
        timerText.text = string.Format("{0:D2}:{1:D2}", (int)gameTimer / 60, (int)gameTimer % 60);

        if (gameTimer <= 0)
        {
            states = gameState.lose;
            gameTimer = 0;
        }
        else if (gameTimer > 0 && playScript.score == 25) states = gameState.win;

        if (states == gameState.win) SceneManager.LoadScene("Win Scene");
        if (states == gameState.lose) SceneManager.LoadScene("Lose Scene");
    }
}
