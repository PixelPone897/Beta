using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace Assets.Scripts.Combat
{
    public class PlayerCombatInput : MonoBehaviour, IInputService
    {
        private TestInput inputActions;

        public void Awake()
        {
            inputActions = new TestInput();
        }

        public event EventHandler<Vector2> InputMove;

        public void EnableInput()
        {
            inputActions.Enable();
            inputActions.Combat.Enable();
            inputActions.Combat.Move.performed += OnMovePerformed;
        }

        private void OnMovePerformed(CallbackContext obj)
        {
            InputMove?.Invoke(this, obj.ReadValue<Vector2>());
        }

        public void DisableInput()
        {
            inputActions.Combat.Move.performed -= OnMovePerformed;
            inputActions.Combat.Disable();
            inputActions.Disable();
        }
    }
}
