using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpolation : MonoBehaviour
{
    public enum EasingType
    {
        Linear = 0,
        EaseIn,
        EaseOut,
        EaseInOut
    }


    public static Vector3 Lerp(Vector3 posA, Vector3 posB, float t, EasingType easingType = 0)
    {
        t = Mathf.Clamp01(t);
        // Apply an easing function to t
        switch (easingType)
        {
            case EasingType.Linear:
                break; // Linear easing means t stays unchanged
            case EasingType.EaseIn:
                t = t * t * t;
                break;
            case EasingType.EaseOut:
                t = 1 - Mathf.Pow((1 - t), 3);
                break;
            case EasingType.EaseInOut:
                if (t < 0.5)
                {
                    t = 4 * t * t * t;
                }
                else
                {
                    t = 1 - Mathf.Pow(-2 * t + 2, 3) / 2;
                }
                break;
        }

        Vector3 interpolatedPos = (1 - t) * posA + t * posB;
        return interpolatedPos;                             //return the interpolated value
    }

    public static float FloatLerp(float posA, float posB, float t, EasingType easingType = 0)
    {
        t = Mathf.Clamp01(t);
        // Apply an easing function to t
        switch (easingType)
        {
            case EasingType.Linear:
                break; // Linear easing means t stays unchanged
            case EasingType.EaseIn:
                t = t * t * t;
                break;
            case EasingType.EaseOut:
                t = 1 - Mathf.Pow((1 - t), 3);
                break;
            case EasingType.EaseInOut:
                if (t < 0.5)
                {
                    t = 4 * t * t * t;
                }
                else
                {
                    t = 1 - Mathf.Pow(-2 * t + 2, 3) / 2;
                }
                break;
        }

        float interpolatedPos = (1 - t) * posA + t * posB;
        return interpolatedPos;                             //return the interpolated value
    }
}
