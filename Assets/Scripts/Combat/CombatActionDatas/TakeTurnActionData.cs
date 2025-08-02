using Assets.Scripts.Combat.CombatActions;
using Assets.Scripts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Combat.CombatActionDatas
{
    public class TakeTurnActionData : CombatActionData
    {
        public override CombatAction BuildAction(UnityServiceProvider serviceProvider)
        {
            TakeTurnAction newAction = new TakeTurnAction(serviceProvider);
            newAction.CombatSteps = CreateSteps(newAction);
            return newAction;
            
        }
    }
}
