using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{    
    public static MovementManager Instance;
    private void Awake(){
        if (Instance != null){
            Destroy(Instance);
        }
        Instance = this;
    }

    public void Sub(ChessComponent chess){
        chess.moving += StartMove;
    }

    private void StartMove(object sender, EventArgs e)
    {
        Debug.Log("start move");
    }
    private void EndMove(){
        
    }
}
