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

    public void TryTriggerFirework(Vector3 spawnPosition, int currentCombo)
    {
        comboScript._currentCombo = currentCombo;
        float spawnChance = baseSpawnChance + (currentCombo * comboScaleFactor);
        spawnChance = Mathf.Clamp(spawnChance, 0f, 0.5f); // Cap at 50%

        if (Random.value < spawnChance) Instantiate(firework, spawnPosition, Quaternion.identity);
    }
}
