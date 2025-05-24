using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SoundFade : MonoBehaviour
{
    private float fadeTime = 3f;
    public GameObject audioObject;
    AudioSource musicSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake() {
        musicSource = audioObject.GetComponent<AudioSource>();
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutMusic(musicSource, fadeTime));
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInMusic(musicSource, fadeTime));
    }

    IEnumerator FadeOutMusic(AudioSource musicSource, float fadeTime)
    {
        float startVolume = musicSource.volume;

        while (musicSource.volume > 0)
        {
            musicSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        musicSource.Stop();
    }

    IEnumerator FadeInMusic(AudioSource musicSource, float fadeTime)
    {
        musicSource.Play();
        musicSource.volume = 0f;
        while (musicSource.volume <= 0.439)
        {
            musicSource.volume += Time.deltaTime / fadeTime;
            yield return null;
        }
    }
}
