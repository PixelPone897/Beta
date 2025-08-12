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
    public class TakePlayerTurnData : CombatActionData
    {
        [SerializeReference, SubclassSelector]
        private IInputService inputService;

        [SerializeReference, SubclassSelector]
        private ILoggerService loggerService;

        [SerializeField]
        private GameObject menuVisual;

        public override CombatAction BuildAction(UnityServiceProvider serviceProvider)
        {
            serviceProvider.RegisterService(inputService);
            serviceProvider.RegisterService(loggerService);
            serviceProvider.RegisterContext(menuVisual);
            var temp = new TakePlayerTurnAction(serviceProvider);
            temp.CombatActionSteps = CreateSteps(temp);
            return temp;
        }
    }
}
