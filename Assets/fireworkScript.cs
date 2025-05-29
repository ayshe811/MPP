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

    public float baseSpawnChance = 0.1f; // 10% chance by default
    // Start is called before the first frame update

    public void TryTriggerFirework(int currentCombo)
    {
        // Calculate dynamic chance (e.g., 10% + 2% per combo)
        float spawnChance = baseSpawnChance + (currentCombo * comboScaleFactor);
        spawnChance = Mathf.Clamp(spawnChance, 0f, 0.5f); // Cap at 50%

        if (Random.value < spawnChance) SpawnFirework();
    }
    void SpawnFirework()
    {
        //Vector2 spawnPos = GetRandomSpawnPosition(); // Your existing method
        Instantiate(firework, new Vector3(Random.Range(-3, 3), player.transform.position.y + 4), Quaternion.identity);
    }
}
