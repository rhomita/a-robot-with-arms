using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle
{
    public class Button : DoorTrigger
    {
        [SerializeField] private Collider _triggerCollider;
        
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
            Debug.Log("Activate");
        }

        protected override void OnDeactivate()
        {
            Debug.Log("Deactivate");
        }
    }
}