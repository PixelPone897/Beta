using Assets.Scripts.Combat.CombatActions;
using Assets.Scripts.Menu;
using Assets.Scripts.Services;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Combat.CombatSteps
{
    public class TakePlayerMainMenu : CombatStep
    {
        private int currentMenuIndex;
        private bool isFinished;
        private List<string> testMenu;

        private ILoggerService serviceLogger;
        private IInputService serviceInputService;
        private GameObject menuParentObject;
        private IMenuVisual menuVisual;

        public TakePlayerMainMenu(CombatAction parent, GameObject menuVisualObject) : base(parent)
        {
            currentMenuIndex = 0;
            isFinished = false;
            testMenu = new List<string>()
            {
                "Attack", "Item"
            };

            serviceLogger = parent.ServiceProvider.GetService<ILoggerService>();
            serviceInputService = parent.ServiceProvider.GetService<IInputService>();
            menuParentObject = menuVisualObject;
            if(menuParentObject != null)
            {
                menuVisual = menuParentObject.GetComponent<IMenuVisual>();
            }
        }

        public override void StartStep()
        {
            serviceInputService.EnableInput();
            serviceLogger?.EnableLogging();
            serviceLogger?.Log("Started Take Player Main Menu!");

            menuVisual?.ShowMenu();
            serviceInputService.OnSelectCanceled += ServiceInputService_OnSelectCanceled;
            serviceInputService.OnMovePerformed += ServiceInputService_OnMovePerformed;
        }

        private void ServiceInputService_OnMovePerformed(object sender, Vector2 e)
        {
            if(e.Equals(Vector2Int.left))
            {
                if(currentMenuIndex != 0)
                {
                    currentMenuIndex--;
                }
                else
                {
                    currentMenuIndex = testMenu.Count-1;
                }
            }
            else if(e.Equals(Vector2Int.right))
            {
                if(currentMenuIndex != testMenu.Count-1)
                {
                    currentMenuIndex++;
                }
                else
                {
                    currentMenuIndex = 0;
                }
            }
            serviceLogger.Log("Current Menu Index: " + currentMenuIndex);
            menuVisual?.UpdateMenu(currentMenuIndex);
        }

        private void ServiceInputService_OnSelectCanceled(object sender, bool e)
        {
            serviceLogger.Log("Button Pressed!");
            isFinished = true;
        }

        public override void UpdateStep()
        {
            
        }

        public override void EndStep()
        {
            serviceLogger?.Log("Ended Take Player Main Menu!");
            serviceLogger?.DisableLogging();

            serviceInputService.DisableInput();

            menuVisual?.HideMenu();
            serviceInputService.OnMovePerformed -= ServiceInputService_OnMovePerformed;
            serviceInputService.OnSelectCanceled -= ServiceInputService_OnSelectCanceled;
        }

        public override bool IsFinished()
        {
            return isFinished;
        }
    }
}
