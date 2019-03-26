using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Health))]
public class AIController : MonoBehaviour
{
    public float spotDistance = 10f;
    public float chaseDistance = 5f;

    private float minimalDistance = 0.5f;
    private bool isChasing = false;
    private Transform player;
    private NavMeshAgent agent;
    private Health health;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
    }

    void Update()
    {
        // TODO: Implement a finate state machine
        if (health.health <= 0)
        {
            agent.enabled = false;
        }

        if (player != null && agent.enabled)
        {
            var threshold = spotDistance;
            var distance = Vector3.Distance(transform.position, player.position);

            if (isChasing)
                threshold += chaseDistance;

            if (distance < threshold)
            {
                if (distance > minimalDistance)
                    agent.destination = player.position;
            }
            else
            {
                isChasing = false;
            }
        }
    }
}
