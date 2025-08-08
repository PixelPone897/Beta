using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Assets.Scripts.Services
{
    /// <summary>
    /// Interface for handling input.
    /// </summary>
    /// <remarks>This is used to abstract "input" sources (direct player input vs
    /// AI methods for example) so that they can be handled in a similar
    /// manner.</remarks>
    public interface IInputService
    {
        // Keep events stupid- don't pass entire Callback context

        /// <summary>
        /// Event triggered when "move" input is performed (either through
        /// direct player input, an AI method, etc).
        /// </summary>
        public event EventHandler<Vector2> OnMovePerformed;

        /// <summary>
        /// Event triggered when "select" input is performed (either through
        /// direct player input, an AI method, etc).
        /// </summary>
        public event EventHandler<bool> OnSelectPerformed;

        public event EventHandler<bool> OnSelectCanceled;

        /// <summary>
        /// Enables input service.
        /// </summary>
        /// <remarks>Should be used for enabling and subscribing to EventHandlers.</remarks>
        public abstract void EnableInput();

        /// <summary>
        /// Disables input service.
        /// </summary>
        /// <remarks>Should be used for disabling and unsubscribing to EventHandlers.</remarks>
        public abstract void DisableInput();
    }
}
