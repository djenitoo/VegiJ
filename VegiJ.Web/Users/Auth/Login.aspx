<%@ Page Language="C#" Title="Log In" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="VegiJ.Web.Users.Auth.Login" %>

<asp:Content runat="server" ID="head" ContentPlaceHolderID="head">
    <link href="../../Content/login-styles.css" type="text/css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="loginForm" class=" form-signin">
        <div class="form-container form-group">
            <h2 class="form-signin-heading">Please sing in</h2>
            <h5>
                <asp:ValidationSummary ID="LogInValidationSummary" runat="server" ForeColor="#CC0000" HeaderText="The following errors occured:" />
            </h5>
            <div class="input-group">
                <span class="input-group-addon glyphicon glyphicon-user" id="user-gly"></span>
                <asp:Label ID="LblUsername" CssClass="sr-only" runat="server" Text="Username"></asp:Label>
                <asp:TextBox ID="TxtboxUsername" CssClass="form-control" aria-describedby="user-gly" placeholder="Username" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredUsernameValidator" CssClass="validator" runat="server" ErrorMessage="Username is required." ControlToValidate="TxtboxUsername" Font-Size="Large">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="UsernameLengthValidator" CssClass="validator" runat="server" ErrorMessage="Username length should be between 5 and 20 characters!" Text="*" ControlToValidate="TxtBoxUsername" ValidationExpression="^[\s\S]{4,20}$"></asp:RegularExpressionValidator>
            </div>
            <div class="input-group">
                <span class="input-group-addon glyphicon glyphicon-pencil" id="psw-gly"></span>
                <asp:Label ID="LblPassword" CssClass="sr-only" runat="server" Text="Password"></asp:Label>
                <asp:TextBox ID="TxtBoxPassword" CssClass="form-control" placeholder="Password" aria-describedby="psw-gly" runat="server" TextMode="Password" ToolTip="Password should not contain spaces, and should be at leat 6 characters."></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredPasswordValidator" CssClass="validator" runat="server" ControlToValidate="TxtBoxPassword" ErrorMessage="Password is required." Font-Size="Large">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="PasswordLengthValidator" CssClass="validator" runat="server" ErrorMessage="Password length should be between 6 and 20 characters and should not contain spaces!" Text="*" ControlToValidate="TxtBoxPassword" ValidationExpression="^[\S]{6,20}$"></asp:RegularExpressionValidator>
            </div>
            <div class="checkbox">
                <asp:CheckBox ID="CheckBoxRememberMe" runat="server" />&nbsp;<asp:Label ID="LblRememberMe" runat="server" Text="Remember me?" AssociatedControlID="CheckBoxRememberMe"></asp:Label>
            </div>
            <div class="row form-footer" role="group">
                <div class="col-xs-4 col-sm-4 col-md-4">
                    <asp:Button ID="BtnReset" CssClass="btn btn-primary btn-warning form-control" runat="server" Text="Reset" OnClick="BtnReset_Click" />

                </div>
                <div class="col-xs-4 col-sm-4 col-md-4 reg-btn">
                    <asp:Button ID="BtnSubmit" CssClass="btn btn-primary btn-success form-control" runat="server" Text="Log In" OnClick="BtnSubmit_Click" />
                </div>
            </div>
        </div>
        <h6>Not a member?
            <asp:HyperLink ID="LinkRegister" runat="server" NavigateUrl="Register.aspx">Register here.</asp:HyperLink>
        </h6>
    </div>
</asp:Content>
