using Assets.Scripts.Combat.CombatActionDatas;
using Assets.Scripts.Combat.CombatActions;
using Assets.Scripts.Services;
using NUnit.Framework;
using Scripts;
using Scripts.Actors;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Combat
{
    public class BattleManager : MonoBehaviour
    {
        private CombatAction currentAction;
        private List<ActorSpecialStats> entityList;
        private List<CombatAction> actionQueue;

        private void Awake()
        {
            actionQueue = new List<CombatAction>();
            currentAction = null;
        }

        private void Start()
        {
            GetInitiative();
            UnityServiceProvider unityServiceProvider = new();
            unityServiceProvider.RegisterContext(this);
            unityServiceProvider.RegisterContext(new object());
            unityServiceProvider.RegisterService<IInputService>(new PlayerCombatInputService());
            CombatAction testAction = new TakeTurnActionData().BuildAction(unityServiceProvider);
            testAction.StartAction();
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
    }
}
