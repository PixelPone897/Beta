using System;

namespace Assets.Scripts.Combat
{
    /// <summary>
    /// Represents a discrete phase or step within a CombatActionData.
    /// </summary>
    public abstract class CombatStep
    {
        /// <summary>
        /// Sets up current context for step.
        /// </summary>
        /// <param name="battleManager">The BattleManager to get global battle context.</param>
        /// <param name="parentAction">The CombatActionData that owns this step.</param>
        public abstract void StartStep(BattleManager battleManager, CombatAction owner);

        /// <summary>
        /// Run the logic for this step.
        /// Should eventually be made into a coroutine that needs to be yielded.
        /// </summary>
        public virtual void UpdateStep()
        {
            // Logic that runs every frame for this step.
        }

        /// <summary>
        /// Cleans up logic for Step.
        /// </summary>
        public abstract void EndStep();

        public abstract bool IsFinished();
    }
}
