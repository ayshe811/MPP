using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class QueueDisplay : MonoBehaviour
{
    public Transform queueDisplayPanel;
    public collisionManager collisionManager; 
    public playerScript playerScript;
    public float spacing = 100;
    public Color outlineColor;

    private void Start()
    {
      //  OnShuffleCompleted();
    }
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
    }
    public void UpdateQueueDisplay()
    {
        for (int i = queueDisplayPanel.childCount - 1; i >= 0; i--) DestroyImmediate(queueDisplayPanel.GetChild(i).gameObject);
        foreach (GameObject obj in collisionManager.objectsInSequence)
        {
            GameObject displayedPrefab = Instantiate(obj, queueDisplayPanel);
            RectTransform rt = displayedPrefab.GetComponent<RectTransform>();
            if (rt != null) rt.localScale = new Vector3(9, 9);
            DisableGameplayScripts(displayedPrefab);
        }
       // AnimateGemAtIndex(0);
    }
    public void shift()
    {
        if (queueDisplayPanel.childCount <= 1) return;

        Transform firstGem = queueDisplayPanel.GetChild(0), lastGem = queueDisplayPanel.GetChild(2);
        RectTransform firstGemRect = firstGem.GetComponent<RectTransform>(), lastGemRect = lastGem.GetComponent<RectTransform>();
        float moveDuration = 0.5f;

        Vector2 firstGemPosition = firstGemRect.anchoredPosition;

        for (int i = 1; i < queueDisplayPanel.childCount; i++) // objects > 1
        {
            Transform gem = queueDisplayPanel.GetChild(i);
            float currentY = gem.gameObject.GetComponent<RectTransform>().anchoredPosition.y;
            Vector2 newPos = (i == 1) ? firstGemPosition : queueDisplayPanel.GetChild(i - 1).GetComponent<RectTransform>().anchoredPosition;
            LeanTween.move(gem.gameObject.GetComponent<RectTransform>(), newPos, moveDuration);
        }
        LeanTween.move(firstGemRect, lastGemRect.anchoredPosition, moveDuration);
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

    //public void AnimateGemAtIndex(int index)
    //{
    //    if (index < queueDisplayPanel.childCount)
    //    {
    //        if (previousGem != null)
    //        {
    //            LeanTween.cancel(previousGem);
    //            LeanTween.scale(previousGem, originalSize, 0.3f).setEase(LeanTweenType.easeOutQuad);
    //        }
    //        GameObject currentGem = queueDisplayPanel.GetChild(index).gameObject;
    //        LeanTween.cancel(currentGem);
    //        AnimateObject(currentGem);
    //        previousGem = currentGem;
    //        Debug.Log("previous gem " + previousGem);
    //    }
    //}
    //public void OnShuffleCompleted()
    //{
    //    previousGem = null;
    //}
}
