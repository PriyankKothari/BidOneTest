using System.ComponentModel.DataAnnotations;

namespace BidOneTest.Api.Models
{
    public class Model
    {
        public Model(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        [Required(ErrorMessage = "FirstName is required")]
        [MaxLength(50, ErrorMessage = "First name cannot be more than 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [MaxLength(50, ErrorMessage = "Last name cannot be more than 50 characters")]
        public string LastName { get; set; }
    }
}