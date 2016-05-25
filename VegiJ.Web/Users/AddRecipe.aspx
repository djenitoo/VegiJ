<%@ Page Title="Create Recipe" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddRecipe.aspx.cs" Inherits="VegiJ.Web.Users.AddRecipe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/add-recipe-styles.css" type="text/css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadDataForm ID="RadDataForm1" runat="server" RenderMode="Lightweight" DataKeyNames="ID"
        OnItemInserting="RadDataForm1_ItemInserting" OnPreRender="RadDataForm1_PreRender" OnNeedDataSource="RadDataForm1_NeedDataSource" OnItemCreated="RadDataForm1_ItemCreated">
        <LayoutTemplate>
            <div id="demo">
                <div class="RadDataForm RadDataForm_<%# Container.Skin %> panel panel-default">
                    <div class="rdfHeader panel-heading">
                        <h2 class="rdfHeaderInner">Create new recipe</h2>
                    </div>
                    <div id="itemPlaceholder" runat="server">
                    </div>
                </div>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
        </ItemTemplate>
        <InsertItemTemplate>
            <fieldset class="rdfFieldset rdfBorders">
                <legend class="rdfLegend"></legend>
                <h5>
                    <asp:ValidationSummary ID="RecipeValidationSummary" runat="server" ForeColor="#CC0000" HeaderText="The following errors occured:" />
                </h5>
                <div class="rdfRow row input-group">
                    <telerik:RadTextBox RenderMode="Lightweight" ID="RecipeTitleTextBox" CssClass="form-control" placeholder="Title" runat="server" Text='<%# Bind("Title") %>' WrapperCssClass="rdfInput" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="RecipeTitleTextBox" ErrorMessage="Title is required!" CssClass="validator">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="RecipeTitleTextBox" ErrorMessage="Minimum length of title is 8 characters!" CssClass="validator" ValidationExpression="^[\s\S]{8,}$">*</asp:RegularExpressionValidator>
                </div>
                <div class="rdfRow row input-group">
                    <telerik:RadTextBox RenderMode="Lightweight" ID="RecipeContentTextBox" CssClass="form-control" placeholder="Content" runat="server" Text='<%# Bind("Content") %>' WrapperCssClass="rdfInput" TextMode="MultiLine" Height="100px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="RecipeContentTextBox" ErrorMessage="Content of recipe is required!" CssClass="validator">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="RecipeContentTextBox" ErrorMessage="Minimum length of content is 50 characters and maximum 500!" CssClass="validator" ValidationExpression="^[\s\S]{50,500}$">*</asp:RegularExpressionValidator>
                </div>
                <hr class="divider" />
                <div class="rdfRow row input-group">
                    <telerik:RadDropDownTree RenderMode="Lightweight" ID="RadDropDownTree1" runat="server"
                        DefaultMessage="Select category" ExpandNodeOnSingleClick="true" AutoPostBack="true" EnableFiltering="true">
                        <DropDownSettings OpenDropDownOnLoad="false" CloseDropDownOnSelection="true" />
                    </telerik:RadDropDownTree>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Category is required!" ControlToValidate="RadDropDownTree1" CssClass="validator">*</asp:RequiredFieldValidator>
                    <telerik:RadTextBox RenderMode="Lightweight" ID="NewCategoryTextBox" CssClass="form-control" placeholder="New Category name" runat="server" Text='<%# Bind("Category.Name") %>' WrapperCssClass="rdfInput" Visible="false"></telerik:RadTextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="New category name min len is 5 characters!" ControlToValidate="NewCategoryTextBox" ValidationExpression="^[\s\S]{5,}$">*</asp:RegularExpressionValidator>
                    <asp:Button runat="server" Text="New Category" ID="BtnNewCategory" OnClick="BtnNewCategory_Click" UseSubmitBehavior="false" CausesValidation="false" CssClass="btn btn-success" />
                    <asp:Button runat="server" Text="Cancel" ID="BtnCancelNewCategory" OnClick="BtnCancelNewCategory_Click" Visible="false" UseSubmitBehavior="false" CausesValidation="false" CssClass="btn btn-danger" />
                </div>
                <div class="rdfRow row input-group">
                    <telerik:RadAutoCompleteBox RenderMode="Lightweight" ID="RadAutoCompleteBox1" EmptyMessage="Add recipe tags"
                        InputType="Token" runat="server" AllowCustomEntry="true" >
                    </telerik:RadAutoCompleteBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Tags are required!" ControlToValidate="RadAutoCompleteBox1" CssClass="validator">*</asp:RequiredFieldValidator>
                </div>
                <asp:Panel runat="server" ID="PanelAdminOptions" Visible='<%# User.IsInRole("admin") %>' CssClass="rdfRow row input-group admin-row">
                    <telerik:RadComboBox runat="server" ID="RadComboBox1" RenderMode="Lightweight" EmptyMessage="Choose another author" ></telerik:RadComboBox>
                    <telerik:RadCheckBox ID="RadCheckBox1" runat="server" Text="Recipe is Approved?" Checked='<%# Bind("IsApproved") %>'></telerik:RadCheckBox>
                </asp:Panel>
                <div class="rdfCommandButtons">
                    <hr class="divider" />
                    <div class="col-xs-4 col-sm-4 col-md-4 ">
                        <telerik:RadButton RenderMode="Lightweight" ID="CancelButton" runat="server" CssClass="btn btn-primary btn-warning form-control" ButtonType="SkinnedButton" CausesValidation="False" CommandName="Cancel" OnClick="CancelButton_Click" Text="Cancel" ToolTip="Cancel" />
                    </div>
                    <div class="col-xs-4 col-sm-4 col-md-4 reg-btn">
                        <telerik:RadButton RenderMode="Lightweight" ID="PerformInsertButton" CssClass="btn btn-primary btn-success form-control" runat="server" ButtonType="SkinnedButton" CommandName="PerformInsert" Text="Add" ToolTip="Insert" />
                    </div>
                </div>
            </fieldset>
        </InsertItemTemplate>
        <EmptyDataTemplate>
            <div class="RadDataForm RadDataForm_<%# Container.Skin %>">
                <div class="rdfEmpty">
                    There are no recipes to be displayed.
                </div>
            </div>
        </EmptyDataTemplate>
    </telerik:RadDataForm>
</asp:Content>
