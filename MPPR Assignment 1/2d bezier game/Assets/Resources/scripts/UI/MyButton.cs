using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isHovering = false;

    public RectTransform cursorUI; // Reference to the UI element acting as the cursor
    public float rotationSpeed = 200f; // Speed of rotation
    public float rotationSmoothing = 10f; // Interpolation smoothing factor

    private Vector3 targetRotation = Vector3.zero; // Target rotation as Vector3
    private Vector3 currentRotation = Vector3.zero; // Current rotation as Vector3

    void Update()
    {
        Debug.Log("Enter exit");
        if (isHovering)
        {
            // Update the target rotation around the Z-axis
            targetRotation.z += rotationSpeed * Time.deltaTime;
            
        }
        else
        {
            float times = Mathf.Ceil(targetRotation.z / 180f);           //calculate how many times semi-loop has passed
            float angleDiff = (180f * times) - targetRotation.z;         // angle needed to complete another semi-loo[
            targetRotation.z += angleDiff;
        }
        currentRotation = Interpolation.Lerp(currentRotation, targetRotation, Time.deltaTime * rotationSmoothing);
        cursorUI.rotation = Quaternion.Euler(currentRotation);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }
}
