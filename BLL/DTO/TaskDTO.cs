using System;
using System.Collections.Generic;

namespace BLL.DTO
{
    /// <summary>
    /// Data transfer object for task
    /// </summary>
    public sealed class TaskDTO
    {
        /// <summary>
        /// Get and set task id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Get and set task name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get and set task description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Get and set task priority
        /// </summary>
        public Priority Priority { get; set; }

        /// <summary>
        /// Get and set foreign key to project by id 
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// Get and set foreign key to user creator by id 
        /// </summary>
        public int? CreatorId { get; set; }
        
        /// <summary>
        /// Get and set foreign key to user executor by id 
        /// </summary>
        public int? ExecutorId { get; set; }

        /// <summary>
        /// Get and set date when task was created
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Get and set deadline for task
        /// </summary>
        public DateTime Deadline { get; set; }

        /// <summary>
        /// Get and set comments collection
        /// </summary>
        public IEnumerable<CommentDTO> Comments { get; set; }
    }

    /// <summary>
    /// Enum for task priority
    /// </summary>
    public enum Priority
    {
        Low,
        Middle,
        High,
        UltraHigh
    }
}
