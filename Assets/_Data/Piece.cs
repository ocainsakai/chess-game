
public static class Piece
{
    public const int TypeMask = 0b00000111; // 3 bit cho loại quân (1-6)
    public const int ColorMask = 0b00001000; // 1 bit cho màu quân (0 = White, 1 = Black)

    public const int None = 0;
    public const int King = 1;
    public const int Queen = 2;
    public const int Bishop = 3;
    public const int Knight = 4;
    public const int Rook = 5;
    public const int Pawn = 6;

    public const int White = 0;  // 0b00000000
    public const int Black = 8;  // 0b00001000

    public static int Colour(int piece)
    {
        return piece & Piece.ColorMask; // Trả về 0 (White) hoặc 8 (Black)
    }

    public static bool IsSlidingPiece(int piece)
    {
        int type = piece & Piece.TypeMask;
        return type == Piece.Rook || type == Piece.Bishop || type == Piece.Queen;
    }

    public static bool IsType(int piece, int type)
    {
        return (piece & Piece.TypeMask) == type;
    }

    public static bool IsInitPawn(int square, int piece)
    {
        int type = piece & Piece.TypeMask;
        int color = piece & Piece.ColorMask;

        return type == Piece.Pawn &&
               ((color == Piece.White && square >= 8 && square < 16) ||
                (color == Piece.Black && square >= 48 && square < 56));
    }

    public static bool IsColour(int piece, int colour)
    {
        return (piece & Piece.ColorMask) == colour;
    }

}