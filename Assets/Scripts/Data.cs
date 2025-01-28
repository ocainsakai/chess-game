using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public static Dictionary<string, List<Vector3Int>> init = new Dictionary<string, List<Vector3Int>>
    {
        { "BlackPawn", new List<Vector3Int> {
            new Vector3Int(1,7,0),
            new Vector3Int(2,7,0),
            new Vector3Int(3,7,0),
            new Vector3Int(4,7,0),
            new Vector3Int(5,7,0),
            new Vector3Int(6,7,0),
            new Vector3Int(7,7,0),
            new Vector3Int(8,7,0)
        }},

        { "WhitePawn", new List<Vector3Int> {
            new Vector3Int(1,2,0),
            new Vector3Int(2,2,0),
            new Vector3Int(3,2,0),
            new Vector3Int(4,2,0),
            new Vector3Int(5,2,0),
            new Vector3Int(6,2,0),
            new Vector3Int(7,2,0),
            new Vector3Int(8,2,0)
        }},

        { "BlackBishop", new List<Vector3Int> {
            new Vector3Int(3,8,0),
            new Vector3Int(6,8,0)
        }},

        { "BlackRook", new List<Vector3Int> {
            new Vector3Int(1,8,0),
            new Vector3Int(8,8,0)
        }},

        { "BlackKnight", new List<Vector3Int> {
            new Vector3Int(2,8,0),
            new Vector3Int(7,8,0)
        }},

        { "WhiteBishop", new List<Vector3Int> {
            new Vector3Int(3,0,0),
            new Vector3Int(6,0,0)
        }},

        { "WhiteRook", new List<Vector3Int> {
            new Vector3Int(1,0,0),
            new Vector3Int(8,0,0)
        }},

        { "WhiteKnight", new List<Vector3Int> { 
            new Vector3Int(2,0,0),
            new Vector3Int(7,0,0)
        }}
    };

    public static Vector3Int BlackKing = new Vector3Int(5, 8, 0);
    public static Vector3Int BlackQueen = new Vector3Int(4, 8, 0);
    public static Vector3Int WhiteKing = new Vector3Int(5, 0, 0);
    public static Vector3Int WhiteQueen = new Vector3Int(4, 0, 0);
}
