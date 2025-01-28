using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    private List<Unit> units;
    private Transform unitPrefab;
    private void Awake()
    {
        InitChess();
    }

    private void InitChess()
    {
        foreach(var chessType in Data.init)
        {

            foreach (var position in chessType.Value)
            {
                
            }
        }
    }
}
