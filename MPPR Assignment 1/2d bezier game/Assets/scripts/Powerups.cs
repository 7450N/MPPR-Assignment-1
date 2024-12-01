using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    public ShatterOnHit shatter;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shatter.phase = true;
            Destroy(gameObject);
            Debug.Log("Phase");
        }
    }
}
