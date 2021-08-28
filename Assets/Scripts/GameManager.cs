using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player")] 
    [SerializeField] private Transform player;
    [SerializeField] private Transform cam;

    private bool isPaused = false;

    public Transform CameraTransform => cam;
    public CameraMovement CameraMovement => _cameraMovement;
    public Transform Player => player;

    public static GameManager Instance { get; private set; }

    private CameraMovement _cameraMovement;

    void Awake()
    {
        _cameraMovement = cam.GetComponent<CameraMovement>();
        Instance = this;
    }

    void Start()
    {
        LockCursor();
    }

    private void Update()
    {
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
        UnlockCursor();
    }

    private void Resume()
    {
        Time.timeScale = 1;
        LockCursor();
    }

    public void Restart()
    {
        Time.timeScale = 1;
    }

}