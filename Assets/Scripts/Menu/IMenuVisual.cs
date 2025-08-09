using Assets.Scripts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Menu
{
    public interface IMenuVisual
    {
        public abstract void Initialize(UnityServiceProvider serviceProvider);
        public abstract void ShowMenu();
        public abstract void HideMenu();
        public abstract void UpdateMenu(int newMenuIndex);
    }
}
