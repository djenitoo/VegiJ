<%@ Page Title="Register" Language="C#" ValidateRequest="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="VegiJ.Web.Users.Auth.Register" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Import Namespace="System.Activities.Expressions" %>
<%@ Import Namespace="System.Globalization" %>

<asp:Content runat="server" ID="head" ContentPlaceHolderID="head">
    <link href="/Content/login-styles.css" type="text/css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div id="registerForm" class="form-group form-reg">
        <asp:Panel ID="PanelRegister" runat="server" CssClass="form-container form-group">
            <div>
                <img src="/images/VegiJ-logo-with-space-50x92.png" runat="server" class="logo-reg-form" alt="vegi j logo" />
            </div>
            <h2 class="form-signin-heading">Create your new account</h2>
            <asp:ValidationSummary ID="RegValidationSummary" runat="server" ForeColor="#CC0000" DisplayMode="BulletList" HeaderText="The following errors occured:" Font-Size="Medium" />
            <div class="row">
                <div class="input-group">
                    <span id="user-gly" aria-hidden="true" class="input-group-addon fa fa-user"></span>
                    <asp:Label ID="LblUsername" runat="server" CssClass="sr-only" Text="Username"></asp:Label>
                    <asp:TextBox ID="TxtboxUsername" runat="server" aria-describedby="user-gly" CssClass="form-control" placeholder="Username"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredUsernameValidator" runat="server" ControlToValidate="TxtboxUsername" CssClass="validator" ErrorMessage="Username is required." Font-Size="Large">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="UsernameLengthValidator" runat="server" ControlToValidate="TxtBoxUsername" CssClass="validator" ErrorMessage="Username length should be between 5 and 20 characters!" Text="*" ValidationExpression="^[\s\S]{4,20}$"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6 col-sm-6 col-md-6">
                    <div class="input-group">
                        <asp:Label ID="LblFirstName" runat="server" CssClass="sr-only" Text="First Name"></asp:Label>
                        <asp:TextBox ID="TxtboxFirstName" runat="server" CssClass="form-control" placeholder="First Name"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="FirstNameLenValidator" runat="server" ControlToValidate="TxtboxFirstName" CssClass="validator" ErrorMessage="First name length should be maximum 15 characters!" Text="*" ValidationExpression="^[\s\S]{0,35}$"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-xs-6 col-sm-6 col-md-6">
                    <div class="input-group">
                        <asp:Label ID="LblLastName" runat="server" CssClass="sr-only" Text="Last Name"></asp:Label>
                        <asp:TextBox ID="TxtboxLastName" runat="server" CssClass="form-control" placeholder="Last Name"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="LastNameLenValidator" runat="server" ControlToValidate="TxtboxLastName" CssClass="validator" ErrorMessage="Last name length should be maximum 15 characters!" Text="*" ValidationExpression="^[\s\S]{0,35}$"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="input-group">
                    <span id="email-gly" aria-hidden="true" class="input-group-addon fa fa-at"></span>
                    <asp:Label ID="LblEmail" runat="server" CssClass="sr-only" Text="Email"></asp:Label>
                    <asp:TextBox ID="TxtboxEmail" runat="server" aria-describedby="email-gly" CssClass="form-control" placeholder="Email" TextMode="Email"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredEmailValidator" runat="server" ControlToValidate="TxtboxEmail" CssClass="validator" ErrorMessage="Email is required." Font-Size="Large">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="EmailRegExValidator" runat="server" ControlToValidate="TxtboxEmail" CssClass="validator" ErrorMessage="Invalid email!" Font-Size="Large" Text="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6 col-sm-6 col-md-6">
                    <div class="input-group">
                        <span id="calendar-gly" aria-hidden="true" class="input-group-addon fa fa-birthday-cake"></span>
                        <asp:Label ID="LabelBirthday" runat="server" CssClass="sr-only" Text="Birthdate"></asp:Label>
                        <%--<asp:TextBox ID="TextBoxBirthDay" runat="server" CssClass="form-control form-control-datepicker input-col-6-gly" placeholder="Birth date" TextMode="SingleLine"></asp:TextBox>--%>
                        <telerik:RadDatePicker runat="server" 
                            CssClass="input-col-6-gly" placeholder="Birth date" ID="RadDatePicker" 
                            RenderMode="Lightweight" Width="100%" MinDate="<%# DateTime.Today.AddYears(-80) %>"
                            MaxDate="<%# DateTime.Today.AddYears(-10)%>"
                            Culture='<%# new CultureInfo("bg-BG") %>' >
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorBirthDate" CssClass="validator" runat="server" ErrorMessage="Birth date is required!" ControlToValidate="RadDatePicker" Font-Size="Large" Text="*"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-xs-6 col-sm-6 col-md-6">
                    <div class="input-group">
                        <span id="gender-fa" aria-hidden="true" class="input-group-addon fa fa-transgender-alt"></span>
                        <asp:Label ID="LabelGender" runat="server" CssClass="sr-only" Text="Gender"></asp:Label>
                        <asp:DropDownList ID="DropDownGender" runat="server" CssClass="form-control form-control-datepicker input-col-6-gly" name="datepicker-input">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6 col-sm-6 col-md-6">
                    <div class="input-group">
                        <asp:Label ID="LblPassword" runat="server" CssClass="sr-only" Text="Password"></asp:Label>
                        <asp:TextBox ID="TxtboxPassword" runat="server" aria-describedby="LblPassword" CssClass="form-control" placeholder="Password" TextMode="Password" ToolTip="Password should not contain spaces, and should be at leat 6 characters."></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredPasswordValidator" runat="server" ControlToValidate="TxtBoxPassword" CssClass="validator" ErrorMessage="Password is required." Font-Size="Large">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="PasswordRegValidator" runat="server" ControlToValidate="TxtBoxPassword" CssClass="validator" ErrorMessage="Password length should be between 6 and 20 characters!" Text="*" ValidationExpression="^[\S]{6,20}$"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-xs-6 col-sm-6 col-md-6">
                    <div class="input-group">
                        <asp:Label ID="LblConfirmPassword" runat="server" CssClass="sr-only" Text="Confirm password"></asp:Label>
                        <asp:TextBox ID="TxtboxConfirmPassword" runat="server" aria-describedby="LblConfirmPassword" CssClass="form-control" placeholder="Confirm Password" TextMode="Password" ToolTip="Password should not contain spaces, and should be at leat 6 characters."></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredConfPasswrdValidator" runat="server" ControlToValidate="TxtboxConfirmPassword" CssClass="validator" ErrorMessage="Confirming password is required." Font-Size="Large" Text="*"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="PasswordCompareValidator" runat="server" ControlToCompare="TxtboxPassword" ControlToValidate="TxtboxConfirmPassword" CssClass="validator" ErrorMessage="Passwords are not equal!" Font-Size="Large" Text="*"></asp:CompareValidator>
                    </div>
                </div>
            </div>
            <div class="row form-footer" role="group">
                <div class="col-xs-4 col-sm-4 col-md-4">
                    <asp:Button ID="BtnReset" runat="server" CausesValidation="False" CssClass="btn btn-primary btn-warning form-control" OnClick="BtnReset_Click" Text="Reset" UseSubmitBehavior="False" />
                </div>
                <div class="col-xs-4 col-sm-4 col-md-4 reg-btn">
                    <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-primary btn-success form-control" OnClick="BtnSubmit_Click" Text="Register" UseSubmitBehavior="True" />
                </div>
            </div>
            <div class="row footer-text">
                <h6>Already a member?
                        <asp:HyperLink ID="HyperLinkLogin" runat="server" NavigateUrl="Login.aspx">Log in.</asp:HyperLink>
                </h6>
            </div>

        </asp:Panel>
        <asp:Panel ID="PanelSuccessfulRegister" runat="server" Visible="False">
            <div class="form-container">
                <h4>Your registration was successful!</h4>
                <h5>Continue to login page?</h5>
                <div class="row form-footer">
                    <div class="col-xs-4 col-sm-4 col-md-4 reg-btn reg-btn-successful">
                        <asp:Button ID="BtnContinueToLogIn" CssClass="btn btn-primary btn-success form-control" runat="server" Text="Log In" PostBackUrl="Login.aspx" />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>

</asp:Content>
