using UnityEngine;
using Player.Scripts;

public class StaminaPickup : MonoBehaviour
{
    [SerializeField] private float STAMINA;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.GetComponentsInChildren<PlayerBar>()[1].GainHealth(STAMINA);
            Destroy(gameObject);
        }
    }
}
