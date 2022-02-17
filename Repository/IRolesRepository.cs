using Sindile.InternAPI.Models;

namespace Sindile.InternAPI.Repository
{
    public interface IRolesRepository
    {
        /// <summary>
        /// Retrieves a list of roles
        /// </summary>
        /// <returns>A list of all vehicles</returns>
        public IQueryable<Role> GetRoles();
        /// <summary>
        /// Retrieves a single role
        /// </summary>
        /// <param name="key">Vehicle Identifier</param>
        /// <returns>A single vehicle</returns>
        public IQueryable<Role> GetRole(int key);
        /// <summary>
        /// Creates a new instance of an role
        /// </summary>
        /// <param name="model">Business model</param>
        /// <returns>Task, operation returns no value</returns>
        public Task CreateRole(Role model);
        /// <summary>
        /// Persists changes on the role entity to database
        /// </summary>
        /// <param name="model">Role model</param>
        /// <returns>Task, operation returns no value</returns>
        public Task UpdateRole(Role model);
        /// <summary>
        /// Removes an instance of an role
        /// </summary>
        /// <param name="model">Role model</param>
        /// <returns>Task, operation returns no value</returns>
        public Task DeleteRole(Role model);
    }
}
