using BlocketLiteEFCoreDB.Entities;

namespace BlocketLiteAPI.Models
{
    /// <summary>
    /// A model for mapping infromation about an <see cref="User"/> to an XML/Json-file.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// The username of an <see cref="User"/>
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The number:<see cref="int"/> of <see cref="Advertisment"/> an <see cref="User"/> have.
        /// </summary>
        public int RealEstates { get; set; }

        /// <summary>
        /// The number:<see cref="int"/> <see cref="Comment"/> a <see cref="User"/> have.
        /// </summary>
        public int Comments { get; set; }

        /// <summary>
        /// The avarage <see cref="Rating.Value"/> an <see cref="User"/> have.
        /// </summary>
        public double? Rating { get; set; }
    }
}
