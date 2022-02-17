namespace Sindile.InternAPI.Models
{
    /// <summary>
    /// Represents an interns salary
    /// </summary>
    public class EmployeeSalary
    {
        /// <summary>
        /// Gets or sets the employee identifier, which is the intern identifier.
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// Gets or sets the interns salary.
        /// </summary>
        public double Salary { get; set; }
        /// <summary>
        /// Gets or sets the number of days worked.
        /// </summary>
        public int DaysWorked { get; set; }
    }
}
