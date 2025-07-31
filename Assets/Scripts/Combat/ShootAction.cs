using Assets.Scripts.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Rendering;

namespace Assets.Scripts.Combat
{
    [Serializable]
    public class ShootAction : CombatAction, IInjectService<IInputService>
    {
        private IInputService inputService;

        public void Inject(IInputService instance) => inputService = instance;

        public ShootAction() { }

        protected override void SetupSteps()
        {
            foreach(CombatStep step in combatSteps)
            {
                if(step is IInjectService<IInputService> inputInject)
                {
                    inputInject.Inject(inputService);
                }
            }
        }

        public override bool CanBePerformed()
        {
            throw new NotImplementedException();
        }

        public override bool IsFinished()
        {
            throw new NotImplementedException();
        }
    }
}
