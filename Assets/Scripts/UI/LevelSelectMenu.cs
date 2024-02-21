using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectMenu : MonoBehaviour
{
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadLevel1Scene()
    {
        SceneManager.LoadScene("GameLevel1");
    }
    public void LoadLevel2Scene()
    {
        SceneManager.LoadScene("GameLevel2");
    }
}
