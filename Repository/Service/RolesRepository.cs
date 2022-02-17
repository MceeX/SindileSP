using AutoMapper;
using Sindile.InternAPI.Models;
using Sindile.InternAPI.Repository;

namespace Sindile.InternAPI.Repository
{
    public class RolesRepository : IRolesRepository
    {

        private readonly InternAPI.Data.InternDbContext _internDb;
        private IMapper mapFromEntity;
        private IMapper mapToEntity;
        public RolesRepository(InternAPI.Data.InternDbContext internDb)
        {
            _internDb = internDb;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<InternAPI.Data.Role, Role>());
            mapFromEntity = config.CreateMapper();
            var configE = new MapperConfiguration(cfg => cfg.CreateMap<Role, InternAPI.Data.Role>());
            mapToEntity = configE.CreateMapper();
        }

        /// <summary>
        /// Creates a new instance of an Role
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task CreateRole(Role model)
        {
            var entity = mapToEntity.Map<InternAPI.Data.Role>(model);
            entity.Deleted = false;
            entity.Created = DateTime.UtcNow;
            _internDb.Roles.Add(entity);
            await SaveAsync();
        }

        /// <summary>
        /// Sets a Role instance to deleted
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task DeleteRole(Role model)
        {
            var entity = mapToEntity.Map<InternAPI.Data.Role>(model);
            entity.Deleted = true;
            _internDb.Update(entity);
            await SaveAsync();
        }

        /// <summary>
        /// Retrieves a single Role
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IQueryable<Role> GetRole(int key)
        {
            return RolesQuery().Where(x => x.Id == key);
        }

        /// <summary>
        /// Retrieves a list of Roles
        /// </summary>
        /// <returns></returns>
        public IQueryable<Role> GetRoles()
        {
            return RolesQuery();
        }

        /// <summary>
        /// Persists changes on the Role entity to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateRole(Role model)
        {
            var entity = mapToEntity.Map<InternAPI.Data.Role>(model);
            entity.Deleted = true;
            _internDb.Update(entity);
            await SaveAsync();
        }

        private IQueryable<Role> RolesQuery()
        {
            var DbResults = DbQuery();
            var dbRoles = mapFromEntity.ProjectTo<Role>(DbResults);
            return dbRoles;
        }
        private IQueryable<InternAPI.Data.Role> DbQuery()
        {
            var DbResults = _internDb.Roles;
            return DbResults;
        }
        private async Task SaveAsync()
        {
            await _internDb.SaveChangesAsync();
        }

    }
}
