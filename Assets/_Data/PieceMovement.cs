
using System.Collections.Generic;
using UnityEngine;

public class PieceMovement
{

    public List<Move> moves { get; private set; } 

    public static readonly int[] DirectionOffset = { 8, -8, -1, 1, 7, -7, -9, 9 };
    public static readonly int[][] NumSquaresToEdge = new int[64][];

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

                NumSquaresToEdge[squareIndex] = new int[] {numNorth, numSouth, numWest, numEast,
                Mathf.Min(numNorth, numWest),
                Mathf.Min(numSouth, numEast),
                Mathf.Min(numNorth, numEast),
                Mathf.Min(numSouth, numWest),

                };
            }
        }
    }
    public List<Move> GenerateMove()
    {
        //PrecomputedMoveData();
        moves = new List<Move>();
        for (int startSquare = 0; startSquare < 64; startSquare++)
        {
            int piece = Board.Square[startSquare];
            if (Piece.IsColour(piece, Board.ColourToMove))
            {
                if (Piece.IsSlidingPiece(piece))
                {

                } else if (Piece.IsType(piece, Piece.Knight))
                {

                } else if (Piece.IsType(piece, Piece.Pawn))
                {
                    GeneratePawnMove(startSquare, piece);
                }
            }
        }
        return moves;
    }
    private void GeneratePawnMove(int startSquare, int piece)
    {
        int dirIndex = Piece.IsColour(piece, Piece.White) ? 0 : 1;
        int targetSquare = startSquare + DirectionOffset[dirIndex];
        moves.Add(new Move(startSquare, targetSquare));
        bool isInit = Piece.IsInitPawn(startSquare, piece);
        if (isInit)
        {
            targetSquare += DirectionOffset[dirIndex];
            moves.Add(new Move(startSquare, targetSquare));
        }
    }
    private void GenerateSlidingMove(int startSquare, int piece)
    {
        int startDirIndex = Piece.IsType(piece, Piece.Bishop) ? 4 : 0;
        int endDirIndex = Piece.IsType(piece, Piece.Rook) ? 4 : 7;
        int friendlyColour = piece & (Piece.White | Piece.Black);
        int opponentColour = (friendlyColour == Piece.White) ? Piece.Black : Piece.White;

        for (int dir = startDirIndex; dir < endDirIndex; dir++)
        {
    
            for (int n = 0; n < NumSquaresToEdge[startSquare][dir]; n++)
            {
                int targetSquare = startSquare + DirectionOffset[dir] * (n+1);
                Debug.Log("tar "+ targetSquare); 
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