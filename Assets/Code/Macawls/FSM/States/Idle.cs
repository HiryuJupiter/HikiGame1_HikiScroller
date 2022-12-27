using Ocean.FSM;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Player.States
{
    [CreateAssetMenu(fileName = "Idle", menuName = "OceanFSM/Player/States/Idle", order = 0)]
    public class Idle : PlayerStateBase, IUpdateHandler
    {
        [BoxGroup(Variables)][SerializeField] private Vector2Variable movementInput;

        public void OnUpdate(float deltaTime)
        {
            if (movementInput.Value == Vector2.zero) return;
            Machine.TransitionToState<Run>();
        }
    }
}

