using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Created by Jason

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip backgroundMusic;
    public float fadeInDuration = 2.0f; // Duration for fade in/out
    public float fadeOutDuration = 1.0f;

    private void Awake()
    {
        // Example: Start playing sound with a fade-in
        audioSource.clip = backgroundMusic;
        StartCoroutine(FadeInAudio());
    }

    public IEnumerator FadeInAudio()
    {

        float startVolume = 0f;
        audioSource.volume = startVolume;
        audioSource.Play();

        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            float t = elapsedTime / fadeInDuration;
            audioSource.volume = Interpolation.FloatLerp(startVolume, 1f, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = 1f;                //cause lerp function outcome could be 0.999999
    }

    public IEnumerator FadeOutAudio()
    {
        Debug.Log("Coroutien stoop");
        float startVolume = audioSource.volume;
        float elapsedTime = 0f;

        while (elapsedTime < fadeOutDuration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / fadeOutDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = 0f;
        audioSource.Stop();
    }
}
