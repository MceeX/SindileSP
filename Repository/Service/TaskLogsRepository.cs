using AutoMapper;
using Sindile.InternAPI.Models;

namespace Sindile.InternAPI.Repository
{
    public class TaskLogsRepository : ITaskLogsRepository
    {

        private readonly InternAPI.Data.InternDbContext _internDb;
        private IMapper mapFromEntity;
        private IMapper mapToEntity;
        public TaskLogsRepository(InternAPI.Data.InternDbContext internDb)
        {
            _internDb = internDb;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<InternAPI.Data.TaskLog, TaskLog>());
            mapFromEntity = config.CreateMapper();
            var configE = new MapperConfiguration(cfg => cfg.CreateMap<TaskLog, InternAPI.Data.TaskLog>());
            mapToEntity = configE.CreateMapper();
        }

        /// <summary>
        /// Creates a new instance of an TaskLog
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task CreateTaskLog(TaskLog model)
        {
            var entity = mapToEntity.Map<InternAPI.Data.TaskLog>(model);
            entity.Deleted = false;
            //entity.DateTime = DateTime.UtcNow;
            _internDb.TaskLogs.Add(entity);
            await SaveAsync();
        }

        /// <summary>
        /// Sets a TaskLog instance to deleted
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task DeleteTaskLog(TaskLog model)
        {
            var entity = mapToEntity.Map<InternAPI.Data.TaskLog>(model);
            entity.Deleted = true;
            _internDb.Update(entity);
            await SaveAsync();
        }

        /// <summary>
        /// Retrieves a single TaskLog
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IQueryable<TaskLog> GetTaskLog(int key)
        {
            return TaskLogsQuery().Where(x => x.Id == key);
        }

        /// <summary>
        /// Retrieves a list of TaskLogs
        /// </summary>
        /// <returns></returns>
        public IQueryable<TaskLog> GetTaskLogs()
        {
            return TaskLogsQuery();
        }

        /// <summary>
        /// Persists changes on the TaskLog entity to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateTaskLog(TaskLog model)
        {
            var entity = mapToEntity.Map<InternAPI.Data.Task>(model);
            entity.Deleted = true;
            _internDb.Update(entity);
            await SaveAsync();
        }


        private IQueryable<TaskLog> TaskLogsQuery()
        {
            var DbResults = DbQuery();
            var dbTaskLogs = mapFromEntity.ProjectTo<TaskLog>(DbResults);
            
            return dbTaskLogs;
        }
        private IQueryable<InternAPI.Data.TaskLog> DbQuery()
        {
            var DbResults = _internDb.TaskLogs;
            return DbResults;
        }
        private async Task SaveAsync()
        {
            await _internDb.SaveChangesAsync();
        }

    }
}
