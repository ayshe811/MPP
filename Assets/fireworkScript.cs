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
    void Start()
    {
        //StartCoroutine(SpawnFireworks());
    }
    //public void UpdateCombo(int combo)
    //{
    //    comboScript._currentCombo = combo; // Called from your ComboTracker
    //}

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
    //public IEnumerator SpawnFireworks()
    //{
    //    while (true)
    //    {
    //        float delay = Mathf.Lerp(maxSpawnDelay, minSpawnDelay, comboScript._currentCombo * comboScaleFactor);
    //        yield return new WaitForSeconds(delay);

    //        Vector3 spawnPos = new Vector3(
    //            Random.Range(-spawnArea, spawnArea),
    //            player.transform.position.y + 4
    //        );
    //        Instantiate(firework, spawnPos, Quaternion.identity);
    //    }
    //}
}
