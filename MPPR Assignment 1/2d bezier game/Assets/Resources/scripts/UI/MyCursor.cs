using UnityEngine;

//Created by Jason

public class MyCursor : MonoBehaviour
{
    public RectTransform cursorUI; // reference to the cursor image 


    void Start()
    {
        // Hide the default cursor at the start
        Cursor.visible = false;                  //disable the default cursor.
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        Vector2 cursorPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(cursorUI.parent as RectTransform, Input.mousePosition, null, out cursorPosition);    //to convert screen-space position(input.mouse position) to local position
        cursorUI.anchoredPosition = cursorPosition;          //set the position of cursor image which will act as cursor which is invisible
    }

}