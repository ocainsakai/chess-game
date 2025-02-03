using UnityEngine;

public static class PrecomputedMoveData
{
    public static readonly int[] DirectionOffset = {8, -8, -1, 1 , 7, -7, -9, 9};
    public static readonly int[][] NumSquaresToEdge;

    static PrecomputedMoveData()
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
}
