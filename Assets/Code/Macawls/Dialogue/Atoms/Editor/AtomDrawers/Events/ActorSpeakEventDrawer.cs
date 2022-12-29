#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `ActorSpeak`. Inherits from `AtomDrawer&lt;ActorSpeakEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(ActorSpeakEvent))]
    public class ActorSpeakEventDrawer : AtomDrawer<ActorSpeakEvent> { }
}
#endif
