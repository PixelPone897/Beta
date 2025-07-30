using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Assets.Scripts.Service
{
    public class PlayerCombatInputService : IInputService
    {
        public event EventHandler<Vector2> OnMoveInput;

        private TestInput inputActions;

        public void EnableInput()
        {
            inputActions = new TestInput();
            inputActions.Combat.Enable();
            inputActions.Combat.Move.performed += OnMovePerformed;
        }

        private void OnMovePerformed(CallbackContext inputContext)
        {
            OnMoveInput?.Invoke(this, inputContext.ReadValue<Vector2>());
        }

        public void DisableInput()
        {
            inputActions.Combat.Move.performed -= OnMovePerformed;
            inputActions.Combat.Disable();
        }
    }
}
