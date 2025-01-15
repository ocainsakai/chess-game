using UnityEngine;

[CreateAssetMenu(fileName = "ChessPieceData", menuName = "Chess/ChessPiece")]
public class ChessPieceSO : ScriptableObject
{
    public GameObject prefab; // Prefab của quân cờ
   
    public Vector2Int defaultPosition; // Vị trí mặc định (theo hàng cột)
}
