using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
      //  StartCoroutine(shuffle());

        // shufflee();
        //  queue = new Queue<GameObject>(objectsInSequence);
    }

    private void Update()
    {
        queue = new Queue<GameObject>(objectsInSequence);
        if (playScript.playState == playerScript.playerStates.mindfulness && !hasShuffled)
        {
            StartCoroutine(shuffle());
            hasShuffled = true;
        }
        else if (playScript.playState == playerScript.playerStates.distracted && hasShuffled)
        {
            StopCoroutine(shuffle());
            hasShuffled = false;
        }
    }

    IEnumerator shuffle()
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

    //public void shufflee()
    //{
    //    for (int i = 0; i < objectsInSequence.Count; i++) // fisher-yates shuffle algorithm
    //    {
    //        int randomIndex = Random.Range(i, objectsInSequence.Count);
    //        GameObject temp = objectsInSequence[i];
    //        objectsInSequence[i] = objectsInSequence[randomIndex];
    //        objectsInSequence[randomIndex] = temp;
    //    }
    //}
}
