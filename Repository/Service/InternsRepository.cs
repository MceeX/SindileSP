using AutoMapper;
using Sindile.InternAPI.Models;

namespace Sindile.InternAPI.Repository
{
    public class InternsRepository : IInternsRepository
    {
        private readonly InternAPI.Data.InternDbContext _internDb;
        private IMapper mapFromEntity;
        private IMapper mapToEntity;
        public InternsRepository(InternAPI.Data.InternDbContext internDb)
        {
            _internDb = internDb;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<InternAPI.Data.Intern, Intern>());
            mapFromEntity = config.CreateMapper();
            var configE = new MapperConfiguration(cfg => cfg.CreateMap<Intern, InternAPI.Data.Intern>());
            mapToEntity = configE.CreateMapper();
        }

        /// <summary>
        /// Creates a new instance of an intern
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> CreateIntern(Intern model)
        {
            var entity = mapToEntity.Map<InternAPI.Data.Intern>(model);
            entity.Deleted = false;
            entity.SignOnDate = DateTime.UtcNow;
            entity.ProfileImage = Convert.FromBase64String(model.Base64Image);

            _internDb.Interns.Add(entity);
            await SaveAsync();
            return entity.Id;
        }

        /// <summary>
        /// Sets a intern instance to deleted
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task DeleteIntern(Intern model)
        {
            var entity = mapToEntity.Map<InternAPI.Data.Intern>(model);
            entity.Deleted = true;
            _internDb.Update(entity);
            await SaveAsync();
        }

        /// <summary>
        /// Retrieves a single intern
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IQueryable<Intern> GetIntern(int key)
        {
            return InternsQuery().Where(x => x.Id == key);
        }

        /// <summary>
        /// Retrieves a list of interns
        /// </summary>
        /// <returns></returns>
        public IQueryable<Intern> GetInterns()
        {
            return InternsQuery();
        }

        /// <summary>
        /// Persists changes on the intern entity to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateIntern(Intern model)
        {
            var entity = mapToEntity.Map<InternAPI.Data.Intern>(model);
            entity.Deleted = true;
            _internDb.Update(entity);
            await SaveAsync();
        }


        private IQueryable<Intern> InternsQuery()
        {
            var DbResults = DbQuery();
            var dbInterns = mapFromEntity.ProjectTo<Intern>(DbResults);
            return dbInterns;
        }
        private IQueryable<InternAPI.Data.Intern> DbQuery()
        {
            var DbResults = _internDb.Interns;
            return DbResults;
        }
        private async Task SaveAsync()
        {
            await _internDb.SaveChangesAsync();
        }
    }
}
