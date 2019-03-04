using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class UserProfile
    {
        /// <summary>
        /// Get and set user profile id
        /// </summary>
        public int Id { get; set; }

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
    }
}
