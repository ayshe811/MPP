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
    public float spacing = 100, scale;
    public Color outlineColor;
    public Material nextMaterial;
    int index;
    private void Update()
    {
        index = collisionManager.objectsInSequence.Count;        
    }
    public void AnimateObject(GameObject obj)
    {
        RectTransform rt = obj.GetComponent<RectTransform>();
        Image img = obj.GetComponent<Image>();

        img.material = nextMaterial;
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
            if (rt != null) rt.localScale = new Vector3(scale, scale);
            DisableGameplayScripts(displayedPrefab);
        }
        AnimateGemAtIndex(0);
    }
    public void shift()
    {
        if (queueDisplayPanel.childCount <= 1) return;

        Transform firstGem = queueDisplayPanel.GetChild(0), lastGem = queueDisplayPanel.GetChild((index - 1));
        RectTransform firstGemRect = firstGem.GetComponent<RectTransform>(), lastGemRect = lastGem.GetComponent<RectTransform>();
        float moveDuration = 0.5f;

        Vector2 firstGemPosition = firstGemRect.anchoredPosition;
        for (int i = 1; i < queueDisplayPanel.childCount; i++) // skips the 1st index (0)
        {
            Transform gem = queueDisplayPanel.GetChild(i);
            Vector2 newPos = (i == 1) ? firstGemPosition : queueDisplayPanel.GetChild(i - 1).GetComponent<RectTransform>().anchoredPosition;
            // (i == 1) is the condition; if the condition is met (?) newPos = firstGemPosition; if the condition is false (:) newPos = position of the child before index. 

            LeanTween.move(gem.gameObject.GetComponent<RectTransform>(), newPos, moveDuration);
        }
        LeanTween.move(firstGemRect, lastGemRect.anchoredPosition, moveDuration);
    }
    public void FadeGemsByPosition()
    {
        int totalGems = queueDisplayPanel.childCount;

        for (int i = 0; i < totalGems; i++)
        {
            Transform gem = queueDisplayPanel.GetChild(i);
            GameObject gemObject = gem.gameObject;

            CanvasGroup canvasGroup = gemObject.GetComponent<CanvasGroup>();
            if (canvasGroup == null) canvasGroup = gemObject.AddComponent<CanvasGroup>();
            float normalizedIndex = (float)i / (totalGems - 1); // Leftmost = 0, Rightmost = 1
            float alphaValue = Mathf.Lerp(1f, 0.3f, normalizedIndex);
            LeanTween.alphaCanvas(canvasGroup, alphaValue, 0.5f).setEase(LeanTweenType.easeOutQuad);
        }
    }

    private void DisableGameplayScripts(GameObject obj)
    {
        Collider2D collider = obj.GetComponent<Collider2D>();
        if (collider != null) collider.enabled = false;

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = false;
    }

    private GameObject previousGem = null;
    private Vector3 originalSize = new Vector3(20, 20, 20);

    public void AnimateGemAtIndex(int index)
    {
        if (previousGem != null)
        {
            Image img = previousGem.GetComponent<Image>();
            img.material = null;
            LeanTween.cancel(previousGem);
            LeanTween.scale(previousGem, (originalSize), 0.3f).setEase(LeanTweenType.easeOutQuad);
        }

        if (index < queueDisplayPanel.childCount)
        {
            GameObject currentGem = queueDisplayPanel.GetChild(index).gameObject;
            LeanTween.cancel(currentGem);
            AnimateObject(currentGem);
            previousGem = currentGem;
            Debug.Log("previous gem " + previousGem);           
        }
    }
}
