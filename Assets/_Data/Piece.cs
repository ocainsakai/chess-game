
public static class Piece
{
    public const int None = 0;
    public const int King = 1;
    public const int Queen = 2;
    public const int Bishop = 3;
    public const int Knight = 4;
    public const int Rook = 5;
    public const int Pawn = 6;

    public const int White = 8;
    public const int Black = 16;
    public static bool IsSlidingPiece(int piece)
    {
        return IsType(piece, Rook) || IsType(piece, Bishop) || IsType(piece, Queen);
    }
    public static bool IsType(int piece, int type)
    {
        return (piece & 7) == type;
    }
    public static bool IsInitPawn(int square, int piece)
    {
        return (square >= 8 && square < 16 && IsColour(piece, White))
            || (square >= 48 && square < 56 && IsColour(piece, Black));
    }
    public static bool IsColour(int piece, int colour)
    {
        return (piece & (White | Black)) == colour;
    }
}