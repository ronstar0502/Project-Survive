using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject projectile;
    public float projectileSpeed = 2f;
    private float damage;

    public float GetDamage()
    {
        return damage;
    }
    public void SetDamage(float attackPower)
    {
        damage = attackPower;
    }

    public float GetProjectile()
    {
        return projectileSpeed;
    }
}
