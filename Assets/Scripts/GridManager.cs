using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Dictionary<Vector3Int, UnitData> gridStatus = new Dictionary<Vector3Int, UnitData>();
    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    void Start()
    {
        // Khởi tạo grid trống
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            gridStatus[pos] = null; // false = ô trống
        }
    }
    public bool IsEnemyPieceAt(Vector3Int pos, UnitData unit)
    {
        if (!IsCellEmpty(pos))
        {
            if (unit.isWhite != gridStatus[pos].isWhite)
                return true;
        }
        return false;
    }
    // Kiểm tra ô có trống không
    public bool IsCellEmpty(Vector3Int cellPos)
    {
        return gridStatus.ContainsKey(cellPos) && !gridStatus[cellPos];
    }
    public bool IsCellValid(Vector3Int cellPos)
    {
        return gridStatus.ContainsKey(cellPos);
    }
    // Đánh dấu ô đã được chiếm
    public void OccupyCell(Vector3Int cellPos, UnitData unit)
    {
        Debug.Log(cellPos.ToString());
        if (gridStatus.ContainsKey(cellPos))
        {
            gridStatus[cellPos] = unit;
        }
    }
    public void FreeCell(Vector3Int cellPos)
    {
        if (gridStatus.ContainsKey(cellPos))
        {
            gridStatus[cellPos] = null;
        }
    }
}
