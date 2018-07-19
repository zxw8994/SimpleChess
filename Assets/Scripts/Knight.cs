using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : ChessPiece{

    public override List<IntVector2> CheckMovement(bool check)
    {
        List<IntVector2> validMoves = new List<IntVector2>();

        if (this.currentTile.coordinates.x + 2 < 8)
        {
            if (this.currentTile.coordinates.z + 1 < 8) {
                validMoves.Add(this.currentTile.coordinates + new IntVector2(2, 1));
            }
            if (this.currentTile.coordinates.z - 1 > -1) {
                validMoves.Add(this.currentTile.coordinates + new IntVector2(2, -1));
            }
        }
        if (this.currentTile.coordinates.x + 1 < 8)
        {
            if (this.currentTile.coordinates.z + 2 < 8)
            {
                validMoves.Add(this.currentTile.coordinates + new IntVector2(1, 2));
            }
            if (this.currentTile.coordinates.z - 2 > -1)
            {
                validMoves.Add(this.currentTile.coordinates + new IntVector2(1, -2));
            }
        }

        if (this.currentTile.coordinates.x - 2 > -1)
        {
            if (this.currentTile.coordinates.z + 1 < 8)
            {
                validMoves.Add(this.currentTile.coordinates + new IntVector2(-2, 1));
            }
            if (this.currentTile.coordinates.z - 1 > -1)
            {
                validMoves.Add(this.currentTile.coordinates + new IntVector2(-2, -1));
            }
        }


        if (this.currentTile.coordinates.x - 1 > -1)
        {
            if (this.currentTile.coordinates.z + 2 < 8)
            { 
                validMoves.Add(this.currentTile.coordinates + new IntVector2(-1, 2));
            }
            if (this.currentTile.coordinates.z - 2 > -1)
            {
                validMoves.Add(this.currentTile.coordinates + new IntVector2(-1, -2));
            }
        }
        

        return validMoves;
    }
}
