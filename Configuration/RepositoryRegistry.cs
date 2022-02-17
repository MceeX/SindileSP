using Sindile.InternAPI.Repository;

namespace Sindile.InternAPI.Configuration
{
    public static class RepositoryRegistry
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IInternsRepository), typeof(InternsRepository));
            services.AddScoped(typeof(ITasksRepository), typeof(TasksRepository));
            services.AddScoped(typeof(ITaskLogsRepository),typeof(TaskLogsRepository));
            services.AddScoped(typeof(IRolesRepository), typeof(RolesRepository));
        }
    }
}
