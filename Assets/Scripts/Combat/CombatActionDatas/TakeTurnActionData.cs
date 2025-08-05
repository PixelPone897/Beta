using Assets.Scripts.Combat.CombatActions;
using Assets.Scripts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Combat.CombatActionDatas
{
    [Serializable]
    public class TakeTurnActionData : CombatActionData
    {
        // This will be removed later on
        [SerializeField]
        private List<MenuOption> testing;

        public override CombatAction BuildAction(UnityServiceProvider serviceProvider)
        {
            TakeTurnAction newAction = new TakeTurnAction(serviceProvider, testing);
            newAction.CombatSteps = CreateSteps(newAction);
            return newAction;
            
        }
    }
}
