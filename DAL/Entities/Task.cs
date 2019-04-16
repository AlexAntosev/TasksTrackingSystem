using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Task : BaseEntity
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

        /// <summary>
        /// Get and set the foreign key for the project.
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// Lazy loading project using the foreign key.
        /// </summary>
        public virtual Project Project { get; set; }

        /// <summary>
        /// Get and set priority of the task.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Get and set type of the task.
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Get and set status of the task.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Get and set time when the task was created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Get and set time when the task was last time updated.
        /// </summary>
        public DateTime Updated { get; set; }

        /// <summary>
        /// Get and set deadline for task.
        /// </summary>
        public DateTime Deadline { get; set; }

        /// <summary>
        /// Get and set the foreign key for the user that created the task.
        /// </summary>
        public int? CreatorId { get; set; }

        /// <summary>
        /// Lazy loading the user who created the task using the foreign key.
        /// </summary>
        public virtual User Creator { get; set; }

        /// <summary>
        /// Get and set the foreign key for the user performing the task.
        /// </summary>
        public int? ExecutorId { get; set; }

        /// <summary>
        /// Lazy loading the user who execute the task using the foreign key.
        /// </summary>
        public virtual User Executor { get; set; }

        /// <summary>
        /// Lazy loading comments of the task.
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; }

        public Task()
        {
            Comments = new HashSet<Comment>();
        }
    }

    
}
