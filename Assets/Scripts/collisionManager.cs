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
    // private int currentIndex = 0;

    private void Start()
    {
        playScript = GameObject.Find("player").GetComponent<playerScript>();
        hasShuffled = false;

        StartCoroutine(shuffle());
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
        List<GameObject> previousOrder = new List<GameObject>(objectsInSequence);

        do // do/while loop exencutes a block of code once before checking its condition; will then continue to execute as long as the condition is met.
        {
            for (int i = 0; i < objectsInSequence.Count; i++) // fisher-yates shuffle algorithm
            {
                int randomIndex = Random.Range(i, objectsInSequence.Count);
                GameObject temp = objectsInSequence[i];
                objectsInSequence[i] = objectsInSequence[randomIndex]; // randomises the count 
                objectsInSequence[randomIndex] = temp; // returns it to temp
            }
        }
        while (IsSameOrder(objectsInSequence, previousOrder)) ;
        yield return null;
    }
    private bool IsSameOrder(List<GameObject> list1, List<GameObject> list2)
    {
        for (int i = 0; i < list1.Count; i++)
        {
            if (list1[i] != list2[i]) return false;
        }
        return true;
    }

    public void OnCorrectCollision()
    {
        queue.Dequeue();
        currentIndex++;
        queueScript.AnimateGemAtIndex(currentIndex);
        queueScript.shift();
    }
}
