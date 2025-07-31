using Assets.Scripts.Service;
using System;
using UnityEngine;

namespace Assets.Scripts.Combat
{
    internal class AimStep : CombatStep
    {
        private IInputService input;

        public AimStep(IInputService input)
        {
            this.input = input;
        }

        private void Input_OnMoveInput(object sender, Vector2 input)
        {
            Debug.Log("TESTING: " + input);
        }

        public override void StartStep(BattleManager battleManager, CombatAction owner)
        {
            input.EnableInput();
            input.OnMoveInput += Input_OnMoveInput;
        }

        public override void EndStep()
        {
            throw new NotImplementedException();
        }

        public override bool IsFinished()
        {
            throw new NotImplementedException();
        }
    }
}
