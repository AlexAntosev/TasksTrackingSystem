using System;

namespace BLL.DTO
{
    public sealed class CommentDTO
    {
        public int Id { get; set; }

        /// <summary>
        /// Get and set the foreign key for the user that made the comment.
        /// </summary>
        public int AuthorId { get; set; }

        public string AuthorUserName { get; set; }
        
        public string Description { get; set; }
        
        public string Time { get; set; }

        /// <summary>
        /// Get and set the foreign key for the task where the comment was made.
        /// </summary>
        public int TaskId { get; set; }
    }
}
