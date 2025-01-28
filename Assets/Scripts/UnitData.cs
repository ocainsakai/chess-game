using UnityEngine;
[CreateAssetMenu(fileName = "Unit", menuName = "Synergy System/Unit")]

public class UnitData : ScriptableObject
{
    public bool isWhite;
    public ChessPieceType pieceType;
    public Sprite sprite;
}
public enum ChessPieceType
{
    Pawn,
    Rook,
    Knight,
    Bishop,
    Queen,
    King,
}
