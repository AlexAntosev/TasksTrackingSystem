namespace BLL.DTO
{
    public sealed class UserProfileDTO
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
