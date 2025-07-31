using System;

namespace Assets.Scripts.Combat
{
    /// <summary>
    /// Represents a discrete phase or step within a CombatActionData.
    /// </summary>
    public abstract class CombatStep
    {
        /// <summary>
        /// Run the logic for this step.
        /// Should yield until the step is complete (e.g., player input, animation, delay).
        /// </summary>
        /// <param name="battleManager">The BattleManager to get global battle context.</param>
        /// <param name="parentAction">The CombatActionData that owns this step.</param>
        public abstract void Execute(BattleManager battleManager, CombatAction owner);
    }
}
