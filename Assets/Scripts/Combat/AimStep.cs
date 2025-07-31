using Assets.Scripts.Service;
using UnityEngine;

namespace Assets.Scripts.Combat
{
    public class AimStep : CombatStep
    {
        private IInputService input;

        public AimStep(IInputService inputService)
        {
            input = inputService;
        }

        public override void Execute()
        {
            input.EnableInput();
            input.OnMoveInput += OnMoveInputReceived;
        }

        private void OnMoveInputReceived(object sender, Vector2 e)
        {
            Debug.Log("TESTING! " + e);
        }
    }
}
