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
        public CombatStepData stepData;

        public void Start()
        {
            UnityServiceProvider testProvider = new();
            testProvider.RegisterService<IInputService>(new PlayerCombatInputService());
            CombatStep test = stepData.Create(testProvider);
            test.Execute();
        }
    }
}
