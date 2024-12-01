using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingSlider;
    public float fillSpeed = 1f;
    public float minLoadTime = 2f;        //no instant loading screen for better ux. 2s should be fine.

    public void LoadScene(string name)
    {
        StartCoroutine(ShowProgress(name));
    }

    IEnumerator ShowProgress(string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        loadingScreen.SetActive(true);
        float elapsedTime = 0f;
        while ((!operation.isDone) || (elapsedTime < minLoadTime))
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(operation.progress / 0.9f);             // as unity loading only use until 0.9 and use other 0.1 for activation
            float interPolatedProgress = Interpolation.FloatLerp(loadingSlider.value, progress, fillSpeed);
            loadingSlider.value = interPolatedProgress;

            yield return null;
        }
    }
}
