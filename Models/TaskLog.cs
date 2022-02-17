using System.ComponentModel.DataAnnotations;

namespace Sindile.InternAPI.Models
{
    public class TaskLog
    {

        /// <summary>
        /// Gets or sets the task log identifier
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the intern identifier
        /// </summary>
        public int InternId { get; set; }
        /// <summary>
        /// Gets or sets the task identifier
        /// </summary>
        public int TaskId { get; set; }
        /// <summary>
        /// Gets or sets the amount of time taken to complete a task
        /// </summary>
        public double Duration { get; set; }
        /// <summary>
        /// Gets or sets whether the log is deleted
        /// </summary>
        public bool? Deleted { get; set; }
        /// <summary>
        /// Gets or sets the current time log's hourly rate
        /// </summary>
        public double HourlyRate { get; set; }
        /// <summary>
        /// Gets or sets the created date time
        /// </summary>
        public DateTime DateTime { get; set; }

    }
}
