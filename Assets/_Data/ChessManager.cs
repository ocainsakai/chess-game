using UnityEngine;

public class ChessManager : SingletonPattern<ChessManager>
{
    public ChessTypeListSO chessSet => Resources.Load<ChessTypeListSO>("ListSO/" + nameof(ChessTypeListSO));
    private Transform chessPiecePrefab => Resources.Load<Transform>("Prefabs/ChessPiece");

    private void Start()
    {
        InitPawn();
    }
    private void InitPawn()
    {
        for (int i = 0; i < 8; i++)
        {
            ChessPiece newBlack = Instantiate(chessPiecePrefab).GetComponent<ChessPiece>();
            newBlack.SetPosition(new Vector2Int(6, i)); 
        }
    }
    
}
