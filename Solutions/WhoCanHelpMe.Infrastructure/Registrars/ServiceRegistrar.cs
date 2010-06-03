namespace WhoCanHelpMe.Infrastructure.Registrars
{
    #region Using Directives

    using System.ComponentModel.Composition;
    using System.Reflection;

    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    using Domain.Contracts.Container;

    using Framework.Extensions;

    using Properties;

    using WhoCanHelpMe.Framework.Security;
    using WhoCanHelpMe.Infrastructure.Security;

    #endregion

    [Export(typeof(IComponentRegistrar))]
    public class ServiceRegistrar : IComponentRegistrar
    {
        public void Register(IWindsorContainer container)
        {
            container.Register(
                    AllTypes.Pick()
                            .FromAssembly(Assembly.GetAssembly(typeof(InfrastructureRegistrarMarker)))
                            .If(f => f.Namespace.Equals("WhoCanHelpMe.Infrastructure.News"))
                            .WithService.FirstNonGenericCoreInterface("WhoCanHelpMe.Domain.Contracts.Services"));

            container.Register(Component.For<IIdentityService>().ImplementedBy<IdentityService>());
        }
    }
}