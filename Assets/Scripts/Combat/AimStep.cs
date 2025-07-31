using Assets.Scripts.Service;
using System;
using UnityEngine;

namespace Assets.Scripts.Combat
{
    public class AimStep : CombatStep, IInjectService<IInputService>
    {
        private IInputService input;

        public void Inject(IInputService instance) => input = instance;

        private void Input_OnMoveInput(object sender, Vector2 input)
        {
            Debug.Log("TESTING: " + input);
        }

        public override void Execute(BattleManager battleManager, CombatAction parentAction)
        {
            input.EnableInput();
            input.OnMoveInput += Input_OnMoveInput;
        }
    }
}
