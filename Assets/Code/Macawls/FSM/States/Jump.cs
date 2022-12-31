using System.Collections;
using Ocean.FSM;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player.States
{
    [CreateAssetMenu(fileName = "Jump", menuName = "OceanFSM/Player/States/Jump")]
    public class Jump : PlayerStateBase, IEnterHandler, IExitHandler
    {
        [BoxGroup(Variables)] [SerializeField] private IntVariable jumpsRemaining;
        [BoxGroup(Variables)] [SerializeField] private Vector2Variable movementInput;
        
        [BoxGroup(Stats)] [SerializeField, Range(3, 20f)] private float yAxisVelocity;
        [BoxGroup(Stats)] [SerializeField, Range(0f, 1f)] private float jumpHoldTime;
        [BoxGroup(Stats)] [SerializeField, Range(2f, 10f)] private float xAxisVelocity;
        [BoxGroup(Stats)] [SerializeField, Range(0f, 10f)] private float upwardsGravity;


        [InfoBox("How many jumps the player has left")]
        [BoxGroup(FeedbackEvents)] [SerializeField] private UnityEvent<int> onJumpEvent;

        private InputAction.CallbackContext JumpInputContext { get; set; }


        public void OnEnter()
        {
            Machine.Runner.StopCoroutine(JumpRoutine());
            Machine.Runner.StartCoroutine(JumpRoutine());
        }
        
        // set under the "events" of player input component in inspector
        public void UpdateJumpInput(InputAction.CallbackContext context)
        {
            JumpInputContext = context;
        }
        
        public void OnExit()
        {
            Machine.Runner.StopCoroutine(JumpRoutine());
        }
        
        private IEnumerator JumpRoutine()
        {
            Rb2D.gravityScale = upwardsGravity;
            jumpsRemaining.Value--;
            onJumpEvent.Invoke(jumpsRemaining.Value);

            float timeElapsed = 0f;
            
            while (JumpInputContext.performed && timeElapsed < jumpHoldTime)
            {
                Rb2D.velocity = new Vector2(x: movementInput.Value.x * xAxisVelocity, y: yAxisVelocity);
                FlipSprite(movementInput.Value.x);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            
            yield return new WaitUntil(() => Rb2D.velocity.y < 0);
            Machine.TransitionToState<Fall>();
        }
    }
}