using System.Collections.Generic;
using UnityEngine;

public class ChessManager : SingletonPattern<ChessManager>
{
    [SerializeField] private List<Transform> chessPieces;
    [SerializeField] private Dictionary<int, Transform> chessMap;
    protected override void Awake()
    {
        base.Awake();

        CreateChessMap();
    }
    private void CreateChessMap()
    {
        if (chessPieces == null) return;
        chessMap = new Dictionary<int, Transform>();
        foreach (Transform t in chessPieces)
        {
            string chess = t.name.ToLower();
            int pieceID = 0;
            if (chess.Contains("black")) 
            {
                pieceID = pieceID | Piece.Black;
            }
            else if (chess.Contains("white"))
            {
                pieceID = pieceID | Piece.White;
            }

            if (chess.Contains("king"))
            {
                pieceID = pieceID | Piece.King;
            }
            else if (chess.Contains("queen")){
                pieceID = pieceID | Piece.Queen;
            }
            else if (chess.Contains("knight")){
                pieceID = pieceID | Piece.Knight;
            }   
            else if (chess.Contains("bishop")){
                pieceID = pieceID | Piece.Bishop;
            }
            else if (chess.Contains("rook")){
                pieceID = pieceID | Piece.Rook;
            }
            else if (chess.Contains("pawn")){
                pieceID = pieceID | Piece.Pawn;
            }
            //Debug.Log(pieceID);
            chessMap.Add(pieceID, t);
        }
    }

    public Transform GetChess(int pieceID)
    {
        return chessMap[pieceID];
    }
}
