<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tags.aspx.cs" Inherits="VegiJ.Web.Tags" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .main-content a:hover {
            text-decoration: none;
        }
        .main-content a {
            font-size: 18px;
        }
        .label-success {
            background-color: mediumseagreen;
            padding: 5px 10px;
            margin: 5px;
        }
        .label-success:hover {
            background-color: forestgreen;

        }
        .badge {
            background-color: darkgrey;
            margin-left: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadListView ID="RadListView1" runat="server" RenderMode="Lightweight"
        ItemPlaceholderID="RecipeContainer" OnNeedDataSource="RadListView1_NeedDataSource" OnPreRender="RadListView1_PreRender">
        <LayoutTemplate>
            <div class="page-header">
                <h1>All Tags</h1>
            </div>
            <asp:PlaceHolder ID="RecipeContainer" runat="server"></asp:PlaceHolder>
            <div style="clear: both;"></div>
        </LayoutTemplate>
        <ItemTemplate>
            <a href="<%#: GetRouteUrl("TagByNameRoute", new {tag = Eval("Name")}) %>"><span class="label label-success">
                <%# Eval("Name") %> <span class="badge"><%# Eval("Recipes.Count") %></span></span>
            </a>
        </ItemTemplate>
    </telerik:RadListView>
</asp:Content>
