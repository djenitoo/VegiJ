<%@ Page Title="Admin page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="VegiJ.Web.Users.Administration.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>This is Admin page.</h3>
    <asp:HyperLink ID="HyperLinkEditProfile"
        NavigateUrl="~/Users/Administration/EditUsers.aspx"
        runat="server" CssClass="fa fa-cog icon-hyperlink" Text="Edit all users"></asp:HyperLink>
</asp:Content>
