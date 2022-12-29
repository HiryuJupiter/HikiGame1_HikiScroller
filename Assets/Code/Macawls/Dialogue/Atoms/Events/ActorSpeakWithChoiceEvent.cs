using UnityEngine;
using Dialogue;

namespace UnityAtoms
{
    /// <summary>
    /// Event of type `ActorSpeakWithChoice`. Inherits from `AtomEvent&lt;ActorSpeakWithChoice&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/ActorSpeakWithChoice", fileName = "ActorSpeakWithChoiceEvent")]
    public sealed class ActorSpeakWithChoiceEvent : AtomEvent<ActorSpeakWithChoice>
    {
    }
}
