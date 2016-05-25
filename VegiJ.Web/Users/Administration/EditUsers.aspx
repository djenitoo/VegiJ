<%@ Page Title="Edit All Users" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditUsers.aspx.cs" Inherits="VegiJ.Web.Users.Administration.EditUsers" %>

<%@ Import Namespace="System.Globalization" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function RowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="edit-users-container">
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="divMsgs"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadFormDecorator RenderMode="Lightweight" ID="RadFormDecorator1" runat="server"
            DecorationZoneID="demo" DecoratedControls="All" EnableRoundedCorners="true" />
        <div id="demo" class="demo-container no-bg">
            <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid1" runat="server" CssClass="RadGrid" GridLines="None"
                AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="False"
                ShowStatusBar="true" OnDeleteCommand="RadGrid1_DeleteCommand" OnUpdateCommand="RadGrid1_UpdateCommand"
                OnInsertCommand="RadGrid1_InsertCommand" OnItemCommand="RadGrid1_ItemCommand"
                OnPreRender="RadGrid1_PreRender" OnNeedDataSource="RadGrid1_NeedDataSource" OnItemDataBound="RadGrid1_ItemDataBound">
                <MasterTableView CommandItemDisplay="TopAndBottom"
                    DataKeyNames="ID,UserName,FirstName,LastName,BirthDate,IsAdmin,Email,GenderID">
                    <Columns>
                        <telerik:GridEditCommandColumn>
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn UniqueName="ID" HeaderText="ID" DataField="ID" Visible="false">
                            <HeaderStyle Width="70px"></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="UserName" HeaderText="Username" DataField="UserName">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="FirstName" HeaderText="First Name" DataField="FirstName">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="LastName" HeaderText="Last Name" DataField="LastName">
                        </telerik:GridBoundColumn>
                        <telerik:GridDateTimeColumn
                            UniqueName="BirthDate" HeaderText="Birth Date" DataField="BirthDate"
                            DataFormatString="{0:dd-MM-yyyy}">
                        </telerik:GridDateTimeColumn>
                        <telerik:GridCheckBoxColumn UniqueName="IsAdmin" HeaderText="Admin" DataField="IsAdmin">
                        </telerik:GridCheckBoxColumn>
                        <telerik:GridBoundColumn UniqueName="Email" HeaderText="Email" DataField="Email">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Gender" HeaderText="Gender" DataField="Gender.Name">
                        </telerik:GridBoundColumn>
                        <telerik:GridHyperLinkColumn DataTextFormatString="View profile" DataNavigateUrlFields="UserName" DataTextField="UserName"
                            UniqueName="ProfileLink" Target="_blank">
                        </telerik:GridHyperLinkColumn>
                        <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="column" ItemStyle-ForeColor="#ff0000">
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings EditFormType="Template">
                        <FormTemplate>
                            <table id="Table2" cellspacing="2" cellpadding="1" width="100%" border="0" rules="none"
                                style="border-collapse: collapse;">
                                <tr class="EditFormHeader">
                                    <td colspan="2">
                                        <b>User Details</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table id="Table3" width="450px" border="0" class="module">
                                            <tr>
                                                <td class="title" style="font-weight: bold;" colspan="2">User Info:</td>
                                            </tr>
                                            <tr>
                                                <td>Username:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("Username") %>'>
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Email:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("Email") %>' TabIndex="2">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Admin:
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("IsAdmin") %>' TabIndex="3"></asp:CheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>New Password:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox5" runat="server" TextMode="Password" TabIndex="3">
                                                    </asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="PasswordRegValidator"
                                                        runat="server" ControlToValidate="TextBox5" CssClass="validator"
                                                        ErrorMessage="Password length should be between 6 and 20 characters!"
                                                        Text="*" ValidationExpression="^[\S]{6,20}$"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Confirm Password:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox8" runat="server" TextMode="Password" TabIndex="4">
                                                    </asp:TextBox>
                                                    <asp:CompareValidator ID="PasswordsCompValidator" runat="server"
                                                        ControlToCompare="TextBox5" ControlToValidate="TextBox8"
                                                        ErrorMessage="Passwords do not match!">
                                                    </asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <b>Personal Info:</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Gender
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlTOC" runat="server" DataSource='<%# this.genderRepository.Table.ToList() %>'
                                                        DataTextField="Name" DataValueField="ID" AutoPostBack="true" Width="100%"
                                                        TabIndex="5" AppendDataBoundItems="True">
                                                        <asp:ListItem Text="Select" Value="">
                                                        </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>First Name:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox2" Text='<%# Bind("FirstName") %>' runat="server" TabIndex="6">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Last Name:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox3" Text='<%# Bind("LastName") %>' runat="server" TabIndex="7">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Birth Date:
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker RenderMode="Lightweight" ID="BirthDatePicker" runat="server" AutoPostBack="true"
                                                        MaxDate="<%# DateTime.Today.AddYears(-10) %>" Culture='<%# new CultureInfo("bg-BG") %>'
                                                        MinDate="<%# DateTime.Today.AddYears(-80) %>" DbSelectedDate='<%# Bind("BirthDate") %>'
                                                        TabIndex="8">
                                                    </telerik:RadDatePicker>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                            runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>&nbsp;
                                    <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                        CommandName="Cancel"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </FormTemplate>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings>
                <ClientEvents OnRowDblClick="RowDblClick"></ClientEvents>
            </ClientSettings>
            </telerik:RadGrid>
        </div>
    </div>
</asp:Content>
