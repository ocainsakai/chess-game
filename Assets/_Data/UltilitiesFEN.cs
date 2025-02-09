using System.Collections.Generic;
using System.Diagnostics;


public static class UltilitiesFEN
{
    public const string startFEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
    public static int[] LoadPosiotionFromFEN(string fen)
    {
        var chessMap = new int[64];
        var PieceTypeFromSymbol = new Dictionary<char, int>()
        {
            ['k'] = Piece.King,
            ['q'] = Piece.Queen,
            ['b'] = Piece.Bishop,
            ['n'] = Piece.Knight,
            ['r'] = Piece.Rook,
            ['p'] = Piece.Pawn,
        };

        string fenBoard = fen.Split(' ')[0];
        int file = 0, rank = 7;
        foreach (char symbol in fenBoard)
        {
            if (symbol == '/')
            {
                file = 0;
                rank--;
            }
            else
            {
                if (char.IsDigit(symbol))
                {
                    file += (int)char.GetNumericValue(symbol);
                }
                else
                {
                    int chessColor = (char.IsUpper(symbol)) ? Piece.White : Piece.Black;
                    int chessType = PieceTypeFromSymbol[char.ToLower(symbol)];
                    chessMap[rank * 8 + file] = chessColor | chessType;
                    
                    file++;
                }
            }
        }
        return chessMap;
    }
}