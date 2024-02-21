using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    private LevelManager levelManager;
    [SerializeField]private TextMeshProUGUI enemyCountText;
    private int enemyCount;
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        enemyCount = levelManager.GetEnemyCount();
    }

    void Update()
    {
        UpdateEnemyCount();
    }

    private void UpdateEnemyCount()
    {
        enemyCount = levelManager.GetEnemyCount();
        enemyCountText.text = "Enemy Remaining: "+enemyCount.ToString();
    }

}
