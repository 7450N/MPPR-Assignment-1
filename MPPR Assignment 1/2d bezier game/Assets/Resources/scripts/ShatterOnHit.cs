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
                rb.velocity = Vector2.zero;

                StartCoroutine(ApplyEaseInForce(rb));

                fragment.transform.localScale = Vector3.one * fragmentSize;

                Destroy(fragment, fragmentLifetime);
            }
        }
        GetComponent<Renderer>().enabled = false;
        Destroy(gameObject, fragmentLifetime);
    }

    private IEnumerator ApplyEaseInForce(Rigidbody2D rb)
    {
        float duration = fragmentLifetime;
        float timeElapsed = 0f;
        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        Vector2 newPosition = rb.position + randomDirection * Random.Range(-6f, 6f);
        Vector2 startPosition = rb.position;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / duration;
            t = Mathf.Clamp01(t);
            t = 1 - Mathf.Pow(1 - t, 2);

            Vector2 interpolatedPosition = (1 - t) * startPosition + t * newPosition;
            //Vector2 interpolatedPosition = Interpolation.Lerp(startPosition, newPosition, t, Interpolation.EasingType.EaseOut);
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
