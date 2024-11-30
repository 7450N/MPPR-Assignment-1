using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public MoveAlongBezierCurve moveBezier;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {//makes the lose screen visible and pauses the game
            Debug.Log("Player Lost");
            moveBezier.movement = false;
        }
    }
}
