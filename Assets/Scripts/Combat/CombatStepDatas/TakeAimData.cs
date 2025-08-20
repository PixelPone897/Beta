using Assets.Scripts.Combat.CombatActions;
using Assets.Scripts.Combat.CombatSteps;
using Assets.Scripts.Combat.SelectionAreas;
using Assets.Scripts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Combat.CombatStepDatas
{
    [Serializable]
    public class TakeAimData : CombatStepData
    {
        [SerializeReference, SubclassSelector]
        private SelectionAreaBase selectionArea;

        public override CombatStep BuildStep(CombatAction combatAction)
        {
            return new TakeAim(combatAction, selectionArea);
        }
    }
}
