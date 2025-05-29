using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireworkScript : MonoBehaviour
{
    [SerializeField] GameObject player, firework;
    [SerializeField] spawnerScript spawner;
    [SerializeField] comboScript comboScript;

    [SerializeField] int spawnArea;
    public float minSpawnDelay = 3f, maxSpawnDelay = 8f;
    public float comboScaleFactor = 0.1f; // Increase spawn rate with combos
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnFireworks());
    }
    //public void UpdateCombo(int combo)
    //{
    //    comboScript._currentCombo = combo; // Called from your ComboTracker
    //}

    public IEnumerator SpawnFireworks()
    {
        while (true)
        {
            float delay = Mathf.Lerp(maxSpawnDelay, minSpawnDelay, comboScript._currentCombo * comboScaleFactor);
            yield return new WaitForSeconds(delay);

            Vector3 spawnPos = new Vector3(
                Random.Range(-spawnArea, spawnArea),
                player.transform.position.y + 4
            );
            Instantiate(firework, spawnPos, Quaternion.identity);
        }
    }
}
