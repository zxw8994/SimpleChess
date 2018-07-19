using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : ChessPiece {

    public override List<IntVector2> CheckMovement(bool check)
    {
        List<IntVector2> validMoves = new List<IntVector2>();

                // Diagnal Movement
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
        for (int i = 1; i < 8; i++)
        {
            if (this.currentTile.coordinates.x - i > -1 && this.currentTile.coordinates.z + i < 8)
            {
                if (chess.tiles[this.currentTile.coordinates.x - i, this.currentTile.coordinates.z + i].occupied)
                {
                    validMoves.Add(new IntVector2(this.currentTile.coordinates.x - i, this.currentTile.coordinates.z + i));
                    break;
                }
                validMoves.Add(new IntVector2(this.currentTile.coordinates.x - i, this.currentTile.coordinates.z + i));
            }
        }

        // Cardinel movement
        for (int x = this.currentTile.coordinates.x + 1; x < 8; x++)
        {
            if (chess.tiles[x, this.currentTile.coordinates.z].occupied)
            {
                validMoves.Add(new IntVector2(x, this.currentTile.coordinates.z));
                break;
            }
            validMoves.Add(new IntVector2(x, this.currentTile.coordinates.z));
        }
        for (int x = this.currentTile.coordinates.x - 1; x > -1; x--)
        {
            if (chess.tiles[x, this.currentTile.coordinates.z].occupied)
            {
                validMoves.Add(new IntVector2(x, this.currentTile.coordinates.z));
                break;
            }
            validMoves.Add(new IntVector2(x, this.currentTile.coordinates.z));
        }
        for (int z = this.currentTile.coordinates.z + 1; z < 8; z++)
        {
            if (chess.tiles[this.currentTile.coordinates.x, z].occupied)
            {
                validMoves.Add(new IntVector2(this.currentTile.coordinates.x, z));
                break;
            }
            validMoves.Add(new IntVector2(this.currentTile.coordinates.x, z));
        }
        for (int z = this.currentTile.coordinates.z - 1; z > -1; z--)
        {
            if (chess.tiles[this.currentTile.coordinates.x, z].occupied)
            {
                validMoves.Add(new IntVector2(this.currentTile.coordinates.x, z));
                break;
            }
            validMoves.Add(new IntVector2(this.currentTile.coordinates.x, z));
        }

        return validMoves;
    }
}
