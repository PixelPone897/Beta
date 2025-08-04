using Assets.Scripts.Services;
using System;
using UnityEngine;

namespace Assets.Scripts.Combat.CombatActions
{
    public class TakeTurnAction : CombatAction
    {
        private IInputService inputService;
        private ILoggerService loggerService;
        private bool isFinished;

        public TakeTurnAction(UnityServiceProvider serviceProvider) : base(serviceProvider)
        {
            inputService = serviceProvider.GetService<IInputService>();
            loggerService = serviceProvider.GetService<ILoggerService>();
            isFinished = false;
        }

        public override void StartAction()
        {
            // Debug.Log("ACTION STARTED!");
            base.StartAction();
            inputService.EnableInput();
            loggerService.EnableLogging();
            inputService.OnMoveInput += InputService_OnMoveInput;
            inputService.OnSelectInput += InputService_OnSelectInput;
            loggerService.Log("STARTED ACTION!");
        }

        private void InputService_OnSelectInput(object sender, bool e)
        {
            loggerService.Log("BUTTON PRESSED!");
            isFinished = true;
        }

        private void InputService_OnMoveInput(object sender, Vector2 e)
        {
            loggerService.Log("CURRENT INPUT: "+e);
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

        public override string ToString()
        {
            return "TakeTurnAction, CountDown: " + CountDown;
        }
    }
}
