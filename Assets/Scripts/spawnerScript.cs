using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerScript : MonoBehaviour
{
    [SerializeField] GameObject[] techPrefab;
    [SerializeField] float secondSpawm = 0.5f;
    [SerializeField] float minTras, maxTras;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(techSpawn());
    }

    IEnumerator techSpawn()
    {
        while (true)
        {
            var wanted = Random.Range(minTras, maxTras);
            var position = new Vector3(wanted, transform.position.y);
            GameObject gameObject = Instantiate(techPrefab[Random.Range(0, techPrefab.Length)],
                position, Quaternion.identity);
            yield return new WaitForSeconds(secondSpawm);
            Destroy(gameObject, 5f);
        }
    }
}
