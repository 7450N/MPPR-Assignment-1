using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isHovering = false;

    public RectTransform cursorUI; // Reference to the UI element acting as the cursor
    public float rotationSpeed = 100f; // Speed of rotation
    public float rotationSmoothing = 5f; // Interpolation smoothing factor

    private Vector3 targetRotation = Vector3.zero; // Target rotation as Vector3
    private Vector3 currentRotation = Vector3.zero; // Current rotation as Vector3

    void Update()
    {
        if (isHovering)
        {
            // Update the target rotation around the Z-axis
            targetRotation.z += rotationSpeed * Time.deltaTime;
            targetRotation.z %= 360;
        }

        // Smoothly interpolate current rotation towards the target rotation
        currentRotation = Vector3.Lerp(currentRotation, targetRotation, Time.deltaTime * rotationSmoothing);

        // Apply the interpolated rotation to the cursor
        cursorUI.localRotation = Quaternion.Euler(currentRotation);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        Debug.Log("Hovering over button");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        Debug.Log("Stopped hovering over button");
    }
}
