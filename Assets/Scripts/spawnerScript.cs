using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class spawnerScript : MonoBehaviour
{
    [SerializeField] GameObject[] techPrefab;
    public GameObject[] otherPrefab;
    public float secondSpawm, previousSpawn;
    [SerializeField] float rangeMax;

    public gameManager gameManagerr; collisionManager collisionManager; public comboScript comboScript;
    public int currentTechLevel;
    private float lastTimeTechLevelIncrease = 0;
    public bool spawnNextInSequence;

    public GameObject player; 

    playerScript playScript;
    public string dummyNumber;
    float offset = 6.4f;
    public int spawnValue;
    // Start is called before the first frame update
    void Start()
    {
      //  StartCoroutine(beforeGame());
        playScript = GameObject.Find("player").GetComponent<playerScript>();
        collisionManager = GameObject.Find("Collision Manager").GetComponent<collisionManager>();

        secondSpawm = .5f;
        spawnValue = 0;

        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, (player.transform.position.y + 10));
        if (comboScript._currentCombo == spawnValue + 5 && secondSpawm > .25f) { secondSpawm = secondSpawm - previousSpawn; spawnValue += 5; }
        else if (secondSpawm <= .25f) secondSpawm = .25f;

    //    if (currentTechLevel >= 3) StopCoroutine(gameManagerr.beforeRoutine);
    }
    public IEnumerator techSpawn()
    {
        if (gameManagerr.states == gameManager.gameState.playable)
        {
            while (true)
            {
                int nextInSequenceIndex = 0;
                if (collisionManager.objectsInSequence.Count > 0 && collisionManager.currentIndex < collisionManager.objectsInSequence.Count)
                {
                    for (int i = 0; i < collisionManager.objectsInSequence.Count; i++)
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
                else nextInSequenceIndex = Random.Range(0, collisionManager.objectsInSequence.Count);

                spawnNextInSequence = Random.value < 0.3f; // 30% chance
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
        else { }
    }
    public IEnumerator otherSpawn()
    {
        if (gameManagerr.states == gameManager.gameState.playable)
        {
            while (true)
            {
                int nextInSequenceIndex = 0;
                if (collisionManager.objectsInSequence.Count > 0 && collisionManager.currentIndex < collisionManager.objectsInSequence.Count)
                {
                    for (int i = 0; i < otherPrefab.Length; i++)
                    {
                        if (otherPrefab[i] == otherPrefab[2])
                        {
                            nextInSequenceIndex = i;
                            break;
                        }
                    }
                }
                bool spawnNextIndex = Random.value < 0f; // 100% chance
                int selectedIndex = spawnNextIndex ? nextInSequenceIndex : Random.Range(0, 2);

                var wanted = Random.Range((transform.position.x - 2), (transform.position.x + 2));
                var position = new Vector3(wanted, transform.position.y);
                GameObject otherObject = Instantiate(otherPrefab[selectedIndex], position, Quaternion.identity);
                yield return new WaitForSeconds(4);
                Destroy(otherObject, 5);
            }
        }
        else { }

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

    public IEnumerator redDistraction()
    {
        while (true)
        {
            var wanted = Random.Range(-rangeMax, rangeMax);
            var position = new Vector3(wanted, transform.position.y);
            GameObject gameObject = Instantiate(otherPrefab[0],
                position, Quaternion.identity);
            yield return new WaitForSeconds(5);
            Destroy(gameObject, 5);
        }
    }
}
