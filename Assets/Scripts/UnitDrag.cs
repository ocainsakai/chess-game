using UnityEngine;

public class UnitDrag : MonoBehaviour
{
    private UnitData data => GetComponent<Unit>().data;

    private bool isDragging = false;
    private Vector3 startPos;
    private UnitPredictor unitPredictor => GetComponent<UnitPredictor>();
    private GridManager gridManager => FindFirstObjectByType<GridManager>();
    //private Vector3Int cellPos => gridManager.tilemap.WorldToCell(transform.position);
   

    void OnMouseDown()
    {
        isDragging = true;
        startPos = transform.position;
        unitPredictor.Predict(data);
    }
    private void Predict()
    {
        foreach (var item in unitPredictor.GetValidMoves(data))
        {
            
        }
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        Vector3Int cellPos = gridManager.tilemap.WorldToCell(transform.position);

        Vector3Int beginPos = gridManager.tilemap.WorldToCell(startPos);

        if (gridManager.IsCellEmpty(cellPos))
        {
            ModifyPosition();
            gridManager.OccupyCell(cellPos, data);
            gridManager.FreeCell(beginPos);

        }
        else
        {
            transform.position = startPos; // Trả về vị trí cũ nếu ô đã đầy
        }
    }
    [ExecuteInEditMode]
    private void OnDrawGizmos()
    {
        if (gridManager == null || gridManager.tilemap == null)
        {
            return;
        }
        ModifyPosition();
    }
    private void ModifyPosition()
    {
        Vector3Int cellPos = gridManager.tilemap.WorldToCell(transform.position);
        transform.position = gridManager.tilemap.GetCellCenterWorld(cellPos);
    }
}
