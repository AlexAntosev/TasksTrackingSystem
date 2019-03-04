using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    /// <summary>
    /// Data transfer object for task
    /// </summary>
    public class TaskDTO
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
        /// Get and set task priority
        /// </summary>
        public Priority Priority { get; set; }

        /// <summary>
        /// Get and set date when task was created
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Get and set deadline for task
        /// </summary>
        public DateTime Deadline { get; set; }

        /// <summary>
        /// Get and set user who create task
        /// </summary>
        public virtual UserDTO Creator { get; set; }

        /// <summary>
        /// Get and set user who execute task
        /// </summary>
        public virtual UserDTO Executor { get; set; }

        /// <summary>
        /// Get and set comments collection
        /// </summary>
        public virtual ICollection<CommentDTO> Comments { get; set; }

        /// <summary>
        /// Get and set users collection who subscribe this task
        /// </summary>
        public virtual ICollection<UserDTO> Subscribers { get; set; }
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
