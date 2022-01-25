using UnityEngine;
using Player.Scripts;

public class TimePickup : MonoBehaviour
{
    [SerializeField] private int TIME;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponentInChildren<Controller>().AddTime(TIME);
            Destroy(gameObject);
        }
    }
}
