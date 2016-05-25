<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Recipes.aspx.cs" Inherits="VegiJ.Web.Recipes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .item .thumbnail {
            height: 210px;
        }

        .item p {
            min-height: 3em;
            overflow: hidden;
        }

        .btn-more {
            float: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadListView ID="RadListView1" runat="server" RenderMode="Lightweight"
        ItemPlaceholderID="RecipeContainer" OnNeedDataSource="RadListView1_NeedDataSource" OnPreRender="RadListView1_PreRender">
        <LayoutTemplate>
            <div class="page-header">
                <h1>Recipes</h1>
                <telerik:RadDropDownTree RenderMode="Lightweight" ID="RadDropDownTree1" runat="server" OnEntryAdded="RadDropDownTree1_EntryAdded"
                    DefaultMessage="Select category" ExpandNodeOnSingleClick="true" AutoPostBack="true" EnableFiltering="true">
                    <DropDownSettings OpenDropDownOnLoad="false" CloseDropDownOnSelection="true" />
                    <FilterSettings Highlight="Matches" EmptyMessage="Type here to filter recipes by category" />
                </telerik:RadDropDownTree>
                <asp:Button ID="Button1" runat="server" Text="Sort ascending" OnClick="Button1_Click" />
                <asp:Button ID="Button2" runat="server" Text="Sort descending" OnClick="Button2_Click" />
                <asp:Button runat="server" ID="BtnMoreTags" PostBackUrl="~/Tags.aspx" Text="Tags" CssClass="btn btn-warning btn-more" />
            </div>
            <asp:PlaceHolder ID="RecipeContainer" runat="server"></asp:PlaceHolder>
            <div style="clear: both;"></div>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="col-sm-6 col-md-4 item">
                <div class="thumbnail">
                    <div class="caption">
                        <h3><%# Eval("Title") %></h3>
                        <small><a href="<%#: GetRouteUrl("CategoryByNameRoute", new { category = Eval("Category.Name") }) %>"><%# Eval("Category.Name") %></a></small>
                        <p><%# Eval("Content") %></p>
                        <a href="<%#: GetRouteUrl("RecipeByNameRoute", new {title = Eval("Title")}) %>"
                            class="btn btn-success">View</a>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </telerik:RadListView>
</asp:Content>
