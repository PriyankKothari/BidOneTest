using System.ComponentModel.DataAnnotations;

namespace BidOneTest.WebApi.Models
{
    public class Model
    {
        public Model(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }
    }
}