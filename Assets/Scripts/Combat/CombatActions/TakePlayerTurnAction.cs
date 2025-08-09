using Assets.Scripts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Combat.CombatActions
{
    public class TakePlayerTurnAction : CombatAction
    {
        public TakePlayerTurnAction(UnityServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override bool IsFinished()
        {
            return false;
        }

        public override void EndAction()
        {
            
        }
    }
}
