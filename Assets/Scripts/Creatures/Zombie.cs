using UnityEngine;

public class Zombie : Creature
{
    private Transform player;

    public Zombie()
    {
        health = 10f;
    }

    protected override void Start()
    {
        player = FindObjectOfType<Player>().transform;
        base.Start();
    }

    protected override void Update()
    {

        base.Update();
    }
}
