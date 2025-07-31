using Assets.Scripts.Combat;
using Assets.Scripts.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Combat
{
    [Serializable]
    public class AimStepData : CombatStepData
    {
        public override CombatStep Create(UnityServiceProvider serviceProvider)
        {
            IInputService service = serviceProvider.GetService<IInputService>();
            return new AimStep(service);
        }
    }
}
