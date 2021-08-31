using Manager;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;

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

        void Start()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Play();
            }    
        }
        

        public void Exit()
        {
            Application.Quit();
        }
    }
}