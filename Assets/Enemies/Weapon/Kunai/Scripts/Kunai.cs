using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemies.Weapon.Kunai.Scripts
{
    public class Kunai : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

        private void Start()
        {
            Destroy(gameObject, 10);
        }
    }
}
