<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="uPubDash.UI.TestPage" %>

<%@ Import Namespace="uPubDash.Models" %>

<%@ Register TagPrefix="uPD" TagName="dashboard" Src="~/UserControl/PublicationDashboard.ascx" %>
<!DOCTYPE html>

<script runat="server">
    protected List<PublicationRequestDto> TestData
    {
        get
        {
            return new List<PublicationRequestDto>()
            {
                new PublicationRequestDto()
                {
                    Id = 1,
                    VersionId = Guid.NewGuid(),
                    DateSubmitted = DateTime.Now,
                    Name = "Item1 Name",
                    SubmittedBy = "User1 Name"
                },
                new PublicationRequestDto()
                {
                    Id = 2,
                    VersionId = Guid.NewGuid(),
                    DateSubmitted = DateTime.Now.AddHours(-1),
                    Name = "Item2 Name",
                    SubmittedBy = "User2 Name"
                }
            };
        }
        private set { }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>uPubDash user control test page</title>
</head>
<body>
    <h1>uPubDash user control test page</h1>
    <form id="form1" runat="server">
        <uPD:dashboard ID="uDPDashboardControl" runat="server" Data="<%# TestData %>" />
    </form>
</body>
</html>
