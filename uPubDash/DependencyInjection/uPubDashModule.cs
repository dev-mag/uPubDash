using Ninject.Modules;
using uPubDash.Persistence;

namespace uPubDash.DependencyInjection
{
    public class uPubDashModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPublicationRequestRepository>().To<PublicationRequestRepository>();
            Bind<IPublicationRequestService>().To<PublicationRequestService>();
        }
    }
}