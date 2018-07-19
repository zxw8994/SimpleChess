using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessPiece {

    public override List<IntVector2> CheckMovement(bool check)
    {
        List<IntVector2> validMoves = new List<IntVector2>();

        for (int i = 1; i < 8; i++)
        {
            if (this.currentTile.coordinates.x + i < 8 && this.currentTile.coordinates.z + i < 8)
            {
                if (chess.tiles[this.currentTile.coordinates.x + i, this.currentTile.coordinates.z + i].occupied)
                {
                    validMoves.Add(new IntVector2(this.currentTile.coordinates.x + i, this.currentTile.coordinates.z + i));
                    break;
                }
                validMoves.Add(new IntVector2(this.currentTile.coordinates.x + i, this.currentTile.coordinates.z + i));
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (this.currentTile.coordinates.x - i > -1 && this.currentTile.coordinates.z - i > -1)
            {
                if (chess.tiles[this.currentTile.coordinates.x - i, this.currentTile.coordinates.z - i].occupied)
                {
                    validMoves.Add(new IntVector2(this.currentTile.coordinates.x - i, this.currentTile.coordinates.z - i));
                    break;
                }
                validMoves.Add(new IntVector2(this.currentTile.coordinates.x - i, this.currentTile.coordinates.z - i));
            }
        }

        for (int i = 1; i < 8; i++)
        {
            if (this.currentTile.coordinates.x + i < 8 && this.currentTile.coordinates.z - i > -1)
            {
                if (chess.tiles[this.currentTile.coordinates.x + i, this.currentTile.coordinates.z - i].occupied)
                {
                    validMoves.Add(new IntVector2(this.currentTile.coordinates.x + i, this.currentTile.coordinates.z - i));
                    break;
                }
                validMoves.Add(new IntVector2(this.currentTile.coordinates.x + i, this.currentTile.coordinates.z - i));
            }
        }
        for (int i = 1; i < 8; i++) { 
            if (this.currentTile.coordinates.x - i > -1 && this.currentTile.coordinates.z + i < 8) {
                if (chess.tiles[this.currentTile.coordinates.x - i, this.currentTile.coordinates.z + i].occupied)
                {
                    validMoves.Add(new IntVector2(this.currentTile.coordinates.x - i, this.currentTile.coordinates.z + i));
                    break;
                }
                    validMoves.Add(new IntVector2(this.currentTile.coordinates.x - i, this.currentTile.coordinates.z + i));
            }
        }


            return validMoves;
    }
}
