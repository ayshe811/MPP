using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collisionManager : MonoBehaviour
{
    public List<GameObject> objectsInSequence;
    public Queue<GameObject> queue;
    public int currentIndex;
    bool hasShuffled;
    playerScript playScript;
    public QueueDisplay queueScript;

    private void Start()
    {
        playScript = GameObject.Find("player").GetComponent<playerScript>();
        hasShuffled = false;
        queueScript.UpdateQueueDisplay();
    }

    private void Update()
    {
        queue = new Queue<GameObject>(objectsInSequence);

        if (currentIndex >= 3)
        {
            StartCoroutine(shuffle());
            queueScript.UpdateQueueDisplay();
            queueScript.OnShuffleCompleted();
            currentIndex = 0;
            playScript.correctCollision++;
        }
    }

    IEnumerator shuffle()
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
    }
}
