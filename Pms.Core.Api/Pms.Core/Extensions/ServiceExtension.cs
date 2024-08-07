using Microsoft.Extensions.DependencyInjection;

namespace Pms.Core.Extensions
{
    public enum DependencyLifetime
    {
        Transient = 1,
        Scoped = 2,
        Singleton = 3
    }

    public static class ServiceExtension
    {
        /// <summary>
        /// Registers the assemblies provided on dependency lifetime
        /// </summary>
        /// <param name="services">Services instance</param>
        /// <param name="assemblyName">Assembly name to be registered (this should be Dll / namespace name)</param>
        /// <param name="lifetime">Lifetime type to be registered</param>
        public static void RegisterAssemblies<TService>(
            this IServiceCollection services,
            string assemblyName,
            DependencyLifetime lifetime)
        {
            var assembly = AppDomain.CurrentDomain.Load(assemblyName);
            var types = assembly.GetTypes().Where(p => typeof(TService).IsAssignableFrom(p)).ToArray();

            var lifeTimeMap = new Dictionary<DependencyLifetime, string>
            {
                { DependencyLifetime.Transient, "AddTransient" },
                { DependencyLifetime.Scoped, "AddScoped" },
                { DependencyLifetime.Singleton, "AddSingleton" }
            };
            lifeTimeMap.TryGetValue(lifetime, out var lifetimeName);

            var addLifetimeMethod = typeof(ServiceCollectionServiceExtensions)
                .GetMethods()
                .FirstOrDefault(method =>
                    method.Name == lifetimeName &&
                    method.IsGenericMethod == true &&
                    method.GetGenericArguments().Count() == 2);

            var allInterfaces = types?.Where(type => type.IsInterface).ToList();
            var allClasses = types?.Where(type => !type.IsInterface).ToList();

            var servicesMap = new Dictionary<Type, Type>();
            allInterfaces.ForEach(serviceInterface =>
            {
                var serviceClass = allClasses.Find(serviceClass => serviceClass.IsAssignableTo(serviceInterface));
                if (serviceClass == null) { return; }
                servicesMap.Add(serviceInterface, serviceClass);
            });

            foreach (var serviceInfo in servicesMap)
            {
                var method = addLifetimeMethod.MakeGenericMethod(new[] { serviceInfo.Key, serviceInfo.Value });
                method.Invoke(services, new[] { services });
            }
        }

        /// <summary>
        /// Registers HTTP Client services.
        /// </summary>
        /// <param name="services">Type of http client to be registered.</param>
        /// <param name="assemblyName">The assembly name to be registered.</param>
        public static void RegisterHttpClientServices<THttpClient>(
            this IServiceCollection services,
            string assemblyName)
        {
            var assembly = AppDomain.CurrentDomain.Load(assemblyName);
            var types = assembly.GetTypes().Where(p => typeof(THttpClient).IsAssignableFrom(p)).ToArray();

            var addHttpClientMethod = typeof(HttpClientFactoryServiceCollectionExtensions)
                .GetMethods()
                .FirstOrDefault(method =>
                    method.Name == "AddHttpClient" &&
                    method.IsGenericMethod == true &&
                    method.GetGenericArguments().Count() == 2);

            var allInterfaces = types?.Where(type => type.IsInterface).ToList();
            var allClasses = types?.Where(type => !type.IsInterface).ToList();

            var servicesMap = new Dictionary<Type, Type>();
            allInterfaces.ForEach(serviceInterface =>
            {
                var serviceClass = allClasses.Find(serviceClass => serviceClass.IsAssignableTo(serviceInterface));
                if (serviceClass == null) { return; }
                servicesMap.Add(serviceInterface, serviceClass);
            });

            foreach (var serviceInfo in servicesMap)
            {
                var method = addHttpClientMethod.MakeGenericMethod(new[] { serviceInfo.Key, serviceInfo.Value });
                method.Invoke(services, new[] { services });
            }
        }
    }
}
