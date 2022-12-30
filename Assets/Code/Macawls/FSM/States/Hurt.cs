using System.Collections;
using Ocean.FSM;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Player.States {
    [CreateAssetMenu(fileName = "Hurt", menuName = "OceanFSM/Player/States/Hurt")]
    public class Hurt : PlayerStateBase, IEnterHandler, IExitHandler {

        

        public void OnEnter() {
            Machine.Runner.StopCoroutine(HurtRoutine());
            Machine.Runner.StartCoroutine(HurtRoutine());
        }

        public void OnExit() {
            
        }

        private IEnumerator HurtRoutine() {


            
            yield return new WaitForSeconds(this.ClipLength);
           
            if (Machine.CurrentState is not Idle) {
                Machine.TransitionToState<Idle>();
            }


        }


    }
}