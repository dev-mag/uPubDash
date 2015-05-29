using System;
using System.Collections.Generic;
using System.ComponentModel;
using uPubDash.Models;

namespace uPubDash.UI.UserControl
{
    public partial class PublicationDashboard : System.Web.UI.UserControl
    {
        [Bindable(true)]
        public List<PublicationRequestDto> Data { private get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Data == null)
            {
                Data = uPubDash.GetPublicationRequests();
            }

            Repeater1.DataSource = Data;
            Repeater1.DataBind();
        }
    }
}