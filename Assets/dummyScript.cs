using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] bool isDummy;
    float offset = 6.4f;
    // Update is called once per frame
    void Update()
    {
        if (isDummy) transform.position = new Vector3(player.transform.position.x + offset, player.transform.position.y);
        else transform.position = new Vector3(player.transform.position.x - offset, player.transform.position.y);
    }
}
