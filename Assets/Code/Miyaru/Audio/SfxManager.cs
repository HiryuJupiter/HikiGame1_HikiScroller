using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SfxManager : MonoBehaviour
{
    public static SfxManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    public AudioSource audioSource;

    public AudioClip UI_Select;
    public List<AudioClip> Hiki_Run;
    public List<AudioClip> Hiki_Walk;

    public AudioClip Hiki_Punch;
    public AudioClip Hiki_Kick;
    public AudioClip Hiki_Jump;
    public AudioClip Hiki_Land;
    public AudioClip Hiki_Hurt;
    public AudioClip Hiki_Slide;
    public AudioClip Hiki_Recover;

    //Credit
    public AudioSource SongSource;
    public AudioClip CreditSong;
    public string CreditName;


    public void Play_UISelect() => Play(UI_Select);
    public void Play_Walk() => Play(Hiki_Walk.Random());
    public void Play_Run() => Play(Hiki_Run.Random());

    public void Play_Jump() => Play(Hiki_Jump);
    public void Play_Land() => Play(Hiki_Land);
    public void Play_Hurt() => Play(Hiki_Hurt);
    public void Play_Punch() => Play(Hiki_Punch);
    public void Play_Kick() => Play(Hiki_Kick);
    public void Play_Slide() => Play(Hiki_Slide);
    public void Play_Recover() => Play(Hiki_Recover);

    void Play(AudioClip clip)
    {
        audioSource.pitch = Random.Range(1.3f, 1.8f);
        audioSource.PlayOneShot(clip);
    }

    //public void Play_Jump() => Instantiate(Hiki_Jump, Vector3.zero, Quaternion.identity);
    //public void Play_Land() => Instantiate(Hiki_Land, Vector3.zero, Quaternion.identity);
    //public void Play_Hurt() => Instantiate(Hiki_Hurt, Vector3.zero, Quaternion.identity);
    //public void Play_Slide() => Instantiate(Hiki_Slide, Vector3.zero, Quaternion.identity);

}
