using UnityEngine;

public class DamageCalculator : MonoBehaviour
{
    public int minDamage;
    public int maxDamage;

    public int CalculateRandomDamage()
    {
        int damage = Random.Range(minDamage, maxDamage);
        return damage;
    }

    public int MinDamage() { return minDamage; }

    public int MaxDamage() { return maxDamage; }
}
