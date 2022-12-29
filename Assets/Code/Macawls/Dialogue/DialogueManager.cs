using CleverCrow.Fluid.Databases;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Graphs;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogue
{
    [CreateAssetMenu(fileName = "DialoguePlayer", menuName = "Game/DialoguePlayer", order = 1)]
    public class DialogueManager : ScriptableObject, IDialoguePlayer
    {
        private readonly DialogueController _mDialogueController;

        [SerializeField] private UnityEvent onDialogueStart;
        [SerializeField] private UnityEvent onDialogueEnd;
        [SerializeField] private UnityEvent<ActorSpeak> onActorSpeak;
        [SerializeField] private UnityEvent<ActorSpeakWithChoice> onActorSpeakWithChoices;

        public DialogueManager()
        {
            var databaseInstance = new DatabaseInstance();
            _mDialogueController = new DialogueController(databaseInstance);
        }
    
        public void PlayDialogue(DialogueGraph graph)
        {
            _mDialogueController.Play(graph);
        }

        public void StopDialogue()
        {
            _mDialogueController.Stop();
        }

        private void OnEnable()
        {
            AddListeners(_mDialogueController.Events);
        }

        private void AddListeners(IDialogueEvents events)
        {
            events.Begin.AddListener(() =>
            {
                onDialogueStart?.Invoke();
            });
            
            events.End.AddListener(() =>
            {
                onDialogueEnd?.Invoke();
            });
            
            events.Speak.AddListener((actor, sentence) =>
            {
                var actorSpeak = new ActorSpeak(actor, sentence);
                onActorSpeak?.Invoke(actorSpeak);
            });
            
            events.Choice.AddListener((actor, sentence, choices) =>
            {
                var speakWithChoices = new ActorSpeakWithChoice(actor, sentence, choices);
                onActorSpeakWithChoices?.Invoke(speakWithChoices);
            });
        }
    }
}


