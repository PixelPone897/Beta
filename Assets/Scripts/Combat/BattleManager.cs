using Assets.Scripts.Combat.CombatActionDatas;
using Assets.Scripts.Combat.CombatActions;
using Assets.Scripts.Services;
using Scripts.Actors;
using UnityEngine;

namespace Assets.Scripts.Combat
{
    public class BattleManager : MonoBehaviour
    {
        private void Start()
        {
            UnityServiceProvider unityServiceProvider = new();
            unityServiceProvider.RegisterContext(this);
            unityServiceProvider.RegisterContext(new object());
            unityServiceProvider.RegisterService<IInputService>(new PlayerCombatInputService());
            CombatAction testAction = new TakeTurnActionData().BuildAction(unityServiceProvider);
            testAction.StartAction();
        }
    }
}
