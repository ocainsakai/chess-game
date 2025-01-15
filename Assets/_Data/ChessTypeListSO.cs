using UnityEngine;

[CreateAssetMenu(fileName = "ChessTypeListSO", menuName = "Chess/ChessTypeListSO")]
public class ChessTypeListSO : ScriptableObject
{
    public ChessTypeSO[] pieces; // Danh sách các quân cờ
}
