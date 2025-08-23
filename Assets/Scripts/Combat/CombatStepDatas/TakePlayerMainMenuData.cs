using Assets.Scripts.Combat.CombatActions;
using Assets.Scripts.Combat.CombatSteps;
using Assets.Scripts.Menu;
using Assets.Scripts.Services;
using System;
using UnityEngine;

namespace Assets.Scripts.Combat.CombatStepDatas
{
    [Serializable]
    public class TakePlayerMainMenuData : CombatStepData
    {
        [SerializeField]
        private GameObject menuVisual;

        public override CombatStep BuildStep(CombatAction combatAction)
        {
            return new TakePlayerMainMenu(combatAction, menuVisual);
        }
    }
}
