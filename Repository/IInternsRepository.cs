using Sindile.InternAPI.Models;

namespace Sindile.InternAPI.Repository
{
    public interface IInternsRepository
    {
        /// <summary>
        /// Retrieves a list of interns
        /// </summary>
        /// <returns>A list of all vehicles</returns>
        public IQueryable<Intern> GetInterns();
        /// <summary>
        /// Retrieves a single intern
        /// </summary>
        /// <param name="key">Vehicle Identifier</param>
        /// <returns>A single vehicle</returns>
        public IQueryable<Intern> GetIntern(int key);
        /// <summary>
        /// Creates a new instance of an intern
        /// </summary>
        /// <param name="model">Business model</param>
        /// <returns>Task, operation returns no value</returns>
        public Task<int> CreateIntern(Intern model);
        /// <summary>
        /// Persists changes on the intern entity to database
        /// </summary>
        /// <param name="model">intern model</param>
        /// <returns>Task, operation returns no value</returns>
        public Task UpdateIntern(Intern model);
        /// <summary>
        /// Removes an instance of an intern
        /// </summary>
        /// <param name="model">intern model</param>
        /// <returns>Task, operation returns no value</returns>
        public Task DeleteIntern(Intern model);
    }
}
