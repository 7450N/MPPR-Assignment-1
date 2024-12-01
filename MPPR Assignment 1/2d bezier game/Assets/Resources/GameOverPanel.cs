using UnityEngine;
using UnityEngine.UI;  // Required for UI components

public class GameOverPanel : MonoBehaviour
{
    public RectTransform panel;  // Panel's RectTransform to animate
    public Vector2 offScreenPosition = new Vector2(-1000, 0);  // Start off-screen position
    public Vector2 onScreenPosition = new Vector2(0, 0);  // Target on-screen position

    public Color startColor = Color.red;  // Initial color for the panel
    public Color endColor = Color.white;  // Final color for the panel

    public float animationDuration = 1.5f;  // Duration for the animation
    private float timeElapsed = 0f;  // Timer to track animation progress
    private bool isAnimating = false;  // To trigger the animation

    private Image panelImage;  // Reference to the panel's Image component

    void Start()
    {
        panel.anchoredPosition = offScreenPosition;  // Start off-screen
        panelImage = panel.GetComponent<Image>();
        panelImage.color = startColor;  // Initialize panel color
    }

    void Update()
    {
        if (isAnimating)
        {
            timeElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapsed / animationDuration);

            // Apply ease-in-out quadratic easing for smooth animation
            t = EaseInOutQuad(t);

            // Interpolate position
            panel.anchoredPosition = Vector2.Lerp(offScreenPosition, onScreenPosition, t);

            // Interpolate color
            panelImage.color = Color.Lerp(startColor, endColor, t);

            // Stop the animation when it completes
            if (t >= 1f)
            {
                isAnimating = false;
            }
        }
    }

    // Public method to trigger the Game Over animation
    public void StartGameOverAnimation()
    {
        isAnimating = true;
        timeElapsed = 0f;  // Reset timer for a fresh animation start
    }

    // Easing function: Ease-In-Out Quadratic
    private float EaseInOutQuad(float t)
    {
        return t < 0.5f ? 2 * t * t : 1 - Mathf.Pow(-2 * t + 2, 2) / 2;
    }
}
