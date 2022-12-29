using CleverCrow.Fluid.Dialogues.Graphs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Dialogue
{
    public class DialoguePlayer : SerializedMonoBehaviour
    {
        [SerializeField] private IDialoguePlayer dialoguePlayer;
        [SerializeField] private DialogueGraph graphToPlay;

        private bool IsPlayMode => Application.isPlaying;
        
        [ShowIf("IsPlayMode")]
        [Button("Play Dialogue")]
        public void Play()
        {
            if (dialoguePlayer is null)
            {
                Debug.LogError("No dialogue player set");
                return;
            }

            if (!graphToPlay)
            {
                Debug.LogError("No dialogue graph set");
                return;
            }
            
            dialoguePlayer.PlayDialogue(graphToPlay);
        }
    }
}