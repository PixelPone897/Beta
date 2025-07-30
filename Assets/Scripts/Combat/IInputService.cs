using System;
using UnityEngine;

namespace Assets.Scripts.Combat
{
    public interface IInputService
    {
        public event EventHandler<Vector2> OnMoveReceived;
        public abstract void EnableInput();
        public abstract void DisableInput();
    }
}
