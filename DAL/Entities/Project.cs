using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        
        public string Tag { get; set; }

        public string Url { get; set; }

        /// <summary>
        /// Get and set the foreign key for the user that created the project.
        /// 
        /// </summary>
        public int? LeadId { get; set; }
        /// <summary>
        /// Lazy loading the user who created the project using the foreign key.
        /// </summary>
        public virtual User Lead { get; set; }

        /// <summary>
        /// Lazy loading of tasks in the project.
        /// </summary>
        public virtual ICollection<Task> Tasks { get; set; }

        /// <summary>
        /// Lazy loading of users in the project.
        /// </summary>
        public virtual ICollection<User> Team { get; set; }
        
        /// <summary>
        /// Lazy loading of invites in the project.
        /// </summary>
        public virtual ICollection<Invite> Invites { get; set; }

        public Project()
        {
            Tasks = new HashSet<Task>();
            Team = new HashSet<User>();
            Invites = new HashSet<Invite>();
        }
    }
}
