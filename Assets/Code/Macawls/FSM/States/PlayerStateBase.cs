using Animancer;
using Ocean.FSM;
using UnityEngine;

namespace Player.States
{
    public class PlayerStateBase : OceanState<PlayerMachine, PlayerRunner>
    {
        [SerializeField] private AnimationClip clip;

        private AnimancerComponent _mAnimancer;
        
        protected const string Stats = "Stats";
        protected const string Variables = "Variables";
        protected SpriteRenderer SpriteRenderer { get; private set; }
        protected Rigidbody2D Rb2D { get; private set; }

        public PlayerStateBase()
        {
            OnStateInit += () =>
            {
                _mAnimancer = Machine.Runner.Animancer;
                Rb2D = Machine.Runner.Rb2D;
                SpriteRenderer = Machine.Runner.SpriteRenderer;
            };

            Events.StateEnter.AddListener(OnStateEnter);
        }

        private void OnStateEnter()
        {
            _mAnimancer.Play(clip);
        }

        protected void FlipSprite(float x)
        {
            SpriteRenderer.flipX = x switch
            {
                > 0f => false,
                < 0f => true,
                _ => SpriteRenderer.flipX
            };
        }
    }
}

