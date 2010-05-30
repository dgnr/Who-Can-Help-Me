namespace WhoCanHelpMe.Web.Controllers.User.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using WhoCanHelpMe.Framework.Validation;

    [PropertiesMustMatch("Password", "ConfirmPassword")]
    public class RegistrationFormModel
    {
        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage = "*")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "*")]
        public string Password { get; set; }

        [Required(ErrorMessage = "*")]
        public string ConfirmPassword { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}