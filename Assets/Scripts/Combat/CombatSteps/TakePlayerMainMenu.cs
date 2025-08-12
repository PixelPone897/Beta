using Assets.Scripts.Combat.CombatActions;
using Assets.Scripts.Menu;
using Assets.Scripts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Combat.CombatSteps
{
    public class TakePlayerMainMenu : CombatStep
    {
        private int currentMenuIndex;
        private List<string> testMenu;

        private ILoggerService serviceLogger;
        private IInputService serviceInputService;
        private GameObject menuParentObject;
        private IMenuVisual menuVisual;

        public TakePlayerMainMenu(CombatAction parent,
            ILoggerService logger, IInputService input, GameObject menuVisualObject) : base(parent)
        {
            currentMenuIndex = 0;
            testMenu = new List<string>()
            {
                "Attack", "Item"
            };

            serviceLogger = logger;
            serviceInputService = input;
            menuParentObject = menuVisualObject;
            if(menuVisualObject != null )
            {
                menuVisual = menuParentObject.GetComponent<IMenuVisual>();
            }
        }

        public override void StartStep()
        {
            serviceInputService.EnableInput();
            serviceLogger?.EnableLogging();
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
        }

        public override void UpdateStep()
        {
            
        }

        public override void EndStep()
        {
            serviceInputService.DisableInput();
            serviceLogger.DisableLogging();
            menuVisual.HideMenu();
            serviceInputService.OnMovePerformed -= ServiceInputService_OnMovePerformed;
            serviceInputService.OnSelectCanceled -= ServiceInputService_OnSelectCanceled;
        }

        public override bool IsFinished()
        {
            return false;
        }
    }
}
