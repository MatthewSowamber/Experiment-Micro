using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;

public class GameUI : MonoBehaviour
{
    public enum GameState { MainMenu, Paused, Playing, GameOver, GameClear};
    public GameState currentState;
    public TextMeshProUGUI lifeText;
    public GameObject allGameUI, mainMenuPanel, pauseMenuPanel, gameOverPanel, titleText, GameClearedPanel;
    // Start is called before the first frame update
    void Awake()
    {
        if(SceneManager.GetActiveScene().name =="Main Menu")
        {
            CheckGameState(GameState.MainMenu);
        }
        else
        {
            CheckGameState(GameState.Playing);
        }
    }

    public void CheckGameState(GameState newGameState)
    {
        currentState = newGameState;
        switch (currentState)
        {
            case GameState.MainMenu:
                MainMenuSetup();
                break;
            case GameState.Paused:
                gamePaused();
                Time.timeScale = 0f;
                Manager.gamePaused = true;
                break;
            case GameState.Playing:
                GameActive();
                Time.timeScale = 1f;
                Manager.gamePaused = false;
                break;
            case GameState.GameOver:
                GameOver();
                Time.timeScale = 0f;
                Manager.gamePaused = true;
                break;
            case GameState.GameClear:
                GameClear();
                Time.timeScale = 0f;
                Manager.gamePaused = true;
                break;
        }
    }

    public void MainMenuSetup()
    {
        allGameUI.SetActive(false);
        mainMenuPanel.SetActive(true);
        pauseMenuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        titleText.SetActive(true);
        GameClearedPanel.SetActive(false);
    }

    public void GameActive()
    {
        allGameUI.SetActive(true);
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        titleText.SetActive(false);
        GameClearedPanel.SetActive(false);
    }

    public void gamePaused()
    {
        allGameUI.SetActive(true);
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        titleText.SetActive(false);
        GameClearedPanel.SetActive(false);
    }

    public void GameOver()
    {
        allGameUI.SetActive(true);
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        titleText.SetActive(true);
        GameClearedPanel.SetActive(false);
    }

    public void GameClear()
    {
        allGameUI.SetActive(true);
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        titleText.SetActive(false);
        GameClearedPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputs();
    }

    void CheckInputs()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
            {
                CheckGameState(GameState.Paused);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else if (currentState == GameState.Paused)
            {
                CheckGameState(GameState.Playing);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level01 Scene");
        CheckGameState(GameState.Playing);
    }

    private void PauseGame()
    {
        Debug.Log("Pause");
        CheckGameState(GameState.Paused);
    }

    public void ResumeGame()
    {
        CheckGameState(GameState.Playing);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        CheckGameState(GameState.MainMenu);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ClearGame()
    {
        CheckGameState(GameState.GameClear);
    }

    public void UpdateLives()
    {
        lifeText.text = Manager.lives.ToString();
    }
}
