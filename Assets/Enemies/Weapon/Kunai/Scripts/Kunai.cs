using System;
using Player.Scripts;
using UnityEngine;

namespace Enemies.Weapon.Kunai.Scripts
{
    public class Kunai : MonoBehaviour
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
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponentInChildren<PlayerBar>().SendMessage("TakeDamage", 20f);
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
