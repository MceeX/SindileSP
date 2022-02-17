using Microsoft.AspNetCore.Mvc;
using Sindile.InternAPI.Models;
using Sindile.InternAPI.Repository;
using System.Drawing;

namespace Sindile.InternAPI.Controllers
{
    /// <summary>
    /// Provides operations to manage interns
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class InternsController : ControllerBase
    {
        private readonly IInternsRepository _internsRepository;
        private readonly IRolesRepository _rolesRepository;
        private readonly ITaskLogsRepository  _taskLogsRepository;

        /// <summary>
        /// Initializes the constructor with the repo service instance
        /// </summary>
        /// <param name="internsRepository"></param>
        /// <param name="rolesRepository"></param>
        public InternsController(IInternsRepository internsRepository, 
            IRolesRepository rolesRepository, ITaskLogsRepository taskLogsRepository)
        {
            _internsRepository = internsRepository;
            _rolesRepository = rolesRepository;
            _taskLogsRepository = taskLogsRepository; 
        }

        /// <summary>
        /// Retrieves a list of employees
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Intern> Get()
        {
            return _internsRepository.GetInterns();
        }

        /// <summary>
        /// Retrieves an employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public System.Web.Http.SingleResult<Intern> Get(int id)
        {
            var result = _internsRepository.GetIntern(id);
            return System.Web.Http.SingleResult.Create(result);
        }

        /// <summary>
        /// Creates a new employee entry
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Intern model)
        {

            var roleExists = model.RoleId != null ? _rolesRepository.GetRole(model.RoleId.GetValueOrDefault()).Any() : false;

            if(model.RoleId.GetValueOrDefault() != 0 && !roleExists)
            {
                return BadRequest("Invalid role Id supplied!");
            }

            //convert the base64string image to bytes
            byte[] imageByte;
            Image image;
            //Maybe validate img and size
            try
            {
                imageByte = Convert.FromBase64String(model.Base64Image);
                if(imageByte.Length > 512000)
                {
                    ModelState.AddModelError("model.base64Image", "Invalid image size");
                    return BadRequest(ModelState);
                }

                image = ConvertToImage(imageByte);               
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("model.profileImage", ex.Message);
                return BadRequest(ModelState);
            }
            var id = await _internsRepository.CreateIntern(model);
            return Ok(id);
        }

        /// <summary>
        /// Updates an employee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Intern model)
        {
            var intern = _internsRepository.GetIntern(id).FirstOrDefault();
            if(intern == null)
            {
                return NotFound();
            }

            _internsRepository.UpdateIntern(model);
            return Ok();
        }

        /// <summary>
        /// Removes an instance of an employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var intern = _internsRepository.GetIntern(id).FirstOrDefault();
            if (intern == null)
            {
                return NotFound();
            }

            _internsRepository.DeleteIntern(intern);
            return Ok();
        }

        /// <summary>
        /// Calculates an employees salary
        /// </summary>
        /// <param name="id"></param>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <returns></returns>
        [HttpGet("CalculateSalary/{id}/{startDateTime}/{endDateTime}")]
        public IActionResult CalculateSalary(int id, DateTime startDateTime, DateTime endDateTime)
        {
            var intern = _internsRepository.GetIntern(id).FirstOrDefault();
            if (intern == null)
            {
                return NotFound();
            }

            List<double> salary = new List<double>();
            
            //retrieve tasks logged, filter a specific intern and period in time
            var loggedHours = _taskLogsRepository.GetTaskLogs()
                .Where(x => x.InternId == id 
                && x.DateTime > startDateTime && x.DateTime < endDateTime);

            //for the time logged calculate the salary
            foreach(var loggedTime in loggedHours)
            {
                var wage = Math.Round(CalSalary(loggedTime.Duration, loggedTime.HourlyRate), 2);
                salary.Add(wage);
            }

            var salaries = new EmployeeSalary
            {
                EmployeeId = id,
                Salary = salary.Sum(),
                DaysWorked = loggedHours.Count()
            };

            return Ok(salaries);
        }

        private static  Image ConvertToImage(byte[] value)
        {
            Image image;
            using (MemoryStream ms = new MemoryStream(value))
            {
                #pragma warning disable CA1416 // Validate platform compatibility
                image = Image.FromStream(ms);
                #pragma warning restore CA1416
            }
           return image;
        }

        private double CalSalary(double hours, double rate)
        {
            if(hours > 11)
            {
                hours = 11;
            }
            return hours * rate;
        }

    }
}
