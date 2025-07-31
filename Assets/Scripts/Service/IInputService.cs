using System;
using UnityEngine;

namespace Assets.Scripts.Service
{
    internal interface IInputService
    {
        public event EventHandler<Vector2> OnMoveInput;
        public abstract void EnableInput();
        public abstract void DisableInput();
    }
}
