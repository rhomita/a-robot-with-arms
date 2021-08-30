using UnityEngine;

namespace Manager
{
    public class LevelManager : MonoBehaviour
    {
        private const string LevelPrefix = "Level_";
        private const int MaxLevels = 4;
        private int _level = 0;

        public void GoToNextLevel()
        {
            _level++;

            if (_level >= MaxLevels)
            {
                GameManager.Instance.SceneLoader.GoToScene("MainMenu");
                return;
            }
            
            GameManager.Instance.SceneLoader.GoToScene(LevelPrefix + _level);
        }
    }
}