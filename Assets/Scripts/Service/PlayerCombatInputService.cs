using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Assets.Scripts.Service
{
    /// <summary>
    /// Service that handles input from the player.
    /// </summary>
    public class PlayerCombatInputService : IInputService
    {
        public event EventHandler<Vector2> OnMoveInput;

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
