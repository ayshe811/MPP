using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collisionManager : MonoBehaviour
{
    public List<GameObject> objectsInSequence;
    public List<GameObject> availableCrystals;
    [SerializeField] GameObject firework;
    public Queue<GameObject> queue;
    public gameManager gameManager;
    public int currentIndex;
    bool hasShuffled, hasCompleted;
    playerScript playScript;
    public QueueDisplay queueScript;
    int nextIndex;
    public audioScript audioScript;
    public int previousValue;
    comboScript comboScript;
   [SerializeField] int SequencesCompleted, previousNumber;
    bool fireworkSpawn;
    [SerializeField] float value;
    //  public spawnerScript spawnerScript;

    private void Start()
    {
        playScript = GameObject.Find("player").GetComponent<playerScript>();
        comboScript = GameObject.Find("COMBO").GetComponent<comboScript>();
        hasShuffled = false;
        queueScript.UpdateQueueDisplay();
        previousValue = 0;
        SequencesCompleted = 0;

        value = .3f;
    }

    private void Update()
    {
        queue = new Queue<GameObject>(objectsInSequence);

        if (currentIndex >= objectsInSequence.Count)
        {
            //Instantiate(firework, new Vector3(Random.Range(-3, 3), transform.position.y + 4), Quaternion.identity);

            if (SequencesCompleted == previousNumber + 5) { AddToSequence(); previousNumber = +5; }
            SequencesCompleted++;
            StartCoroutine(shuffle());
            queueScript.UpdateQueueDisplay();
            currentIndex = 0;
            playScript.correctCollision++;

            //fireworkSpawn = Random.value < value; // 30% chance
            //float hasSpawned = fireworkSpawn ? 

        }
    }
    public void AddToSequence()
   {
       if (nextIndex >= availableCrystals.Count) return;

       GameObject newCrystal = availableCrystals[nextIndex];
       objectsInSequence.Add(newCrystal);
       nextIndex++;
   }

    public IEnumerator shuffle()
    {
        for (int i = 0; i < objectsInSequence.Count; i++) // fisher-yates shuffle algorithm
        {
            int randomIndex = Random.Range(i, objectsInSequence.Count);
            GameObject temp = objectsInSequence[i];
            objectsInSequence[i] = objectsInSequence[randomIndex]; // randomises the count 
            objectsInSequence[randomIndex] = temp; // returns it to temp
        }
        yield return null;
    }
    public void OnCorrectCollision()
    {
        queue.Dequeue();
        currentIndex++;
        queueScript.AnimateGemAtIndex(currentIndex);
        queueScript.shift();
        //   queueScript.FadeGemsByPosition();

        if (!audioScript.src.isPlaying) { audioScript.src.Play(); audioScript.src.volume = .07f; }
        else if (audioScript.src.isPlaying && playScript.combo == previousValue + 5)
        {
            previousValue += 5;
            audioScript.src.volume += .05f;
        }
    }
    public void OnIncorrectCollision()
    {
        StartCoroutine(shuffle());
        queueScript.UpdateQueueDisplay();
        currentIndex = 0;
        queueScript.AnimateGemAtIndex(currentIndex);
    }
}
