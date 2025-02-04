using UnityEngine;

public static class Board
{
    public static int[] Square; // contain pieceID
    public static int ColourToMove = Piece.White;

    static Board()
    {
        Square = new int[64];
    }
    public static int GetPiece(int square)
    {

        return Square[square];
    }

    public static void Move(int startSquare, int targetSquare)
    {
        int piece = Square[startSquare];
        Square[startSquare] = 0;
        Square[targetSquare] = piece;
    }
}
