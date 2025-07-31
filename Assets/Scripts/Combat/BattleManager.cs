using Assets.Scripts.Service;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Combat
{
    public class BattleManager : MonoBehaviour
    {
        private void Start()
        {
            PlayerCombatInputService testInput = new PlayerCombatInputService();
            ShootAction action = new ShootAction();
            action.combatSteps = new Queue<CombatStep>();
            action.combatSteps.Enqueue(new AimStep());
            action.Inject(testInput);
            action.Initialize(this, null);
            action.Execute();
        }
    }
}
