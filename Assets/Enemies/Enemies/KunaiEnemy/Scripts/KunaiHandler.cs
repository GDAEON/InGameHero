using UnityEngine;

namespace Enemies.Enemies.KunaiEnemy.Scripts
{
    public class KunaiHandler : MonoBehaviour
    {
        [SerializeField] private GameObject kunai;
        [SerializeField] private GameObject projectile;
        
        public void ShowKunai()
        {
            kunai.SetActive(true);
        }

        public void HideKunai()
        {
            kunai.SetActive(false);
        }

        public void ThrowKunai()
        {
            var newProjectile = Instantiate(projectile, kunai.transform.position, kunai.transform.rotation);
            
            var rb = newProjectile.GetComponent<Rigidbody>();

            if (gameObject.CompareTag("Player"))
            {
                rb.AddForce(transform.forward * 20, ForceMode.Impulse);
            }
            else
            {
                rb.AddRelativeForce(Vector3.forward * 20, ForceMode.Impulse);
            }
        }
    }
}
