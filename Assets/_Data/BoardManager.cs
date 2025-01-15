using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : SingletonPattern<BoardManager>
{
    [SerializeField] private Dictionary< Vector2Int, RectTransform> cells;
    protected override void Awake()
    {
        base.Awake();
        cells = new Dictionary<Vector2Int, RectTransform>();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Transform child = this.transform.GetChild(i + j*8);
                Color color = (i+j)%2 == 0 ? Color.black : Color.white;
                child.GetComponent<Image>().color = color;  
                cells.Add( new Vector2Int(i, j),(RectTransform) child); 
            }
        }
    }

    public RectTransform GetCell(Vector2Int position)
    {
        return cells.TryGetValue(position, out var cell) ? cell : null;
    }
}
