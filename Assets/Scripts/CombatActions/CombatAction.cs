using Scripts.Actors;
using UnityEngine;

namespace Scripts.CombatActions
{
    /// <summary>
    /// Represents a phase of battle.
    /// </summary>
    /// <remarks>
    /// <para>
    ///  In Unity, there is no set order for which objects call their Awake, Start, and Update methods
    ///  (even for a GameObject with child GameObjects). This means that a child's Awake method can
    ///  run before its parent's, leading to potential null reference errors when initializing.
    /// </para>
    ///  <para>
    ///  In the context of this state machine system, it means that is possible for a state's Start method to run 
    ///  before the Start method of its associated state machine. To control the order of initilization and updating so 
    ///  that state machines' methods run before their states, these methods will be explicitly called using this class.
    /// </para>
    /// </remarks>
    public abstract class CombatAction
    {
        /// <summary>
        /// Represents how soon the CombatAction is due to execute.
        /// </summary>
        /// <remarks>
        /// <para>
        ///  The CombatAction with the lowest CountDown value is the one that is set to execute next- it is removed from 
        ///  the CombatStateQueue and all the other CountDown values are decreased by one.
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
        /// Indicates “who” owns the CombatAction. Useful when removing all CombatStates associated with a certain owner from
        /// CombatStateQueue.
        /// </summary>
        [field: SerializeField]
        public ActorSpecialStats Owner { get; set; }

        /// <summary>
        /// The instance of BattleManager that is associated with this CombatAction.
        /// </summary>
        protected BattleManager battleManager;

        /// <summary>
        /// Runs when the CombatAction is first run (can be used setup values need for CombatAction)
        /// </summary>
        /// <remarks>
        /// This is equivalent to the Start method for Monobehaviour but is being explicitly called 
        /// instead of being run on its own. It also can be run multiple times.
        /// </remarks>
        /// <param name="battleManager">The BattleManager that is associated with this CombatAction.</param>
        public virtual void StartAction(BattleManager battleManager)
        {
            startTime = Time.time;
            this.battleManager = battleManager;
            if (Owner == null)
            {
                Debug.LogWarning("This CombatState does not have any Owner associated with it!");
            }
        }

        /// <summary>
        /// Updates components associated with the CombatAction every frame.
        /// </summary>
        /// <remarks>
        /// This is equivalent to the Update method for Monobehaviour, but is being explicitly called 
        /// instead of being run on its own. 
        /// </remarks>
        public abstract void UpdateAction();

        /// <summary>
        /// Cleans up the logic for the CombatAction.
        /// </summary>
        public abstract void EndAction();

        /// <summary>
        /// Indicates if the CombatAction is finished.
        /// </summary>
        /// <returns>
        /// True- if the CombatAction is finished.
        /// False- if the CombatAction is not finished.
        /// </returns>
        public abstract bool IsFinished();

    }
}