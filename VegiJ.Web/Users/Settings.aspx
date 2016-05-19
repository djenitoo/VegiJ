<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="Settings.aspx.cs" Inherits="VegiJ.Web.Users.Settings" %>

<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/settings-styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel runat="server" CssClass="settings-container">
        <asp:Panel ID="PanelSettings2" runat="server" CssClass="form-container form-group">
            <asp:FormView ID="userSettings2" runat="server" DefaultMode="Edit"
                SelectMethod="GetUser" UpdateMethod="userSettings2_UpdateItem" ItemType="VegiJ.DataAccess.User" 
                DataKeyNames="ID" RenderOuterTable="False" EnableModelValidation="false">
                <HeaderTemplate>
                    <h2 class="form-signin-heading">Edit account</h2>
                    <asp:ValidationSummary ID="RegValidationSummary" runat="server"
                        ForeColor="#CC0000" DisplayMode="BulletList" HeaderText="The following errors occured:"
                        Font-Size="Medium" />
                    <div class="row">
                        <div class="input-group">
                            <span id="user-gly" aria-hidden="true" class="input-group-addon fa fa-user"></span>
                            <asp:Label ID="LblUsername" runat="server" CssClass="sr-only"
                                Text="Username"></asp:Label>
                            <asp:TextBox ID="TxtboxUsername" runat="server" aria-describedby="user-gly" CssClass="form-control" Text="<%#: Item.UserName %>" Enabled="False"></asp:TextBox>
                        </div>
                    </div>
                </HeaderTemplate>
                <EditItemTemplate runat="server">
                    <div class="row">
                        <div class="col-xs-6 col-sm-6 col-md-6">
                            <div class="input-group">
                                <asp:Label ID="LblFirstName" runat="server" CssClass="sr-only" Text="First Name"></asp:Label>
                                <asp:TextBox ID="TxtboxFirstName" runat="server"
                                    CssClass="form-control" placeholder="First Name" Text="<%#: BindItem.FirstName %>"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="FirstNameLenValidator"
                                    runat="server" ControlToValidate="TxtboxFirstName" CssClass="validator" ErrorMessage="First name length should be maximum 15 characters!"
                                    Text="*" ValidationExpression="^[\s\S]{0,35}$"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-xs-6 col-sm-6 col-md-6">
                            <div class="input-group">
                                <asp:Label ID="LblLastName" runat="server" CssClass="sr-only"
                                    Text="Last Name"></asp:Label>
                                <asp:TextBox ID="TxtboxLastName" runat="server"
                                    CssClass="form-control" placeholder="Last Name" Text="<%#: BindItem.LastName %>"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="LastNameLenValidator"
                                    runat="server" ControlToValidate="TxtboxLastName" CssClass="validator" ErrorMessage="Last name length should be maximum 15 characters!"
                                    Text="*" ValidationExpression="^[\s\S]{0,35}$"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-group">
                            <span id="email-gly" aria-hidden="true" class="input-group-addon fa fa-at"></span>
                            <asp:Label ID="LblEmail" runat="server" CssClass="sr-only"
                                Text="Email"></asp:Label>
                            <asp:TextBox ID="TxtboxEmail" runat="server" aria-describedby="email-gly" CssClass="form-control" placeholder="Email"
                                TextMode="Email" Text="<%#: BindItem.Email %>"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredEmailValidator"
                                runat="server" ControlToValidate="TxtboxEmail" CssClass="validator" ErrorMessage="Email is required."
                                Font-Size="Large">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="EmailRegExValidator"
                                runat="server" ControlToValidate="TxtboxEmail" CssClass="validator" ErrorMessage="Invalid email!"
                                Font-Size="Large" Text="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-6 col-sm-6 col-md-6">
                            <div class="input-group">
                                <span id="calendar-gly" aria-hidden="true" class="input-group-addon fa fa-birthday-cake"></span>
                                <asp:Label ID="LabelBirthday" runat="server" CssClass="sr-only"
                                    Text="Birthdate"></asp:Label>
                                <telerik:RadDatePicker runat="server"
                                    CssClass="input-col-6-gly" placeholder="Birth date"
                                    ID="RadDatePicker2" AutoPostBack="true"
                                    RenderMode="Lightweight" Width="100%" MinDate="<%# DateTime.Today.AddYears(-80) %>"
                                    MaxDate="<%# DateTime.Today.AddYears(-10) %>"
                                    Culture='<%# new CultureInfo("bg-BG") %>'
                                    DbSelectedDate='<%# Bind("BirthDate") %>' >
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidatorBirthDate" CssClass="validator" runat="server"
                                    ErrorMessage="Birth date is required!"
                                    ControlToValidate="RadDatePicker2" Font-Size="Large"
                                    Text="*"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-xs-6 col-sm-6 col-md-6">
                            <div class="input-group">
                                <span id="gender-fa" aria-hidden="true" class="input-group-addon fa fa-transgender-alt"></span>
                                <asp:Label ID="LabelGender" runat="server" CssClass="sr-only"
                                    Text="<%#: BindItem.Gender.ID %>"></asp:Label>
                                <asp:DropDownList ID="DropDownGender" runat="server" AutoPostBack="true"
                                    CssClass="form-control form-control-datepicker input-col-6-gly"
                                    name="datepicker-input" AppendDataBoundItems="true" >
                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-6 col-sm-6 col-md-6">
                            <div class="input-group">
                                <asp:Label ID="LblPassword" runat="server" CssClass="sr-only"
                                    Text="Password"></asp:Label>
                                <asp:TextBox ID="TxtboxPassword" runat="server" aria-describedby="LblPassword" CssClass="form-control" placeholder="New password" TextMode="Password"
                                    ToolTip="Password should not contain spaces, and should be at leat 6 characters."></asp:TextBox>
                                <asp:RegularExpressionValidator ID="PasswordRegValidator"
                                    runat="server" ControlToValidate="TxtBoxPassword" CssClass="validator"
                                    ErrorMessage="Password length should be between 6 and 20 characters!"
                                    Text="*" ValidationExpression="^[\S]{6,20}$"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-xs-6 col-sm-6 col-md-6">
                            <div class="input-group">
                                <asp:Label ID="LblConfirmPassword" runat="server"
                                    CssClass="sr-only" Text="Confirm password"></asp:Label>
                                <asp:TextBox ID="TxtboxConfirmPassword"  aria-describedby="LblConfirmPassword" CssClass="form-control" placeholder="Confirm Password"
                                    TextMode="Password" ToolTip="Password should not contain spaces, and should be at leat 6 characters." runat="server" ></asp:TextBox>
                                <asp:CompareValidator ID="PasswordCompareValidator"
                                    runat="server" ControlToCompare="TxtboxPassword" ControlToValidate="TxtboxConfirmPassword"
                                    CssClass="validator" ErrorMessage="Passwords are not equal!" Font-Size="Large"
                                    Text="*"></asp:CompareValidator>
                            </div>
                        </div>
                    </div>
                </EditItemTemplate>
                <FooterTemplate>
                    <div class="row form-footer" role="group">
                    <div class="col-xs-4 col-sm-4 col-md-4">
                        <asp:Button ID="BtnCancel" runat="server" CausesValidation="False" OnCommand="BtnCancel_Command"
                            CssClass="btn btn-primary btn-warning form-control" Text="Cancel" UseSubmitBehavior="False" CommandName="Cancel" />
                    </div>
                    <div class="col-xs-4 col-sm-4 col-md-4 reg-btn">
                        <asp:Button ID="BtnSave" runat="server" CssClass="btn btn-primary btn-success form-control"
                            Text="Save" UseSubmitBehavior="False" CommandName="Update" />
                    </div>
                </div>
                </FooterTemplate>                
            </asp:FormView>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
