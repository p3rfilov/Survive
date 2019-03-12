
public class Pistol : Firearm
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
