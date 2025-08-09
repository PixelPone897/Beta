using Assets.Scripts.Combat.CombatActions;
using Assets.Scripts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Combat.CombatActionDatas
{
    [Serializable]
    public class TakePlayerTurnData : CombatActionData
    {
        public override CombatAction BuildAction(UnityServiceProvider serviceProvider)
        {
            var temp = new TakePlayerTurnAction(serviceProvider);
            temp.CombatActionSteps = CreateSteps(temp);
            return temp;
        }
    }
}
