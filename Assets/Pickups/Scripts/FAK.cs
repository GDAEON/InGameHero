using UnityEngine;
using Player.Scripts;

public class FAK : MonoBehaviour
{
    [SerializeField] private float HP;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponentInChildren<PlayerBar>().GainHealth(HP);
            Destroy(gameObject);
        }
    }
}
