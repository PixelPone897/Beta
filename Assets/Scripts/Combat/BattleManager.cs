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

        public void Start()
        {
            UnityServiceProvider testProvider = new();
            testProvider.RegisterService<IInputService>(new PlayerCombatInputService());
            CombatAction combatAction = actionData.Create(testProvider);
            combatAction.StartAction(this, null);
            combatAction.UpdateAction();
        }
    }
}
