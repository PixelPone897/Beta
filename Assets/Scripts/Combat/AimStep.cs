using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Combat
{
    public class AimStep : CombatStep
    {

        private IInputService combatInput;

        public AimStep() { }

        public AimStep(IInputService combatInput)
        {
            this.combatInput = combatInput;
            combatInput.EnableInput();
            combatInput.InputMove += TestMethod;
        }

        public override void Execute(BattleManager battleManager, CombatAction parentAction)
        {
           
        }

        private void TestMethod(object sender, Vector2 e)
        {
            Debug.Log(e.ToString());
        }
    }
}
