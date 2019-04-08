using System.Collections.Generic;

namespace DAL.Entities
{
    public class User : BaseEntity
    {
        /// <summary>
        /// Get and set unique username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Get and set user firstname
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Get and set user lastname
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Get and set user position
        /// </summary>
        public string Position { get; set; }

        public string AuthenticationUserId { get; set; }
        public virtual AuthenticationUser AuthenticationUser { get; set; }

        /// <summary>
        /// Get and set collection of projects
        /// </summary>
        public virtual ICollection<Project> Projects { get; set; }

        /// <summary>
        /// Get and set collection of created task
        /// </summary>
        public virtual ICollection<Task> CreatedTasks { get; set; }

        /// <summary>
        /// Get and set collection of created task
        /// </summary>
        public virtual ICollection<Task> TasksInProgress { get; set; }
    }
}
