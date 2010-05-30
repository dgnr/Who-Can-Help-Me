namespace WhoCanHelpMe.Web.Controllers.User.ViewModels
{
    #region Using Directives

    using System.ComponentModel.DataAnnotations;

    using WhoCanHelpMe.Framework.Validation;

    #endregion

    public class LoginFormModel
    {
        #region Properties

        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage = "*")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "*")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        #endregion
    }
}