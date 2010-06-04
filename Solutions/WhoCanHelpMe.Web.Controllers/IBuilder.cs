namespace WhoCanHelpMe.Web.Controllers
{
    public interface IBuilder<T>
    {
        T Get();
    }
}