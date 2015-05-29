<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PublicationDashboard.ascx.cs" Inherits="uPubDash.UI.UserControl.PublicationDashboard" %>
<p>Publication Queue</p>
<asp:Repeater ID="Repeater1" runat="server">
    <HeaderTemplate>
        <table>
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Name</th>
                    <th>SubmittedBy</th>
                    <th>DateSubmitted</th>
                    <th>VersionId</th>
                </tr>
            </thead>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td><asp:Label runat="server" ID="Label2" Text='<%# Eval("Id") %>' /></td>
            <td><asp:Label runat="server" ID="Label5" Text='<%# Eval("Name") %>' /></td>
            <td><asp:Label runat="server" ID="Label1" Text='<%# Eval("SubmittedBy") %>' /></td>
            <td><asp:Label runat="server" ID="Label3" Text='<%# Eval("VersionId") %>' /></td>
            <td><asp:Label runat="server" ID="Label4" Text='<%# Eval("DateSubmitted") %>' /></td>
        </tr>
    </ItemTemplate>
</asp:Repeater>
