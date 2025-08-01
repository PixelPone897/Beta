using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Services
{
    /// <summary>
    /// Provider that handles registering and storing specific logic services and contexts at runtime.
    /// </summary>
    public class UnityServiceProvider
    {
        /// <summary>
        /// Singleton/shared global services like Audio, Input, etc.
        /// </summary>
        private Dictionary<Type, object> services;
        /// <summary>
        /// Per-instance services/contexts like ItemInstance.
        /// </summary>
        /// <remarks>Think of contexts as providing background info for logic (a specific Actor,
        /// a specific item, etc).</remarks>
        private Dictionary<Type, object> contexts;

        public UnityServiceProvider()
        {
            services = new Dictionary<Type, object>();
            contexts = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Registers service to provider.
        /// </summary>
        /// <typeparam name="T">Type being mapped to this new service.</typeparam>
        /// <param name="service">Service being added to the provider.</param>
        /// <exception cref="ArgumentNullException">Thrown if service being passed is null.</exception>
        public void RegisterService<T>(T service) where T : class
        {
            services[typeof(T)] = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Retrives specific service from provider.
        /// </summary>
        /// <typeparam name="T">Type of service that is being retrived.</typeparam>
        /// <returns>Service associated with specific type.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if type of service being requested can't be found.</exception>
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

        /// <summary>
        /// Retrives specific context from provider.
        /// </summary>
        /// <typeparam name="T">Type of context that is being retrived.</typeparam>
        /// <returns>Context associated with specific type.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if type of context being requested can't be found.</exception>
        public void RegisterContext<T>(T instance) where T : class
        {
            contexts[typeof(T)] = instance ?? throw new ArgumentNullException(nameof(instance));
        }

        /// <summary>
        /// Retrives specific context from provider.
        /// </summary>
        /// <typeparam name="T">Type of context that is being retrived.</typeparam>
        /// <returns>Context associated with specific type.</returns>
        public T GetContext<T>() where T : class
        {
            if (contexts.TryGetValue(typeof(T), out var context))
            {
                return (T)context;
            }

            return null;
        }

        /// <summary>
        /// Removes all current contexts from provider.
        /// </summary>
        public void ClearContexts()
        {
            contexts.Clear();
        }
    }
}
