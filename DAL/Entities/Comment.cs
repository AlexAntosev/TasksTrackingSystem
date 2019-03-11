using System;

namespace DAL.Entities
{
    public class Comment : BaseEntity
    {
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
