using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public enum gameState { menu, playable, over }
    public gameState states;

    public float gameTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameTimer += Time.deltaTime; // game timer
    }
}
