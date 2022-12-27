using UnityEngine;
using Animancer;
using Ocean.FSM;
using Player.States;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerRunner : OceanRunner<PlayerMachine, PlayerRunner>
    {
        private const string Components = "Components";
        private const string Variables = "Variables";

        [BoxGroup(Components)] [SerializeField] private AnimancerComponent animancer; 
        [BoxGroup(Components)] [SerializeField] private Rigidbody2D rb2D; 
        [BoxGroup(Components)] [SerializeField] private SpriteRenderer spriteRenderer;

        [BoxGroup(Variables)] [SerializeField] private IntVariable jumpsRemaining;

        public Rigidbody2D Rb2D => rb2D;
        public SpriteRenderer SpriteRenderer => spriteRenderer; 
        public AnimancerComponent Animancer => animancer;

        public void OnJumpInput(InputAction.CallbackContext context)
        {
            if (jumpsRemaining.Value <= 0) return;

            if (!context.performed) return;
            
            if (Machine.CurrentState is not Jump)
            {
                Machine.TransitionToState<Jump>();
            }
            else if (Machine.CurrentState is Jump jump)
            {
                jump.OnEnter();
            }
        }
    }
}


