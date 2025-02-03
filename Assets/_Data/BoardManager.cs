using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : SingletonPattern<BoardManager>
{
    [SerializeField] private float cellWidth = 5f;
    //[SerializeField] private GameObject prefabs;

    [SerializeField] private Dictionary<int, Vector3> board;
    [SerializeField] private Dictionary<int, int> chessPositions;
    public int[] Square ;
    protected override void Awake()
    {
        base.Awake();
        GenerateSquares();
    }
    private void Start()
    {
        InitGame();

    }
    private void InitGame()
    {
        chessPositions = UltilitiesFEN.LoadPosiotionFromFEN(UltilitiesFEN.startFEN);
        //Debug.Log(chessPositions.Count);
        foreach (var item in chessPositions)
        {
            //Debug.Log(item);
            int pos = item.Key;
            Transform piece = ChessManager.Instance.GetChess(item.Value);
            //Debug.Log(piece);
            Transform chess = Instantiate(piece, board[pos], this.transform.rotation);

        }
    }
    private void GenerateSquares()
    {
        board = new Dictionary<int, Vector3>();
        board.Clear();
        for (int file = 0; file < 8; file++)
        {
            for (int rank = 0; rank < 8; rank++)
            {
                board.Add(rank * 8 + file, new Vector3((file - 3.5f) * cellWidth, 0, (rank - 3.5f) * cellWidth));
            }
        }
    }

}
