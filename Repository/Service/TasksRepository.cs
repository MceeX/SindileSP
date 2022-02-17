using AutoMapper;
using Sindile.InternAPI.Models;

namespace Sindile.InternAPI.Repository
{
    public class TasksRepository    :   ITasksRepository
    {
        private readonly InternAPI.Data.InternDbContext _internDb;
        private IMapper mapFromEntity;
        private IMapper mapToEntity;
        public TasksRepository(InternAPI.Data.InternDbContext internDb)
        {
            _internDb = internDb;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<InternAPI.Data.Task, WorkTask>());
            mapFromEntity = config.CreateMapper();
            var configE = new MapperConfiguration(cfg => cfg.CreateMap<WorkTask, InternAPI.Data.Task>());
            mapToEntity = configE.CreateMapper();
        }

        /// <summary>
        /// Creates a new instance of an WorkTask
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task CreateWorkTask(WorkTask model)
        {
            var entity = mapToEntity.Map<InternAPI.Data.Task>(model);
            entity.Deleted = false;
            entity.Created = DateTime.UtcNow;
            _internDb.Tasks.Add(entity);
            await SaveAsync();
        }

        /// <summary>
        /// Sets a WorkTask instance to deleted
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task DeleteWorkTask(WorkTask model)
        {
            var entity = mapToEntity.Map<InternAPI.Data.Task>(model);
            entity.Deleted = true;
            _internDb.Update(entity);
            await SaveAsync();
        }

        /// <summary>
        /// Retrieves a single WorkTask
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IQueryable<WorkTask> GetWorkTask(int key)
        {
            return WorkTasksQuery().Where(x => x.Id == key);
        }

        /// <summary>
        /// Retrieves a list of WorkTasks
        /// </summary>
        /// <returns></returns>
        public IQueryable<WorkTask> GetWorkTasks()
        {
            return WorkTasksQuery();
        }

        /// <summary>
        /// Persists changes on the WorkTask entity to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateWorkTask(WorkTask model)
        {
            var entity = mapToEntity.Map<InternAPI.Data.Task>(model);
            entity.Deleted = true;
            _internDb.Update(entity);
            await SaveAsync();
        }


        private IQueryable<WorkTask> WorkTasksQuery()
        {
            var DbResults = DbQuery();
            var dbWorkTasks = mapFromEntity.ProjectTo<WorkTask>(DbResults);
            return dbWorkTasks;
        }
        private IQueryable<InternAPI.Data.Task> DbQuery()
        {
            var DbResults = _internDb.Tasks;
            return DbResults;
        }
        private async Task SaveAsync()
        {
            await _internDb.SaveChangesAsync();
        }
    }
}
