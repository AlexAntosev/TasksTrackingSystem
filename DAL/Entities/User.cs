using System.Collections.Generic;

namespace DAL.Entities
{
    public class User : BaseEntity
    {
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

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        /// <summary>
        /// Get and set collection of projects
        /// </summary>
        public virtual ICollection<Project> Projects { get; set; }
    }
}
