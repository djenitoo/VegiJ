<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewCategory.aspx.cs" Inherits="VegiJ.Web.ViewCategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadListView ID="RadListView2" runat="server" RenderMode="Lightweight"
        ItemPlaceholderID="RecipeContainer" OnNeedDataSource="RadListView1_NeedDataSource" OnPreRender="RadListView1_PreRender">
        <LayoutTemplate>
            <div class="page-header">
                <h1><%= Eval("Category.Name") %> category</h1>
            </div>
            <asp:PlaceHolder ID="RecipeContainer" runat="server"></asp:PlaceHolder>
            <div style="clear:both;"></div>
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
            <h2>There are no recipes in this category.</h2>
        </EmptyItemTemplate>
    </telerik:RadListView>
</asp:Content>
