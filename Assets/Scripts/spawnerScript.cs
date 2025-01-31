using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class spawnerScript : MonoBehaviour
{
    [SerializeField] GameObject[] techPrefab;
    [SerializeField] float secondSpawm = 0.5f;
    [SerializeField] float minTras, maxTras;

    public bool hasRun; 

    playerScript playScript;
    // Start is called before the first frame update
    void Start()
    {
        playScript = GameObject.Find("player").GetComponent<playerScript>();
    }
    private void Update()
    {
        if (playScript.playState == playerScript.playerStates.distracted && !hasRun)
        {
            StartCoroutine(techSpawn());
            hasRun = true;
        }
        else if (playScript.playState == playerScript.playerStates.mindfulness && hasRun)
        {
            StopAllCoroutines();
            hasRun = false;
        }
    }

    IEnumerator techSpawn()
    {
        while (true)
        {
            var wanted = Random.Range(minTras, maxTras);
            var position = new Vector3(wanted, transform.position.y);
            GameObject gameObject = Instantiate(techPrefab[Random.Range(0, techPrefab.Length)],
                position, Quaternion.identity);
            yield return new WaitForSeconds(secondSpawm);
            Destroy(gameObject, 5f);
        }
    }
}
