using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public GameObject gameOverScreen;
    public GameManager gm;

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void RestartGame()
    {
        gm.RestartGame();
        gameOverScreen.SetActive(false);
    }

    public void ExitGame()
    {
        gm.ExitGame();
    }
}
