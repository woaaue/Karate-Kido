using System;
using UnityEngine;
using JetBrains.Annotations;

namespace Scripts.Control
{
    public sealed class ClickHandler : MonoBehaviour
    {
        public event Action<float> OnDirectionChanged;

        private Camera _mainCamera;

        private void Start() => _mainCamera = Camera.main;

        [UsedImplicitly]
        public void HandleClick()
        {
            Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 targetPosition = worldPosition;

            OnDirectionChanged?.Invoke(targetPosition.x);
        }    
    }
}
