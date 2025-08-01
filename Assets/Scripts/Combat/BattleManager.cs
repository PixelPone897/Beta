using Assets.Scripts.Items;
using Assets.Scripts.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Combat
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeReference, SubclassSelector]
        public CombatActionData actionData;

        [SerializeField]
        private ItemInstance item;

        public void Start()
        {
            UnityServiceProvider testProvider = new();
            testProvider.RegisterService<IInputService>(new PlayerCombatInputService());
            CombatAction combatAction = actionData.Create(testProvider);
            combatAction.InjectContextToSteps(item);
            combatAction.StartAction(this, null);
            combatAction.UpdateAction();
        }
    }
}
