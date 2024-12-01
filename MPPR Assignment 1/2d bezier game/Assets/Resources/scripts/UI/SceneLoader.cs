using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string name)
    {
        StartCoroutine(ShowProgress(name));
    }

    IEnumerator ShowProgress(string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);             // as unity loading only use until 0.9 and use other 0.1 for activation

            yield return null;
        }
    }
}
