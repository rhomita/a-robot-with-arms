using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;

namespace Puzzle
{
    public class Door : MonoBehaviour
    {
        [Header("Behaviour")] [SerializeField] private List<Trigger> _doorTriggers;
        [SerializeField] private Transform _doorMesh;
        [SerializeField] private Vector3 _doorOpenOffset;
        [SerializeField] private bool _canBeClosed = false;

        [Header("Visual")] [SerializeField] private Material _activatedMaterial;
        [SerializeField] private Material _deactivatedMaterial;
        [SerializeField] private MeshRenderer _meshToChangeMaterial;
        [SerializeField] private int _materialToChangeIndex;

        private Vector3 _doorOpenPosition;
        private Vector3 _doorClosePosition;
        private bool _isClosed = true;
        private int _triggersActivated = 0;
        private Collider _collider;
        private Coroutine _movingCoroutine;

        private const float SecondsToMoveDoor = 3f;
        private const float SecondsToShowCamera = 3.2f;

        private SoundEffect _audioSource;

        void Awake()
        {
            _collider = transform.GetComponent<Collider>();
            _audioSource = transform.GetComponent<SoundEffect>();
        }

        void Start()
        {
            _doorOpenPosition = _doorMesh.position + _doorOpenOffset;
            _doorClosePosition = _doorMesh.position;

            foreach (Trigger doorTrigger in _doorTriggers)
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

            if (!_canBeClosed)
            {
                GameManager.Instance.CameraMovement.SetTemporaryTarget(transform, SecondsToShowCamera);
            }

            Material[] materials = _meshToChangeMaterial.materials;
            materials[_materialToChangeIndex] = _activatedMaterial;
            _meshToChangeMaterial.materials = materials;
            _audioSource.Play();

            if (!_canBeClosed)
            {
                foreach (Trigger doorTrigger in _doorTriggers)
                {
                    Destroy(doorTrigger);
                }
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

            Material[] materials = _meshToChangeMaterial.materials;
            materials[_materialToChangeIndex] = _deactivatedMaterial;
            _meshToChangeMaterial.materials = materials;

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