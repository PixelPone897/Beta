using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Services
{
    public interface ILoggerService
    {
        public abstract void EnableLogging();

        public abstract void DisableLogging();

        public abstract void Log(string message);
    }
}
