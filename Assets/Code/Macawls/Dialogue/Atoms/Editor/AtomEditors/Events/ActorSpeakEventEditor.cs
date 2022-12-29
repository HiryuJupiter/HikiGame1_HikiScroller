#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using Dialogue;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `ActorSpeak`. Inherits from `AtomEventEditor&lt;ActorSpeak, ActorSpeakEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(ActorSpeakEvent))]
    public sealed class ActorSpeakEventEditor : AtomEventEditor<ActorSpeak, ActorSpeakEvent> { }
}
#endif
