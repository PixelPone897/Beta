using Assets.Scripts.Items;
using Assets.Scripts.Service;
using System;
using UnityEngine;

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
