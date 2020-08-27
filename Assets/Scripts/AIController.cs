using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(ItemHolder))]
public class AIController : MonoBehaviour
{
    public float spotDistance = 10f;
    public float chaseDistance = 5f;
    public float attackDistance = 1f;
    public string enemyTag = "Player";

    bool heardPlayer;
    bool isChasing;
    Transform enemy;
    NavMeshAgent agent;
    Health health;
    ItemHolder itemHolder;

    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag(enemyTag)?.transform;
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
        itemHolder = GetComponent<ItemHolder>();
        EventManager.OnPlayerNoiseMade += ChasePlayer;
    }

    void ChasePlayer (float noiseLevel)
    {
        if (!heardPlayer)
        {
            spotDistance += noiseLevel;
            isChasing = true;
            heardPlayer = true;
        }
    }

    void Update()
    {
        // TODO: Implement a finite state machine
        if (health.health <= 0)
        {
            agent.enabled = false;
        }

        if (enemy != null && agent.enabled)
        {
            var threshold = spotDistance;
            var distance = Vector3.Distance(transform.position, enemy.position);

            if (isChasing)
                threshold += chaseDistance;

            if (distance < threshold)
            {
                if (distance > attackDistance)
                    agent.destination = enemy.position;
                else
                {
                    var enemyHealth = enemy.gameObject?.GetComponent<Health>();
                    if (enemyHealth != null && enemyHealth.health > 0)
                    {
                        var usable = itemHolder.Object?.GetComponent<IUsable>();
                        if (usable != null)
                        {
                            usable.Use();
                        }
                    }
                }
            }
            else
            {
                isChasing = false;
            }
        }
    }
}
