using System.Collections.Generic;
using UnityEngine;

public class QueueDisplay : MonoBehaviour
{
    public Transform queueDisplayPanel;
    public collisionManager collisionManager; 
    public playerScript playerScript;
    public float spacing = 50; 

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
                rt.localScale = new Vector3(5, 5, 5);
            }
            else
            {
                displayedPrefab.transform.localPosition = new Vector3(index * spacing, 0, 0);
                displayedPrefab.transform.localScale = new Vector3(5, 5, 5);
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
    }

    void Start()
    {
        UpdateQueueDisplay();
    }
    private void Update()
    {
        if (playerScript.playState == playerScript.playerStates.mindfulness) UpdateQueueDisplay();
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
