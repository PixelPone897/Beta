using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Assets.Scripts.Services
{
    /// <summary>
    /// Service that handles input from the player.
    /// </summary>
    [Serializable]
    public class CombatPlayerInputService : IInputService
    {
        public event EventHandler<Vector2> OnMovePerformed;
        public event EventHandler<bool> OnSelectPerformed;
        public event EventHandler<bool> OnSelectCanceled;

        /// <summary>
        /// Input Actions used by Unity to handle player input events.
        /// </summary>
        /// <remarks>
        /// Note- events are useful for responding to when inputs are pressed.
        /// However, they will only be triggered once! If you want to have
        /// repeated logic run while an input is pressed, it is better to
        /// poll for an action in a Update() method rather than listening
        /// to an event.
        /// </remarks>
        private TestInput inputActions;

        public void EnableInput()
        {
            inputActions = new TestInput();
            inputActions.Combat.Enable();
            inputActions.Combat.Move.performed += Move_performed;
            inputActions.Combat.Select.performed += Select_performed;
            inputActions.Combat.Select.canceled += Select_canceled;
        }

        private void Move_performed(CallbackContext inputContext)
        {
            OnMovePerformed?.Invoke(this, inputContext.ReadValue<Vector2>());
        }

        private void Select_performed(CallbackContext inputContext)
        {
            OnSelectPerformed?.Invoke(this, inputContext.ReadValueAsButton());
        }

        private void Select_canceled(CallbackContext inputContext)
        {
            OnSelectCanceled?.Invoke(this, inputContext.ReadValueAsButton());
        }

        public void DisableInput()
        {
            inputActions.Combat.Move.performed -= Move_performed;
            inputActions.Combat.Select.performed -= Select_performed;
            inputActions.Combat.Select.canceled -= Select_canceled;
            inputActions.Combat.Disable();
        }
    }
}
