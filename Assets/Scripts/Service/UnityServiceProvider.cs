using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Service
{
    public class UnityServiceProvider
    {
        private Dictionary<Type, object> services;
        private Dictionary<Type, object> contexts;

        public UnityServiceProvider()
        {
            services = new Dictionary<Type, object>();
            contexts = new Dictionary<Type, object>();
        }

        public void RegisterService<T>(T service) where T : class
        {
            services[typeof(T)] = service ?? throw new ArgumentNullException(nameof(service));
        }

        public T GetService<T>()
        {
            if(services.TryGetValue(typeof(T), out object result))
            {
                return (T)result;
            }
            else
            {
                throw new InvalidOperationException($"Service of type {typeof(T).Name} not registered.");
            }  
        }

        public void RegisterContext<T>(T instance) where T : class
        {
            contexts[typeof(T)] = instance ?? throw new ArgumentNullException(nameof(instance));
        }

        public T GetContext<T>() where T : class
        {
            if (contexts.TryGetValue(typeof(T), out var context))
            {
                return (T)context;
            }

            return null;
        }

        public void ClearContexts()
        {
            contexts.Clear();
        }
    }
}
