using System.ComponentModel.DataAnnotations;

namespace BidOneTest.Api.Models
{
    /// <summary>
    /// Api Model
    /// </summary>
    public class Model
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="firstName">First Name</param>
        /// <param name="lastName">Last Name</param>
        public Model(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        /// <summary>
        /// First Name
        /// </summary>
        [Required(ErrorMessage = "FirstName is required")]
        [MaxLength(50, ErrorMessage = "First name cannot be more than 50 characters")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        [Required(ErrorMessage = "LastName is required")]
        [MaxLength(50, ErrorMessage = "Last name cannot be more than 50 characters")]
        public string LastName { get; set; }
    }
}