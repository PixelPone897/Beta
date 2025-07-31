using Assets.Scripts.Service;
using System;

namespace Assets.Scripts.Combat
{
    [Serializable]
    public abstract class CombatStepData
    {
        public abstract CombatStep Create(UnityServiceProvider serviceProvider);

    }
}

