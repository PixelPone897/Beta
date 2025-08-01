using Assets.Scripts.Items;
using Assets.Scripts.Service;
using Scripts.Actors;
using System;
using System.Collections.Generic;
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

        protected CombatStep currentCombatStep;

        public virtual void StartAction(BattleManager bm, ActorSpecialStats owner)
        {
            battleManager = bm;
            Owner = owner;
        }

        /// <summary>
        /// Updates logic each frame while this CombatAction is active.
        /// </summary>
        public virtual void UpdateAction()
        {
            while(combatSteps.Count > 0)
            {
                if(currentCombatStep != null && !currentCombatStep.IsFinished())
                {
                    currentCombatStep.UpdateStep();
                }
                else
                {
                    currentCombatStep?.EndStep();
                    currentCombatStep = combatSteps.Dequeue();
                    if(currentCombatStep.CanBePerformed())
                    {
                        currentCombatStep.StartStep(battleManager, this);
                    }
                    else
                    {
                        combatSteps.Clear();
                    }
                }
            }
            // Optional override in derived classes for per-frame behavior
        }

        /// <summary>
        /// Cleans up Action- logic called when all steps of this CombatAction is finished.
        /// </summary>
        public abstract void EndAction();

        /// <summary>
        /// Indicates if CombatAction can be performed.
        /// </summary>
        /// <returns>
        /// True- if the CombatAction can be performed.
        /// False- if the CombatAction can't be performed.
        /// </returns>
        public abstract bool CanBePerformed();

        /// <summary>
        /// Indicates if the CombatAction is finished.
        /// </summary>
        /// <returns>
        /// True- if the CombatAction is finished.
        /// False- if the CombatAction is not finished.
        /// </returns>
        public abstract bool IsFinished();

        public virtual void InjectContextToSteps<T>(T context)
        {
            foreach (var step in combatSteps)
            {
                if (step is IInjectContext<T> injectable)
                {
                    injectable.InjectContext(context);
                }
            }
        }
    }

    public class TestAction : CombatAction
    {
        public TestAction(Queue<CombatStep> combatSteps)
        {
            this.combatSteps = combatSteps;
        }

        public override bool CanBePerformed()
        {
            throw new NotImplementedException();
        }

        public override void EndAction()
        {
            throw new NotImplementedException();
        }

        public override bool IsFinished()
        {
            throw new NotImplementedException();
        }
    }
}
