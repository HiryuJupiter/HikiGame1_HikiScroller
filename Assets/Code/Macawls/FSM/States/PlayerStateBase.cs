using Animancer;
using Ocean.FSM;
using UnityEngine;

namespace Player.States
{
    public class PlayerStateBase : OceanState<PlayerMachine, PlayerRunner>
    {
        [SerializeField] private AnimationClip clip;

        private AnimancerComponent _mAnimancer;
        private SpriteRenderer _mSpriteRenderer;
        protected Rigidbody2D Rb2D { get; private set; }
        
        
        protected AnimancerState AnimancerState { get; private set; }
        
        protected const string Stats = "Stats";
        protected const string Variables = "Variables";
        protected const string FeedbackEvents = "FeedbackEvents";
        
        public PlayerStateBase()
        {
            OnStateInit += () =>
            {
                _mAnimancer = Machine.Runner.Animancer;
                _mSpriteRenderer = Machine.Runner.SpriteRenderer;
                Rb2D = Machine.Runner.Rb2D;
            };

            Events.StateEnter.AddListener(PlayStateAnimation);
        }

        private void PlayStateAnimation()
        {
            if (!clip) return;
            AnimancerState = _mAnimancer.Play(clip);
        }

        protected void FlipSprite(float x)
        {
            _mSpriteRenderer.flipX = x switch
            {
                > 0f => false,
                < 0f => true,
                _ => _mSpriteRenderer.flipX
            };
        }
    }
}

