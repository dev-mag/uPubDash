using System.Diagnostics;
using umbraco.cms.businesslogic;
using umbraco.cms.businesslogic.web;
using Umbraco.Core;

namespace uPubDash
{
    public class uPubDashEventHandler : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            uPubDash.Initialise();

            Document.AfterSendToPublish += DocumentOnAfterSendToPublish;

            Document.AfterPublish += DocumentOnAfterPublish;
        }

        private void DocumentOnAfterSendToPublish(Document sender, SendToPublishEventArgs sendToPublishEventArgs)
        {
            uPubDash.Enqueue(sender);
        }

        private void DocumentOnAfterPublish(Document sender, PublishEventArgs publishEventArgs)
        {
            uPubDash.Dequeue(sender);
        }
    }
}