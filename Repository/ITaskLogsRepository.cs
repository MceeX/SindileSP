using Sindile.InternAPI.Models;

namespace Sindile.InternAPI.Repository
{
    public interface ITaskLogsRepository
    {
        /// <summary>
        /// Retrieves a list of task logs
        /// </summary>
        /// <returns>A list of all vehicles</returns>
        public IQueryable<TaskLog> GetTaskLogs();
        ///// <summary>
        ///// Retrieves a single task log
        ///// </summary>
        ///// <param name="key">Vehicle Identifier</param>
        ///// <returns>A single vehicle</returns>
        //public IQueryable<TaskLog> GetTaskLog(int key);
        /// <summary>
        /// Creates a new instance of a task log
        /// </summary>
        /// <param name="model">Business model</param>
        /// <returns>Task, operation returns no value</returns>
        public Task CreateTaskLog(TaskLog model);
        /// <summary>
        /// Persists changes on the task log entity to database
        /// </summary>
        /// <param name="model">TaskLog model</param>
        /// <returns>Task, operation returns no value</returns>
        public Task UpdateTaskLog(TaskLog model);
        /// <summary>
        /// Removes an instance of an task log
        /// </summary>
        /// <param name="model">TaskLog model</param>
        /// <returns>Task, operation returns no value</returns>
        public Task DeleteTaskLog(TaskLog model);
    }
}
