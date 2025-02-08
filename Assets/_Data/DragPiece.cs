using UnityEngine;

public class DragPiece : MonoBehaviour
{
    bool dragging;
    int startSquare;
    int targetSquare;


    //bool isCapture;
    private void OnMouseDown()
    {
        startSquare = BoardUI.instance.GetSquare(this.transform.position);
        if ( GameManager.instance.PredictionMove(startSquare).Length == 0) return;
        dragging = true;
    }
    private void OnMouseDrag()
    {
        if (dragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        }
    }
    private void OnMouseUp() {
        dragging = false;
        targetSquare = BoardUI.instance.GetSquare(this.transform.position);
        GameManager.instance.Move(startSquare, targetSquare, this.transform);
    }
   
}
