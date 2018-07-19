using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : ChessPiece {

    public override List<IntVector2> CheckMovement(bool check)
    {
        List<IntVector2> validMoves = new List<IntVector2>();


        if (this.currentTile.coordinates.x + 1 < 8)
        {
            if (this.currentTile.coordinates.z + 1 < 8)
            {
                validMoves.Add(new IntVector2(this.currentTile.coordinates.x + 1, this.currentTile.coordinates.z + 1));
            }
            if (this.currentTile.coordinates.z - 1 > -1) {
                validMoves.Add(new IntVector2(this.currentTile.coordinates.x + 1, this.currentTile.coordinates.z - 1));
            }
            validMoves.Add(new IntVector2(this.currentTile.coordinates.x + 1, this.currentTile.coordinates.z));
        }
        if (this.currentTile.coordinates.x - 1 > -1)
        {
            if (this.currentTile.coordinates.z - 1 > -1) {
                validMoves.Add(new IntVector2(this.currentTile.coordinates.x - 1, this.currentTile.coordinates.z - 1));
            }
            if (this.currentTile.coordinates.z + 1 < 8) {
                validMoves.Add(new IntVector2(this.currentTile.coordinates.x - 1, this.currentTile.coordinates.z + 1));
            }

            validMoves.Add(new IntVector2(this.currentTile.coordinates.x - 1, this.currentTile.coordinates.z));
        }
        if (this.currentTile.coordinates.z + 1 < 8) { 
            validMoves.Add(new IntVector2(this.currentTile.coordinates.x, this.currentTile.coordinates.z + 1));
        }
        if (this.currentTile.coordinates.z - 1 > -1) {
            validMoves.Add(new IntVector2(this.currentTile.coordinates.x, this.currentTile.coordinates.z - 1));
        }
        


        return validMoves;
    }
}
