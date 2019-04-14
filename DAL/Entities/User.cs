using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class User : BaseEntity
    {
        /// <summary>
        /// Get and set unique username
        /// </summary>
        public string UserName { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Position { get; set; }

        /// <summary>
        /// Get and set the foreign key for the authenticatiob user.
        /// </summary>
        public string AuthenticationUserId { get; set; }

        /// <summary>
        /// Lazy loading the authentication user by the foreign key.
        /// </summary>
        public virtual AuthenticationUser AuthenticationUser { get; set; }

        /// <summary>
        /// Lazy loading of user projects.
        /// </summary>
        public virtual ICollection<Project> Projects { get; set; }

        /// <summary>
        /// Lazy loading of tasks created by the user.
        /// </summary>
        public virtual ICollection<Task> CreatedTasks { get; set; }

        /// <summary>
        /// Lazy loading of tasks performed by the user.
        /// </summary>
        
        public virtual ICollection<Task> TasksInProgress { get; set; }

        /// <summary>
        /// Lazy loading of invites which were received by the user.
        /// </summary>
        public virtual ICollection<Invite> Invites { get; set; }

        /// <summary>
        /// Lazy loading of invites which were created by the user.
        /// </summary>
        public virtual ICollection<Invite> CreatedInvites { get; set; }
    }
}
