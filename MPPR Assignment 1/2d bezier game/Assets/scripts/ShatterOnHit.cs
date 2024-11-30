using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterOnHit : MonoBehaviour
{
    //public MoveAlongBezierCurve moveBezier;
    public int fragmentCount = 4;
    public float fragmentLifetime = 2f;

     void OnTriggerEnter2D(Collider2D collision)  
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with enemy");
            Shatter();
        }
    }

    private void Shatter()
    {
        Vector2 size = GetComponent<Renderer>().bounds.size;
        Sprite playerSprite = GetComponent<SpriteRenderer>().sprite;
        Color playerColor = GetComponent<SpriteRenderer>().color;
 
        float fragmentSize = size.x / fragmentCount;
        Vector2 startPos = (Vector2)transform.position - (Vector2.right + Vector2.up) * size / 2;
        for (int i = 0; i < fragmentCount; i++)
        {
            for (int j = 0; j < fragmentCount; j++)
            {
                Vector2 position = startPos + new Vector2(i, j) * fragmentSize;

                GameObject fragment = new GameObject("Fragment");
                fragment.transform.position = position;

                SpriteRenderer sr = fragment.AddComponent<SpriteRenderer>();
                sr.sprite = playerSprite;
                sr.color = playerColor;

                Rigidbody2D rb = fragment.AddComponent<Rigidbody2D>();
                rb.gravityScale = 0;
                //rb.AddForce(new Vector2(Random.Range(-3f,3f), Random.Range(-3f,3f)) * 5f, ForceMode2D.Impulse);
                float t = Random.Range(0.0f, 1.0f);
                float easedForce = 1 - Mathf.Pow(1 - t, 2);
                Vector2 randomForce = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f)) * 4f * easedForce;
                rb.AddForce(randomForce, ForceMode2D.Impulse);
                    
                fragment.transform.localScale = Vector3.one * fragmentSize;

                Destroy(fragment, fragmentLifetime);

            }
        }

        //Destroy(gameObject);
    }
}
