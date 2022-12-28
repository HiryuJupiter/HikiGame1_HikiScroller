using System.Collections;
using UnityEngine;


public class PersistentMusicPlayer : MonoBehaviour
{
    public static PersistentMusicPlayer Instance;

    public AudioSource source;
    public AudioClip song1;
    public AudioClip song2;
    public float fadeSpeed = 2f;

    AudioClip nextClip;
    bool fadeToNextSong;
    bool fadeToSilence;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            DestroyImmediate(gameObject);
        }

        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (fadeToNextSong)
        {
            if (source.volume > 0f)
            {
                source.volume -= Time.deltaTime * fadeSpeed;
                if (source.volume <= 0f)
                {
                    source.clip = nextClip;
                    source.Play();
                    source.volume = 1f;
                }
            }
        }

        if (fadeToSilence)
            if (source.volume > 0f)
                source.volume -= Time.deltaTime * fadeSpeed;
    }

    public void PlaySong2(bool fadeIn)
    {
        if (fadeIn)
        {
            fadeToNextSong = true;
            nextClip = song2;
        }
        else
        {
            fadeToNextSong = false;
            source.clip = song2;
            source.Play();
        }
    }

    public void FadeToSilence() => fadeToSilence = true;
}
