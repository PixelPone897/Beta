using Scripts.Actors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Combat
{
    public class BattleManager : MonoBehaviour
    {
        /// <summary>
        /// The list of Actors that are currently in battle.
        /// </summary>
        [SerializeField] private List<ActorSpecialStats> entityList;

        /// <summary>
        /// Maintains the CombatActions that are waiting to be executed in battle.
        /// </summary>
        /// <remarks>
        /// CombatActions at the front of the queue are executed first (have lower CountDown values)
        /// CombatActions are inserted and ordered by their CountDown value.
        /// </remarks>
        private List<CombatAction> combatActionQueue;

        private CombatAction currentCombatAction;

        private void Awake()
        {
            if (entityList.Count == 0)
            {
                Debug.LogWarning("Battle Manager does not have any Actors assigned to it!", transform.gameObject);
                entityList = new List<ActorSpecialStats>();
            }

            currentCombatAction = null;
            combatActionQueue = new List<CombatAction>();
        }

        private void Update()
        {
            currentCombatAction?.UpdateAction();
        }

        private IEnumerator RunCombatAction(CombatAction action)
        {
            currentCombatAction = action;
            action.Initialize(this, action.Owner);
            yield return StartCoroutine(action.Execute());
            currentCombatAction = null;
        }

        /// <summary>
        /// Adds a CombatAction to its proper place in the CombatActionQueue.
        /// </summary>
        /// <remarks>
        /// <para>
        /// CombatActions are ordered based on their CountDown value (how soon they are expected to execute). 
        /// Adding goes like this:
        /// <br></br>
        /// If CountDown == -1, add it to the front of the queue(after all events that have -1),<br></br>
        /// If CountDown != -1, iterate through the queue until a CombatAction with a higherCountDown has been found, 
        /// and then insert CombatAction right before it.
        /// </para>
        /// </remarks>
        /// <param name="actorOwner">The owner that is associated with the new CombatAction being added.</param>
        /// <param name="newCombatAction">The new CombatAction that is being added to the CombatActionQueue.</param>
        /// <param name="eventCountDown">The CountDown that is associated with the CombatAction.</param>
        public void AddCombatEvent(ActorSpecialStats actorOwner, CombatAction newCombatAction, int eventCountDown)
        {
            if (IsEmpty())
            {
                newCombatAction.CountDown = eventCountDown;
                newCombatAction.Owner = actorOwner;
                combatActionQueue.Add(newCombatAction);
            }
            else
            {
                int properIndex = 0;
                for (int i = 0; i < combatActionQueue.Count; i++)
                {
                    newCombatAction.CountDown = eventCountDown;
                    newCombatAction.Owner = actorOwner;

                    CombatAction current = combatActionQueue[i];
                    if (current.CountDown < eventCountDown)
                    {
                        properIndex++;
                    }
                }

                combatActionQueue.Insert(properIndex, newCombatAction);
            }
        }

        /// <summary>
        /// Checks if an Actor has at least one CombatAction associated with it currently in the CombatActionQueue.
        /// </summary>
        /// <remarks>
        /// Note!- This does not include any substates that are currently being processed in the Substate Queue.
        /// Might change this for later.
        /// </remarks>
        /// <param name="actor">The Actor that is being looked for.</param>
        /// <returns>
        /// True- if at least one CombatAction is found in the current CombatActionQueue.
        /// False- if no CombatActions are found in the current CombatActionQueue.
        /// </returns>
        public bool DoesActorHaveCombatEvent(ActorSpecialStats actor)
        {

            foreach (CombatAction queueEvent in combatActionQueue)
            {
                if (queueEvent.Owner == actor)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Removes all CombatActions associated with an Actor in the CombatActionQueue.
        /// </summary>
        /// <param name="actor">The Actor whose events are getting removed.</param>
        public void RemoveEventsOwnedBy(ActorSpecialStats actor)
        {

            for (int i = 0; i < combatActionQueue.Count; i++)
            {
                if (combatActionQueue[i].Owner == actor)
                {
                    combatActionQueue.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Clears the entirety of the CombatActionQueue.
        /// </summary>
        public void ClearCombatActionQueue()
        {
            combatActionQueue.Clear();
        }

        /// <summary>
        /// Returns if the CombatActionQueue is currently empty.
        /// </summary>
        /// <remarks>
        /// This does not check for any substates in the 
        /// even if the current CombatActionQueue is empty.
        /// </remarks>
        /// <returns>
        /// True- if no CombatActions are found in the current CombatActionQueue.
        /// False- if at least one CombatAction is found in the current CombatActionQueue.
        /// </returns>
        public bool IsEmpty()
        {
            return combatActionQueue.Count == 0;
        }

    }
}
