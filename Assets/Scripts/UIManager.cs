using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public GameObject gameOverScreen;
    public GameManager gm;
    public GameObject Instructions;
    bool instructionsActive = true;

    private void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            ActivateInstruction();
        }
    }

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

    public void ActivateInstruction()
    {
        if (instructionsActive)
        {
            Instructions.SetActive(false);
            instructionsActive = false;
        }
        else {
            Instructions.SetActive(true);
            instructionsActive = true;
        }
    }
}
