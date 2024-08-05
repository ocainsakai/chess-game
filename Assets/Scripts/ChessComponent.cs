using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessComponent : MonoBehaviour
{
    public event EventHandler moving;
    public void OnMouseDown(){
        moving?.Invoke(this, EventArgs.Empty);
    }
    private void Simulate(Vector2 position){
        this.transform.position = position;
    }
}
