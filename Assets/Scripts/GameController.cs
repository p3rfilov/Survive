using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class GameController : MonoBehaviour
{
    public SpawnZone[] enemySpawnZones;
    public SpawnZone[] playerSpawnZones;
    public float spawnRate = 1f;
    public int maxEnemies;
    public float difficultyIncreaseRate;
    public int addEnemyPerInterval;
    [Range(0, 200)] public int totalEnemyCap;
    [Range(0f, 100f)] public float itemDropChance;
    public AnimationCurve itemDropChanceCurve;

    public int EnemyCount { get; private set; }
    public int Score { get; private set; }

    Inventory inventory;
    GameObject player;
    float nextSpawn;
    float nextDifficultyIncrease;

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
        nextDifficultyIncrease = difficultyIncreaseRate;
        SpawnPlayer();
    }

    void Update ()
    {
        if (Time.time > difficultyIncreaseRate)
        {
            difficultyIncreaseRate = Time.time + difficultyIncreaseRate;
            maxEnemies = Mathf.Min(maxEnemies + addEnemyPerInterval, totalEnemyCap);
            EventManager.RaiseOnGameStatsChanged();
        }
        if (Time.time > nextSpawn)
        {
            GetRandomIventoryItemIndex();
            nextSpawn = Time.time + spawnRate;
            for (int i = 0; i < enemySpawnZones.Length; i++)   
            {
                if (EnemyCount < maxEnemies)
                {
                    if (enemySpawnZones[i].Spawn() != null)
                    {
                        EnemyCount++;
                        EventManager.RaiseOnGameStatsChanged();
                    }
                }
            }
        }
    }

    void DropItemAndDecrementEnemyCount (Transform obj)
    {
        if (obj != null)
        {
            if (obj.tag == "Enemy")
            {
                EnemyCount--;
                Score++;
                EventManager.RaiseOnGameStatsChanged();
            }
            Health health = obj.GetComponent<Health>();
            if (health != null && health.allowDrops && itemDropChance >= Random.Range(0f, 100f))
            {
                Item item = inventory.GetItem(GetRandomIventoryItemIndex());
                if (item != null)
                {
                    Instantiate(item, obj.position, Quaternion.identity).gameObject.SetActive(true);
                }
            }
        }
    }

    private int GetRandomIventoryItemIndex ()
    {
        float itemIndex = Mathf.Clamp(inventory.Size * itemDropChanceCurve.Evaluate(Random.value), 0, inventory.Size);
        return (int)itemIndex;
    }
}
