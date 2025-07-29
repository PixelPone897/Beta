using Scripts.Actors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Combat
{
    /// <summary>
    /// Represents a full combat decision in battle such as "Fire" or "Reload".
    /// Manages a queue of CombatSteps which actually execute the action.
    /// </summary>
    public abstract class CombatAction
    {

        /// <summary>
        /// Represents how soon the CombatAction is due to execute.
        /// </summary>
        /// <remarks>
        /// <para>
        ///  The CombatAction with the lowest CountDown value is the one that is set to execute next- it is removed from 
        ///  the CombatActionQueue and all the other CountDown values are decreased by one.
        /// </para>
        ///  <para>
        ///  Note!- CountDown is only used to determine the order of a CombatAction, not how much time it will take for 
        ///  a CombatAction to run.
        /// </para>
        /// </remarks>
        public int CountDown { get; set; }

        /// <summary>
        /// The time in which this CombatAction started.
        /// </summary>
        protected float startTime;

        /// <summary>
        /// The duration of how long this CombatAction has been running for.
        /// </summary>
        public float CurrentDuration => Time.time - startTime;

        /// <summary>
        /// Indicates "who" owns the CombatAction. Useful when removing all CombatActions associated with
        /// a certain owner from CombatActionQueue.
        /// </summary>
        public ActorSpecialStats Owner { get; set; }

        /// <summary>
        /// The instance of BattleManager that is associated with this CombatState.
        /// </summary>
        protected BattleManager battleManager;

        protected Queue<CombatStep> combatSteps;

        public virtual void Initialize(BattleManager bm, ActorSpecialStats owner)
        {
            battleManager = bm;
            Owner = owner;
            SetupSteps();
        }

        /// <summary>
        /// Define the sequence of CombatSteps this action will execute.
        /// </summary>
        protected abstract void SetupSteps();

        /// <summary>
        /// Updates logic each frame while this CombatAction is active.
        /// </summary>
        public virtual void UpdateAction()
        {
            // Optional override in derived classes for per-frame behavior
        }

        /// <summary>
        /// Cleans up Action- logic called when all steps of this CombatAction is finished.
        /// </summary>
        public virtual void EndAction()
        {
            // Cleanup, notifications, etc. (optional override)
        }

        /// <summary>
        /// Coroutine that runs all steps in sequence.
        /// </summary>
        public virtual IEnumerator Execute()
        {
            while (combatSteps.Count > 0)
            {
                CombatStep step = combatSteps.Dequeue();
                yield return step.Execute(battleManager, this);
            }
        }

        /// <summary>
        /// Indicates if CombatAction can be performed.
        /// </summary>
        /// <returns>
        /// True- if the CombatAction can be performed.
        /// False- if the CombatAction can't be performed.
        /// </returns>
        public abstract bool CanBePerformed();

        /// <summary>
        /// Indicates if the CombatState is finished.
        /// </summary>
        /// <returns>
        /// True- if the CombatState is finished.
        /// False- if the CombatState is not finished.
        /// </returns>
        public abstract bool IsFinished();

    }

    /// <summary>
    /// Represents a discrete phase or step within a CombatAction.
    /// </summary>
    public abstract class CombatStep
    {
        /// <summary>
        /// Run the logic for this step.
        /// Should yield until the step is complete (e.g., player input, animation, delay).
        /// </summary>
        /// <param name="battleManager">The BattleManager to get global battle context.</param>
        /// <param name="parentAction">The CombatAction that owns this step.</param>
        public abstract IEnumerator Execute(BattleManager battleManager, CombatAction parentAction);
    }
}
