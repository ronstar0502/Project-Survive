
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    PlayerController player;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private TextMeshProUGUI healthBarText;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float playerHP = player.playerAttributes.healthPoints;
        float playerMaxHP = player.playerAttributes.maxHealthPoints;
        float targetFillAmount = playerHP / playerMaxHP;
        healthBarFill.fillAmount = targetFillAmount;
        healthBarText.text = playerHP+"  /  "+ playerMaxHP;
    }
}
