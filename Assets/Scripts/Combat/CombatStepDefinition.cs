
using Assets.Scripts.Service;
using System;

namespace Assets.Scripts.Combat
{
    [Serializable]
    public abstract class CombatStepDefinition
    {
        public abstract CombatStep CreateStep(IGameServiceProvider serviceProvider);
    }

    [Serializable]
    public class AimStepDefinition : CombatStepDefinition
    {


        public AimStepDefinition() { }

        public override CombatStep CreateStep(IGameServiceProvider serviceProvider)
        {
            IInputService inputTest = serviceProvider.GetService<IInputService>();
            return new AimStep(inputTest);
        }
    }
}
