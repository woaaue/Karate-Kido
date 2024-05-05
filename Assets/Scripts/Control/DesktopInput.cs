using System;
using UnityEngine;

namespace Scripts.Control
{
    public class DesktopInput : MonoBehaviour, IInput
    {
        public event Action<Vector3> ClickDown;

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                ClickDown.Invoke(worldPosition);
            }
        }
    }
}
