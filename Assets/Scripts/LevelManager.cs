using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private string levelName;
    public PlayerController player;
    public List<EnemySpawner> enemySpawners;
    private int enemyCount;

    public ExpLevel expBar;
    public GameObject buffSelectionUI;
    public int playerLevel = 1;
    public int currentExp = 0;
    public int expForNextLevel = 5;

    
    void Start()
    {
        Time.timeScale = 1;
        buffSelectionUI.SetActive(false);
        levelName = SceneManager.GetActiveScene().name;
        enemyCount = EnemyAmountToKill();
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        
        if(enemyCount == 0)
        {
            SceneManager.LoadScene("LevelSelectMenu");
        }
    }

    private int EnemyAmountToKill()
    {
        int enemyAmount = 0;
        for(int i = 0; i < enemySpawners.Count(); i++)
        {
            enemyAmount += enemySpawners[i].spawnAmount;
        }
        return enemyAmount;
    }

    public int GetEnemyCount()
    {
        return enemyCount;
    }
    public void SetEnemyCount()
    {
        enemyCount--;
    }

    public void AddEXP(int amount)
    {
        currentExp += amount;
        if (currentExp >= expForNextLevel)
        {
            int overflow = currentExp - expForNextLevel;
            OnLevelUp(overflow);
        }
        expBar.UpdateExpBar();
    }

    void OnLevelUp(int overflow)
    {
        playerLevel++;
        currentExp = overflow;
        expForNextLevel = CalculateXPForNextLevel(playerLevel);
        expBar.UpdateExpBar();
        buffSelectionUI.SetActive(true); 
    }

    int CalculateXPForNextLevel(int level)
    {
        return level * 10;
    }
}
