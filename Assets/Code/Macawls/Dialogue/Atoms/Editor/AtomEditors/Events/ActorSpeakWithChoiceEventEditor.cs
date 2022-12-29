#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using Dialogue;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `ActorSpeakWithChoice`. Inherits from `AtomEventEditor&lt;ActorSpeakWithChoice, ActorSpeakWithChoiceEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(ActorSpeakWithChoiceEvent))]
    public sealed class ActorSpeakWithChoiceEventEditor : AtomEventEditor<ActorSpeakWithChoice, ActorSpeakWithChoiceEvent> { }
}
#endif
