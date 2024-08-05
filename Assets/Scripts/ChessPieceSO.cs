
using UnityEngine;
[CreateAssetMenu(menuName = "Chess/Chess Piece SO")]

public class ChessPieceSO : ScriptableObject
{

    public GameObject prefab;
    public ChessType chessType;
    public BoardManager.ChessColor chessColor;
}
