using UnityEngine.SceneManagement;
using UnityEngine;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    public void ResetGame()//resets the level
    {
        Debug.Log("Rewinded");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
