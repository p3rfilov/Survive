using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SpawnZone[] enemySpawnZones;
    public SpawnZone[] playerSpawnZones;
    public float spawnRate = 1f;

    private GameObject player;
    private float nextSpawn;

    public void SpawnPlayer ()
    {
        if (player == null)
        {
            player = playerSpawnZones[Random.Range(0, playerSpawnZones.Length)].Spawn();
        }
    }

    void Start ()
    {
        SpawnPlayer();
    }

    void Update ()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            for (int i = 0; i < enemySpawnZones.Length; i++)   
            {
                enemySpawnZones[i].Spawn();
            }
        }
    }
}
