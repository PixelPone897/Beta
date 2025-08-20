using Assets.Scripts.Combat.CombatActionDatas;
using Assets.Scripts.Combat.CombatActions;
using Assets.Scripts.Services;
using Scripts;
using Scripts.Actors;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
