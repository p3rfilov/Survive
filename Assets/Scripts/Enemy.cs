using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 10f;
    public bool isAlive = true;

    void Start()
    {
        
    }

    void Update()
    {
        if (health <= 0f)
        {
            Kill();
        }
    }

    void Kill()
    {
        isAlive = false;
        transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
