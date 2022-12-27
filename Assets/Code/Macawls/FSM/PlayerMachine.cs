using Ocean.FSM;
using UnityEngine;


namespace Player
{
    [CreateAssetMenu(fileName = "PlayerMachine", menuName = "OceanFSM/Machines/PlayerMachine", order = 0)]
    public class PlayerMachine : OceanMachine<PlayerMachine, PlayerRunner>
    {
    
    }
}




