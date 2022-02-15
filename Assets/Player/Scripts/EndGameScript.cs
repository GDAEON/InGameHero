using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Player.Scripts
{
    public class EndGameScript : MonoBehaviour
    {
        [SerializeField] private GameObject winScreen;
        [SerializeField] private GameObject deathScene;

        public void SetupWinScreen()
        {
            SetupScreen(winScreen);
        }

        public void SetupDeathScreen()
        {
            SetupScreen(deathScene);
        }
    
        private void SetupScreen(GameObject screen)
        {
            gameObject.GetComponent<Controller>().enabled = false;
            gameObject.GetComponent<PlayerInput>().enabled = false;
            screen.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
}
