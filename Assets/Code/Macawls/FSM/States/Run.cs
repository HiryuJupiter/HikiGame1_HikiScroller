using Ocean.FSM;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Player.States
{
    [CreateAssetMenu(fileName = "Run", menuName = "OceanFSM/Player/States/Run")]
    public class Run : PlayerStateBase, IUpdateHandler, IFixedUpdateHandler
    {
        [BoxGroup(Variables)][SerializeField] private Vector2Variable movementInput;
        [BoxGroup(Variables)][SerializeField] private FloatVariable friction;
        
        [BoxGroup(Stats)][SerializeField, Range(0f, 20f)] private float maxSpeed;
        [BoxGroup(Stats)][SerializeField, Range(0f, 100f)] private float maxAcceleration;

        private Vector2 _mDesiredVelocity, _mVelocity;
        
        public void OnFixedUpdate(float fixedDeltaTime)
        {
            _mVelocity = Rb2D.velocity;
            _mVelocity.x = Mathf.MoveTowards(_mVelocity.x, _mDesiredVelocity.x, maxAcceleration * Time.deltaTime);
            Rb2D.velocity = _mVelocity;
            
            FlipSprite(movementInput.Value.x);
        }
        
        public void OnUpdate(float deltaTime)
        {
            UpdateDesiredVelocity(maxSpeed, friction.Value, out _mDesiredVelocity);

            if (Rb2D.velocity == Vector2.zero && movementInput.Value.x == 0)
            {
                Machine.TransitionToState<Idle>();
            }
        }
        
        private void UpdateDesiredVelocity(in float speed, in float frictionVal, out Vector2 newVelocity)
        {
            newVelocity = new Vector2(movementInput.Value.x, 0f) * Mathf.Max( speed - frictionVal, 0f);
        }
    }
}