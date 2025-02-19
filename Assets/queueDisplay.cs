using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QueueDisplay : MonoBehaviour
{
    public Transform queueDisplayPanel;
    public collisionManager collisionManager; 
    public playerScript playerScript;
    public float spacing = 50;
    public Color outlineColor;

    public void AnimateObject(GameObject obj)
    {
        RectTransform rt = obj.GetComponent<RectTransform>();

        float sizeUp = 1.7f;
        float beatSpeed = 0.5f;

        if (rt != null)
        {
           LeanTween.scale(rt, rt.localScale * sizeUp, beatSpeed)
                .setEase(LeanTweenType.easeInOutSine)
                .setLoopPingPong();
        }
        else Debug.LogError("Missing RectTransform on " + obj.name);
    }

    public void UpdateQueueDisplay()
    {
        foreach (Transform child in queueDisplayPanel) Destroy(child.gameObject);
        int index = 0;
        foreach (GameObject obj in collisionManager.objectsInSequence)
        {
            GameObject displayedPrefab = Instantiate(obj, queueDisplayPanel);
            RectTransform rt = displayedPrefab.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchoredPosition = new Vector2(index * spacing, 0); 
                rt.localScale = new Vector3(9, 9);
            }
            DisableGameplayScripts(displayedPrefab);
            index++;
        }
        AnimateGemAtIndex(0);
    }
    private void DisableGameplayScripts(GameObject obj)
    {
        Collider2D collider = obj.GetComponent<Collider2D>();
        if (collider != null) collider.enabled = false;

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = false;
    }
    public void DequeueAndUpdate()
    {
        if (collisionManager.queue.Count > 0)
        {
            collisionManager.queue.Dequeue(); 
            UpdateQueueDisplay();
        }
    }
    private GameObject previousGem = null;
    private Vector3 originalSize = new Vector3(9, 9, 9);

    public void AnimateGemAtIndex(int index)
    {
        if (index < queueDisplayPanel.childCount)
        {
            if (previousGem != null)
            {
                LeanTween.cancel(previousGem); /*LeanTween.cancel(queueDisplayPanel.GetChild(0).gameObject);*/
                LeanTween.scale(previousGem, originalSize, 0.3f).setEase(LeanTweenType.easeOutQuad);
            }
          //  else AnimateObject(queueDisplayPanel.GetChild(0).gameObject); Debug.Log("no previous gem!");

            GameObject currentGem = queueDisplayPanel.GetChild(index).gameObject;
            AnimateObject(currentGem);
            previousGem = currentGem;
        }
    }
    public void OnShuffleCompleted()
    {
        previousGem = null;
      //  AnimateGemAtIndex(0);
    }
}
