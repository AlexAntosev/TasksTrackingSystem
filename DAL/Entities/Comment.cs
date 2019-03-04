using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Comment
    {
        /// <summary>
        /// Get and set comment id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Get and set comment author id
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// Get and set comment author
        /// </summary>
        public virtual User Author { get; set; }

        /// <summary>
        /// Get and set comment description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Get and set date time when comment was created
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Get and set foreign key for Task connected with 
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// Get and set connected task by foreign key
        /// </summary>
        public virtual Task Task { get; set; }
    }
}
