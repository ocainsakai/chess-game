using UnityEngine;
[CreateAssetMenu(fileName = "ChessTypeSO", menuName = "Chess/ChessTypeSO")]

public class ChessTypeSO : ScriptableObject
{
    public Sprite sprite;
    public ChessColor  pieceColor; // Màu sắc của quân cờ
    public ChessType pieceName; // Tên quân cờ (e.g., Pawn, Knight, King)
}

public enum ChessColor
{
    white = 0,
    black = 1,
}

public enum ChessType
{
    pawn,
    rook,
    knight,
    bishop,
    queen,
    king,
}

