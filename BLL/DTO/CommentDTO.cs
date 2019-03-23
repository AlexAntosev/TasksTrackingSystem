using System;

namespace BLL.DTO
{
    public sealed class CommentDTO
    {
        /// <summary>
        /// Get and set comment id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Get and set comment user who create comment
        /// </summary>
        public UserDTO Author { get; set; }

        /// <summary>
        /// Get and set comment description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Get and set date time when comment was created
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Get and set commented task
        /// </summary>
        public TaskDTO Task { get; set; }
    }
}
