using System;
using UnityEngine;

namespace Scripts.Control
{
    public interface IInput
    {
        public event Action<Vector3> ClickDown; 
    }
}
