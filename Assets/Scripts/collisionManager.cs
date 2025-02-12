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
    // private int currentIndex = 0;

    private void Start()
    {
        playScript = GameObject.Find("player").GetComponent<playerScript>();
        hasShuffled = false;

        StartCoroutine(shuffle());
    }

    private void Update()
    {
        queue = new Queue<GameObject>(objectsInSequence);
        //if (playScript.playState == playerScript.playerStates.mindfulness && !hasShuffled)
        //{
        //    StartCoroutine(shuffle());
        //    hasShuffled = true;
        //}
        //if (playScript.playState == playerScript.playerStates.distracted && hasShuffled)
        //{
        //    StopCoroutine(shuffle());
        //    hasShuffled = false;
        //}

        if (currentIndex >= 3)
        {
            currentIndex = 0;
            StartCoroutine(shuffle());
            playScript.correctCollision++;
        }
    }

    IEnumerator shuffle()
    {
        List<GameObject> previousOrder = new List<GameObject>(objectsInSequence);

        do
        {
            for (int i = 0; i < objectsInSequence.Count; i++) // fisher-yates shuffle algorithm
            {
                int randomIndex = Random.Range(i, objectsInSequence.Count);
                GameObject temp = objectsInSequence[i];
                objectsInSequence[i] = objectsInSequence[randomIndex];
                objectsInSequence[randomIndex] = temp;
                yield return temp;
            }
        }
        while (IsSameOrder(objectsInSequence, previousOrder)) ; 
    }
    private bool IsSameOrder(List<GameObject> list1, List<GameObject> list2)
    {
        for (int i = 0; i < list1.Count; i++)
        {
            if (list1[i] != list2[i]) return false;
        }
        return true;
    }
}
