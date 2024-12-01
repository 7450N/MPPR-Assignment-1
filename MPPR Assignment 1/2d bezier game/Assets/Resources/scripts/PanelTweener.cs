using UnityEngine;

public class PanelTweener : MonoBehaviour
{
    public RectTransform panel;            // Reference to the panel's RectTransform
    public Vector2 offScreenPosition = new Vector2(0, 1200); // Target position off the screen
    public float tweenDuration = 1.0f;     // Duration of the tween

    private Vector2 originalPosition;      // The starting position of the panel
    private float elapsedTime = 0f;        // Tracks the time elapsed during interpolation
    private bool isTweening = false;       // To control when the tweening starts

    void Start()
    {
        originalPosition = panel.anchoredPosition;  // Store the panel's initial position
    }

    void Update()
    {
        if (isTweening)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / tweenDuration);  // Normalize the time (0 to 1)

            // Interpolate the panel's position using Lerp
            panel.anchoredPosition = Vector2.Lerp(originalPosition, offScreenPosition, t);

            if (t >= 1f)  // Tweening complete
            {
                isTweening = false;  // Stop the tween
                StartGame();  // Trigger game start
            }
        }
    }

    // Called when the player presses the button to move the panel
    public void MovePanelUp()
    {
        if (!isTweening)
        {
            isTweening = true;   // Start the tween
            elapsedTime = 0f;    // Reset elapsed time
        }
    }

    // Placeholder for starting the game
    private void StartGame()
    {
        Debug.Log("Game Started!");  // Replace this with actual game start logic
    }
}
