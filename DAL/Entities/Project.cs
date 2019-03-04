using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Project
    {
        /// <summary>
        /// Get and set project id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Get and set project name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get and set project tag
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Get and set task collection in project
        /// </summary>
        public virtual ICollection<Task> Tasks { get; set; }

        /// <summary>
        /// Get and set collection of users in project
        /// </summary>
        public virtual ICollection<User> Team { get; set; }
    }
}
