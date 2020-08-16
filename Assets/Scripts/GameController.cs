using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SpawnZone[] spawnZones;
    public float spawnRate = 1f;

    private float nextSpawn;


    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            for (int i = 0; i < spawnZones.Length; i++)   
            {
                spawnZones[i].Spawn();
            }
        }
    }
}
