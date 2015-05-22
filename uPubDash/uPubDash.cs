using Ninject;
using uPubDash.DependencyInjection;
using uPubDash.Persistence;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace uPubDash
{
    public class uPubDash
    {
        public static void Initialise()
        {
            TableFactory.CreateTables();
        }

        public static void Enqueue(IContentService sender, SendToPublishEventArgs<IContent> e)
        {
            Ioc.Initialize(NinjectIocContainer.Create(new StandardKernel(new uPubDashModule())));
            //Ioc.Get<>()
        }
    }
}