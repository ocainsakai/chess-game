using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] protected Transform cellTemplate;
    [SerializeField] protected Transform board;

    protected virtual void Reset()
    {
        cellTemplate = transform.Find("CellTemplate");
        board = transform.Find("Board");
    }
    protected virtual void Start()
    {
        GenerateBoard();
    }
    protected virtual void GenerateBoard()
    {
        for (int i = 0; i < 64; i++)
        {
            Transform cell = Instantiate(cellTemplate, transform.position, Quaternion.identity);
            cell.gameObject.SetActive(true);
            cell.SetParent(board);
        }
    }
}
