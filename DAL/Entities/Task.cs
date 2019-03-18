using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Task : BaseEntity
    {
        /// <summary>
        /// Get and set task name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get and set task description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Get and set foreign key to project by id 
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// Get and set project by foreign key
        /// </summary>
        public virtual Project Project { get; set; }

        /// <summary>
        /// Get and set foreign key to task priority by id 
        /// </summary>
        public int Priority { get; set; }

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
        public virtual ICollection<Comment> Comments { get; set; }
    }

    
}
