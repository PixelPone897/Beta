using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Service
{
    public class UnityServiceProvider : IServiceProvider
    {
        private Dictionary<Type, object> services;

        public UnityServiceProvider()
        {
            services = new Dictionary<Type, object>();
        }

        public void RegisterService<T>(T service)
        {
            services[typeof(T)] = service;
        }

        public object GetService(Type serviceType)
        {
            services.TryGetValue(serviceType, out object result);
            return result;
        }

        public T GetService<T>()
        {
            return (T) GetService(typeof(T));
        }
    }
}
