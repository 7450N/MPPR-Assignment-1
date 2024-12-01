using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Required for color manipulation

public class ShatterOnHit : MonoBehaviour
{
    // Panel reference
    public RectTransform panel;  // Panel to move in
    public Vector2 offScreenPosition = new Vector2(-1000, 0);  // Offscreen position (left)
    public Vector2 onScreenPosition = new Vector2(0, 0);  // Onscreen position (center)
    public float tweenDuration = 1.5f;  // Duration for the tween

    // Color-related
    public Image panelImage;  // Panel background image for color fade
    public Color startColor = new Color(1, 1, 1, 0);  // Transparent
    public Color endColor = new Color(1, 1, 1, 1);  // Fully visible

    // Fragment details
    public int fragmentCount = 4;
    public float fragmentLifetime = 2f;
    public MoveAlongBezierCurve moveBezier;
    public Powerups powerups;
    public bool phase = false;
    public float phaseDuration = 0.5f;

    // Tweening control
    private bool isPanelMoving = false;
    private float panelTimeElapsed = 0.0f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!phase && collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with enemy");
            Shatter();
            moveBezier.movement = false;
            StartPanelTween();  // Trigger the game-over animation
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
            t = 1 - Mathf.Pow(1 - t, 2);  // Ease-in quadratic interpolation

            Vector2 interpolatedPosition = (1 - t) * startPosition + t * newPosition;
            rb.position = interpolatedPosition;

            yield return null;
        }
    }

    private void StartPanelTween()
    {
        isPanelMoving = true;
        panelTimeElapsed = 0.0f;  // Reset time counter
        panel.anchoredPosition = offScreenPosition;  // Start offscreen
        if (panelImage != null) panelImage.color = startColor;  // Set initial transparency
    }

    private void Update()
    {
        if (isPanelMoving)
        {
            panelTimeElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(panelTimeElapsed / tweenDuration);

            // Ease-in-out quadratic easing for smoother movement
            float easedT = EaseInOutQuad(t);

            // Position interpolation
            panel.anchoredPosition = Vector2.Lerp(offScreenPosition, onScreenPosition, easedT);

            // Color interpolation for fading
            if (panelImage != null)
            {
                panelImage.color = Color.Lerp(startColor, endColor, easedT);
            }

            // Stop animation once complete
            if (t >= 1.0f)
            {
                isPanelMoving = false;
            }
        }

        if (phase)
        {
            StartCoroutine(PhaseDuration());
        }
    }

    private float EaseInOutQuad(float t)
    {
        return t < 0.5f ? 2 * t * t : 1 - Mathf.Pow(-2 * t + 2, 2) / 2;
    }

    IEnumerator PhaseDuration()
    {
        yield return new WaitForSeconds(phaseDuration);
        phase = false;
        Debug.Log("Phase deactivated");
    }
}
