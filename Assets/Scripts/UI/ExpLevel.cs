
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpLevel : MonoBehaviour
{
    LevelManager levelManager;
    [SerializeField] private Image ExpBarFill;
    [SerializeField] private TextMeshProUGUI ExpText;
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        levelManager.expBar = this;

        UpdateExpBar();
    }
    public void UpdateExpBar()
    {
        float playerExp = levelManager.currentExp;
        float playerNextLvlExp = levelManager.expForNextLevel;
        float targetFillAmount = playerExp / playerNextLvlExp;
        ExpBarFill.fillAmount = targetFillAmount;
        string text = "  "+playerExp + "   |   " + playerNextLvlExp+"          Level: "+levelManager.playerLevel;
        ExpText.text = text;
    }

}
