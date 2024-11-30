using UnityEngine;

public class EasingMovements : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float duration = 2.0f;
    public EasingType easingType = EasingType.Linear;
    public bool loop = false;
    public bool pingPong = false;

    private float elapsedTime = 0.0f;
    private bool isReversing = false; // For ping-pong behavior

    // Enumeration for easing types
    public enum EasingType { Linear, EaseIn, EaseOut, EaseInOut, EaseInOutBack }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / duration;
        t = Mathf.Clamp01(t);

        // Apply the easing function
        t = ApplyEasing(t);

        // Interpolate between pointA and pointB
        Vector3 startPos = isReversing ? pointB.position : pointA.position;
        Vector3 endPos = isReversing ? pointA.position : pointB.position;
        transform.position = Vector3.Lerp(startPos, endPos, t);

        // Handle looping or ping-pong behavior
        if (elapsedTime >= duration)
        {
            elapsedTime = 0.0f;
            if (pingPong) isReversing = !isReversing;
            if (!loop && !pingPong) transform.position = pointB.position;
        }
    }

    // Applies the selected easing function to t
    private float ApplyEasing(float t)
    {
        switch (easingType)
        {
            case EasingType.Linear: return t;
            case EasingType.EaseIn: return EaseInCubic(t);
            case EasingType.EaseOut: return EaseOutCubic(t);
            case EasingType.EaseInOut: return EaseInOutCubic(t);
            case EasingType.EaseInOutBack: return EaseInOutBack(t);
            default: return t;
        }
    }

    // Cubic ease-in (t^3)
    private float EaseInCubic(float t)
    {
        return t * t * t;
    }

    // Cubic ease-out (1 - (1 - t)^3)
    private float EaseOutCubic(float t)
    {
        return 1f - Mathf.Pow(1f - t, 3f);
    }

    // Cubic ease-in-out
    private float EaseInOutCubic(float t)
    {
        return t < 0.5f ? 4f * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 3f) / 2f;
    }

    // Ease-in-out back (with overshoot effect)
    private float EaseInOutBack(float t)
    {
        float c1 = 1.70158f;
        float c2 = c1 * 1.525f;
        return t < 0.5f
            ? (Mathf.Pow(2f * t, 2f) * ((c2 + 1f) * 2f * t - c2)) / 2f
            : (Mathf.Pow(2f * t - 2f, 2f) * ((c2 + 1f) * (2f * t - 2f) + c2) + 2f) / 2f;
    }

    // Draw Gizmos to visualize the path
    private void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(pointA.position, 0.2f);

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(pointB.position, 0.2f);

            Gizmos.color = Color.green;
            int steps = 20;
            for (int i = 0; i <= steps; i++)
            {
                float t = i / (float)steps;
                t = ApplyEasing(t);
                Vector3 interpolatedPosition = Vector3.Lerp(pointA.position, pointB.position, t);
                Gizmos.DrawSphere(interpolatedPosition, 0.1f);
            }
        }
    }
}
