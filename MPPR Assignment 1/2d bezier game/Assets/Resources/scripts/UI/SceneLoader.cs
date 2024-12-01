using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingSlider;

    public void LoadScene(string name)
    {
        StartCoroutine(ShowProgress(name));
    }

    IEnumerator ShowProgress(string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        loadingScreen.SetActive(true);


        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);             // as unity loading only use until 0.9 and use other 0.1 for activation
            loadingSlider.value = progress;

            yield return null;
        }
    }
}
