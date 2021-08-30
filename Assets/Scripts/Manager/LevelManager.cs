using UnityEngine;

namespace Manager
{
    public class LevelManager : MonoBehaviour
    {
        private const string MainLevel = "MainMenu";
        private const string LevelPrefix = "Level_";
        private const int MaxLevels = 5;
        private int _level = 0;

        public void GoToNextLevel()
        {
            _level++;

            if (_level >= MaxLevels)
            {
                GoToMainMenu();
                return;
            }

            GameManager.Instance.SceneLoader.GoToScene(LevelPrefix + _level);
        }

        public void GoToMainMenu()
        {
            GameManager.Instance.SceneLoader.GoToScene(MainLevel);
        }
    }
}