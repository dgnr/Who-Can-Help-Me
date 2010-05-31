namespace WhoCanHelpMe.Web.Controllers.User.ViewModels
{
    #region Using Directives

    using Shared.ViewModels;

    #endregion

    public class RegisterPageViewModel : PageViewModel
    {
        public RegisterPageViewModel()
        {
            this.FormModel = new RegistrationFormModel();
        }

        public string Message { get; set; }

        public RegistrationFormModel FormModel { get; set; }    
    }
}