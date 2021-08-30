using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class SoundEffect : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup audioMixerGroup;
        [SerializeField] private List<AudioClip> clips;
        [SerializeField] [Range(0f, 1f)] private float volume;
        [SerializeField] [Range(0f, 1f)] private float spatialBlend = 1f;

        private AudioSource audioSource;

        void Awake()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.volume = volume;
            audioSource.loop = false;
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = spatialBlend;
            audioSource.outputAudioMixerGroup = audioMixerGroup;
        }

        public void Play(float pitch = 1f)
        {
            if (audioSource.isPlaying) return;
            int random = Random.Range(0, clips.Count);
            AudioClip clip = clips[random];
            audioSource.Stop();
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(clip);
        }
    }
}