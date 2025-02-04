﻿using UnityEngine;

public class BoardUI : MonoBehaviour
{
    public static BoardUI instance;
    private Transform[] square; // contain piece tranfrom
    public Color lightColor = Color.white;
    public Color darkColor = Color.black;
    public float cellWidth = 1f;

    [SerializeField] Transform cellPrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
        square = new Transform[64];
        for (int file = 0; file < 8; file++)
        {
            for (int rank = 0; rank < 8; rank++)
            {
                Vector3 position = new Vector3((file - 3.5f) * cellWidth,(rank - 3.5f) * cellWidth, 0);
                Transform newCell = Instantiate(cellPrefab, position, Quaternion.identity);
                newCell.gameObject.SetActive(true);
                newCell.SetParent(this.transform);
                newCell.GetComponent<SpriteRenderer>().color = (file+rank)%2==0 ? darkColor : lightColor;
                newCell.name = (rank * 8 + file).ToString();
                square[rank * 8 + file] = newCell;
            }
        }
    }
    public void SetColor(int index)
    {
        square[index].GetComponent<SpriteRenderer>().color = Color.red;
    }
    public Transform GetCell(int index)
    {
        return square[index];
    }
    public Transform GetNearestCell(Vector3 chessPosition)
    {
        int index = GetSquare(chessPosition);

        return square[index];
    }
    public int GetSquare(Vector3 chessPosition)
    {

        int file = Mathf.Clamp(Mathf.RoundToInt(chessPosition.x / cellWidth + 3.5f), 0, 7);
        int rank = Mathf.Clamp(Mathf.RoundToInt(chessPosition.y / cellWidth + 3.5f), 0, 7);

        return rank * 8 + file;
    }
}
