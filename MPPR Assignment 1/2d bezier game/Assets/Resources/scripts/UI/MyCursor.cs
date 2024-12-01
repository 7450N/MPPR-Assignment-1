using UnityEngine;

public class MyCursor : MonoBehaviour
{
    public RectTransform cursorUI; // Reference to the custom cursor UI element


    void Start()
    {
        // Hide the default cursor at the start
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        Vector2 cursorPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(cursorUI.parent as RectTransform, Input.mousePosition, null, out cursorPosition);
        cursorUI.anchoredPosition = cursorPosition;
    }

}