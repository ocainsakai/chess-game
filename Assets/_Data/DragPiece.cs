using UnityEngine;

public class DragPiece : MonoBehaviour
{
    bool dragging;
    int startSquare;
    int targetSquare;


    //bool isCapture;
    private void OnMouseDown()
    {
        dragging = true;
        startSquare = BoardUI.instance.GetSquare(this.transform.position);
    }
    private void OnMouseDrag()
    {
        if (dragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
            GameManager.instance.PredictionMove(startSquare);
        }
    }
    private void OnMouseUp() {
        dragging = false;
        targetSquare = BoardUI.instance.GetSquare(this.transform.position);
        GameManager.instance.Move(startSquare, targetSquare);
        this.transform.position = BoardUI.instance.GetCell(targetSquare).position;
        
    }
   
}
