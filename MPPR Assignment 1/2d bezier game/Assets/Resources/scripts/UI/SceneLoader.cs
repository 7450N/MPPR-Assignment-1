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
    public AudioManager audioManager;

    public void LoadScene(string name)
    {
        if (audioManager != null)
        {
            StartCoroutine(audioManager.FadeOutAudio());
        }
        else
        {
            Debug.Log("Audio is not attache!");
        }
        StartCoroutine(ShowProgress(name));
    }

    IEnumerator ShowProgress(string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        operation.allowSceneActivation = false; // Prevent scene activation until we're ready
        loadingScreen.SetActive(true);

        float elapsedTime = 0f;

        while (elapsedTime < minLoadTime || operation.progress < 0.9f)
        {
            elapsedTime += Time.deltaTime;

            // Calculate progress (Unity loading progresses up to 0.9)
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            // Smoothly interpolate the progress bar
            float interpolatedProgress = Interpolation.FloatLerp(loadingSlider.value, progress, fillSpeed * Time.deltaTime);
            loadingSlider.value = interpolatedProgress;

            yield return null;
        }

        // Ensure the progress bar reaches 100% and wait a moment if needed
        loadingSlider.value = 1f;

        // Now activate the scene
        operation.allowSceneActivation = true;
    }
}
