using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterOnHit : MonoBehaviour
{
    // Panel reference
    public RectTransform panel;  // Panel to move down
    public Vector2 offScreenPosition = new Vector2(0, 1000);  // Offscreen position
    public Vector2 onScreenPosition = new Vector2(0, 0);  // Onscreen position
    public float tweenDuration = 1.0f;  // Duration for the tween

    // Fragment details
    public int fragmentCount = 4;
    public float fragmentLifetime = 2f;
    public MoveAlongBezierCurve moveBezier;
    public Powerups powerups;
    public bool phase = false;
    public float phaseDuration = 0.5f;

    // Tweening flag
    private bool isPanelMoving = false;
    private float panelTimeElapsed = 0.0f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Phase value: " + phase);
        if (!phase && collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with enemy");
            Shatter();
            moveBezier.movement = false;
            StartPanelTween();  // Start the panel movement
        }
    }

    private void Shatter()
    {   // Get the size of the player sprite
        Vector2 size = GetComponent<Renderer>().bounds.size;

        // Retrieve the sprite and color of the current object
        Sprite playerSprite = GetComponent<SpriteRenderer>().sprite;
        Color playerColor = GetComponent<SpriteRenderer>().color;

        // Calculate size of the fragments
        float fragmentSize = size.x / fragmentCount;

        // Calculate the starting position for fragment placement (top-left corner of the object's bounds)
        Vector2 startPos = (Vector2)transform.position - (Vector2.right + Vector2.up) * size / 2;

        // Loop through a grid to create fragments
        for (int i = 0; i < fragmentCount; i++)
        {
            for (int j = 0; j < fragmentCount; j++)
            {
                // Calculate the position of each fragment in the grid
                Vector2 position = startPos + new Vector2(i, j) * fragmentSize;

                // Create a new GameObject to represent the fragment
                GameObject fragment = new GameObject("Fragment");
                fragment.transform.position = position;

                // Set the same sprite and color as the parent object;
                SpriteRenderer sr = fragment.AddComponent<SpriteRenderer>();
                sr.sprite = playerSprite;
                sr.color = playerColor;

                // Add a Rigidbody2D to make the fragment movable
                Rigidbody2D rb = fragment.AddComponent<Rigidbody2D>();
                rb.gravityScale = 0;  // Disable gravity as it will not be used in this game
                rb.velocity = Vector2.zero;

                // Start a coroutine to apply an "ease-in" force to the fragment
                StartCoroutine(ApplyEaseInForce(rb));

                // Set the fragment's size by scaling it down
                fragment.transform.localScale = Vector3.one * fragmentSize;

                Destroy(fragment, fragmentLifetime);
            }
        }
        // Disable the current object's Renderer to make it invisible during shatter period
        GetComponent<Renderer>().enabled = false;

        // Destroy the original object after all fragments are gone
        Destroy(gameObject, fragmentLifetime);
    }

    private IEnumerator ApplyEaseInForce(Rigidbody2D rb)
    {
        float duration = fragmentLifetime;
        float timeElapsed = 0f;

        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;  // Choose a random direction for the fragment's movement
        Vector2 newPosition = rb.position + randomDirection * Random.Range(-6f, 6f);    // Calculate the final position based on the random direction and range
        Vector2 startPosition = rb.position;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / duration;
            t = Mathf.Clamp01(t);

            // Interpolate the position using a custom easing function (EaseOut)
            Vector2 interpolatedPosition = Interpolation.Lerp(startPosition, newPosition, t, Interpolation.EasingType.EaseOut);
            rb.position = interpolatedPosition;

            yield return null;
        }
    }

    // Start the panel tween when triggered
    private void StartPanelTween()
    {
        isPanelMoving = true;
        panelTimeElapsed = 0.0f;  // Reset time counter
    }

    private void Update()
    {
        if (isPanelMoving)
        {
            panelTimeElapsed += Time.deltaTime;
            float t = panelTimeElapsed / tweenDuration;
            panel.anchoredPosition = Vector2.Lerp(onScreenPosition, offScreenPosition, t);

            if (t >= 1.0f)
            {
                isPanelMoving = false;  // Stop panel movement when finished
            }
        }

        if (phase)
        {
            StartCoroutine(PhaseDuration());
        }
    }   

    IEnumerator PhaseDuration()
    {
        yield return new WaitForSeconds(phaseDuration);
        phase = false;
        Debug.Log("Phase deactivated");
    }
}
