<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Edit_User_Group_Details.aspx.cs" Inherits="PPT.Edit_User_Group_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Edit User Group Details</h3>
        </div>
        <!-- /.box-header -->
        <!-- text input -->
        <div class="box-body">
            <div class="row">
                <div class="lblError">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
            </div>
            <table>
                <tr>
                    <td class="label">
                        Select Project:
                    </td>
                    <td class="requiredSign">
                        <asp:Label ID="Label2" runat="server" Text="*"></asp:Label>
                    </td>
                    <td class="Control">
                        <asp:UpdatePanel ID="Upd_Pan_Sel_Dept" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="Drp_Project_Name" runat="server" AutoPostBack="True" class="form-control drpControl required"
                                    OnSelectedIndexChanged="Drp_Project_Name_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="style10">
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Select User Group:
                    </td>
                    <td class="requiredSign">
                        <asp:Label ID="Label1" runat="server" Text="*"></asp:Label>
                    </td>
                    <td class="Control">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="Drp_User_Group" runat="server" AutoPostBack="True" class="form-control drpControl required"
                                    OnSelectedIndexChanged="Drp_User_Group_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="style10">
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Enter User Group Name:
                    </td>
                    <td class="requiredSign">
                        <asp:Label ID="Label3" runat="server" Text="*"></asp:Label>
                    </td>
                    <td class="Control">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_User_Group" runat="server" class="form-control required"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="style10">
                    </td>
                </tr>
                <tr>
                    <td class="label">
                    </td>
                    <td class="requiredSign">
                    </td>
                    <td class="Control">
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label" colspan="5">
                        <asp:Button ID="Btn_Update_UG" runat="server" Text="Update" CssClass="btn btn-primary btn-sm cls-btnSave"
                            OnClick="Btn_Update_UG_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
