using Microsoft.AspNetCore.Mvc;
using Sindile.InternAPI.Models;
using Sindile.InternAPI.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sindile.InternAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITasksRepository _tasksRepository;

        public TasksController(ITasksRepository TasksRepository)
        {
            _tasksRepository = TasksRepository;
        }

        [HttpGet]
        public IEnumerable<WorkTask> Get()
        {
            return _tasksRepository.GetWorkTasks();
        }

        // GET api/<TasksController>/5
        [HttpGet("{id}")]
        public System.Web.Http.SingleResult<WorkTask> Get(int id)
        {
            var result = _tasksRepository.GetWorkTask(id);
            return System.Web.Http.SingleResult.Create(result);
        }

        /// <summary>
        /// Creates a new task
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WorkTask model)
        {
            var exists = _tasksRepository.GetWorkTasks().Where(x => x.Name.ToLower() == model.Name.ToLower()).Any();
            if (exists)
            {
                return Conflict(model);
            }

            await _tasksRepository.CreateWorkTask(model);

            return Ok();
        }

        /// <summary>
        /// Updates a task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] WorkTask model)
        {
            var task = _tasksRepository.GetWorkTask(id).Where(x => x.Deleted != true);

            if(task == null || !task.Any())
            {
                return NotFound();            
            }

            if(task.Where(x => x.Name.ToLower() == model.Name.ToLower()).Any())
            {
                return Conflict();
            }
            await _tasksRepository.UpdateWorkTask(model);

            return Ok();
        }

        /// <summary>
        /// Removes a work task
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = _tasksRepository.GetWorkTask(id).Where(x => x.Deleted != true);

            if (task == null || !task.Any())
            {
                return NotFound();
            }

            await _tasksRepository.DeleteWorkTask(task.FirstOrDefault());
            return Ok();
        }
    }
}
