using Assets.Scripts.Services;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Combat.CombatActions
{
    public class TakeTurnAction : CombatAction
    {
        private IInputService inputService;
        private ILoggerService loggerService;
        private bool isFinished;

        private List<MenuOption> menuOptions;
        private int currentMenuIndex;
        private int previousMenuIndex;

        public TakeTurnAction(UnityServiceProvider serviceProvider,
            List<MenuOption> menuOptions) : base(serviceProvider)
        {
            this.menuOptions = menuOptions;
            currentMenuIndex = 0;
            previousMenuIndex = -1;
            isFinished = false;
            inputService = serviceProvider.GetService<IInputService>();
            loggerService = serviceProvider.GetService<ILoggerService>();
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
        }

        private void InputService_OnMoveInput(object sender, Vector2 e)
        {
            loggerService.Log("CURRENT INPUT: "+e);
            previousMenuIndex = currentMenuIndex;
            if (e.Equals(Vector2Int.right))
            {
                if(currentMenuIndex == menuOptions.Count-1)
                {
                    currentMenuIndex = 0;
                }
                else
                {
                    currentMenuIndex++;
                }
            }
            else if (e.Equals(Vector2Int.left))
            {
                if (currentMenuIndex == 0)
                {
                    currentMenuIndex = menuOptions.Count-1;
                }
                else
                {
                    currentMenuIndex--;
                }
            }
        }

        public override void UpdateAction()
        {
            if(previousMenuIndex != currentMenuIndex)
            {
                if (previousMenuIndex != -1)
                {
                    menuOptions[previousMenuIndex].SetSelected(false);
                }
                
                menuOptions[currentMenuIndex].SetSelected(true);
            }

            base.UpdateAction();
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
