using Player.Scripts;
using UnityEngine;

namespace Enemies.Weapon.Kunai.Scripts
{
    public class Kunai : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            if(other.gameObject.CompareTag("Player"))
                other.gameObject.GetComponentInChildren<PlayerBar>().SendMessage("TakeDamage", 20f);
            Destroy(gameObject, 10);
        }
    }
}
