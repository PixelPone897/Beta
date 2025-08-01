using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Service
{
    internal interface IInjectContext<T>
    {
        public void InjectContext(T context);
    }
}
