using Microsoft.AspNetCore.Mvc;
using Sindile.InternAPI.Models;
using Sindile.InternAPI.Repository;


namespace Sindile.InternAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TaskLogsController : ControllerBase
    {
        private readonly ITaskLogsRepository _taskLogsRepository;
        private readonly IInternsRepository _internsRepository;
        private readonly IRolesRepository _rolesRepository;

        public TaskLogsController(ITaskLogsRepository taskLogsRepository, IInternsRepository internsRepository,
            IRolesRepository rolesRepository)
        {
            _taskLogsRepository = taskLogsRepository;
            _internsRepository = internsRepository;
            _rolesRepository = rolesRepository; 
        }

        // GET: api/<TaskLogsController>
        [HttpGet]
        public IEnumerable<TaskLog> Get()
        {
            return _taskLogsRepository.GetTaskLogs();
        }

        // GET: api/<TaskLogsController>
        [HttpGet("{id}")]
        public System.Web.Http.SingleResult<TaskLog> Get(int id)
        {
            var model = _taskLogsRepository.GetTaskLogs().Where(i => i.Id == id);
            return System.Web.Http.SingleResult.Create(model);
        }

        /// <summary>
        /// Retrieves logged tasks for an employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<TaskLogsController>/5
        [HttpGet("GetLogByEmployee/{employeeId}")]
        public IEnumerable<TaskLog> GetLogByEmployee(int employeeId)
        {
            return _taskLogsRepository.GetTaskLogs().Where(x => x.InternId == employeeId);
        }

        /// <summary>
        /// Creates a new task log entry
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST api/<TaskLogsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TaskLog model)
        {
            //var timeLogged = _taskLogsRepository.GetTaskLogs()
            //    .Where(x=>x.InternId == model.InternId && x.Deleted != true);
            //get the day being logged and confirm it has task, and are within daily hour limit
            //var dayWithTask = timeLogged.Where(d => d.DateTime.Date == model.DateTime.Date);            
            //if(model.Duration > 11)
            //{
            //    return BadRequest("Invalid, Daily working hours cannot exceed 11 hours");
            //}
            //if (dayWithTask.Any())
            //{
            //    if(dayWithTask.Where(h => (h.Duration + model.Duration) > 11).Any())
            //    {
            //        return BadRequest("Invalid, accumulated daily working hours cannot exceed 11 hours");
            //    }
            //}
            if(model.Duration < 0)
            {
                return BadRequest("Invalid, please provide task duration");
            }
            var intern = _internsRepository.GetIntern(model.InternId).FirstOrDefault();
            if (intern == null)
            {
                return BadRequest("Invalid, please provide a valid intern id");
            }
            var role = _rolesRepository.GetRole((int)intern.RoleId).FirstOrDefault();

            model.HourlyRate = (double)role.HourlyRate;

            await _taskLogsRepository.CreateTaskLog(model);

            return Ok();
        }

        // PUT api/<TaskLogsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] TaskLog model)
        {
            var intern = _internsRepository.GetIntern(model.InternId).FirstOrDefault();
            if (intern == null)
            {
                return BadRequest("Invalid, please provide a valid intern id");
            }

            var exists = _taskLogsRepository.GetTaskLogs().Where(x => x.Id == id).Any();
            if (!exists)
            {
                return NotFound();
            }
            await _taskLogsRepository.UpdateTaskLog(model);

            return Ok();
        }

        // DELETE api/<TaskLogsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var taskLog = _taskLogsRepository.GetTaskLogs().FirstOrDefault(x => x.Id == id);
            if (taskLog == null)
            {
                return NotFound();
            }

            _taskLogsRepository.DeleteTaskLog(taskLog);
            return Ok();
        }
    }
}
