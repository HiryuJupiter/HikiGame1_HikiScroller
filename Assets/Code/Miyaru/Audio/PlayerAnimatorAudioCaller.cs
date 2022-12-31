using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorAudioCaller : MonoBehaviour
{
    SfxManager sfx;

    void Start()
    {
        sfx = SfxManager.Instance;
    }

    public void Play_Walk() => sfx.Play_Walk();

    public void Play_Run() => sfx.Play_Run();

    public void Play_Land() => sfx.Play_Land();

    public void Play_Jump() => sfx.Play_Jump();
    public void Play_Hurt() => sfx.Play_Hurt();

    public void Play_Slide() => sfx.Play_Slide();
    public void Play_Punch() => sfx.Play_Punch();
    public void Play_Kick() => sfx.Play_Kick();
    public void Play_Recover() => sfx.Play_Recover();
}
