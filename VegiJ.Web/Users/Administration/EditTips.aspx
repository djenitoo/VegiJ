﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditTips.aspx.cs" Inherits="VegiJ.Web.Users.Administration.EditTips" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        @media (max-height: 768px) {
            .navbar-nav {
                float: left;
                margin: 0;
            }
            .navbar-right {
                float: right !important;
                margin: 0;
                margin-right: 15px;
                
            }
        }
        #Table3 td input, #Table3 td textarea, #Table3 td > div {
            width: 50% !important;
            margin: 5px 10px;
        }
        #Table3 td textarea {
            min-height: 7em;
        }
        #Table3 td > div {
            max-width: 40%;
        }
        .RadGrid [type="button"] {
            max-width: 150px;
            border-radius: 4px !important;
        }
        #Table3 td input.riTextBox {
            width: 200px !important;
            margin: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="edit-tips-container">
        <telerik:RadFormDecorator RenderMode="Lightweight" ID="RadFormDecorator1" runat="server"
            DecorationZoneID="demo" DecoratedControls="All" EnableRoundedCorners="true" />
        <div id="demo" class="demo-container no-bg">
            <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid1" runat="server" CssClass="RadGrid" GridLines="None"
                AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="False"
                ShowStatusBar="true" OnDeleteCommand="RadGrid1_DeleteCommand" OnUpdateCommand="RadGrid1_UpdateCommand"
                OnInsertCommand="RadGrid1_InsertCommand" OnItemCommand="RadGrid1_ItemCommand"
                OnPreRender="RadGrid1_PreRender" OnNeedDataSource="RadGrid1_NeedDataSource" OnItemDataBound="RadGrid1_ItemDataBound">
                <MasterTableView CommandItemDisplay="TopAndBottom"
                    DataKeyNames="ID,AuthorID">
                    <Columns>
                        <telerik:GridEditCommandColumn>
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn UniqueName="ID" HeaderText="ID" DataField="ID" Visible="false">
                            <HeaderStyle Width="70px"></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Title" HeaderText="Title" DataField="Title">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Content" HeaderText="Content" DataField="Content">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Author" HeaderText="Author" DataField="Author.UserName">
                        </telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn UniqueName="IsApproved" HeaderText="Is Approved" DataField="IsApproved">
                        </telerik:GridCheckBoxColumn>
                        <telerik:GridHyperLinkColumn DataTextFormatString="View autor" DataNavigateUrlFields="Author.Username" DataTextField="Author.Username"
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
                                        <b>TIp Details</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table id="Table3" width="800px" border="0" class="module">
                                            <tr>
                                                <td class="title" style="font-weight: bold;" colspan="2">Recipe Info:</td>
                                            </tr>
                                            <tr>
                                                <td>Title:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("Title") %>'>
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox7" ErrorMessage="Title is required!" CssClass="validator">*</asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox7" ErrorMessage="Minimum length of title is 8 characters!" CssClass="validator" ValidationExpression="^[\s\S]{8,}$">*</asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Content:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("Content") %>' TabIndex="2" TextMode="MultiLine">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox9" ErrorMessage="Content of recipe is required!" CssClass="validator">*</asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBox9" ErrorMessage="Minimum length of content is 50 characters and maximum 500!" CssClass="validator" ValidationExpression="^[\s\S]{50,500}$">*</asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Approved:
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("IsApproved") %>' TabIndex="3"></asp:CheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Author:</td>
                                                <td>
                                                    <telerik:RadComboBox runat="server" ID="RadComboBox1" RenderMode="Lightweight" EmptyMessage="Choose another author"></telerik:RadComboBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
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
