using Microsoft.AspNetCore.Mvc;
using Sindile.InternAPI.Models;
using Sindile.InternAPI.Repository;


namespace Sindile.InternAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesRepository _rolesRepository;

        public RolesController(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        /// <summary>
        /// Retrieves a list of roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Role> Get()
        {
            return _rolesRepository.GetRoles();
        }

        /// <summary>
        /// Retrieves a specific role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public System.Web.Http.SingleResult<Role> Get(int id)
        {
            var result = _rolesRepository.GetRole(id);
            return System.Web.Http.SingleResult.Create(result);
        }

        /// <summary>
        /// Creates a new role entry
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Role model)
        {
            var exists = _rolesRepository.GetRoles()
                .Where(x => x.Role1.ToLower() == model.Role1.ToLower()).Any();
            if(exists)
            {
                return Conflict(model);
            }
            await _rolesRepository.CreateRole(model);

            return Ok();
        }

        /// <summary>
        /// Updates a role
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Role model)
        {
            var exists = _rolesRepository.GetRoles()
                .Where(x => x.Id == id).Any();
            if(!exists)
            {
                return NotFound();
            }

            await _rolesRepository.UpdateRole(model);
            return Ok();
        }

        /// <summary>
        /// Removes a role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = _rolesRepository.GetRoles()
                .Where(x => x.Id == id);
            if(role != null && !role.Any())
            {
                return NotFound();
            }  
            await _rolesRepository.DeleteRole(role.FirstOrDefault());

            return Ok();
        }
    }
}
