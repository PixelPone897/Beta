using System;
using UnityEngine;

namespace Assets.Scripts.Services
{
    /// <summary>
    /// Interface for handling input.
    /// </summary>
    /// <remarks>This is used to abstract "input" sources (direct player input vs
    /// AI methods for example) so that they can be handled in a similar
    /// manner.</remarks>
    internal interface IInputService
    {
        /// <summary>
        /// Event triggered when "move" input is retrived (either through
        /// direct player input, an AI method, etc).
        /// </summary>
        public event EventHandler<Vector2> OnMoveInput;

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
