using Enemies.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LVLTransit : MonoBehaviour
{
    [SerializeField] private int numberOfNextLvl;

    private void Update()
    {
        if (!FindObjectOfType<EnemyController>())
        {
            SceneManager.LoadScene("LVL" + numberOfNextLvl);
        }
    }
}
