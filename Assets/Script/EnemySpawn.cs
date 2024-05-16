using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] Transform spawner;
    [SerializeField] GameObject enemy;
    [SerializeField] float spawnMultiplier = 5;
    float timer;
    Gun gun;

    void Start ()
    {
        gun = GameObject.FindGameObjectWithTag("Player").GetComponent<Gun>();
    }
 
    void Update () 
    {
        timer += Time.deltaTime;
        if (timer > spawnMultiplier && !gun.playerDead)
        {
            Instantiate(enemy, spawner);
            timer = 0f;
            if (spawnMultiplier > 0.6f) spawnMultiplier -= 0.2f;
        }
    }
}
