
using Assets.Scripts.Combat.CombatActions;
using Assets.Scripts.Services;

namespace Assets.Scripts.Combat.CombatSteps
{
    public abstract class CombatStep
    {
        /// <summary>
        /// Parent CombatAction associated with this CombatStep
        /// </summary>
        protected CombatAction parentAction;

        // Services and Contexts available through the action's service provider
        protected UnityServiceProvider Services => parentAction.ServiceProvider;

        protected BattleManager BattleManagerInstance => parentAction.BattleManagerRef;

        public CombatStep(CombatAction parent)
        {
            parentAction = parent;
        }

        /// <summary>
        /// Called once when this step begins execution.
        /// </summary>
        /// <remarks>Think of this like Monobehavior's OnEnable() method.</remarks>
        public virtual void StartStep()
        {

        }

        /// <summary>
        /// Called once per frame while the step is active.
        /// </summary>
        /// <remarks>Think of this like Monobehavior's Update() method.</remarks>
        public abstract void UpdateStep();

        /// <summary>
        /// Called once when the step finishes.
        /// <remarks>Think of this like Monobehavior's OnDisable() method.</remarks>
        public virtual void EndStep() { }

        /// <summary>
        /// Whether this CombatStep is finished and the CombatAction
        /// should continue to the next step.
        /// </summary>
        /// <returns>
        /// True- if the CombatStep is finished.
        /// False- if the CombatStep is not finished.
        /// </returns>
        public abstract bool IsFinished();

        /// <summary>
        /// Indicates if the CombatStep can be performed at all.
        /// </summary>
        /// <returns>
        /// True- if the CombatStep can be performed.
        /// False- if the CombatStep can not be performed.
        /// </returns>
        public virtual bool CanBePerformed() => true;
    }
}