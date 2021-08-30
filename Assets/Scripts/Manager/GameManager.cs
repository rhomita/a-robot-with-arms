using Manager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
        [Header("UI")]
        // [SerializeField] private GameUI gameUi;

        // public GameUI UI => gameUi;
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
                _sceneLoader = transform.GetComponent<SceneLoader>();
                _levelManager = transform.GetComponent<LevelManager>();
                
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            LockCursor();
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
            // gameUi.PauseUI.SetActive(true);
            UnlockCursor();
        }

        private void Resume()
        {
            Time.timeScale = 1;
            // gameUi.PauseUI.SetActive(false);
            LockCursor();
        }

        public void Restart()
        {
            Time.timeScale = 1;
            _sceneLoader.Reload();
        }
        
        public void GoToMainMenu()
        {
            _sceneLoader.GoToScene("MainMenu");
        }

        public void SetCamera(CameraMovement cameraMovement)
        {
            _cameraMovement = cameraMovement;
        }
}