using Assets.Scripts.Combat.CombatActions;
using Assets.Scripts.Combat.CombatActionDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Combat.CombatSteps;

namespace Assets.Scripts.Combat.CombatStepDatas
{
    /// <summary>
    /// Design instance of a CombatStep (Represents a substep of a CombatAction-
    /// aiming, shooting, animations, etc).
    /// </summary>
    /// <para>
    /// When constructing a CombatStep- a substep of a CombatAction that an Actor
    /// can perform in battle- it consists of three parts: Design, Logic, and
    /// Context. Design refers to values that are set within the inspector.
    /// Logic refers to injecting the proper dependencies at runtime to define the
    /// behavior that uses this previously established data. Context
    /// refers to providing any objects/actor specific objects needed for the
    /// CombatStep to perform. Unlike CombatActions, who get their logic and context
    /// via the service provider directly, they read it from their parent CombatAction's
    /// provider (this might change in the future).
    /// </para>
    /// <para>
    /// Through using the CombatActionData as a base,
    /// the proper runtime instance of the CombatAction is created
    /// through the builder pattern using the BuildAction() method.
    /// </para>
    /// <seealso cref="CombatAction"/>
    public abstract class CombatStepData
    {
        // Might have to update this to pass service provider instead?

        /// <summary>
        /// 
        /// </summary>
        /// <param name="combatAction">The parent CombatAction associated with
        /// this CombatStep.</param>
        /// <returns>A newly created CombatStep using the data provided from
        /// this CombatStepData as well as logic and context provided from its
        /// parent CombatAction.</returns>
        public abstract CombatStep BuildStep(CombatAction combatAction);
    }
}
