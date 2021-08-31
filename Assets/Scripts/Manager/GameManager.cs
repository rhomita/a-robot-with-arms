using System;
using Manager;
using UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
        [Header("UI")]
        [SerializeField] private PauseUI _pauseUi;

        private bool isPaused = false;

        public static GameManager Instance { get; private set; }

        private Transform _robot;
        private CameraMovement _cameraMovement;
        private SceneLoader _sceneLoader;
        private LevelManager _levelManager;

        public CameraMovement CameraMovement => _cameraMovement;
        public SceneLoader SceneLoader => _sceneLoader;
        public LevelManager LevelManager => _levelManager;

        void Awake()
        {
            if (Instance == null)
            {
                _sceneLoader = transform.GetComponentInChildren<SceneLoader>();
                _levelManager = transform.GetComponent<LevelManager>();
                
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            Resume();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _sceneLoader.Reload();
            }
            
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = !isPaused;
                if (isPaused)
                {
                    Pause();
                }
                else
                {
                    Resume();
                }
            }
        }

        public void UnlockCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void LockCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Pause()
        {
            Time.timeScale = 0;
            _pauseUi.gameObject.SetActive(true);
            UnlockCursor();
        }

        public void Resume()
        {
            Time.timeScale = 1;
            _pauseUi.gameObject.SetActive(false);
            LockCursor();
        }

        public void Restart()
        {
            Time.timeScale = 1;
            _sceneLoader.Reload();
        }
        
        public void GoToMainMenu()
        {
            Time.timeScale = 1;
            LevelManager.GoToMainMenu();
        }

        public void SetCamera(CameraMovement cameraMovement)
        {
            _cameraMovement = cameraMovement;
        }
}
