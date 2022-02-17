namespace Sindile.InternAPI.Models
{
    public class WorkTask
    {
        /// <summary>
        /// Gets or sets the task identifier
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name/description of the task
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the pre-defined duration for a task
        /// </summary>
        public double? Duration { get; set; }
        /// <summary>
        /// Gets or sets whether task is deleted
        /// </summary>
        public bool? Deleted { get; set; }
        /// <summary>
        /// Gets or sets the created date time
        /// </summary>
        public DateTime Created { get; set; }
    }
}
