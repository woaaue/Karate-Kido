using UnityEngine;

namespace Scripts.Control
{
    public class MovementHandler
    {
        private IInput _input;

        public MovementHandler(IInput input)
        {
            _input = input;
            Debug.Log(_input.GetType());

            _input.ClickDown += OnClickDown;
        }

        private void OnClickDown(Vector3 position)
        {
            Debug.Log(position);
        }
    }
}
