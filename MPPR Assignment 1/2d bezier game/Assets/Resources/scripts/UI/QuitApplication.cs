using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Created by Jason

public class QuitApplication : MonoBehaviour
{
    // Start is called before the first frame update
    public void QuitGame()
    {
        // Closes the application
        Application.Quit();

        // For testing in the Unity editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
