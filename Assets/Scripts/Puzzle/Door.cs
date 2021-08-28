using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private List<DoorTrigger> _doorTriggers;
        [SerializeField] private Transform _doorMesh;
        [SerializeField] private Vector3 _doorOpenOffset;
        [SerializeField] private bool _canBeClosed = false;

        private Vector3 _doorOpenPosition;
        private Vector3 _doorClosePosition;
        private bool _isClosed = true;
        private int _triggersActivated = 0;
        private Collider _collider;
        private Coroutine _movingCoroutine;

        private const float SecondsToMoveDoor = 2f;

        void Awake()
        {
            _collider = transform.GetComponent<Collider>();
        }
        
        void Start()
        {
            _doorOpenPosition = _doorMesh.position + _doorOpenOffset;
            _doorClosePosition = _doorMesh.position;

            foreach (DoorTrigger doorTrigger in _doorTriggers)
            {
                doorTrigger.OnActivateTrigger += () =>
                {
                    _triggersActivated++;
                    CheckDoor();
                };
                
                doorTrigger.OnDeactivateTrigger += () =>
                {
                    _triggersActivated--;
                    CheckDoor();
                };
            }
        }

        void CheckDoor()
        {
            if (_triggersActivated == _doorTriggers.Count)
            {
                Open();
            }
            else
            {
                Close();
            }
        }

        void Open()
        {
            if (!_isClosed) return;
            _isClosed = false;
            _collider.enabled = false;

            if (_movingCoroutine != null)
            {
                StopCoroutine(_movingCoroutine);
            }
            _movingCoroutine = StartCoroutine(ChangePosition(_doorOpenPosition));
        }

        void Close()
        {
            if (_isClosed) return;
            if (!_canBeClosed) return;
            _collider.enabled = true;
            _isClosed = true;

            if (_movingCoroutine != null)
            {
                StopCoroutine(_movingCoroutine);
            }
            _movingCoroutine = StartCoroutine(ChangePosition(_doorClosePosition));
        }
        
        private IEnumerator ChangePosition(Vector3 endPosition)
        {
            Vector3 startPosition = _doorMesh.position;

            float elapsedTime = 0;
            while (elapsedTime < SecondsToMoveDoor)
            {
                elapsedTime += Time.deltaTime;
                Vector3 newPosition = Vector3.Lerp(startPosition, endPosition, (elapsedTime / SecondsToMoveDoor));
                _doorMesh.transform.position = newPosition;
                yield return null;
            }

            yield return null;
        }

    }
}