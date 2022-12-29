using System;
using UnityEngine.Events;
using Dialogue;

namespace UnityAtoms
{
    /// <summary>
    /// None generic Unity Event of type `ActorSpeak`. Inherits from `UnityEvent&lt;ActorSpeak&gt;`.
    /// </summary>
    [Serializable]
    public sealed class ActorSpeakUnityEvent : UnityEvent<ActorSpeak> { }
}
