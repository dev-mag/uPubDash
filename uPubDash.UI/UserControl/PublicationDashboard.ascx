<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PublicationDashboard.ascx.cs"  Inherits="uPubDash.UI.UserControl.PublicationDashboard" %>

<link href="upubdash/usercontrol/css/publication-dashboard.css" rel="stylesheet" />

<h1>Publication Queue</h1>

<asp:Repeater ID="rptPublicationRequests" runat="server">
    <HeaderTemplate>
        <table class="publication-table publication-table-striped publication-table-bordered publication-table-hover">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Name</th>
                    <th>SubmittedBy</th>
                    <th>VersionId</th>
                    <th>DateSubmitted</th>
                </tr>
            </thead>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td><asp:Label runat="server" ID="lblId" Text='<%# Eval("Id") %>' /></td>
            <td>
                <asp:HyperLink runat="server" ID="hlnkContent" NavigateUrl='<%# string.Format("/umbraco/editContent.aspx?id={0}", Eval("NodeId")) %>' Text='<%# Eval("Name") %>'></asp:HyperLink>
            </td>
            <td><asp:Label runat="server" ID="lblSubmittedBy" Text='<%# Eval("SubmittedBy") %>' /></td>
            <td><asp:Label runat="server" ID="lblVersionId" Text='<%# Eval("VersionId") %>' /></td>
            <td><asp:Label runat="server" ID="lblDateSubmitted" Text='<%# Eval("DateSubmitted") %>' /></td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>
