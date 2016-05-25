<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="VegiJ.Web.Users.Profile" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Import Namespace="VegiJ.DataAccess" %>
<%@ Import Namespace="Microsoft.Ajax.Utilities" %>
<%@ Import Namespace="System.Web.Routing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/profile-page.css" type="text/css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="PanelProfile" CssClass="panel panel-default panel-profile" runat="server">
        <asp:Panel ID="PanelHeading" CssClass="panel panel-heading panel-profile-heading" runat="server">
            <h1 runat="server">Профилът на <%= this.currentUser.UserName %>
                <%= Boolean.Parse((this.currentUser.FullName.Trim() != "").ToString()) ? " - " + this.currentUser.FullName : "" %>
            </h1>
        </asp:Panel>
        <asp:Panel runat="server" ID="PanelProfileBody" CssClass="panel panel-body">
            <asp:FormView ID="userDetail" runat="server" SelectMethod="GetUser" ItemType="VegiJ.DataAccess.User" RenderOuterTable="False">
                <ItemTemplate>
                    <div class="col-lg-4">
                        <div class="panel panel-default panel-profile-info">
                            <div class=" panel-heading">
                                <h3>Основна информация
                                <asp:HyperLink ID="HyperLinkEditProfile"
                                    Visible='<%# Boolean.Parse(IsOwnProfilePage().ToString()) || Boolean.Parse(User.IsInRole("admin").ToString()) %>'
                                    NavigateUrl='<%#: GetRouteUrl("SettingsByUserNameRoute", new {username = Item.UserName}) %>'
                                    runat="server" CssClass="fa fa-cog icon-hyperlink"></asp:HyperLink>
                                </h3>
                            </div>
                            <div class="list-group-item">
                                <asp:Label ID="LabelUserRole" runat="server" Text="Role:"></asp:Label>
                                &nbsp;
                        <span><%#: Item.IsAdmin ? "Admin" : "User" %></span>
                            </div>

                            <asp:Panel ID="PanelProfileBirthday" CssClass="list-group-item" Visible='<%# Boolean.Parse(Item.BirthDate.HasValue.ToString()) %>' runat="server">
                                <asp:Label ID="LabelBirthdate" runat="server" Text="Birthday:"></asp:Label>
                                &nbsp;
                                <span><%#: Item.BirthDate.GetValueOrDefault().ToString(GlobalConstants.DateFormat) %></span>
                            </asp:Panel>
                            <div class="list-group-item">
                                <asp:Label ID="LabelGender" runat="server" Text="Gender:"></asp:Label>
                                &nbsp;
                            <span><%#: Item.GenderID.HasValue ? Item.Gender.Name : GlobalConstants.UnknownString %></span>
                            </div>
                            <div class="list-group-item">
                                <asp:Label ID="LabelUserEmail" Text="Email:" runat="server"></asp:Label>
                                &nbsp;
                            <span><%#: Item.Email %></span>
                            </div>
                            <div class="list-group-item">
                                <asp:Label ID="LabelUserRegDate" runat="server" Text="Regirster date:"></asp:Label>
                                &nbsp;
                            <span><%#: Item.CreatedDate.ToString(GlobalConstants.DateTimeFormat) %></span>
                            </div>
                            <div class="list-group-item">
                                <asp:Label ID="LabelUserLastLoginDate" runat="server" Text="Last login date:"></asp:Label>
                                &nbsp;
                            <span><%#: Item.LastLoginDate.HasValue ? Item.LastLoginDate.Value.ToString(GlobalConstants.DateTimeFormat) : GlobalConstants.NoLoginDate %></span>
                            </div>
                            <asp:Panel ID="PanelProfilModifiedDate" CssClass="list-group-item" Visible='<%# Boolean.Parse(IsOwnProfilePage().ToString()) %>' runat="server">
                                <asp:Label ID="LabelUserModifiedDate" runat="server" Text="Profile last edited date:"></asp:Label>
                                &nbsp;
                                <span><%#: Item.LastModifiedDate.ToString(GlobalConstants.DateTimeFormat) %></span>
                            </asp:Panel>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:FormView>
            <div class="col-lg-4">
                <div class="panel panel-default panel-profile-info">
                    <div class="panel-heading">
                        <h3>Recipes</h3>
                    </div>
                    <telerik:RadListView ID="RadListView1" runat="server" RenderMode="Lightweight"
                        OnNeedDataSource="RadListView1_NeedDataSource" DataKeyNames="ID">
                        <ItemTemplate>
                            <div class="list-group-item">
                                <asp:HyperLink ID="HyperLinkEditProfile"
                                    NavigateUrl='<%# GetRouteUrl("RecipeByNameRoute", new { title = Eval("Title")}) %>'
                                    runat="server" Text='<%# Eval("Title") %>'>
                                </asp:HyperLink>
                            </div>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <div class="RadDataForm RadDataForm_<%# Container.Skin %>">
                                <div class="list-group-item">
                                    <em>There are no recipes to be displayed.</em>
                                </div>
                            </div>
                        </EmptyDataTemplate>
                    </telerik:RadListView>
                </div>
            </div>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
