namespace WhoCanHelpMe.Web.Controllers.User.ViewModels
{
    #region Using Directives

    using Shared.ViewModels;

    #endregion

    public class LoginPageViewModel : PageViewModel
    {
        public LoginPageViewModel()
        {
            this.FormModel = new LoginFormModel();
        }

        public string Message { get; set; }

        public LoginFormModel FormModel { get; set; }
    }
}