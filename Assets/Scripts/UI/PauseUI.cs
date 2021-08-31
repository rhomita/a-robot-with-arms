using Manager;
using UnityEngine;

namespace UI
{
    public class PauseUI : MonoBehaviour
    {
        public void Restart()
        {
            gameObject.SetActive(false);
            GameManager.Instance.Restart();
        }

        public void MainMenu()
        {
            gameObject.SetActive(false);
            GameManager.Instance.GoToMainMenu();
        }
    }
}