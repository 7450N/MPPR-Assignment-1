using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Created by Jason

public class GoToMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SceneLoader loadScene;      
    private string scene = "StartingMenu";      //adding loading scene before the main menu

    private void Start()
    {
        loadScene.LoadScene(scene);
    }
}
