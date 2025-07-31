using Assets.Scripts.Items;
using Assets.Scripts.Service;
using UnityEngine;

namespace Assets.Scripts.Combat
{
    public class BattleManager : MonoBehaviour
    {
        public RangeWeaponData testItem;

        private void Start()
        {
            PlayerCombatInputService testInput = new();
            foreach(CombatAction action in testItem.combatActions)
            {
                if(action is IInjectService<IInputService> injectService)
                {
                    injectService.Inject(testInput);
                }
                action.Initialize(this, null);
            }

            testItem.combatActions[0].Execute();
        }
    }
}
