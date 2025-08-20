using Assets.Scripts.Combat.CombatActions;
using Assets.Scripts.Combat.SelectionAreas;
using Assets.Scripts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Combat.CombatSteps
{
    public class TakeAim : CombatStep
    {
        private SelectionAreaBase shape;
        private BattleManager battleManager;
        private IInputService inputService;
        private ILoggerService loggerService;

        public TakeAim(CombatAction parent, SelectionAreaBase shape) : base(parent)
        {
            this.shape = shape;
            battleManager = parent.ServiceProvider.GetContext<BattleManager>();
            inputService = parent.ServiceProvider.GetService<IInputService>();
            loggerService = parent.ServiceProvider.GetService<ILoggerService>();
        }

        public override void StartStep()
        {
            loggerService?.EnableLogging();
            loggerService?.Log("Enabled Take Aim Step!");
        }

        public override bool IsFinished()
        {
            return false;
        }

        public override void UpdateStep()
        {
            
        }
    }
}
