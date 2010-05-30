namespace WhoCanHelpMe.Web.Controllers.User.ViewModels
{
    #region Using Directives

    using Shared.ViewModels;

    #endregion

    public class LoginPageViewModel : PageViewModel
    {
        public string Message { get; set; }

        public LoginFormModel LoginFormModel { get; set; }

        public RegistrationFormModel RegistrationFormModel { get; set; }    
    }
}