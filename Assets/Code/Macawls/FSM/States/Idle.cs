using Ocean.FSM;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Player.States
{
    [CreateAssetMenu(fileName = "Idle", menuName = "OceanFSM/Player/States/Idle")]
    public class Idle : PlayerStateBase, IEnterHandler, IUpdateHandler
    {
        [BoxGroup(Variables)][SerializeField] private Vector2Variable movementInput;
        [BoxGroup(Variables)][SerializeField] private IntVariable jumpsRemaining;

        public void OnEnter()
        {
            jumpsRemaining.Value = jumpsRemaining.InitialValue;
        }
        
        public void OnUpdate(float deltaTime)
        {
            if (movementInput.Value.x == 0f) return;
            Machine.TransitionToState<Run>();
        }
    }
}

