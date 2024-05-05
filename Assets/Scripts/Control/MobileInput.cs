using System;
using UnityEngine;

namespace Scripts.Control
{
    public class MobileInput : MonoBehaviour, IInput
    {
        public event Action<Vector3> ClickDown;

        private void Update()
        {
            if (Input.touchCount == 1)
            {
                var touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    var touchPosition = touch.position;
                    var worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                    
                    ClickDown.Invoke(worldPosition);
                }
            }
        }
    }
}
