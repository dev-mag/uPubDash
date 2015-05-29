using System;

namespace uPubDash.UI
{
    public partial class TestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            uDPDashboardControl.DataBind();
        }
    }
}