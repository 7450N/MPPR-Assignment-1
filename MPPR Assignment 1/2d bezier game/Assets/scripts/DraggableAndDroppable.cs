using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableAndDroppable : MonoBehaviour
{
    public MoveAlongBezierCurve moveBezier;
    private bool isDragging;

    private void OnMouseDown()
    {
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void Update()
    {
        if (isDragging && !moveBezier.movement)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }
    }
}

