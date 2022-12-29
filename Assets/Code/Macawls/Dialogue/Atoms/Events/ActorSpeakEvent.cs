using UnityEngine;
using Dialogue;

namespace UnityAtoms
{
    /// <summary>
    /// Event of type `ActorSpeak`. Inherits from `AtomEvent&lt;ActorSpeak&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/ActorSpeak", fileName = "ActorSpeakEvent")]
    public sealed class ActorSpeakEvent : AtomEvent<ActorSpeak>
    {
    }
}
