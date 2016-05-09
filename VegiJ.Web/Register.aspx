<%@ Page Title="Register" Language="C#" ValidateRequest="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="VegiJ.Web.Register" %>


<asp:Content runat="server" ID="head" ContentPlaceHolderID="head">
    <link href="Content/login-styles.css" type="text/css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="registerForm" class="form-group form-reg">
        <asp:Panel ID="PanelRegister" runat="server" CssClass="form-container form-group">
            <h2 class="form-signin-heading">Create your new account</h2>
            <h5>
                <asp:ValidationSummary ID="RegValidationSummary" runat="server" ForeColor="#CC0000" DisplayMode="BulletList" HeaderText="The following errors occured:" />
            </h5>
            <div class="row">
                <div class="input-group">
                    <span class="input-group-addon glyphicon glyphicon-user" id="user-gly"></span>
                    <asp:Label ID="LblUsername" CssClass="sr-only" runat="server" Text="Username"></asp:Label>
                    <asp:TextBox ID="TxtboxUsername" CssClass="form-control" aria-describedby="user-gly" placeholder="Username" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredUsernameValidator" CssClass="validator" runat="server" ErrorMessage="Username is required." ControlToValidate="TxtboxUsername" Font-Size="Large">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="UsernameLengthValidator" CssClass="validator" runat="server" ErrorMessage="Username length should be between 5 and 20 characters!" Text="*" ControlToValidate="TxtBoxUsername" ValidationExpression="^[\s\S]{5,20}$"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6 col-sm-6 col-md-6">
                    <div class="input-group">
                        <asp:Label ID="LblFirstName" CssClass="sr-only" runat="server" Text="First Name"></asp:Label>
                        <asp:TextBox ID="TxtboxFirstName" CssClass="form-control" placeholder="First Name" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="FirstNameLenValidator" CssClass="validator" runat="server" ErrorMessage="First name length should be maximum 15 characters!" Text="*" ControlToValidate="TxtboxFirstName" ValidationExpression="^[\s\S]{0,35}$"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-xs-6 col-sm-6 col-md-6">
                    <div class="input-group">
                        <asp:Label ID="LblLastName" CssClass="sr-only" runat="server" Text="Last Name"></asp:Label>
                        <asp:TextBox ID="TxtboxLastName" CssClass="form-control" placeholder="Last Name" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="LastNameLenValidator" CssClass="validator" runat="server" ErrorMessage="Last name length should be maximum 15 characters!" Text="*" ControlToValidate="TxtboxLastName" ValidationExpression="^[\s\S]{0,35}$"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="input-group">
                    <span class="input-group-addon glyphicon glyphicon-envelope" id="email-gly"></span>
                    <asp:Label ID="LblEmail" CssClass="sr-only" runat="server" Text="Email"></asp:Label>
                    <asp:TextBox ID="TxtboxEmail" CssClass="form-control" aria-describedby="email-gly" placeholder="Email" runat="server" TextMode="Email"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredEmailValidator" CssClass="validator" runat="server" ErrorMessage="Email is required." ControlToValidate="TxtboxEmail" Font-Size="Large">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="EmailRegExValidator" CssClass="validator" runat="server" ErrorMessage="Invalid email!" Text="*" ControlToValidate="TxtboxEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Font-Size="Large"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6 col-sm-6 col-md-6">
                    <div class="input-group">
                        <asp:Label ID="LblPassword" CssClass="sr-only" runat="server" Text="Password"></asp:Label>
                        <asp:TextBox ID="TxtboxPassword" CssClass="form-control" placeholder="Password" aria-describedby="LblPassword" runat="server" TextMode="Password" ToolTip="Password should not contain spaces, and should be at leat 6 characters."></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredPasswordValidator" CssClass="validator" runat="server" ControlToValidate="TxtBoxPassword" ErrorMessage="Password is required." Font-Size="Large">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="PasswordRegValidator" CssClass="validator" runat="server" ErrorMessage="Password length should be between 6 and 20 characters!" Text="*" ControlToValidate="TxtBoxPassword" ValidationExpression="^[\S]{6,20}$"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-xs-6 col-sm-6 col-md-6">
                    <div class="input-group">
                        <asp:Label ID="LblConfirmPassword" CssClass="sr-only" runat="server" Text="Confirm password"></asp:Label>
                        <asp:TextBox ID="TxtboxConfirmPassword" CssClass="form-control" placeholder="Confirm Password" aria-describedby="LblConfirmPassword" runat="server" TextMode="Password" ToolTip="Password should not contain spaces, and should be at leat 6 characters."></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredConfPasswrdValidator" CssClass="validator" runat="server" ControlToValidate="TxtboxConfirmPassword" ErrorMessage="Confirming password is required." Text="*" Font-Size="Large"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="PasswordCompareValidator" runat="server" ErrorMessage="Passwords are not equal!" ControlToCompare="TxtboxPassword" ControlToValidate="TxtboxConfirmPassword" Text="*" Font-Size="Large" CssClass="validator"></asp:CompareValidator>
                    </div>
                </div>
            </div>
            <div class="row form-footer" role="group">
                <div class="col-xs-4 col-sm-4 col-md-4">
                    <asp:Button ID="BtnReset" CssClass="btn btn-primary btn-warning form-control" runat="server" Text="Reset" CausesValidation="False" OnClick="BtnReset_Click" />
                </div>
                <div class="col-xs-4 col-sm-4 col-md-4 reg-btn">
                    <asp:Button ID="BtnSubmit" CssClass="btn btn-primary btn-success form-control" runat="server" Text="Register" OnClick="BtnSubmit_Click" />
                </div>
            </div>
            <div class="row footer-text">
                <h6>Already a member?
                    <asp:HyperLink ID="HyperLinkLogin" runat="server" NavigateUrl="~/Login.aspx">Log in.</asp:HyperLink></h6>
            </div>
        </asp:Panel>
        <asp:Panel ID="PanelSuccessfulRegister" runat="server" Visible="False">
            <div class="form-container">
                <h4>Your registration was successful!</h4>
                <h5>Continue to login page?</h5>
                <div class="row form-footer">
                    <div class="col-xs-4 col-sm-4 col-md-4 reg-btn">
                        <asp:Button ID="BtnContinueToLogIn" CssClass="btn btn-primary btn-success form-control" runat="server" Text="Log In" PostBackUrl="~/Login.aspx" />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
