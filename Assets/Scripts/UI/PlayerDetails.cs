using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDetails : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerDetailsText;
    PlayerController player;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
    void Update()
    {
        UpdatePlayerDetails();
    }

    private void UpdatePlayerDetails()
    {
        float coolDownTime=player.GetCooldownTime();
        string coolDownText;
        if (coolDownTime>0)
        {
            coolDownText = "Dash Cooldown: " +coolDownTime.ToString("F1")+" s";
        }
        else
        {
            coolDownText = "Dash Cooldown: Ready!";
        }
        string details = "Attack Damage: "+player.playerAttributes.attackPower
                                          +"\nAttack Rate:  " + player.playerAttributes.attackRate
                                          + "\nHealth Regen: "+player.playerAttributes.healthRegen+" per/s"
                                          +"\n"+coolDownText;
        playerDetailsText.text = details;
    }
}
