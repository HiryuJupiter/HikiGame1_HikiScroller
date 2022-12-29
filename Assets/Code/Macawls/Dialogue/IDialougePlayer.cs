using CleverCrow.Fluid.Dialogues.Graphs;

namespace Dialogue
{
    public interface IDialoguePlayer
    {
        void PlayDialogue(DialogueGraph graph);
        void StopDialogue();
    }
}