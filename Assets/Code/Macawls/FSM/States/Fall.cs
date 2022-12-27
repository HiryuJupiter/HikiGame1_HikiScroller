using System.Collections;
using Ocean.FSM;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Player.States
{
    [CreateAssetMenu(fileName = "Fall", menuName = "OceanFSM/Player/States/Fall")]
    public class Fall : PlayerStateBase, IEnterHandler, IExitHandler
    {
        [BoxGroup(Variables)] [SerializeField] private BoolVariable isGrounded;
        [BoxGroup(Variables)] [SerializeField] private Vector2Variable moveInput;
        
        [BoxGroup(Stats)] [SerializeField, Range(3, 20f)] private float xAxisVelocity;
        [BoxGroup(Stats)] [SerializeField, Range(0f, 10f)] private float downwardsGravity;
        
        public void OnEnter()
        {
            Machine.Runner.StopCoroutine(FallRoutine());
            Machine.Runner.StartCoroutine(FallRoutine());
        }
        
        public void OnExit()
        {
            Rb2D.gravityScale = 1f;
        }
        
        private IEnumerator FallRoutine()
        {
            Rb2D.gravityScale = downwardsGravity;
            
            while (!isGrounded.Value)
            {
                if (moveInput.Value.x != 0)
                {
                    FlipSprite(moveInput.Value.x);
                    Rb2D.velocity = new Vector2(moveInput.Value.x * xAxisVelocity, Rb2D.velocity.y);
                }
                yield return null;
            }

            if (Machine.CurrentState is not Idle)
            {
                Machine.TransitionToState<Idle>();
            }

           
        }


    }
}