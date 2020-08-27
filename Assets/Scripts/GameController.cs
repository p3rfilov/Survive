using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class GameController : MonoBehaviour
{
    public SpawnZone[] enemySpawnZones;
    public SpawnZone[] playerSpawnZones;
    public float spawnRate = 1f;
    public int maxEnemies;
    [Range(0f, 100f)] public float itemDropChance;
    public AnimationCurve itemDropChanceCurve;

    Inventory inventory;
    GameObject player;
    float nextSpawn;
    int enemyCount;

    public void SpawnPlayer ()
    {
        if (player == null)
        {
            player = playerSpawnZones[Random.Range(0, playerSpawnZones.Length)].Spawn();
        }
    }

    void Start ()
    {
        inventory = GetComponent<Inventory>();
        EventManager.OnSomethingDied += DropItemAndDecrementEnemyCount;
        SpawnPlayer();
    }

    void Update ()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            for (int i = 0; i < enemySpawnZones.Length; i++)   
            {
                if (enemyCount < maxEnemies)
                {
                    if (enemySpawnZones[i].Spawn() != null)
                    {
                        enemyCount++;
                        //print(enemyCount);
                    }
                }
            }
        }
    }

    void DropItemAndDecrementEnemyCount (Transform obj)
    {
        if (obj != null && obj.gameObject.activeSelf && obj.gameObject != player)
        {
            enemyCount--;
            if (itemDropChance >= Random.Range(0f, 100f))
            {
                float itemChanceIndex = (inventory.Size - 1) * Mathf.Clamp(itemDropChanceCurve.Evaluate(Random.value), 0f, 1f);
                int itemIndex = Random.Range(0, (int)itemChanceIndex);
                Item item = inventory.GetItem(itemIndex);
                if (item != null)
                {
                    Instantiate(inventory.GetItem(itemIndex), obj.position, obj.rotation).gameObject.SetActive(true);
                }
            }
        }
    }
}
