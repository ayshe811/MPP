using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class spawnerScript : MonoBehaviour
{
    [SerializeField] GameObject[] techPrefab;
    [SerializeField] float secondSpawm = 1f;
    [SerializeField] float minTras, maxTras;

    public gameManager gameManager;

    public bool hasRun;

    private int currentTechLevel = 1;
    private float lastTimeTechLevelIncrease = 0;

    playerScript playScript;
    // Start is called before the first frame update
    void Start()
    {
       // StartCoroutine(techSpawn());
        playScript = GameObject.Find("player").GetComponent<playerScript>();
    }
    private void Update()
    {
        if (playScript.playState == playerScript.playerStates.distracted && !hasRun)
        {
            StartCoroutine(techSpawn());
            hasRun = true;
        }


        if (gameManager.gameTimer >= lastTimeTechLevelIncrease + 7)
        {
            currentTechLevel ++;
            lastTimeTechLevelIncrease = gameManager.gameTimer;
        }

        if (currentTechLevel > 3) currentTechLevel = 3;
    }

    IEnumerator techSpawn()
    {
        while (true)
        {
            var wanted = Random.Range(minTras, maxTras);
            var position = new Vector3(wanted, transform.position.y);
            GameObject gameObject = Instantiate(techPrefab[Random.Range(0, currentTechLevel)],
                position, Quaternion.identity);
            yield return new WaitForSeconds(secondSpawm);
            Destroy(gameObject, 5f);
        }
    }
}
