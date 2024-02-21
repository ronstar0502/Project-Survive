using UnityEngine;

[CreateAssetMenu(fileName = "New Buff", menuName = "Buff", order = 1)]
public class Buff : ScriptableObject
{
    public string buffName;
    public string description;
    public BuffType type;
    //public BuffRarity rarity;
    public float value; //amount of health or damage increase
}

public enum BuffType {Health,Damage,MoveSpeed,AttackRate,HealthRegen}
//public enum BuffRarity {Common,Uncommon,Rare,Epic,Legendary}
