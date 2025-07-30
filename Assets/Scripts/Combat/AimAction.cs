using Assets.Scripts.Service;
using System;
using UnityEngine;

namespace Assets.Scripts.Combat
{
    public class AimAction : CombatAction, IInjectService<IInputService>
    {
        private IInputService input;

        public override void SetupSteps()
        {
            input.EnableInput();
            input.OnMoveInput += Input_OnMoveInput;
        }

        private void Input_OnMoveInput(object sender, Vector2 input)
        {
            Debug.Log("TESTING: "+ input);
        }

        public override bool CanBePerformed()
        {
            throw new NotImplementedException();
        }

        public override bool IsFinished()
        {
            throw new NotImplementedException();
        }

        public void Inject(IInputService instance)
        {
            input = instance;
        }

    }
}
