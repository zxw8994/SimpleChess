using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Chess chessPrefab;

    private Chess chessInstance;

    Color original;
    Color selectedColor = Color.red;

	// Use this for initialization
	void Start () {
        BeginGame();
        //chessInstance.NewTurn();
	}
	
    private void BeginGame()
    {
        chessInstance = Instantiate(chessPrefab) as Chess;
        chessInstance.GenerateTiles();
        chessInstance.GeneratePieces();
    }

    public void RestartGame()
    {
        GameObject[] allPieces = GameObject.FindGameObjectsWithTag("Piece");
        for (int i = 0; i < allPieces.Length; i++)
        {
            Destroy(allPieces[i]);
        }
        foreach(ChessTile tile in chessInstance.tiles)
        {
            tile.occupiedBy = null;
            tile.occupied = false;
        }

        chessInstance.isGameOver = false;
        // Clears Team Lists
        chessInstance.ClearTeams();
        // Generates new pieces
        chessInstance.GeneratePieces();
        chessInstance.whoseTurn = TeamColor.White;
    }
    public void ExitGame()
    {
        Application.Quit();
    } 

}
