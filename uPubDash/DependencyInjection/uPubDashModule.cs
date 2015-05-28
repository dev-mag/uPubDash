using Ninject.Modules;
using uPubDash.Persistence;
using uPubDash.Services;
using Umbraco.Core.Services;

namespace uPubDash.DependencyInjection
{
    public class uPubDashModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPublicationRequestRepository>().To<PublicationRequestRepository>();
            Bind<IPublicationRequestService>().To<PublicationRequestService>();
            Bind<IUserService>().ToMethod(context => global::Umbraco.Core.ApplicationContext.Current.Services.UserService);
            Bind<IContentService>().ToMethod(context => global::Umbraco.Core.ApplicationContext.Current.Services.ContentService);
        }
    }
}