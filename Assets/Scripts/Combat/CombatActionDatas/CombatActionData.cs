using Assets.Scripts.Combat.CombatActions;
using Assets.Scripts.Combat.CombatStepDatas;
using Assets.Scripts.Combat.CombatSteps;
using Assets.Scripts.Services;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Combat.CombatActionDatas
{
    /// <summary>
    /// Design instance of a CombatAction (an action in battle).
    /// </summary>
    /// <remarks>
    /// <para>
    /// When constructing a CombatAction- a major action that an Actor
    /// can perform in battle- it consists of three parts: Design, Logic, and
    /// Context. Design refers to values that are set within the inspector.
    /// Alongside this, these are used for specifying the structure of the
    /// CombatAction in question (helped with its CombatStepData substeps). 
    /// Logic refers to injecting the proper
    /// dependencies at runtime to define the behavior that uses this previously
    /// established data. This is done through the service provider. Context
    /// refers to providing any objects/actor specific objects needed for the
    /// CombatAction to perform. This is also provided by the service provider.
    /// </para>
    /// <para>
    /// Through using the CombatActionData as a base,
    /// the proper runtime instance of the CombatAction is created
    /// through the builder pattern using the BuildAction() method.
    /// </para>
    /// <para>
    /// When creating a CombatAction, first the CombatAction is created and injected,
    /// and then any CombatSteps it contains will be constructed afterwards.
    /// </para>
    /// </remarks>
    /// <seealso cref="CombatActionData"/>
    public abstract class CombatActionData
    {
        [SerializeReference, SubclassSelector]
        public List<CombatStepData> combatStepDatas;

        /// <summary>
        /// Creates all CombatSteps from the serialized CombatStepData list
        /// </summary>
        /// <param name="parent">Parent CombatAction that holds these CombatSteps.</param>
        /// <returns>A sequence of CombatSteps.</returns>
        protected Queue<CombatStep> CreateSteps(CombatAction parent)
        {
            Queue<CombatStep> steps = new Queue<CombatStep>();

            if (combatStepDatas != null)
            {
                foreach (var stepData in combatStepDatas)
                {
                    // Create a step instance using the factory method on CombatStepData
                    CombatStep step = stepData.BuildStep(parent);

                    // Enqueue it for the CombatAction to consume
                    steps.Enqueue(step);
                }
            }

            return steps;
        }

        /// <summary>
        /// Builds a CombatAction based on the data, services and contexts provided to it.
        /// </summary>
        /// <remarks>CombatActions should not be created using new. It should be through
        /// its proper BuildAction method.</remarks>
        /// <param name="serviceProvider">The service provider used to provide any services
        /// and contexts that the runtime instance of this CombatAction might need.</param>
        /// <returns>A newly created CombatAction using the data of this CombatActionData,
        /// and the proper services and contexts injected into it via the service
        /// provider.</returns>
        public abstract CombatAction BuildAction(UnityServiceProvider serviceProvider);
    }
}

