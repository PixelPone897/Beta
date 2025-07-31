using Assets.Scripts.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Combat
{
    [Serializable]
    public abstract class CombatActionData
    {
        [SerializeReference, SubclassSelector]
        public List<CombatStepData> combatStepDatas;

        /// <summary>
        /// Creates all CombatSteps from the serialized CombatStepData list,
        /// injecting services via the service provider.
        /// </summary>
        protected Queue<CombatStep> CreateSteps(UnityServiceProvider serviceProvider)
        {
            Queue<CombatStep> steps = new Queue<CombatStep>();

            if (combatStepDatas != null)
            {
                foreach (var stepData in combatStepDatas)
                {
                    // Create a step instance using the factory method on CombatStepData
                    CombatStep step = stepData.Create(serviceProvider);

                    // Enqueue it for the CombatAction to consume
                    steps.Enqueue(step);
                }
            }

            return steps;
        }
        public abstract CombatAction Create(UnityServiceProvider serviceProvider);
    }

    [Serializable]
    public class TestActionData : CombatActionData
    {
        public override CombatAction Create(UnityServiceProvider serviceProvider)
        {
            Queue<CombatStep> steps = CreateSteps(serviceProvider);

            return new TestAction(steps);
        }
    }
}
