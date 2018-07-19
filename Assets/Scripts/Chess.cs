using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chess : MonoBehaviour {

    public TeamColor whoseTurn = TeamColor.White;

    UIManager uiManager;

    private int boardXZ = 8;
    private int counter = 2;

    public ChessTile tileWhitePrefab;
    public ChessTile tileBlackPrefab;

    public ChessTile[,] tiles;
    public ChessTile selectedTile;
    public ChessTile moveToTile;
    private int moveToCounter = 0;

    public IntVector2 size;

    public Pawn pawnPrefab;
    public Rook rookPrefab;
    public Bishop bishopPrefab;
    public Knight knightPrefab;
    public Queen queenPrefab;
    public King kingPrefab;

    private Shader outline;
    private Shader defaultShader;

    private List<ChessPiece> whiteTeam;
    private List<ChessPiece> blackTeam;

    ChessPiece kingBlack;
    ChessPiece kingWhite;

    List<IntVector2> validMoves;
    List<IntVector2> movesCheck;
    List<ChessPiece> threatPieces;

    public bool isGameOver = false;
    public bool inCheck = false;
    public bool PieceIsSelected = false;
    private ChessPiece selectedPiece;

    Color selectOriginal;
    Color selectedColor = Color.red;
    Color moveToOriginal;
    Color moveToColor = Color.green;

    private void Start()
    {
        uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();

        outline = Shader.Find("Outline");

        whiteTeam = new List<ChessPiece>();
        blackTeam = new List<ChessPiece>();

        whoseTurn = TeamColor.Black;

        NewTurn();
    }

    public void NewTurn()
    {
        /*if(threatPieces != null)
        {
            foreach (ChessPiece piece in threatPieces)
            {
                // Revert all shaders
            }
            threatPieces.Clear();
        }*/

        if(whoseTurn == TeamColor.White)
        {
            whoseTurn = TeamColor.Black;
            //defaultShader = blackTeam[0].GetComponent<Renderer>().material.shader;

            #region King in Check - IN-PROGRESS
            /* foreach (ChessPiece piece in whiteTeam)
            {
                //Debug.Log("Blep");
                movesCheck = piece.CheckMovement(true);
                //Debug.Log(movesCheck.Count);
                for(int i=0; i < movesCheck.Count;i++)
                {
                    //Debug.Log("Hekkin ");
                    if (tiles[movesCheck[i].x, movesCheck[i].z].occupied && tiles[movesCheck[i].x, movesCheck[i].z].occupiedBy == kingBlack)
                    {
                        Debug.Log("Bloop");
                        if (tiles[movesCheck[i].x, movesCheck[i].z].occupiedBy.isKing )
                        {
                            Debug.Log("Bliiip");
                            threatPieces.Add(tiles[movesCheck[i].x, movesCheck[i].z].occupiedBy);
                            movesCheck.RemoveAt(i);
                            i = 0;
                            break;
                        }
                    }
                }
                movesCheck.Clear();
            }
            
            if (threatPieces != null)
            {
                foreach (ChessPiece piece in threatPieces)
                {
                   
                    Debug.Log(piece.name);
                    // Change Shader to Outline
                    piece.GetComponent<Renderer>().material.shader = outline;
                }
            } */
#endregion

        }
        else {
            whoseTurn = TeamColor.White;
        }
    }

    private void Update()
    {
        // Checks if Space was pressed, if so deselects selected Piece
        if (PieceIsSelected) {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PieceIsSelected = false;
                UnHighlightValidTiles(selectedPiece);
                selectedPiece.GetComponent<Renderer>().material.color = selectedPiece.originalColor;
                selectedPiece = null;

            }
        }
    }

    public void SelectedAPiece(ChessPiece sel)
    {
        selectedPiece = sel;
        PieceIsSelected = true;
        HighlightValidTiles(selectedPiece);
    }
    
    public void MovePiece(ChessTile newSpot)
    {
        // Checks if space piece is moving to is occupied, if so attacked piece is destroyed, 
        // bool is true if it was a King,
        bool wasKing = false;
        if (newSpot.occupied)
        {
            if (newSpot.occupiedBy.isKing)
            {
                wasKing = true;
            }
            
            Destroy(newSpot.occupiedBy.gameObject);
            //newSpot.occupiedBy = selectedPiece;
        }
        // Starts LERP process for moved piece
        selectedPiece.StartMovement(newSpot.transform.position,newSpot);

        // Resets everything to do with selecting and moving a piece
        UnHighlightValidTiles(selectedPiece);
        selectedPiece.currentTile.occupied = false;
        selectedPiece.currentTile.occupiedBy = null;
        //selectedPiece.SetLocation(newSpot);


        selectedPiece.GetComponent<Renderer>().material.color = selectedPiece.originalColor;
        if (selectedPiece.isPawn) // if a pawn was moved make onStartingTile false, so it can only move twice once
        {
            selectedPiece.GetComponent<Pawn>().onStartingTile = false;

            // If pawn reaches opposite side of board, replace it with a Queen
            if(newSpot.coordinates.z == 0f )
            {
                CreateQueen(blackTeam, queenPrefab, selectedPiece.currentTile.coordinates, selectedPiece.team);
                Destroy(selectedPiece.gameObject);
            }else if(newSpot.coordinates.z == 7f)
            {
                CreateQueen(whiteTeam, queenPrefab, selectedPiece.currentTile.coordinates, selectedPiece.team);
                Destroy(selectedPiece.gameObject);
            }

        }

        

        // If King was destroyed, end game
        if (wasKing)
        {
            // Game Over Screen
            isGameOver = true;
            whiteTeam = null;
            blackTeam = null;
            uiManager.GameOver();
        }

        //NewTurn();
    }

    // Checks and selected Piece's valid moves then highlights
    private void HighlightValidTiles(ChessPiece selected)
    {
        ChessPiece target = selected;
        validMoves = target.CheckMovement(false);

        // Removes any tiles from validMoves if they are occupied by a piece of the same team
        for (int i = 0; i < validMoves.Count; i++) {
            if (tiles[validMoves[i].x, validMoves[i].z].occupied && tiles[validMoves[i].x, validMoves[i].z].occupiedBy.team == whoseTurn) { 
                if (tiles[validMoves[i].x, validMoves[i].z].occupiedBy.team == target.team) {
                    validMoves.RemoveAt(i);
                    i = 0;
                } 
            }
        }

        // changes tiles that can be moved to blue
        for (int i = 0; i < validMoves.Count; i++)
        {
            if (!tiles[validMoves[i].x, validMoves[i].z].occupied || tiles[validMoves[i].x, validMoves[i].z].occupiedBy.team != whoseTurn)
            {
                tiles[validMoves[i].x, validMoves[i].z].gameObject.GetComponentInChildren<Renderer>().material.color =
                    Color.blue;
                tiles[validMoves[i].x, validMoves[i].z].validTile = true;
            }
            print(validMoves[i].x + " " + validMoves[i].z);
        }
        print(validMoves.Count);
    }

    // changes highlighted tiles back to original color
    private void UnHighlightValidTiles(ChessPiece selected)
    {
        ChessPiece target = selected;

        for (int i = 0; i < validMoves.Count; i++)
        {
            tiles[validMoves[i].x, validMoves[i].z].gameObject.GetComponentInChildren<Renderer>().material.color =
                    tiles[validMoves[i].x, validMoves[i].z].originalColor;
            tiles[validMoves[i].x, validMoves[i].z].validTile = false;
        }

        validMoves.Clear();
    }

    // Creates all the tiles of the board
    public void GenerateTiles()
    {
        tiles = new ChessTile[size.x, size.z];
        for(int x = 0; x < size.x; x++)
        {
            if (counter == 2) { counter--; }
            else if (counter == 1){ counter++; }
            for(int z = 0; z < size.z;z++) {
                if (counter == 1) {
                    CreateTile(new IntVector2(x,z),tileBlackPrefab,Color.black);
                    counter++;
                } else if( counter == 2){
                    CreateTile(new IntVector2(x,z), tileWhitePrefab,Color.white);
                    counter--;
                }
            }
        }
    }

    // Creates a tile for the board
    private void CreateTile(IntVector2 coordinates, ChessTile tile,Color color)
    {
        ChessTile newTile = Instantiate(tile) as ChessTile;
        tiles[coordinates.x, coordinates.z] = newTile;
        newTile.coordinates = coordinates;
        newTile.originalColor = color;
        newTile.name = "Board Tile " + coordinates.x + ", " + coordinates.z;
        newTile.transform.parent = transform;
        newTile.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f,coordinates.z - size.z * 0.5f + 0.5f);
    }

    public void GeneratePieces()
    {
        whiteTeam = new List<ChessPiece>();
        blackTeam = new List<ChessPiece>();

        // Spawn Pawns
        for (int i = 0; i < 8; i++){
            CreatePawn(whiteTeam, pawnPrefab,new IntVector2(i,1),TeamColor.White);
            CreatePawn(blackTeam, pawnPrefab, new IntVector2(i, 6), TeamColor.Black);
        }

        // Spawn Rooks
        CreateRook(whiteTeam, rookPrefab, new IntVector2(0, 0), TeamColor.White);
        CreateRook(whiteTeam, rookPrefab, new IntVector2(7, 0), TeamColor.White);
        CreateRook(blackTeam, rookPrefab, new IntVector2(0, 7), TeamColor.Black);
        CreateRook(blackTeam, rookPrefab, new IntVector2(7, 7), TeamColor.Black);
        // Spawn Bishop
        CreateBishop(whiteTeam, bishopPrefab, new IntVector2(2, 0), TeamColor.White);
        CreateBishop(whiteTeam, bishopPrefab, new IntVector2(5, 0), TeamColor.White);
        CreateBishop(blackTeam, bishopPrefab, new IntVector2(2, 7), TeamColor.Black);
        CreateBishop(blackTeam, bishopPrefab, new IntVector2(5, 7), TeamColor.Black);
        // Spawn Knight
        CreateKnight(whiteTeam, knightPrefab, new IntVector2(1, 0), TeamColor.White);
        CreateKnight(whiteTeam, knightPrefab, new IntVector2(6, 0), TeamColor.White);
        CreateKnight(blackTeam, knightPrefab, new IntVector2(1, 7), TeamColor.Black);
        CreateKnight(blackTeam, knightPrefab, new IntVector2(6, 7), TeamColor.Black);
        // Spawn Queen
        CreateQueen(whiteTeam, queenPrefab, new IntVector2(4, 0), TeamColor.White);
        CreateQueen(blackTeam, queenPrefab, new IntVector2(4, 7), TeamColor.Black);
        // Spawn King
        CreateKing(whiteTeam, kingPrefab, new IntVector2(3, 0), TeamColor.White);
        CreateKing(blackTeam, kingPrefab, new IntVector2(3, 7), TeamColor.Black);

        
    }

    // Each method below creates one of the chess pieces
    private void CreatePawn(List<ChessPiece> team, Pawn piece, IntVector2 coordinates ,TeamColor color)
    {
        Pawn newPawn = Instantiate(piece) as Pawn;
        team.Add(newPawn);
        newPawn.SetLocation(tiles[coordinates.x,coordinates.z]);
        newPawn.team = color;
        newPawn.isPawn = true;
        if (color == TeamColor.Black)
        {
            newPawn.originalColor = Color.black;
        }
        else
        {
            newPawn.originalColor = Color.white;
        }
    }

    private void CreateRook(List<ChessPiece> team, Rook piece, IntVector2 coordinates, TeamColor color)
    {
        Rook newRook = Instantiate(piece) as Rook;
        team.Add(newRook);
        newRook.SetLocation(tiles[coordinates.x, coordinates.z]);
        newRook.team = color;
        if (color == TeamColor.Black)
        {
            newRook.originalColor = Color.black;
        }
        else
        {
            newRook.originalColor = Color.white;
        }
    }

    private void CreateBishop(List<ChessPiece> team, Bishop piece, IntVector2 coordinates, TeamColor color)
    {
        Bishop newBishop = Instantiate(piece) as Bishop;
        team.Add(newBishop);
        newBishop.SetLocation(tiles[coordinates.x, coordinates.z]);
        newBishop.team = color;
        if (color == TeamColor.Black)
        {
            newBishop.originalColor = Color.black;
        }
        else
        {
            newBishop.originalColor = Color.white;
        }
    }

    private void CreateKnight(List<ChessPiece> team, Knight piece, IntVector2 coordinates, TeamColor color) {
        Knight newKnight = Instantiate(piece) as Knight;
        team.Add(newKnight);
        newKnight.SetLocation(tiles[coordinates.x, coordinates.z]);
        newKnight.team = color;
        if (color == TeamColor.Black)
        {
            newKnight.originalColor = Color.black;
        }
        else
        {
            newKnight.originalColor = Color.white;
        }
    }

    private void CreateQueen(List<ChessPiece> team, Queen piece, IntVector2 coordinates, TeamColor color) {
        Queen newQueen = Instantiate(piece) as Queen;
        team.Add(newQueen);
        newQueen.SetLocation(tiles[coordinates.x, coordinates.z]);
        newQueen.team = color;
        if (color == TeamColor.Black)
        {
            newQueen.originalColor = Color.black;
        }
        else
        {
            newQueen.originalColor = Color.white;
        }
    }

    private void CreateKing(List<ChessPiece> team, King piece, IntVector2 coordinates, TeamColor color) {
        King newKing = Instantiate(piece) as King;
        team.Add(newKing);
        newKing.SetLocation(tiles[coordinates.x, coordinates.z]);
        newKing.team = color;
        newKing.isKing = true;
        if (color == TeamColor.Black)
        {
            newKing.originalColor = Color.black;
            kingBlack = newKing;
        }
        else
        {
            newKing.originalColor = Color.white;
            kingWhite = newKing;
        }
    }

    public void ClearTeams()
    {
        whiteTeam = new List<ChessPiece>();
        blackTeam = new List<ChessPiece>();
    }
}
