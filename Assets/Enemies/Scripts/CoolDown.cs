using System.Collections;
using UnityEngine;

namespace Enemies.Scripts
{
    public class CoolDown : MonoBehaviour
    {
        public string skillName;
        public float coolDownTime;
        private Animator _animator;
        [HideInInspector]
        public bool coolDown;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (coolDown)
                StartCoroutine(nameof(TimerTake));
        }
    
        private IEnumerator TimerTake()
        {
            coolDown = false;
            yield return new WaitForSeconds(coolDownTime);
            _animator.SetBool(skillName, true);
        }
    }
}
