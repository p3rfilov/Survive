
public class Pistol : Weapon
{
    public Pistol()
    {
        int ammo = 1000;
        maxDamage = 5;
        minDamage = 2;
        accuracyVariance = 1f;
        automatic = true;
        fireRate = 2f;
    }

    public override void Use()
    {

    }
}
