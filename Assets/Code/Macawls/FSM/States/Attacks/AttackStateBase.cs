using Ocean.FSM;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Player.States
{
    public class AttackStateBase : PlayerStateBase, IEnterHandler, IExitHandler, IUpdateHandler
    {
        [SerializeField, Range(0f, 0.99f)] private float attackTime;
        
        [InfoBox("the next attack state to be changed to after the player has attacked")]
        [SerializeField] private AttackStateBase nextAttackState;
        
        private bool _mHasAttacked;

        public void OnEnter()
        {
            _mHasAttacked = false;
        }

        public void OnAttackInput()
        {
            if (_mHasAttacked) return;
            _mHasAttacked = true;
        }

        public void OnExit()
        {
            _mHasAttacked = false;
        }

        public void OnUpdate(float deltaTime)
        {
            if (!(AnimancerState.NormalizedTime >= attackTime)) return;

            if (_mHasAttacked && nextAttackState)
            {
                Machine.TransitionToState(nextAttackState);
            }
            else
            {
                Machine.TransitionToState<Idle>();
            }
        }
    }
}