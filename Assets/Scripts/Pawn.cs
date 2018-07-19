using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPiece {

    

    public bool onStartingTile = true;

	public override List<IntVector2> CheckMovement(bool check)
    {
        List<IntVector2> validMoves = new List<IntVector2>();

        // Since Pawn only move in a single direction, Movement depends on Team
        if (this.team == TeamColor.White)
        {

            // Move forward one tile
            if (this.currentTile.coordinates.z + 1 < 8 && !check) {
                if (!chess.tiles[this.currentTile.coordinates.x, this.currentTile.coordinates.z + 1].occupied) {
                    validMoves.Add(new IntVector2(this.currentTile.coordinates.x, this.currentTile.coordinates.z + 1));
                }
            }
            // Diagnal Attack
            if (this.currentTile.coordinates.x - 1 > -1)
            {
                if (chess.tiles[this.currentTile.coordinates.x - 1, this.currentTile.coordinates.z + 1].occupied)
                {
                    validMoves.Add(new IntVector2(this.currentTile.coordinates.x - 1, this.currentTile.coordinates.z + 1));
                }
            }
            if (this.currentTile.coordinates.x + 1 < 8) {
                if (chess.tiles[this.currentTile.coordinates.x + 1, this.currentTile.coordinates.z + 1].occupied)
                {
                    validMoves.Add(new IntVector2(this.currentTile.coordinates.x + 1, this.currentTile.coordinates.z + 1));
                }
            }
            // If on starting tile, can move forward two tiles
            if (onStartingTile && !check)
            {
                if (!chess.tiles[this.currentTile.coordinates.x, this.currentTile.coordinates.z + 2].occupied)
                {
                    validMoves.Add(new IntVector2(this.currentTile.coordinates.x, this.currentTile.coordinates.z + 2));
                }
            }
        }else if(this.team == TeamColor.Black)
        {
            // Move forward one tile
            if (this.currentTile.coordinates.z - 1 > -1 && !check) {
                if (!chess.tiles[this.currentTile.coordinates.x, this.currentTile.coordinates.z - 1].occupied)
                {
                    validMoves.Add(this.currentTile.coordinates + new IntVector2(0, -1));
                }
            }
            // Diagnal Attack
            if (this.currentTile.coordinates.x - 1 > -1) {
                if (chess.tiles[this.currentTile.coordinates.x - 1, this.currentTile.coordinates.z - 1].occupied)
                {
                    validMoves.Add(this.currentTile.coordinates + new IntVector2(-1, -1));
                }
            }
            if (this.currentTile.coordinates.x + 1 < 8)
            {
                if (chess.tiles[this.currentTile.coordinates.x + 1, this.currentTile.coordinates.z - 1].occupied)
                {
                    validMoves.Add(this.currentTile.coordinates + new IntVector2(1, -1));
                }
            }
            // If on starting tile, can move forward two tiles
            if (onStartingTile && !check)
            {
                if (!chess.tiles[this.currentTile.coordinates.x, this.currentTile.coordinates.z - 2].occupied)
                {
                    validMoves.Add(this.currentTile.coordinates + new IntVector2(0, -2));
                }
            }
        }
       


        return validMoves;
    }
}
