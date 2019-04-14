using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public sealed class TaskDTO
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public Priority Priority { get; set; }
        
        public Type Type { get; set; }
        
        public Status Status { get; set; }

        /// <summary>
        /// Get and set time when the task was created.
        /// </summary>
        public string Created { get; set; }

        /// <summary>
        /// Get and set time when the task was last time updated.
        /// </summary>
        public string Updated { get; set; }

        /// <summary>
        /// Get and set deadline for task
        /// </summary>
        public string Deadline { get; set; }

        /// <summary>
        /// Get and set the foreign key for the project.
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Get and set the foreign key for the user that created the task.
        /// </summary>
        public int CreatorId { get; set; }

        public string CreatorUserName { get; set; }

        /// <summary>
        /// Get and set the foreign key for the user performing the task.
        /// </summary>
        public int? ExecutorId { get; set; }

        public string ExecutorUserName { get; set; }
        
        /// <summary>
        /// Get and set comments of the task.
        /// </summary>
        public IEnumerable<CommentDTO> Comments { get; set; }
    }
    
    public enum Priority
    {
        Low,
        Middle,
        High,
        Blocker
    }

    public enum Type
    {
        Task,
        Bug,
        Improvment,
        Feature
    }

    public enum Status
    {
        ToDo,
        InProgress,
        OnReview,
        Done
    }
}
