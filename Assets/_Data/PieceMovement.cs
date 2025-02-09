
using System;
using System.Collections.Generic;
using UnityEngine;

public class PieceMovement
{

    public List<Move> moves { get; private set; } 
    
    public static readonly int[] DirectionOffset = { 8, -8, -1, 1, 7, -7, -9, 9 };
    public static readonly int[] DirectionKnightOffset = {15, -15, 10, -10, -6, 6, 17, -17};
    public static readonly int[][] NumSquaresToEdge = new int[64][];
    private int friendlyColour;
    private int opponentColour;

    public PieceMovement()
    {
        for (int file = 0; file < 8; file++)
        {
            for (int rank = 0; rank < 8; rank++)
            {
                int numNorth = 7 - rank;
                int numSouth = rank;
                int numWest = file;
                int numEast = 7 - file;

                int squareIndex = rank * 8 + file;

                NumSquaresToEdge[squareIndex] = new int[] {
                    numNorth,// 
                    numSouth,
                    numWest,//
                    numEast,
                Mathf.Min(numWest, numNorth),
                Mathf.Min(numEast, numSouth),
                Mathf.Min(numSouth, numWest),
                Mathf.Min(numNorth, numEast),
                };
                //Debug.Log("square: " + squareIndex + "min " + Mathf.Min(numNorth, numEast));
            }
        }
    }
    public List<Move> GenerateMove()
    {
        friendlyColour = Board.ColourToMove;
        opponentColour = friendlyColour ^ Piece.ColorMask;
        moves = new List<Move>();
        for (int startSquare = 0; startSquare < 64; startSquare++)
        {
            int piece = Board.Square[startSquare];
            if (Piece.IsColour(piece, friendlyColour))
            {
                if (Piece.IsType(piece, Piece.King))
                {
                    GenerateKingMove(startSquare, piece);
                } else
                if (Piece.IsSlidingPiece(piece))
                {
                    GenerateSlidingMove(startSquare, piece);
                } else if (Piece.IsType(piece, Piece.Knight))
                {
                    GenerateKnightMove(startSquare, piece);
                } else if (Piece.IsType(piece, Piece.Pawn))
                {
                    GeneratePawnMove(startSquare, piece);
                }
            }
        }
        return moves;
    }

    private void GenerateKingMove(int startSquare, int piece)
    {
        Debug.Log(piece);
        for (int i = 0; i < 8; i++) {
            if (NumSquaresToEdge[startSquare][i] == 0) return;
            int targetSquare = startSquare + DirectionOffset[i];
            //if (Piece.IsColour(Board.Square[targetSquare], friendlyColour)) continue;
            moves.Add(new Move(startSquare, targetSquare));
        }
    }

    private void GenerateKnightMove(int start, int piece)
    {
        for (int dirIndex = 0; dirIndex < 8; dirIndex++)
        {
            int targetSquare = start + DirectionKnightOffset[dirIndex];

            if (IsValidKnightMove(start, targetSquare))
            {
                //if (Piece.IsColour(Board.Square[targetSquare], friendlyColour)) continue;
                moves.Add(new Move(start, targetSquare));

                
            }
        }        

    }
    private bool IsValidKnightMove(int start, int target)
    {
        if (target < 0 || target >=64) return false;
        int startRow = start / 8;   // Hàng ban đầu (0-7)
        int startCol = start % 8;   // Cột ban đầu (0-7)
        int targetRow = target / 8; // Hàng đích (0-7)
        int targetCol = target % 8; // Cột đích (0-7)

        // Tính khoảng cách giữa hàng và cột
        int rowDiff = Mathf.Abs(startRow - targetRow);
        int colDiff = Mathf.Abs(startCol - targetCol);

        // Quân Mã chỉ có thể di chuyển theo hình chữ "L": (2,1) hoặc (1,2)
        return (rowDiff == 2 && colDiff == 1) || (rowDiff == 1 && colDiff == 2);
    }
    private void GeneratePawnMove(int startSquare, int piece)
    {

        int dirIndex = Piece.IsColour(piece, Piece.White) ? 0 : 1;

        GenerateNormalMove(startSquare, dirIndex, true);
        GeneratePawnCapture(startSquare, piece, dirIndex);
        GeneratePawnInitMove(startSquare, piece, dirIndex);
    }
    private void GenerateNormalMove(int startSquare, int dirIndex, bool block)
    {
        int targetSquare = startSquare + DirectionOffset[dirIndex];
        GenerateNormalMove(startSquare, dirIndex);
    }
    private void GenerateNormalMove(int startSquare, int dirIndex)
    {
        if (NumSquaresToEdge[startSquare][dirIndex] == 0) return;
        int targetSquare = startSquare + DirectionOffset[dirIndex];
        if (Board.Square[targetSquare] != 0) return;
        moves.Add(new Move(startSquare, targetSquare));
    }

    private void GeneratePawnInitMove(int startSquare, int piece, int dirIndex)
    {
        
        bool isInit = Piece.IsInitPawn(startSquare, piece);
        if (isInit)
        {
            int targetSquare = startSquare + DirectionOffset[dirIndex]*2;
            moves.Add(new Move(startSquare, targetSquare));
        }
    }

    private void GeneratePawnCapture(int startSquare, int piece, int dirIndex)
    {
        int targetSquare = startSquare + DirectionOffset[dirIndex];
        if (NumSquaresToEdge[startSquare][dirIndex + 6] != 0)
        {
            int rightChess = Board.Square[targetSquare + 1];
            if (rightChess != 0 && NumSquaresToEdge[targetSquare][3] > 0)
            {
                if (Piece.Colour(piece) != Piece.Colour(rightChess))
                    moves.Add(new Move(startSquare, targetSquare + 1));
            }
        }
        if (NumSquaresToEdge[startSquare][dirIndex + 4] != 0)
        {
            int leftChess = Board.Square[targetSquare - 1];

            if (leftChess != 0 && NumSquaresToEdge[targetSquare][2] > 0)
            {
                if (Piece.Colour(piece) != Piece.Colour(leftChess))
                    moves.Add(new Move(startSquare, targetSquare - 1));
            }
        }

        
    }

    private void GenerateSlidingMove(int startSquare, int piece)
    {
        int startDirIndex = Piece.IsType(piece, Piece.Bishop) ? 4 : 0;
        int endDirIndex = Piece.IsType(piece, Piece.Rook) ? 4 : 8;

        for (int dir = startDirIndex; dir < endDirIndex; dir++)
        {
            
            //if (NumSquaresToEdge[startSquare][dir] == 0) return;
            for (int n = 0; n < NumSquaresToEdge[startSquare][dir]; n++)
            {
                int targetSquare = startSquare + DirectionOffset[dir] * (n+1);
                //if (!Board.IsInBoard(targetSquare)) return;
                int pieceOnTargetSquare = Board.Square[targetSquare]; 
                if (Piece.IsColour(pieceOnTargetSquare, friendlyColour))
                {
                    break;
                }
                moves.Add(new Move(startSquare, targetSquare));
                if (Piece.IsColour(pieceOnTargetSquare, opponentColour))
                {
                    break;
                }
            }
        }

    }

    public struct Move
    {
        public readonly int startSquare;
        public readonly int targetSquare;
        public Move(int startSquare, int targetSquare)
        {
            this.startSquare = startSquare;
            this.targetSquare = targetSquare;
        }
    }

}