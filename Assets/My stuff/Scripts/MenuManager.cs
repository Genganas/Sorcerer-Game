using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void GameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Exit()
    {
        Application.Quit(); 
    }
}
