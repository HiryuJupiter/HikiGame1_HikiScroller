using Animancer;
using Ocean.FSM;
using UnityEngine;

namespace Player
{
    public class PlayerRunner : OceanRunner<PlayerMachine, PlayerRunner>
    { 
        [SerializeField] private AnimancerComponent animancer; 
        [SerializeField] private Rigidbody2D rb2D; 
        [SerializeField] private SpriteRenderer spriteRenderer; 
        public Rigidbody2D Rb2D => rb2D;
        public SpriteRenderer SpriteRenderer => spriteRenderer; 
        public AnimancerComponent Animancer => animancer;
    }
}


