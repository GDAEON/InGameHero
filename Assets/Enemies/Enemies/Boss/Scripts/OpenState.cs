using Unity.VisualScripting;
using UnityEngine;

namespace Enemies.Enemies.Boss.Scripts
{
    public class OpenState : StateMachineBehaviour
    {
        private float _tmpTimer;
        private static readonly int Timer = Animator.StringToHash("Timer");

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.transform.Find("EnergyShield").gameObject.SetActive(false);
            _tmpTimer = animator.GetFloat(Timer);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetFloat(Timer, animator.GetFloat(Timer) - Time.deltaTime); ;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetFloat(Timer, _tmpTimer);
        }
    }
}
