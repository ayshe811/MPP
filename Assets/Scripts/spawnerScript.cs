using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class spawnerScript : MonoBehaviour
{
    [SerializeField] GameObject[] techPrefab;
    public GameObject otherPrefab;
    [SerializeField] float secondSpawm;
    [SerializeField] float rangeMax;

    public gameManager gameManagerr; collisionManager collisionManager;
    public int currentTechLevel;
    private float lastTimeTechLevelIncrease = 0;
    public bool spawnNextInSequence;

    public GameObject player; 

    playerScript playScript;
    public string dummyNumber;
     float offset = 6.4f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(beforeGame());
        playScript = GameObject.Find("player").GetComponent<playerScript>();
        collisionManager = GameObject.Find("Collision Manager").GetComponent<collisionManager>();
    }
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, (player.transform.position.y + 10));
    }
    public IEnumerator techSpawn()
    {
        while (true)
        {
            //GameObject nextInSequenceIndex = collisionManager.objectsInSequence.Count > 0 ? 
            //    collisionManager.objectsInSequence[0] : 
            //    techPrefab[Random.Range(0, techPrefab.Length)];
            // ternary operator didn't work in this context

            int nextInSequenceIndex = 0;
            if (collisionManager.objectsInSequence.Count > 0 && collisionManager.currentIndex < collisionManager.objectsInSequence.Count) 
            {
                for (int i = 0; i < techPrefab.Length; i++)
                {
                    if (techPrefab[i].GetComponent<techieScript>().color == 
                        collisionManager.objectsInSequence[collisionManager.currentIndex].GetComponent<techieScript>().color)
                        // same condition applied when detecting correct collisions (see playerScript)
                    {
                        nextInSequenceIndex = i;
                        break;
                    }
                }
            }
            else nextInSequenceIndex = Random.Range(0, techPrefab.Length);

            spawnNextInSequence = Random.value < 0.3f; // 40% chance
            int selectedIndex = spawnNextInSequence ? 
                nextInSequenceIndex : 
                Random.Range(0, collisionManager.objectsInSequence.Count);

            var wanted = Random.Range((transform.position.x - rangeMax), (transform.position.x + rangeMax));
            var position = new Vector3(wanted, transform.position.y);
            GameObject gameObject = Instantiate(techPrefab[selectedIndex],
                position, Quaternion.identity);
            yield return new WaitForSeconds(secondSpawm);
            Destroy(gameObject, 5f);
        }
    }
    public IEnumerator otherSpawn()
    {
        while (true)
        {
            var wanted = Random.Range((transform.position.x - rangeMax), (transform.position.x + rangeMax));
            var position = new Vector3(wanted, transform.position.y);
            GameObject otherObject = Instantiate(otherPrefab, position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
            Destroy(otherObject, 5);
        }
    }
    public IEnumerator beforeGame()
    {
        while (true)
        {
            var wanted = Random.Range(-rangeMax, rangeMax);
            var position = new Vector3(wanted, transform.position.y);
            GameObject gameObject = Instantiate(techPrefab[currentTechLevel],
                position, Quaternion.identity);
            yield return new WaitForSeconds(5);
            Destroy(gameObject, 5);
        }
    }
}
