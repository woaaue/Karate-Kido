using System;
using UnityEngine;

namespace Scripts.Control
{
    public sealed class MovementHandler : MonoBehaviour
    {
        public event Action<float> OnDirectionChanged;

        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Handle—lick();
            }
        }

        private void Handle—lick()
        {
            Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 targetPosition = worldPosition;

            OnDirectionChanged?.Invoke(targetPosition.x);
        }    
    }
}
