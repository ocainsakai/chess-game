using System.Collections.Generic;
using UnityEngine;

public class UnitPredictor : MonoBehaviour
{
    [SerializeField] private Transform predictPrefab => Resources.Load<Transform>("dot");
    private GridManager gridManager => FindFirstObjectByType<GridManager>();

    public void Predict(UnitData data)
    {
        foreach (Vector3Int item in GetValidMoves(data))
        {
            Instantiate(predictPrefab, gridManager.tilemap.CellToWorld(item), Quaternion.identity);
        }
    }
    public List<Vector3Int> GetValidMoves(UnitData data)
    {
        List<Vector3Int> validMoves = new List<Vector3Int>();
        Vector3Int currentPos = gridManager.tilemap.WorldToCell(transform.position);

        switch (data.pieceType)
        {
            case ChessPieceType.Pawn:
                validMoves = GetPawnMoves(currentPos, data);
                break;
            case ChessPieceType.Rook:
                validMoves = GetRookMoves(currentPos);
                break;
            case ChessPieceType.Knight:
                validMoves = GetKnightMoves(currentPos);
                break;
            case ChessPieceType.Bishop:
                validMoves = GetBishopMoves(currentPos);
                break;
            case ChessPieceType.Queen:
                validMoves = GetQueenMoves(currentPos);
                break;
            case ChessPieceType.King:
                validMoves = GetKingMoves(currentPos);
                break;
        }

        return validMoves;
    }

    private List<Vector3Int> GetPawnMoves(Vector3Int pos, UnitData data)
    {
        List<Vector3Int> moves = new List<Vector3Int>();

        // Hướng di chuyển của quân tốt (đi 1 ô về phía trước)
        int direction = data.isWhite ? 1 : -1;
        Vector3Int forwardMove = new Vector3Int(pos.x, pos.y + direction, 0);

        if (gridManager.IsCellEmpty(forwardMove))
        {
            moves.Add(forwardMove);
        }

        // Nước đi đầu tiên có thể đi 2 ô
        if ((data && pos.y == 2) || (!data && pos.y == 7))
        {
            Vector3Int doubleMove = new Vector3Int(pos.x, pos.y + 2 * direction, 0);
            if (gridManager.IsCellEmpty(doubleMove))
            {
                moves.Add(doubleMove);
            }
        }

        // Kiểm tra nước ăn chéo
        Vector3Int leftDiagonal = new Vector3Int(pos.x - 1, pos.y + direction, 0);
        Vector3Int rightDiagonal = new Vector3Int(pos.x + 1, pos.y + direction, 0);

        if (gridManager.IsEnemyPieceAt(leftDiagonal, data))
            moves.Add(leftDiagonal);
        if (gridManager.IsEnemyPieceAt(rightDiagonal, data))
            moves.Add(rightDiagonal);

        return moves;
    }

    private List<Vector3Int> GetRookMoves(Vector3Int pos)
    {
        return GetStraightMoves(pos);
    }

    private List<Vector3Int> GetBishopMoves(Vector3Int pos)
    {
        return GetDiagonalMoves(pos);
    }

    private List<Vector3Int> GetQueenMoves(Vector3Int pos)
    {
        List<Vector3Int> moves = new List<Vector3Int>();
        moves.AddRange(GetStraightMoves(pos));
        moves.AddRange(GetDiagonalMoves(pos));
        return moves;
    }

    private List<Vector3Int> GetKingMoves(Vector3Int pos)
    {
        List<Vector3Int> moves = new List<Vector3Int>();
        Vector3Int[] directions = {
            new Vector3Int(1, 0, 0), new Vector3Int(-1, 0, 0),
            new Vector3Int(0, 1, 0), new Vector3Int(0, -1, 0),
            new Vector3Int(1, 1, 0), new Vector3Int(-1, -1, 0),
            new Vector3Int(1, -1, 0), new Vector3Int(-1, 1, 0)
        };

        foreach (var dir in directions)
        {
            Vector3Int newPos = pos + dir;
            if (gridManager.IsCellValid(newPos))
                moves.Add(newPos);
        }

        return moves;
    }

    private List<Vector3Int> GetKnightMoves(Vector3Int pos)
    {
        List<Vector3Int> moves = new List<Vector3Int>();
        Vector3Int[] offsets = {
            new Vector3Int(2, 1, 0), new Vector3Int(2, -1, 0),
            new Vector3Int(-2, 1, 0), new Vector3Int(-2, -1, 0),
            new Vector3Int(1, 2, 0), new Vector3Int(1, -2, 0),
            new Vector3Int(-1, 2, 0), new Vector3Int(-1, -2, 0)
        };

        foreach (var offset in offsets)
        {
            Vector3Int newPos = pos + offset;
            if (gridManager.IsCellValid(newPos))
                moves.Add(newPos);
        }

        return moves;
    }

    private List<Vector3Int> GetStraightMoves(Vector3Int pos)
    {
        List<Vector3Int> moves = new List<Vector3Int>();
        Vector3Int[] directions = {
            new Vector3Int(1, 0, 0), new Vector3Int(-1, 0, 0),
            new Vector3Int(0, 1, 0), new Vector3Int(0, -1, 0)
        };

        foreach (var dir in directions)
        {
            Vector3Int newPos = pos + dir;
            while (gridManager.IsCellValid(newPos))
            {
                moves.Add(newPos);
                if (!gridManager.IsCellEmpty(newPos))
                    break;
                newPos += dir;
            }
        }

        return moves;
    }

    private List<Vector3Int> GetDiagonalMoves(Vector3Int pos)
    {
        List<Vector3Int> moves = new List<Vector3Int>();
        Vector3Int[] directions = {
            new Vector3Int(1, 1, 0), new Vector3Int(1, -1, 0),
            new Vector3Int(-1, 1, 0), new Vector3Int(-1, -1, 0)
        };

        foreach (var dir in directions)
        {
            Vector3Int newPos = pos + dir;
            while (gridManager.IsCellValid(newPos))
            {
                moves.Add(newPos);
                if (!gridManager.IsCellEmpty(newPos))
                    break;
                newPos += dir;
            }
        }

        return moves;
    }
}
