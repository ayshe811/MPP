using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class gameManager : MonoBehaviour
{
    public enum gameState { menu, playable, over }
    public gameState states;

    public float gameTimer, tabTimer;
    public GameObject tab;
    playerScript playScript;
    // Start is called before the first frame update
    void Start()
    {
        playScript = GameObject.Find("player").GetComponent<playerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        gameTimer += Time.deltaTime; // game timer

        tabTimer += Time.deltaTime;
        if (tabTimer > 15 && !playScript.tabShowed) tab.SetActive(true);
        else if (playScript.tabShowed)
        {
            tabTimer = 0;
            tab.SetActive(false);
        }

    }
}
