using System.Collections.Generic;
using System.Linq;
using Player.Scripts;
using UnityEngine;

namespace Enemies.Scripts
{
    public class EnemyController : MonoBehaviour
    {
        private bool _isDead;
        private Healthbar _healthbar;
        private Animator _animator;
        public Material material;
        public List<AudioClip> AudioClips;
        [Header("Fight settings")]
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private List<Transform> attackPoint;
        [SerializeField] private GameObject lastHitIndicator;
        private AudioSource _audioSource;
        public float attackRange;
        private static readonly int Death = Animator.StringToHash("Death");

        private void Start()
        {
            _healthbar = GetComponentInChildren<Healthbar>();
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (_healthbar.health == 0 && !_isDead)
            {
                GetComponent<Collider>().enabled = false;
                _animator.SetTrigger(Death);
                _isDead = true;
                Destroy(gameObject, 5);
            }
            var health = GetComponentsInChildren<EnemyBar>()[0];
            var stamina = GetComponentsInChildren<EnemyBar>()[1];
            lastHitIndicator.SetActive(stamina.health <= 30 || health.health <= 30);
        }

        private void PlayAudioAttackSucces()
        {
            _audioSource.clip = AudioClips[Random.Range(0, AudioClips.Count - 1)];
            _audioSource.Play();
        }

        public void PlayAudio(AudioClip audioClip)
        {
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }

        public void Attack(int damage)
        {
            foreach (var player in attackPoint.Select(point =>
                // ReSharper disable once Unity.PreferNonAllocApi
                Physics.OverlapSphere(point.position, attackRange, playerLayer)).SelectMany(hitPlayer => hitPlayer))
            {
                PlayAudioAttackSucces();
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
