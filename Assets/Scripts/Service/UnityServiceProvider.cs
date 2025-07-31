using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Service
{
    public interface IGameServiceProvider : IServiceProvider
    {
        T GetService<T>();
    }

    public class UnityServiceProvider : IGameServiceProvider
    {
        private Dictionary<Type, object> services;

        public UnityServiceProvider()
        {
            services = new Dictionary<Type, object>();
        }

        public T GetService<T>()
        {
            return (T) GetService(typeof(T));
        }

        public object GetService(Type serviceType)
        {
            services.TryGetValue(serviceType, out object result);
            return result;
        }

        public void RegisterService<T>(T instance)
        {
            services[typeof(T)] = instance;
        }
    }
}
