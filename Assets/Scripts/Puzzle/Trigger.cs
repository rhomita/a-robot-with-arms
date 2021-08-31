using UnityEngine;

namespace Puzzle
{
    public abstract class Trigger : MonoBehaviour
    {
        public delegate void OnActivateDelegate();
        public OnActivateDelegate OnActivateTrigger;

        public delegate void OnDeactivateDelegate();
        public OnDeactivateDelegate OnDeactivateTrigger;

        protected void Activate()
        {
            OnActivateTrigger?.Invoke();
            OnActivate();
        }

        protected void Deactivate()
        {
            OnDeactivateTrigger?.Invoke();
            OnDeactivate();
        }

        protected abstract void OnActivate();

        protected abstract void OnDeactivate();
    }
}