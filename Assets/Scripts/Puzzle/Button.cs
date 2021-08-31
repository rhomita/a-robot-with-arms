using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;

namespace Puzzle
{
    public class Button : Trigger
    {
        [Header("Behaviour")]
        [SerializeField] private Collider _triggerCollider;

        [Header("Visual")] 
        [SerializeField] private Material _activatedMaterial;
        [SerializeField] private Material _deactivatedMaterial;
        [SerializeField] private MeshRenderer _meshToChangeMaterial;
        [SerializeField] private int _materialToChangeIndex;

        [Header("SFX")] 
        [SerializeField] private SoundEffect _activateSoundEffect;
        [SerializeField] private SoundEffect _deactivateSoundEffect;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.Equals(_triggerCollider))
            {
                Activate();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.Equals(_triggerCollider))
            {
                Deactivate();
            }
        }

        protected override void OnActivate()
        {
            _activateSoundEffect.Play();   
            Material[] materials = _meshToChangeMaterial.materials;
            materials[_materialToChangeIndex] = _activatedMaterial;
            _meshToChangeMaterial.materials = materials;
        }

        protected override void OnDeactivate()
        {
            _deactivateSoundEffect.Play();
            Material[] materials = _meshToChangeMaterial.materials;
            materials[_materialToChangeIndex] = _deactivatedMaterial;
            _meshToChangeMaterial.materials = materials;
        }
    }
}