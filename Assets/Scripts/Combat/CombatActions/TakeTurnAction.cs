using Assets.Scripts.Services;
using System;
using UnityEngine;

namespace Assets.Scripts.Combat.CombatActions
{
    public class TakeTurnAction : CombatAction
    {
        private IInputService inputService;
        private bool isFinished;

        public TakeTurnAction(UnityServiceProvider serviceProvider) : base(serviceProvider)
        {
            inputService = serviceProvider.GetService<IInputService>();
            isFinished = false;
        }

        public override void StartAction()
        {
            Debug.Log("ACTION STARTED!");
            base.StartAction();
            inputService.EnableInput();
            inputService.OnMoveInput += InputService_OnMoveInput;
            inputService.OnSelectInput += InputService_OnSelectInput;
        }

        private void InputService_OnSelectInput(object sender, bool e)
        {
            Debug.Log("BUTTON PRESSED!");
            isFinished = true;
        }

        private void InputService_OnMoveInput(object sender, Vector2 e)
        {
            Debug.Log("TESTING! "+e);
        }

        public override void EndAction()
        {
            Debug.Log("ACTION ENDED!");
            inputService.OnMoveInput -= InputService_OnMoveInput;
            inputService.OnSelectInput -= InputService_OnSelectInput;
            inputService.DisableInput();
            ClearContext();
        }

        public override bool IsFinished()
        {
            return isFinished;
        }
    }
}
