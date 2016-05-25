<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewRecipe.aspx.cs" Inherits="VegiJ.Web.ViewRecipe" %>

<%@ Import Namespace="VegiJ.DataAccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:FormView ID="FormView1" runat="server" SelectMethod="GetRecipe" DataKeyNames="ID" ItemType="VegiJ.DataAccess.Recipe">
        <ItemTemplate>
            <div class="page-header">
                <span runat="server" visible='<%# Item.IsApproved ? false : true %>' class="label label-danger">Pending approval</span>
                <h1><%# Item.Title %>
                    <small>by 
                    <a href="<%#: GetRouteUrl("UserByNameRoute", new {username = Item.Author.UserName}) %>">
                        <%# Item.Author.UserName %>
                    </a>
                    </small>
                </h1>
                <small>Published : <%# Item.CreatedDate.ToString(GlobalConstants.DateTimeFormat) %></small>
                <p>
                    Category: <a href="<%#: GetRouteUrl("CategoryByNameRoute", new {category = Item.Category.Name}) %>">
                        <%# Item.Category.Name %>
                    </a>
                </p>
                <p>
                    Tags: 
                    <telerik:RadListView ID="RadListView1" runat="server" DataSource='<%# Item.Tags.ToList() %>'>
                        <ItemTemplate>
                            <a href="<%#: GetRouteUrl("TagByNameRoute", new {tag = Eval("Name")}) %>"><span class="label label-default">
                                <%# Eval("Name") %>&nbsp;</span>
                            </a>
                        </ItemTemplate>
                    </telerik:RadListView>
                </p>
            </div>
            <div class="container">
                <p><%# Item.Content %></p>
            </div>
        </ItemTemplate>
    </asp:FormView>
</asp:Content>
