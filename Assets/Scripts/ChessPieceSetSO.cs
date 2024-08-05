
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Chess/Chess Piece Set SO")]

public class ChessPieceSetSO : ScriptableObject
{
   [SerializeField] public List<ChessPieceSO> pieces;
   public BoardManager.ChessColor color;
}
