using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SfxManager : MonoBehaviour
{
    public static SfxManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    public GameObject UI_Select;




    public GameObject UI_Click;
    public GameObject UI_Flick;
    public GameObject UI_Pop;
    public GameObject UI_KeyboardKey;
    public GameObject UI_SoftAlarm;

    //Credit
    public AudioSource SongSource;
    public AudioClip CreditSong;
    public string CreditName;


    public void Play_UISelect() => Instantiate(UI_Select, Vector3.zero, Quaternion.identity);

    public void Play_UIClick() => Instantiate(UI_Click, Vector3.zero, Quaternion.identity);
    public void Play_UIFlick() => Instantiate(UI_Flick, Vector3.zero, Quaternion.identity);
    public void Play_UIPop() => Instantiate(UI_Pop, Vector3.zero, Quaternion.identity);
    public void Play_UIKeyboardKey() => Instantiate(UI_KeyboardKey, Vector3.zero, Quaternion.identity);
    public void Play_UISoftAlarm() => Instantiate(UI_SoftAlarm, Vector3.zero, Quaternion.identity);
}
