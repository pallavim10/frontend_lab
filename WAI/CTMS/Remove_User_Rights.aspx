<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Remove_User_Rights.aspx.cs" Inherits="BZ_eCRF.Remove_User_Rights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
            Remove User Rights
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
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="Drp_Project_Name" runat="server" class="form-control drpControl required"
                                    AutoPostBack="True" OnSelectedIndexChanged="Drp_Project_Name_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="label">
                        Select User Group:
                    </td>
                    <td class="requiredSign">
                        <asp:Label ID="Label3" runat="server" Text="*"></asp:Label>
                    </td>
                    <td class="Control">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="Drp_User_Group" runat="server" AutoPostBack="True" class="form-control drpControl required"
                                    OnSelectedIndexChanged="Drp_User_Group_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="label">
                        Select User:
                    </td>
                    <td class="requiredSign">
                        <asp:Label ID="Label4" runat="server" Text="*"></asp:Label>
                    </td>
                    <td class="Control">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="Drp_User" runat="server" class="form-control drpControl required">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="label">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                            OnClick="btn_Get_Doc_List_Click" Text="Get Functions" />
                    </td>
                    <td class="style9">
                    </td>
                </tr>
                <tr>
                    <td class="style12">
                        &nbsp;
                    </td>
                    <td class="style14">
                        &nbsp;
                    </td>
                    <td class="style5">
                        &nbsp;
                    </td>
                    <td class="style11">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CssClass="table table-bordered table-striped Datatable" AlternatingRowStyle-CssClass="alt"
                PagerStyle-CssClass="pgr" OnPreRender="grd_data_PreRender">
                <Columns>
                    <asp:TemplateField HeaderText="Function ID" ItemStyle-CssClass="width20px">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_Fun_ID" runat="server" Text='<%# Bind("FunctionID") %>' Enabled="False"
                                Height="16px" Width="48px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Parent Function" ItemStyle-CssClass="width20px">
                        <ItemTemplate>
                            <asp:Label ID="txt_Parent" runat="server" Text='<%# Bind("Parent") %>' BorderStyle="None"
                                Enabled="False" Font-Overline="False" Height="16px" Width="147px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Function Name" ItemStyle-CssClass="width20px">
                        <ItemTemplate>
                            <asp:Label ID="txt_Fun_Name" runat="server" Text='<%# Bind("FunctionName") %>' Enabled="False"
                                Font-Bold="True" Font-Overline="False" Height="16px" Width="178px"></asp:Label>
                            &nbsp;
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="width20px">
                        <HeaderTemplate>
                            <asp:Button ID="Btn_Add_Fun" runat="server" CssClass="btn btn-primary btn-sm" OnClick="Btn_Add_Fun_Click"
                                Text="Delete" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
