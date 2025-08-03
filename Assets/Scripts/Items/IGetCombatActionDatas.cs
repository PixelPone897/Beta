using Assets.Scripts.Combat.CombatActionDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Items
{
    public interface IGetCombatActionDatas
    {
        public abstract List<CombatActionData> GetCombatActionDatas();
    }
}
