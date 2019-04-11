using System;

namespace DAL.Entities
{
    public class Comment : BaseEntity
    {
        /// <summary>
        /// Get and set the foreign key for the user that made the comment.
        /// </summary>
        public int? AuthorId { get; set; }

        /// <summary>
        /// Lazy loading the user who made the comment using the foreign key.
        /// </summary>
        public virtual User Author { get; set; }
        
        public string Description { get; set; }

        /// <summary>
        /// Get and set date time when comment was made.
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Get and set the foreign key for the task where the comment was made.
        /// </summary>
        public int? TaskId { get; set; }

        /// <summary>
        /// Lazy loading the task where the comment was made using the foreign key.
        /// </summary>
        public virtual Task Task { get; set; }
    }
}
