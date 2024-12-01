using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoveAlongBezierCurve : MonoBehaviour
{
    public BezierCurve bezierCurve;   
    public float travelSpeed = 2f;    
    public Transform player;
    public bool movement = false;
    public float t = 0f;

    void Update()
    {
        if (bezierCurve == null || bezierCurve.controlPoints.Count < 2)
        {
            Debug.LogError("BezierCurve or control points not set.");
            return;
        }

        if (movement == true)
        {        
            Vector3 position = bezierCurve.CalculateBezierPoint(t, bezierCurve.controlPoints);
            t += (travelSpeed/100) * Time.deltaTime / bezierCurve.resolution;
            if (player != null)
            {
                player.position = position;
            }    
        }


        if (t > 1f)
        {
            movement = false;
        }
    }
    public void StartMovement()
    {
        t = 0f;
        movement = true;
        Debug.Log("Button Pressed");
    }


}
