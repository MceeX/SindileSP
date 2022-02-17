using System.ComponentModel.DataAnnotations;

namespace Sindile.InternAPI.Models
{
    /// <summary>
    /// Reprsents an employee
    /// </summary>
    public class Intern
    {
        /// <summary>
        /// Gets or sets an employees identifier
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets an employees first name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets an employees last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets an employees date of birth
        /// </summary>
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyy-MM-dd}")]
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Gets or sets an employees identity number
        /// </summary>
        public string Idnumber { get; set; }
        /// <summary>
        /// Gets or sets an employees email Address
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// Gets or sets an employees profile picture
        /// </summary>
        public string Base64Image { get; set; }
        /// <summary>
        /// Gets or sets an employees firstname
        /// </summary>
        public int? RoleId { get; set; }
        /// <summary>
        /// Gets or sets the date an employee joined
        /// </summary>
        public DateTime SignOnDate { get; set; }
    }
}
