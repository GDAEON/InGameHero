using UnityEngine;

namespace Enemies.Weapon.Kunai.Scripts
{
    public class Kunai : MonoBehaviour
    {
        private void OnCollisionEnter()
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Destroy(gameObject, 10);
        }
    }
}
