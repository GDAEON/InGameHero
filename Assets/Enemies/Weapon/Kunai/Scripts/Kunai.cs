using System;
using UnityEngine;

namespace Enemies.Weapon.Kunai.Scripts
{
    public class Kunai : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
