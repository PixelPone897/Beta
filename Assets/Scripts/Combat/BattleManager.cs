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
        public CombatStepDefinition stepDefinition;

        private void Start()
        {
            UnityServiceProvider testProvider = new UnityServiceProvider();
            testProvider.RegisterService<IInputService>(new PlayerCombatInputService());
            AimStepDefinition test = new();
            CombatStep testStep = test.CreateStep(testProvider);
            testStep.Execute();
        }
    }
}
