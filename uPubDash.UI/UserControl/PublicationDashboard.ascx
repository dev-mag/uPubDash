<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PublicationDashboard.ascx.cs" Inherits="uPubDash.UI.UserControl.PublicationDashboard" %>
<%@ Import Namespace="uPubDash.UI" %>

<link href="<%= Page.ClientScript.GetWebResourceUrl(typeof(uPubDashResources), "uPubDash.UI.Content.bootstrap.min.css") %>" rel="stylesheet" />

<script type="text/javascript" src="<%= Page.ClientScript.GetWebResourceUrl(typeof(uPubDashResources), "uPubDash.UI.Scripts.jquery-1.9.1.min.js") %>"></script>
<script type="text/javascript" src="<%= Page.ClientScript.GetWebResourceUrl(typeof(uPubDashResources), "uPubDash.UI.Scripts.bootstrap.min.js") %>"></script>

<h1>Publication Queue</h1>

<asp:Repeater ID="rptPublicationRequests" runat="server">
    <HeaderTemplate>
        <table class="table table-striped">
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
                <asp:HyperLink runat="server" ID="hlnkContent" NavigateUrl='<%# string.Format("/editContent.aspx?id={0}", Eval("Id")) %>' Text='<%# Eval("Name") %>'></asp:HyperLink>
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
