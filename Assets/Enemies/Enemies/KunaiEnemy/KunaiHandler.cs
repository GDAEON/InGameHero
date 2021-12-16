using UnityEngine;

namespace Enemies.Enemies.KunaiEnemy
{
    public class KunaiHandler : MonoBehaviour
    {
        [SerializeField] private GameObject kunai;
        
        public void ShowKunai()
        {
            kunai.SetActive(true);
        }

        public void HideKunai()
        {
            kunai.SetActive(false);
        }
    }
}
