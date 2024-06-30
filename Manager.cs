using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance;
    public static int lives;
    public static bool gamePaused;
    static GameUI gameUI;
    public static bool lifeLost = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        gameUI = FindObjectOfType<GameUI>();
        if(!lifeLost)
        {
            lives = 3;
        }
        gameUI.UpdateLives();
    }

    public static void Lives(int lifeValue)
    {
        lives += lifeValue;
        gameUI.UpdateLives();
        
        if (lives <= 0)
        {
            lives = 0;
            gameUI.CheckGameState(GameUI.GameState.GameOver);
        }
    }
}
