using UnityEditor.ShortcutManagement;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    [SerializeField] public Vector2Int position {  get; private set; }
    [SerializeField] public ChessTypeSO typeSO;

    public void SetPosition(Vector2Int position)
    {
        this.position = position;
        this.transform.SetParent(BoardManager.Instance.GetCell(position), worldPositionStays: true);
        
    }
}
