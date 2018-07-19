using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessPiece : MonoBehaviour {

    public TeamColor team;

    public ChessTile currentTile;

    public bool isPawn = false;
    public bool isKing = false;

    public Color originalColor;

    protected Chess chess;

    public bool isMoving;
    private float startTime;
    private float journeyLength;
    private float speed = 2f;
    private Vector3 startMarker;
    private Vector3 endMarker;
    private ChessTile newSpot;

    private void Start()
    {
        chess = FindObjectOfType<Chess>();
        this.GetComponent<Renderer>().material.color = originalColor;
        isMoving = false;
    }

    private void Update()
    {
        if (isMoving)
        {

            this.transform.position = Vector3.MoveTowards(this.transform.position, endMarker, speed * Time.deltaTime);

            if(this.transform.position == endMarker)
            {
                Debug.Log("Finished LERPing");
                isMoving = false;
                chess.PieceIsSelected = false;
                SetLocation(newSpot);
                chess.NewTurn();
            }
        }
    }

    public void StartMovement(Vector3 tilePos,ChessTile ns)
    {
        newSpot = ns;
        endMarker = tilePos;
        endMarker.y = this.transform.position.y;
        isMoving = true;
    }

    // Moves Piece to a tile and tells tile who it is occupied by
    public void SetLocation (ChessTile tile)
    {
        currentTile = tile;
        transform.localPosition = tile.transform.localPosition;
        currentTile.occupied = true;
        currentTile.occupiedBy = this;
        this.transform.position += new Vector3(0, GetComponent<MeshFilter>().mesh.bounds.extents.y, 0);
        //print("Hello");
        
    }

    

    // Highlights piece
    private void OnMouseEnter()
    {
        if (chess.whoseTurn == team && !isMoving && !chess.isGameOver)
        {
            if (!chess.PieceIsSelected)
            {
                this.GetComponent<Renderer>().material.color = Color.yellow;
            }
        }
    }
    // Removes highlight
    private void OnMouseExit()
    {
        if (chess.whoseTurn == team && !isMoving && !chess.isGameOver)
        {
            if (!chess.PieceIsSelected)
            {
                this.GetComponent<Renderer>().material.color = originalColor;
            }
        }
    }

    // Selects a Piece
    private void OnMouseDown()
    {
        if (!chess.PieceIsSelected && !chess.isGameOver)
        {
            if(chess.whoseTurn == team)
            {
                this.GetComponent<Renderer>().material.color = Color.yellow;
                chess.SelectedAPiece(this);
            }
        }
    }

    public abstract List<IntVector2> CheckMovement(bool check);
}
