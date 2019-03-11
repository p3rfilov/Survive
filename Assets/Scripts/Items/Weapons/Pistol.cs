
public class Pistol : Weapon
{
    public Pistol()
    {
        ammo = 1000;
        maxDamage = 5;
        minDamage = 2;
        accuracyVariance = 1f;
        automatic = true;
        fireRate = 2f;
    }
}
