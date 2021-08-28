using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
        [Header("Player")]
        [SerializeField] private Transform player;
        [SerializeField] private Transform cam;
        // [SerializeField] private Stats playerStats;
        [Header("UI")]
        // [SerializeField] private GameUI gameUi;
        // [SerializeField] private Inventory inventory;
        // [SerializeField] private SceneLoader sceneLoader;
        [Header("Music and sounds")]
        // [SerializeField] private SoundEffect soundEffectLost;

        private bool isPaused = false;
        private bool settingsOpened = false;

        public Transform CameraTransform => cam;
        // public Camera Camera => _camera;
        public CameraMovement CameraMovement => _cameraMovement;
        public Transform Player => player;
        // public GameUI UI => gameUi;

        public static GameManager Instance { get; private set; }

        private CameraMovement _cameraMovement;

        void Awake()
        {
            // _camera = cam.GetComponent<Camera>();
            _cameraMovement = cam.GetComponent<CameraMovement>();
            Instance = this;
        }

        // private Camera _camera;

        void Start()
        {
            LockCursor();
            // LevelManager.instance.onAllEnemiesKilled = () =>
            // {
            //     AudioManager.instance.AudioSource.Stop();
            //     AudioManager.instance.AudioSource.clip = bossAudioClip;
            //     AudioManager.instance.AudioSource.Play();
            //     Destroy(obstacleToBoss);
            //     boss.enabled = true;
            // };

            // LevelManager.instance.onBossKilled = () =>
            // {
            //     Win();
            // };
        }

        private void Update()
        {
            if (settingsOpened) return;

            // if (Input.GetKeyDown(KeyCode.Escape) && gameUi.IntroUI.activeSelf)
            // {
            //     gameUi.IntroUI.SetActive(false);
            //     return;
            // }
            
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
            // sceneLoader.Reload();
        }
        
        public void GoToMainMenu()
        {
            // sceneLoader.GoToScene("MainMenu");
        }

        public void OpenSettings()
        {
            settingsOpened = true;
            // SettingsManager.instance.Open(() =>
            // {
            //     settingsOpened = false;
            // });
        }
}
