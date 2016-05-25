<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="VegiJ.Web._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .panel-body .btn-more {
            float: right;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>VegiJ</h1>
        <p class="lead">The place where you can find nice vegetarian recipes.</p>
    </div>
    <asp:Panel ID="AuthenticatedMessagePanel" runat="server" Visible="False">
        <div class="panel panel-warning">
            <div class="panel-heading">
                <asp:Label ID="WelcomeBackMessage" runat="server" Text="Label"></asp:Label>
            </div>
        </div>

    </asp:Panel>
    <telerik:RadListView ID="RadListView2" runat="server" RenderMode="Lightweight"
        ItemPlaceholderID="RecipeContainer" OnNeedDataSource="RadListView1_NeedDataSource" OnPreRender="RadListView1_PreRender">
        <LayoutTemplate>
            <div class="panel panel-success">
                <div class="panel-heading">
                    <h2 class="panel-title">Recent recipes</h2>
                </div>
                <div class="panel-body">
                    <asp:PlaceHolder ID="RecipeContainer" runat="server"></asp:PlaceHolder>
                    <div style="clear: both;"></div>
                    <asp:Button runat="server" ID="BtnMoreRecipes" PostBackUrl="~/Recipes.aspx" Text="More" CssClass="btn btn-success btn-more" />
                </div>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="col-sm-6 col-md-4 item">
                <div class="thumbnail">
                    <div class="caption">
                        <h3><%# Eval("Title") %></h3>
                        <p><%# Eval("Content") %></p>
                        <a href="<%#: GetRouteUrl("RecipeByNameRoute", new {title = Eval("Title")}) %>"
                            class="btn btn-success">View</a>
                    </div>
                </div>
            </div>
        </ItemTemplate>
        <EmptyItemTemplate>
            <h2>There are no recent recipes.</h2>
        </EmptyItemTemplate>
    </telerik:RadListView>
    <div class="panel panel-info">
        <div class="panel-heading">
            <p>
                Last registered user is <a href="<%= GetRouteUrl("UserByNameRoute", new {username = this.RecentUser().UserName}) %>">
                    <%= this.RecentUser().UserName %>
                </a>
            </p>
        </div>
    </div>
</asp:Content>
