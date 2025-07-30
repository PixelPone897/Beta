using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Combat
{
    public interface IInputService
    {
        public event EventHandler<Vector2> InputMove;
        public abstract void EnableInput();
        public abstract void DisableInput();
    }
}
