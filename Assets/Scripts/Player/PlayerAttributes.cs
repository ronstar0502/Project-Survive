

[System.Serializable]
public class PlayerAttributes
{
    public float maxHealthPoints = 2f;
    public float healthPoints = 2f;
    public float healthRegen = 0.5f;
    public float movementSpeed = 75f;
    public float dashForce = 40f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public float attackPower =1f;
    public float attackRate =0.5f;

    public void HealthRegen()
    {
        if (healthPoints < maxHealthPoints)
        {
            if(healthPoints+ healthRegen > maxHealthPoints)
            {
                healthPoints = maxHealthPoints;
            }
            else
            {
                healthPoints += healthRegen;
            }
        }
    }
    public void BuffAddHealth(float bonusHP)
    {
        maxHealthPoints += bonusHP;
        healthPoints += bonusHP;
    }
    public void BuffAddDamage(float bonusDamage)
    {
        attackPower += bonusDamage;
    }
    public void BuffAttackRate(float buffAttackRate)
    {
        attackRate -= buffAttackRate;
    }
    public void BuffMovementSpeed(float bonusMovementSpeed)
    {
        movementSpeed += bonusMovementSpeed;
    }
    public void BuffAddHealthRegen(float bonusHealthRegen)
    {
        healthRegen += bonusHealthRegen;
    }
}

