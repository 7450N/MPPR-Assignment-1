using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorUI : MonoBehaviour
{
    public Texture2D customCursor;                  //texture of mouse cursor
    public RectTransform cursorUI;
    [SerializeField] private float rotationSpeed = 100f;           //rotation speed of cursor when hover

    void Start()
    {
        Cursor.SetCursor(customCursor, Vector2.zero, CursorMode.Auto);         //Set texture
        Cursor.visible = false;                                                // hide cursor
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cursorPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            cursorUI.parent as RectTransform,
            Input.mousePosition,
            null,
            out cursorPos);

        cursorUI.anchoredPosition = cursorPos;
    }
}
