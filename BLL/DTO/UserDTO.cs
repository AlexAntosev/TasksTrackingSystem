using System.Collections.Generic;

namespace BLL.DTO
{
    public sealed class UserDTO
    {
        public int Id { get; set; }

        /// <summary>
        /// Get and set unique username
        /// </summary>
        public string UserName { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Position { get; set; }

        /// <summary>
        /// Get and set collection of projects
        /// </summary>
        public IEnumerable<ProjectDTO> Projects { get; set; }

        /// <summary>
        /// Get and set collection of created tasks
        /// </summary>
        public IEnumerable<TaskDTO> CreatedTasks { get; set; }

        /// <summary>
        /// Get and set collection of task in process
        /// </summary>
        public IEnumerable<TaskDTO> TasksInProcess { get; set; }

        /// <summary>
        /// Get and set the foreign key for the authenticatiob user.
        /// </summary>
        public string AuthenticationUserId { get; set; }
    }
}
