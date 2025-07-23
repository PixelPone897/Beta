using Scripts.Actors;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.CombatActions
{
    public class BattleManager : MonoBehaviour
    {
        private int subStateIndex;

        /// <summary>
        /// The list of Actors that are currently in battle.
        /// </summary>
        [SerializeField] private List<ActorSpecialStats> entityList;

        /// <summary>
        /// Maintains the CombatStates that are waiting to be executed in battle.
        /// </summary>
        /// <remarks>
        /// CombatStates at the front of the queue are executed first (have lower CountDown values)
        /// CombatStates are inserted and ordered by their CountDown value.
        /// </remarks>
        private List<CombatAction> combatStateQueue;

        /// <summary>
        /// Any CombatAction substates that are associated with the current CombatAction being run
        /// </summary>
        private List<CombatAction> substateQueue;
        private CombatAction currentSubstate;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Sets up the initial turn order for a battle.
        /// </summary>
        private void GetInitiative()
        {
            Dice initiativeModifier = new Dice("1d10");
            Dice coin = new Dice("1d2");

            entityList.Sort(delegate (ActorSpecialStats one, ActorSpecialStats two)
            {
                //Negative value of CompareTo is returned in order to make Actors with higher stats toward front of order
                //Do I use Base values here?
                int initiativeTotalOne = (int)one.Perception.BaseValue + initiativeModifier.RollDice();
                int initiativeTotalTwo = (int)two.Perception.BaseValue + initiativeModifier.RollDice();

                int compare = initiativeTotalOne.CompareTo(initiativeTotalTwo);
                if (compare != 0)
                {
                    return -compare;
                }

                //If Initiative rolls are both equal, next compare Perception
                compare = one.Agility.CompareTo(two.Agility);
                if (compare != 0)
                {
                    return -compare;
                }

                //If Agilities are both equal, then next use Luck
                compare = one.Luck.CompareTo(two.Luck);
                if (compare != 0)
                {
                    return -compare;
                }

                //If all fails, then just flip a coin for position
                int coinResult = coin.RollDice();
                if (coinResult == 1)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            });
        }
    }
}
