using Manager;
using UnityEditor.SearchService;
using UnityEngine;

namespace UI
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField] private SceneLoader _sceneLoader;
        private string LEVEL_ONE = "Level_0";

        public void Play()
        {
            _sceneLoader.GoToScene(LEVEL_ONE);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}