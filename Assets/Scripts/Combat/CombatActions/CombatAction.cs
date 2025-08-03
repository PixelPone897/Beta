using Assets.Scripts.Combat.CombatStepDatas;
using Assets.Scripts.Combat.CombatSteps;
using Assets.Scripts.Services;
using Scripts.Actors;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Combat.CombatActions
{
    /// <summary>
    /// Represents an action in battle (taking a turn, shooting, etc).
    /// </summary>
    /// <remarks>
    /// This was constructed and injected with the proper services/contexts through
    /// a CombatActionData's BuildAction() method. If a CombatActionData is the
    /// scafolding of a CombatAction, think of CombatAction as being constructed,
    /// runtime version of that.
    /// </remarks>
    /// <seealso cref="CombatAction"/>
    public abstract class CombatAction
    {
        /// <summary>
        /// Represents how soon the CombatAction is due to execute.
        /// </summary>
        /// <remarks>
        /// <para>
        ///  The CombatAction with the lowest CombatAction value is the one that is set to execute next-
        ///  it is removed from the CombatActionQueue and all the other CountDown values are decreased by one.
        /// </para>
        ///  <para>
        ///  Note!- CountDown is only used to determine the order of a CombatAction, not how much
        ///  time it will take for a CombatAction to run.
        /// </para>
        /// </remarks>
        public int CountDown { get; set; }

        /// <summary>
        /// The duration of how long this CombatAction has been running for.
        /// </summary>
        public float CurrentDuration => Time.time - startTime;

        /// <summary>
        /// The time in which this CombatAction started.
        /// </summary>
        protected float startTime;

        /// <summary>
        /// The service provider injected into this action.
        /// </summary>
        /// <remarks>
        /// This is passed into the CombatAction so that its children CombatSteps
        /// can access it if needed.
        /// </remarks>
        public UnityServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// Instance to the BattleManager.
        /// </summary>
        public BattleManager BattleManagerRef { get; private set; }

        /// <summary>
        /// Indicates "who" owns the CombatAction. Useful when removing all
        /// CombatActions associated with a certain owner from
        /// CombatActionQueue.
        /// </summary>
        public ActorSpecialStats Owner { get; private set; }

        public Queue<CombatStep> CombatSteps { get; set; }
        protected CombatStep currentCombatStep;

        public CombatAction(UnityServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Owner = ServiceProvider.GetContext<ActorSpecialStats>();
            BattleManagerRef = ServiceProvider.GetContext<BattleManager>();
            currentCombatStep = null;
        }

        /// <summary>
        /// Runs when the CombatAction first starts (can be used to set up values if needed).
        /// </summary>
        /// <remarks>Think of this like Monobehavior's OnEnable() method.</remarks>
        public virtual void StartAction()
        {
            startTime = Time.time;
        }

        /// <summary>
        /// Updates logic of CombatAction every frame.
        /// </summary>
        /// <remarks>Think of this like Monobehavior's Update() method.</remarks>
        public virtual void UpdateAction()
        {
            while (CombatSteps.Count > 0)
            {
                if (currentCombatStep != null && !currentCombatStep.IsFinished())
                {
                    currentCombatStep.UpdateStep();
                    return;
                }

                currentCombatStep?.EndStep();
                currentCombatStep = CombatSteps.Dequeue();

                if (currentCombatStep.CanBePerformed())
                {
                    currentCombatStep.StartStep();
                }
                else
                {
                    CombatSteps.Clear(); // Cancel remaining steps if one is invalid
                }
            }
        }

        /// <summary>
        /// Cleans up logic associated with CombatAction.
        /// </summary>
        /// <remarks>Think of this like Monobehavior's OnDisable() method.</remarks>
        public abstract void EndAction();

        /// <summary>
        /// Indicates if the CombatAction is finished.
        /// </summary>
        /// <returns>
        /// True- if the CombatAction is finished.
        /// False- if the CombatAction is not finished.
        /// </returns>
        public abstract bool IsFinished();

        /// <summary>
        /// Indicates if the CombatAction can be performed at all.
        /// </summary>
        /// <returns>
        /// True- if the CombatAction can be performed.
        /// False- if the CombatAction can not be performed.
        /// </returns>
        public virtual bool CanBePerformed() => true;

        /// <summary>
        /// Automatically clears all registered context types from the service provider.
        /// Call this at the end of this action to avoid leaking context into future actions.
        /// </summary>
        public void ClearContext()
        {
            ServiceProvider.ClearContexts();
        }

        public void AddCombatStep(CombatStepData data)
        {
            CombatStep step = data.BuildStep(this);
            CombatSteps.Enqueue(step);
        }

    }
}
