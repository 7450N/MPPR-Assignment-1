using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SceneLoader loadScene;
    private string scene = "StartingMenu";
    void Awake()
    {
        
    }
    private void Start()
    {
        loadScene.LoadScene(scene);
    }
}
