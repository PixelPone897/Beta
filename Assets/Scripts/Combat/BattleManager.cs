using Assets.Scripts.Service;
using UnityEngine;

namespace Assets.Scripts.Combat
{
    public class BattleManager : MonoBehaviour
    {
        public IInputService testing;
        private AimAction testAction;

        private void Start()
        {
            testing = new PlayerCombatInputService();
            testAction = new AimAction();
            testAction.Inject(testing);
            testAction.SetupSteps();
        }
    }
}
