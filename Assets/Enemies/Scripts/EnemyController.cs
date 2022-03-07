using System;
using System.Collections.Generic;
using System.Linq;
using Player.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;

namespace Enemies.Scripts
{
    public class EnemyController : MonoBehaviour
    {
        public bool isDead;
        private Animator _lastHitAnimator;
        private Healthbar _healthbar;
        private Animator _animator;
        [Header("Fight settings")]
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private List<Transform> attackPoint;
        [SerializeField] private GameObject lastHitIndicator;
        [SerializeField] private Material material;
        [SerializeField] private Material selectedMaterial;
        public float attackRange;
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int Select = Animator.StringToHash("Select");

        private void Start()
        {
            if(GameObject.FindWithTag("EnemyCounter"))
                GameObject.FindWithTag("EnemyCounter").GetComponent<EnemyCounter>().enemies.Add(this);
            _healthbar = GetComponentInChildren<Healthbar>();
            _animator = GetComponent<Animator>();
            _lastHitAnimator = lastHitIndicator.GetComponent<Animator>();
        }

        private void Update()
        {
            
            if (_healthbar.health == 0 && !isDead)
            {
                if(GameObject.FindWithTag("EnemyCounter"))
                    GameObject.FindWithTag("EnemyCounter").GetComponent<EnemyCounter>().enemies.Remove(this);
                GetComponent<Collider>().enabled = false;
                _animator.SetTrigger(Death);
                isDead = true;
                Destroy(gameObject, 5);
            }
            var health = GetComponentsInChildren<EnemyBar>()[0];
            var stamina = GetComponentsInChildren<EnemyBar>()[1];
            lastHitIndicator.SetActive(stamina.health <= 30 || health.health <= 30 && !isDead);
        }

        public void Selected()
        {
            _lastHitAnimator.SetBool(Select, true);
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = selectedMaterial;
        }

        public void Deselected()
        {
            _lastHitAnimator.SetBool(Select, false);
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = material;
        }

        public void Attack(int damage)
        {
            foreach (var player in attackPoint.Select(point =>
                // ReSharper disable once Unity.PreferNonAllocApi
                Physics.OverlapSphere(point.position, attackRange, playerLayer)).SelectMany(hitPlayer => hitPlayer))
            {
                GetComponent<AudioController>().DealDamageSound();
                player.GetComponentInChildren<PlayerBar>().SendMessage("TakeDamage", damage);
            }
        }

        private void OnDrawGizmosSelected()
        {
            foreach (var point in attackPoint)
            {
                Gizmos.DrawWireSphere(point.position, attackRange);
            }
        }
    }
}
