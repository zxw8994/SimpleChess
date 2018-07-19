using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessTile : MonoBehaviour {

    public IntVector2 coordinates;

    public bool occupied;

    public bool validTile = false;

    public ChessPiece occupiedBy;

    public Color originalColor;

    private Chess chess;

    private void Start()
    {
        chess = FindObjectOfType<Chess>();
    }

    // Highlights tile
    private void OnMouseEnter()
    {
        if (chess.PieceIsSelected && validTile)
        {
            this.gameObject.GetComponentInChildren<Renderer>().material.color = Color.green;
        }
    }
    // removes highlights
    private void OnMouseExit()
    {
        if (chess.PieceIsSelected && validTile)
        {
            this.gameObject.GetComponentInChildren<Renderer>().material.color = Color.blue;
        }
    }

    // When a Piece is selected, clicking on a validTile will move it to this Tile
    private void OnMouseDown()
    {
        if (chess.PieceIsSelected && validTile)
        {
            chess.MovePiece(this);
        }
        
    }


}
