using Sindile.InternAPI.Models;

namespace Sindile.InternAPI.Repository
{
    public interface ITasksRepository
    {
        /// <summary>
        /// Retrieves a list of WorkTasks
        /// </summary>
        /// <returns>A list of all vehicles</returns>
        public IQueryable<WorkTask> GetWorkTasks();
        /// <summary>
        /// Retrieves a single WorkTask
        /// </summary>
        /// <param name="key">Vehicle Identifier</param>
        /// <returns>A single vehicle</returns>
        public IQueryable<WorkTask> GetWorkTask(int key);
        /// <summary>
        /// Creates a new instance of an WorkTask
        /// </summary>
        /// <param name="model">Business model</param>
        /// <returns>Task, operation returns no value</returns>
        public Task CreateWorkTask(WorkTask model);
        /// <summary>
        /// Persists changes on the WorkTask entity to database
        /// </summary>
        /// <param name="model">WorkTask model</param>
        /// <returns>Task, operation returns no value</returns>
        public Task UpdateWorkTask(WorkTask model);
        /// <summary>
        /// Removes an instance of an WorkTask
        /// </summary>
        /// <param name="model">WorkTask model</param>
        /// <returns>Task, operation returns no value</returns>
        public Task DeleteWorkTask(WorkTask model);
    }
}
