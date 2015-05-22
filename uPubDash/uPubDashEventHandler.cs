using System;
using umbraco.cms.businesslogic;
using umbraco.cms.businesslogic.web;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace uPubDash
{
    public class uPubDashEventHandler : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            uPubDash.Initialise();

            ContentService.SendingToPublish += Enqueue;
            Document.AfterSendToPublish += DocumentOnAfterSendToPublish;

        }

        private void DocumentOnAfterSendToPublish(Document sender, SendToPublishEventArgs sendToPublishEventArgs)
        {
            throw new NotImplementedException();
        }

        private void Enqueue(IContentService sender, SendToPublishEventArgs<IContent> e)
        {
            uPubDash.Enqueue(sender, e);
        }
    }
}