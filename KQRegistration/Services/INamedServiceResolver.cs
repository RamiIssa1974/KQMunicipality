using Microsoft.Extensions.DependencyInjection;

namespace KQApi.Services
{
    public interface INamedServiceResolver
    {
        IBasateenRepository GetNamedService(ServiceName serviceScopeFactory);
    }
    public enum ServiceName
    {
        SQLBasateenRepository,
        BasateenRepository
    }
    public class NamedServiceResolver : INamedServiceResolver
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly Dictionary<ServiceName, Type> _namedServices;

        public NamedServiceResolver(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;

            // Map service names to their corresponding types
            _namedServices = new Dictionary<ServiceName, Type>
            {
                { ServiceName.SQLBasateenRepository, typeof(SQLBasateenRepository) },
                { ServiceName.BasateenRepository, typeof(BasateenRepository) }
            };
        }
         

        public IBasateenRepository GetNamedService(ServiceName serviceName)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                // Resolve the service within the scope
                return scope.ServiceProvider.GetRequiredService(_namedServices[serviceName]) as IBasateenRepository;
            }
        }        
    }

}
