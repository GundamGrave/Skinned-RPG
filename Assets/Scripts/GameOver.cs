using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private const string MainMenu = "Main Menu";
    public static bool GameOverScreen = false;

    public GameObject gameOverUI;

    void Update()
    {
        if (GameOverScreen)
        {
            Pause();
        }
    }

    void Pause()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        GameOverScreen = true;
    }

    public void RestartGame()
    {
        Debug.Log("Restart Game");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 001");
    }

    public void LoadMenu()
    {
        Debug.Log("Load Menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
    public void QuitGame()
    {
        Debug.Log("quit game...");
        Application.Quit();
    }
}
