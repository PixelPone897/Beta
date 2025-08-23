using Assets.Scripts.Combat.CombatActionDatas;
using Assets.Scripts.Combat.CombatActions;
using Assets.Scripts.Services;
using Scripts;
using Scripts.Actors;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Combat
{
    public class BattleManager : MonoBehaviour
    {
        private bool isBattleFinished;

        [SerializeReference, SubclassSelector]
        private CombatActionData testActionData;

        private CombatAction currentAction;
        [SerializeField]
        private List<ActorSpecialStats> entityList;
        private List<CombatAction> actionQueue;

        [SerializeField]
        private GameObject loggerVisual;

        // Temporary for now- refractor later when needed
        [field: SerializeField]
        public Grid GridProperty { get; set; }

        public Tilemap MainGridMap { get; set; }

        [SerializeField]

        private void Awake()
        {
            entityList ??= new List<ActorSpecialStats>();
            actionQueue = new List<CombatAction>();
            currentAction = null;
            isBattleFinished = false;
        }

        private void Start()
        {
            UnityServiceProvider test = new UnityServiceProvider();
            ILoggerService logger = loggerVisual.GetComponent<ILoggerService>();
            MainGridMap = GridProperty.transform.Find("GridLines").GetComponent<Tilemap>();

            BoundsInt bounds = MainGridMap.cellBounds;

            // Iterate through each position
            foreach (Vector3Int pos in bounds.allPositionsWithin)
            {
                if (MainGridMap.HasTile(pos)) // only print filled cells
                {
                    Debug.Log($"Tile at index: {pos}");
                }
            }

            test.RegisterService(logger);
            test.RegisterContext(this);

            AddCombatAction(testActionData, -1, test);
        }

        private void Update()
        {
            if (currentAction != null && !currentAction.IsFinished())
            {
                currentAction.UpdateAction();
            }
            else
            {
                currentAction?.EndAction();

                while (actionQueue.Count > 0)
                {
                    currentAction = actionQueue[0];
                    actionQueue.RemoveAt(0);

                    if (currentAction.CanBePerformed())
                    {
                        currentAction.StartAction();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Gets starting turn order
        /// </summary>
        private void GetInitiative()
        {
            Dice initiativeBase = new Dice("1d10");
            Dice coin = new Dice("1d2");
            entityList.Sort(delegate (ActorSpecialStats one, ActorSpecialStats two)
            {
                // Total Agility and Perception is used here

                float temp = one.Perception.GetValue() + one.Agility.GetValue();

                float initiativeOne = (temp / 2) + initiativeBase.RollDice();
                temp = two.Perception.GetValue() + two.Agility.GetValue();
                float initiativeTwo = (temp / 2) + initiativeBase.RollDice();

                int compare = initiativeOne.CompareTo(initiativeTwo);

                // Negative value of CompareTo is returned in order to make Actors with
                // higher stats toward front of order
                if (compare != 0)
                {
                    return -compare;
                }

                // If they are both equal, then use Perception
                compare = one.Perception.GetValue().CompareTo(two.Perception.GetValue());
                if (compare != 0)
                {
                    return -compare;
                }

                // If they are both equal, then use Luck
                compare = one.Luck.GetValue().CompareTo(two.Luck.GetValue());
                if (compare != 0)
                {
                    return -compare;
                }

                // If that fails, just flip a coin
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

        /// <summary>
        /// Checks if an Actor has at least one CombatAction associated with it currently in the CombatActionQueue.
        /// </summary>
        /// <remarks>
        /// Note!- That while CombatActions will get disconnected from
        /// </remarks>
        /// <param name="actor">The Actor that is being looked for.</param>
        /// <returns>
        /// True- if at least one CombatAction is found in the current CombatActionQueue.
        /// False- if no CombatActions are found in the current CombatActionQueue.
        /// </returns>
        public bool DoesActorHaveCombatAction(ActorSpecialStats actor)
        {

            foreach (CombatAction queueEvent in actionQueue)
            {
                if (queueEvent.Owner == actor)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Removes all CombatStates associated with an Actor in the CombatActionQueue.
        /// </summary>
        /// <param name="actor">The Actor whose events are getting removed.</param>
        public void RemoveEventsOwnedBy(ActorSpecialStats actor)
        {

            for (int i = 0; i < actionQueue.Count; i++)
            {
                if (actionQueue[i].Owner == actor)
                {
                    actionQueue.RemoveAt(i);
                }
            }
        }

        public void AddCombatAction(CombatActionData newActionData, int countDown,
            UnityServiceProvider serviceProvider)
        {
            CombatAction combatAction = newActionData.BuildAction(serviceProvider);
            combatAction.CountDown = countDown;

            for(int i = 0; i < actionQueue.Count; i++)
            {
                if(countDown < actionQueue[i].CountDown)
                {
                    actionQueue.Insert(i, combatAction);
                    return;
                }
            }

            // If we made it to the end of the current CombatActionQueue, just add
            // it add the end
            actionQueue.Add(combatAction);
        }

        public override string ToString()
        {
            string baseString = "Combat Action order:\n";
            foreach(CombatAction combatAction in actionQueue)
            {
                string actionString = combatAction.ToString();
                baseString += actionString + "\n";
            }
            return baseString;
        }
    }
}
