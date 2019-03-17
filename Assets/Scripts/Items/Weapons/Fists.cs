
public class Fists : Weapon
{
    public Fists()
    {
        maxDamage = 2;
        minDamage = 1;
        force = 3f;
        effectiveDistance = 0.5f;
        automatic = true;
        fireRate = 2f;
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
