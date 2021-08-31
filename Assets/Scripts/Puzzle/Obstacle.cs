using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using Puzzle;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Trigger trigger;
    private SoundEffect _soundEffect;

    void Awake()
    {
        _soundEffect = transform.GetComponent<SoundEffect>();

        if (trigger != null)
        {
            trigger.OnActivateTrigger += () =>
            {
                gameObject.SetActive(false);
            };
            
            trigger.OnDeactivateTrigger += () =>
            {
                gameObject.SetActive(true);
            };
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out RobotArmPart robotArmPart))
        {
            _soundEffect.Play();
            robotArmPart.Reset();
        }
    }
}
