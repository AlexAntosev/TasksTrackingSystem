namespace DAL.Entities
{
    public class UserProfile : BaseEntity
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
    }
}
