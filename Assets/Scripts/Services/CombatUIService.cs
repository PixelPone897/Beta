using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Services
{
    public interface CombatUIService
    {
        public abstract void EnableUI();
        public abstract void DisableUI();
    }
}
