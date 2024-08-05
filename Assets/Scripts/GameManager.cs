using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool onMoving {get; private set;}
    private void Awake(){
        if (Instance != null){
            Destroy(Instance);
        }
        Instance = this;
        onMoving = false;
    }
    public void SetMoving(bool state){
        onMoving = state;
    }
    private void Update(){
        if (Input.GetMouseButtonDown(0)){
            onMoving = true;
        }
        if (Input.GetMouseButtonUp(0)){
            onMoving = false;
        }
    }
}
