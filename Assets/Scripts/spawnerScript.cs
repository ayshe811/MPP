using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class spawnerScript : MonoBehaviour
{
    [SerializeField] GameObject[] techPrefab;
    [SerializeField] float secondSpawm;
    [SerializeField] float rangeMax;

    public gameManager gameManagerr; collisionManager collisionManager;
    public int currentTechLevel;
    private float lastTimeTechLevelIncrease = 0;

    public GameObject player; 

    playerScript playScript;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(beforeGame());
        playScript = GameObject.Find("player").GetComponent<playerScript>();
        collisionManager = GameObject.Find("Collision Manager").GetComponent<collisionManager>();
    }
    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x, (player.transform.position.y + 10)); // player follows the spawner
    }
    public IEnumerator techSpawn()
    {
        while (true)
        {
            var wanted = Random.Range((transform.position.x - rangeMax), (transform.position.x + rangeMax));
            var position = new Vector3(wanted, transform.position.y);
            GameObject gameObject = Instantiate(techPrefab[Random.Range(0, collisionManager.objectsInSequence.Count)],
                position, Quaternion.identity);
            yield return new WaitForSeconds(secondSpawm);
            Destroy(gameObject, 5f);
        }
    }
    public IEnumerator beforeGame()
    {
        var wanted = Random.Range(-rangeMax, rangeMax);
        var position = new Vector3(wanted, transform.position.y);
        GameObject gameObject = Instantiate(techPrefab[currentTechLevel],
            position, Quaternion.identity);
        yield return null;
    }
}
