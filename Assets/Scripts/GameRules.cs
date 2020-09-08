using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Inventory))]
public class GameRules : MonoBehaviour
{
    public SpawnZone[] enemySpawnZones;
    public SpawnZone[] playerSpawnZones;
    public float spawnInterval = 1f;
    public int startEnemyCount;
    public float difficultyIncreaseInterval;
    public int addEnemyPerInterval;
    [Range(0, 200)] public int totalEnemyCap;
    [Range(0f, 100f)] public float itemDropChance;
    public AnimationCurve itemDropChanceCurve;

    public bool bulletTime = true;
    public bool onExplosion = true;
    [Range(0f, 1f)] public float slowDownValue = 0.35f;
    [Range(0f, 3f)] public float bulletTimedDration = 0.5f;

    public int EnemyCount { get; private set; }
    public int Score { get; private set; }

    float defaultTimeScale;
    float defaultFixetTimeScale;

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

    public void RunBulletTime (GameObject obj)
    {
        if (bulletTime)
        {
            if (onExplosion)
            {
                Exploder exploder = obj.GetComponent<Exploder>();
                if (exploder != null)
                {
                    StartCoroutine(BulletTime());
                }
            }
        }
    }

    void Awake ()
    {
        EventManager.OnSomethingDied += DropItemAndDecrementEnemyCount;
        EventManager.OnGameRulesChanged += ChangeGameRules;
        EventManager.OnObjectAboutToBeDestroyed += RunBulletTime;
        defaultTimeScale = Time.timeScale;
        defaultFixetTimeScale = Time.fixedDeltaTime;
    }

    void Start ()
    {
        inventory = GetComponent<Inventory>();
        nextDifficultyIncrease = difficultyIncreaseInterval;
        SpawnPlayer();
    }

    void Update ()
    {
        if (Time.time > difficultyIncreaseInterval)
        {
            difficultyIncreaseInterval = Time.time + difficultyIncreaseInterval;
            startEnemyCount = Mathf.Min(startEnemyCount + addEnemyPerInterval, totalEnemyCap);
            EventManager.RaiseOnGameStatsChanged();
        }
        if (Time.time > nextSpawn)
        {
            GetRandomIventoryItemIndex();
            nextSpawn = Time.time + spawnInterval;
            for (int i = 0; i < enemySpawnZones.Length; i++)   
            {
                if (EnemyCount < startEnemyCount)
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

    int GetRandomIventoryItemIndex ()
    {
        float itemIndex = Mathf.Clamp(inventory.Size * itemDropChanceCurve.Evaluate(Random.value), 0, inventory.Size);
        return (int)itemIndex;
    }

    void ChangeGameRules(int startCount, float spawnInterval, float increaseInterval, int increaseCount, float itemDropChance, bool bulletTime)
    {
        startEnemyCount = startCount;
        this.spawnInterval = spawnInterval;
        difficultyIncreaseInterval = increaseInterval;
        addEnemyPerInterval = increaseCount;
        this.itemDropChance = itemDropChance;
        this.bulletTime = bulletTime;
    }

    IEnumerator BulletTime ()
    {
        Time.timeScale = slowDownValue;
        Time.fixedDeltaTime = defaultFixetTimeScale * slowDownValue;
        yield return new WaitForSeconds(bulletTimedDration);
        Time.timeScale = defaultTimeScale;
        Time.fixedDeltaTime = defaultFixetTimeScale;
    }
}
