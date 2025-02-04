using System;
using System.Collections.Generic;
using UnityEngine;
//using static PieceMovement;

public class GameManager : MonoBehaviour
{
    [SerializeField] PieceUI pieceUI;
    Dictionary<int,Transform> pieces;
    public static GameManager instance;
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

    }
    public void Move(int startSquare, int targetSquare)
    {
        Board.Move(startSquare, targetSquare);
        Transform start = pieces[startSquare];

        if (pieces.TryGetValue(targetSquare, out Transform target))
        {
            target.gameObject.SetActive(false);
            pieces.Remove(targetSquare);
        }
        pieces.Remove(startSquare);
        pieces.Add(targetSquare, start);
        
    }
    public void UpdateChess()
    {
        foreach (var item in pieces)
        {
            
        }
    }
}
