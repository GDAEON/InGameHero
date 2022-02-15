using Enemies.Scripts;
using UnityEngine;

namespace Player.Prefabs
{
    public class PlayerKunai : MonoBehaviour
    {
        public AudioClip targetPlayer;
        public AudioClip targetOther;
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnCollisionEnter(Collision other)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            if (other.gameObject.tag.Contains("Enemy"))
            {
                other.gameObject.GetComponentInChildren<EnemyBar>().SendMessage("TakeDamage", 20f);
                _audioSource.clip = targetPlayer;
                _audioSource.Play();
            }
            else
            {
                _audioSource.clip = targetOther;
                _audioSource.Play();
            }
            Destroy(gameObject, 10);
        }
    }
}
