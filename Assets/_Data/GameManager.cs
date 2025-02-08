using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PieceUI pieceUI;
    Dictionary<int,Transform> pieces;
    public static GameManager instance;
    private List<int> squareCanMove;
    PieceMovement pieceMovement = new PieceMovement();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
        pieces = new Dictionary<int,Transform>();
    }
    private void Start()
    {
        // new board
        Board.Square = UltilitiesFEN.LoadPosiotionFromFEN(UltilitiesFEN.startFEN);
        //Board.Square = UltilitiesFEN.LoadPosiotionFromFEN("R7/8/8/8/3b3p/8/B7/8");

        // update ui
        for (int i = 0; i < 64; i++)
        {

            Transform newPiece = pieceUI.GetPiece(Board.Square[i]);
            if (newPiece == null) continue;

            //PieceData data = newPiece.GetComponent<PieceData>();
            //data.currentSquare = i;
            //data.pieceID = Board.Square[i];
            
            newPiece.position = BoardUI.instance.GetCell(i).position;
            newPiece.gameObject.SetActive(true);
            pieces.Add(i, newPiece);
        }
        pieceMovement.GenerateMove();
    }
    public void Move(int startSquare, int targetSquare, Transform chess)
    {
        BoardUI.instance.ResetColor();
        if (!CheckValidMove(targetSquare))
        {
            chess.position = BoardUI.instance.GetCell(startSquare).position;
        }
        else
        {
            Debug.Log(CheckValidMove(targetSquare));
            Board.Move(startSquare, targetSquare);
            Transform start = pieces[startSquare];

            chess.position = BoardUI.instance.GetCell(targetSquare).position;
            if (pieces.TryGetValue(targetSquare, out Transform target))
            {
                target.gameObject.SetActive(false);
                pieces.Remove(targetSquare);
            }
            pieces.Remove(startSquare);
            pieces.Add(targetSquare, start);
            pieceMovement.GenerateMove();
        }

    }
    private bool CheckValidMove(int target)
    {
        return squareCanMove != null && squareCanMove.Contains(target);
    }
    public int[] PredictionMove(int startSquare)
    {
        List<int> moves = new List<int>();
        foreach (var move in pieceMovement.moves)
        {
            if (move.startSquare == startSquare)
            {
                BoardUI.instance.HightLight(move.targetSquare);
                moves.Add(move.targetSquare);
            }
        }
        squareCanMove = moves;
        Debug.Log(squareCanMove.Count);
        return moves.ToArray();
    }
}
