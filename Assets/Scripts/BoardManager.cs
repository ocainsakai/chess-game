using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class BoardManager : MonoBehaviour
{
    private float SQUARE_SIZE => transform.Find("sprite").GetComponent<SpriteRenderer>().size.x / 9.2f;
    private float OFFSET_AMOUNT => SQUARE_SIZE / 0.6f;
    ChessPieceSetSO blackSet => Resources.Load<ChessPieceSetSO>("ChessSet/BlackSet");
    ChessPieceSetSO whiteSet => Resources.Load<ChessPieceSetSO>("ChessSet/WhiteSet");
    Dictionary<Vector2, Transform> chessPositionDict;
    public enum ChessColor{
        white, black
    }
    void Awake(){
        SetUpPiece();
        
    }
    float GetRow(int x){
        return this.transform.position.y * (x+1) + OFFSET_AMOUNT;
    }
    float GetCol(int y){
        return this.transform.position.y * (y+1) + OFFSET_AMOUNT;
    }
    Vector2 GetSquarePosition(int x, int y){
        return new Vector2 (GetCol(x), GetRow(y));
    }
    void CreateChess(int col, int row, ChessPieceSO chessPiece){
        Vector2 position =  GetSquarePosition(col,row);
        Transform chessTf = Instantiate(chessPiece.prefab,position , Quaternion.identity).transform;
        chessPositionDict[position] = chessTf;
        MovementManager.Instance.Sub(chessTf.GetComponent<ChessComponent>());
    }   
    void SetUpPiece(){
        chessPositionDict = new Dictionary<Vector2, Transform>();
        foreach (ChessPieceSO chessPieceSO in blackSet.pieces)
        {
            int row = 8;
            switch (chessPieceSO.chessType)
            {
                case ChessType.pawn:
                    for (int i = 0; i < 8; i++)
                    {
                        CreateChess(i, row-2,chessPieceSO);
                    }
                    break;
                case ChessType.king:
                    CreateChess(4, row-1,chessPieceSO);
                    break;
                case ChessType.queen:
                    CreateChess(3, row-1,chessPieceSO);
                    break;
                case ChessType.bishop:
                    CreateChess(2, row-1,chessPieceSO);
                    CreateChess(5, row-1,chessPieceSO);
                    break;
                case ChessType.rook:
                    CreateChess(0, row-1,chessPieceSO);
                    CreateChess(7, row-1,chessPieceSO);
                    break;
                case ChessType.knight:
                    CreateChess(1, row-1,chessPieceSO);
                    CreateChess(6, row-1,chessPieceSO);
                    break;
            }
        }

        foreach (ChessPieceSO chessPieceSO in whiteSet.pieces)
        {
            int row = 1;
            switch (chessPieceSO.chessType)
            {
                case ChessType.pawn:
                    for (int i = 0; i < 8; i++)
                    {
                        CreateChess(i, row,chessPieceSO);
                    }
                    break;
                case ChessType.king:
                    CreateChess(4, row-1,chessPieceSO);
                    break;
                case ChessType.queen:
                    CreateChess(3, row-1,chessPieceSO);
                    break;
                case ChessType.bishop:
                    CreateChess(2, row-1,chessPieceSO);
                    CreateChess(5, row-1,chessPieceSO);
                    break;
                case ChessType.rook:
                    CreateChess(0, row-1,chessPieceSO);
                    CreateChess(7, row-1,chessPieceSO);
                    break;
                case ChessType.knight:
                    CreateChess(1, row-1,chessPieceSO);
                    CreateChess(6, row-1,chessPieceSO);
                    break;
            }
        
        }        
    }
}
