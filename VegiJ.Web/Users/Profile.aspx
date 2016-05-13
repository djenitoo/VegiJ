<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="VegiJ.Web.Users.Profile" %>

<%@ Import Namespace="VegiJ.DataAccess" %>
<%@ Import Namespace="Microsoft.Ajax.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:FormView ID="userDetail" runat="server" SelectMethod="GetUser" ItemType="VegiJ.DataAccess.User" RenderOuterTable="False">
        <ItemTemplate>
            <div>
                <h2 <%#: Item.IsAdmin ? "id=profile-name-admin" : "" %> ><%#: Item.UserName %></h2>
            </div>
            <table>
                <tr>
                    <td>Role:</td>
                    <td><%#: Item.IsAdmin ? "Admin" : "User" %></td>
                </tr>
                <asp:Panel ID="PanelFullName" Visible='<%# Boolean.Parse((Item.FullName.Trim() != "").ToString()) %>'  runat="server">
                    <tr>
                    <td>Name:</td>
                    <td><%#: Item.FullName %></td>
                </tr>
                </asp:Panel>
                <tr>
                    <td>Email:</td>
                    <td><%#: Item.Email %></td>
                </tr>
                <tr>
                    <td>Regirster date:</td>
                    <td><%#: Item.CreatedDate.ToString(GlobalConstants.DateTimeFormat) %></td>
                </tr>
                <tr>
                    <td>Last login date:</td>
                    <td><%#: Item.LastLoginDate.HasValue ? Item.LastLoginDate.Value.ToString(GlobalConstants.DateTimeFormat) : GlobalConstants.NoLoginDate %></td>
                </tr>
                <asp:Panel ID="PanelProfilModifiedDate" Visible='<%# Boolean.Parse(IsOwnProfilePage().ToString()) %>'  runat="server">
                <tr>
                    <td>Profile last edited date:</td>
                    <td><%#: Item.LastModifiedDate.ToString(GlobalConstants.DateTimeFormat) %></td>
                </tr>
                </asp:Panel>
            </table>
        </ItemTemplate>
    </asp:FormView>
</asp:Content>
