using System.ComponentModel.DataAnnotations;

namespace BidOneTest.Mvc.Models
{
    /// <summary>
    /// View Model
    /// </summary>
    public class ViewModel
    {
        /// <summary>
        /// First Name
        /// </summary>
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(50)]
        public string LastName { get; set; }
    }
}