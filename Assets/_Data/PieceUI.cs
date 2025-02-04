using UnityEngine;
using System.Collections.Generic;

public class PieceUI : MonoBehaviour
{
    [SerializeField] private Sprite r_b, r_w, q_b, q_w, k_b, k_w, n_b, n_w, b_b, b_w, p_b, p_w;
    [SerializeField] private Transform piecePrefab;

    private Dictionary<int, Sprite> pieceSprites;

    private void Awake()
    {
        pieceSprites = new Dictionary<int, Sprite>
        {
            { Piece.Black | Piece.Bishop, b_b },
            { Piece.Black | Piece.King, k_b },
            { Piece.Black | Piece.Knight, n_b },
            { Piece.Black | Piece.Queen, q_b },
            { Piece.Black | Piece.Rook, r_b },
            { Piece.Black | Piece.Pawn, p_b },
            { Piece.White | Piece.Bishop, b_w },
            { Piece.White | Piece.King, k_w },
            { Piece.White | Piece.Knight, n_w },
            { Piece.White | Piece.Queen, q_w },
            { Piece.White | Piece.Rook, r_w },
            { Piece.White | Piece.Pawn, p_w }
        };
    }

    public Transform GetPiece(int pieceID)
    {
        if (!pieceSprites.TryGetValue(pieceID, out Sprite sprite))
        {
            return null;
        }

        Transform newPiece = Instantiate(piecePrefab);
        newPiece.GetComponent<SpriteRenderer>().sprite = sprite;
        newPiece.transform.SetParent(piecePrefab.parent);


        string color = (pieceID & Piece.White) != 0 ? "White" : "Black";


        string pieceType = (pieceID &7) switch
        {
            Piece.King => "King",
            Piece.Queen => "Queen",
            Piece.Bishop => "Bishop",
            Piece.Knight => "Knight",
            Piece.Rook => "Rook",
            Piece.Pawn => "Pawn",
            _ => "Unknown"
        };
        newPiece.name = $"{color}_{pieceType}";

        return newPiece;
    }

}
