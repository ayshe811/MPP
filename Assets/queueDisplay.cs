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

    private void AnimateFirstObject(GameObject obj)
    {
        RectTransform rt = obj.GetComponent<RectTransform>();

        float sizeUp = 1.2f;
        float beatSpeed = 0.5f;

        if (rt != null)
        {
            Debug.Log("UI Scaling: " + obj.name);
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

                if (index == 0)
                {
                    Debug.Log("Animating first object: " + displayedPrefab.name);
                    AnimateFirstObject(displayedPrefab);                
                }                
            }
            DisableGameplayScripts(displayedPrefab);
            index++;
        }
    }
    private void DisableGameplayScripts(GameObject obj)
    {
        Collider2D collider = obj.GetComponent<Collider2D>();
        if (collider != null) collider.enabled = false;

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = false;

        Animation anim = obj.GetComponent<Animation>();
        if (anim != null) anim.enabled = false;
    }
    public void DequeueAndUpdate()
    {
        if (collisionManager.queue.Count > 0)
        {
            collisionManager.queue.Dequeue(); 
            UpdateQueueDisplay();
        }
    }
}
