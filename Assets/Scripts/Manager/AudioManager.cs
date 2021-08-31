using UnityEngine;

namespace Manager
{
    public class AudioManager : MonoBehaviour
    {
        private AudioSource _audioSource;
        
        public static AudioManager Instance { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _audioSource = transform.GetComponent<AudioSource>();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                _audioSource.mute = !_audioSource.mute;
            }
        }
    }
}