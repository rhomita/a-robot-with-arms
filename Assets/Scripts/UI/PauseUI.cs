using Manager;
using UnityEngine;

namespace UI
{
    public class PauseUI : MonoBehaviour
    {
        public void Resume()
        {
            GameManager.Instance.Resume();
        }
        
        public void Restart()
        {
            GameManager.Instance.Restart();
        }

        public void MainMenu()
        {
            GameManager.Instance.GoToMainMenu();
        }
    }
}