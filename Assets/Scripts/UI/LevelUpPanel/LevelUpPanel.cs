using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelUpPanel : MonoBehaviour
{
    [SerializeField]private Button buffButton1;
    [SerializeField] private TextMeshProUGUI buff1Text;
    [SerializeField]private Button buffButton2;
    [SerializeField] private TextMeshProUGUI buff2Text;
    [SerializeField]private Button buffButton3;
    [SerializeField] private TextMeshProUGUI buff3Text;

    public List<Buff> buffs;
    private List<Buff> selectedBuffs = new List<Buff>();

    private PlayerController player;
    private void OnEnable()
    {
        Time.timeScale = 0;
        player = FindObjectOfType<PlayerController>();
        InitBuffPanel();
    }
    private void InitBuffPanel()
    {
        SelectRandomBuff();
        SetBuffsTextDetails();
        SetBuffsButtonsOnClickEvent();
    }

    void SelectRandomBuff()
    {
        selectedBuffs.Clear();
        List<Buff> buffList = new List<Buff>(buffs); // Deep copy of the list
        int buffsSelected = 0;

        while (buffsSelected < 3 && buffList.Count > 0)
        {
            int randomIndex = Random.Range(0, buffList.Count);
            selectedBuffs.Add(buffList[randomIndex]);
            buffList.RemoveAt(randomIndex);
            buffsSelected++;
        }
    }

    void SetBuffsTextDetails()
    {
        buff1Text.text = GetTextBuffDetails(selectedBuffs[0]);
        buff2Text.text = GetTextBuffDetails(selectedBuffs[1]);
        buff3Text.text = GetTextBuffDetails(selectedBuffs[2]);
    }

    void SetBuffsButtonsOnClickEvent()
    {
        buffButton1.GetComponent<Button>().onClick.AddListener(() => ApplyBuff(selectedBuffs[0]));
        buffButton2.GetComponent<Button>().onClick.AddListener(() => ApplyBuff(selectedBuffs[1]));
        buffButton3.GetComponent<Button>().onClick.AddListener(() => ApplyBuff(selectedBuffs[2]));
    }

    private void ApplyBuff(Buff buff)
    {
        switch (buff.type)
        {
            case BuffType.Health:
                player.playerAttributes.BuffAddHealth(buff.value);
                break;
            case BuffType.Damage:
                player.playerAttributes.BuffAddDamage(buff.value);
                break;
            case BuffType.AttackRate:
                player.playerAttributes.BuffAttackRate(buff.value);
                break;
            case BuffType.MoveSpeed:
                player.playerAttributes.BuffMovementSpeed(buff.value);
                break;
            case BuffType.HealthRegen:
                player.playerAttributes.BuffAddHealthRegen(buff.value);
                break;
        }
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    string GetTextBuffDetails(Buff buff)
    {
        string details = "Name: " + buff.buffName
                          + "\nDescription:  " + buff.description;
        return details;
    }
}
