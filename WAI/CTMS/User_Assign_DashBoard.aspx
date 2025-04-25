<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="User_Assign_DashBoard.aspx.cs" Inherits="CTMS.User_Assign_DashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Add User DashBoard</h3>
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
                        <asp:Label ID="Lbl_Dept" runat="server" Text="*"></asp:Label>
                    </td>
                    <td class="Control">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="Drp_Project" runat="server" class="form-control drpControl required"
                                    AutoPostBack="True" OnSelectedIndexChanged="Drp_Project_Name_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="label">
                        Select User Group:
                    </td>
                    <td class="requiredSign">
                        <asp:Label ID="Label1" runat="server" Text="*"></asp:Label>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="Upd_Pan_Sel_UG" runat="server">
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
                        <asp:Label ID="Label2" runat="server" Text="*"></asp:Label>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="Upd_Pan_Sel_User" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="Drp_User" runat="server" class="form-control drpControl required">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Select Type:
                    </td>
                    <td class="requiredSign">
                        <asp:Label ID="Label3" runat="server" Text="*"></asp:Label>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="Drp_Type" runat="server" class="form-control drpControl required">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="label">
                    </td>
                    <td class="label">
                        <asp:Button ID="btn_Get_DashBoard" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                            Text="Get DashBoard" OnClick="btn_Get_DashBoard_Click" />
                    </td>
                </tr>
            </table>
            <div class="">
                <div class="margin-top6" style="display: inline-flex;" id="divselectchk" runat="server"
                    visible="false">
                    <label class="label">
                        Select All
                    </label>
                    <div class="Control">
                        <asp:CheckBox ID="Chk_Select_All" runat="server" AutoPostBack="True" OnCheckedChanged="Chk_Select_All_CheckedChanged"
                            Style="font-size: x-small" />
                    </div>
                </div>
                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CssClass="table table-bordered table-striped txt_center Datatable" OnPreRender="GridView1_PreRender">
                    <Columns>
                        <asp:TemplateField HeaderText="DASHBOARD ID" ItemStyle-Width="3%" HeaderStyle-Width="3%">
                            <ItemTemplate>
                                <asp:TextBox ID="txtdashId" runat="server" Text='<%# Bind("ID") %>' Enabled="False"
                                    CssClass="form-control  txt_center" BorderStyle="None"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CHART NAME" ItemStyle-CssClass="width20px">
                            <ItemTemplate>
                                <asp:TextBox ID="txtchartname" runat="server" Text='<%# Bind("Chart_Name") %>' Visible="false"
                                    Enabled="False" CssClass="form-control  txt_center" BorderStyle="None"></asp:TextBox>
                                <asp:Label ID="lblChartName" runat="server" Text='<%# Eval("Chart_Name") %>' CssClass=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CHART TYPE" ItemStyle-Width="20%" HeaderStyle-Width="20%">
                            <ItemTemplate>
                                <asp:TextBox ID="txtChartType" runat="server" Text='<%# Bind("Chart_Type") %>' Enabled="False"
                                    Font-Bold="True" Visible="false" CssClass="form-control  txt_center" BorderStyle="None"></asp:TextBox>
                                <asp:Label ID="lblChartType" runat="server" Text='<%# Eval("Chart_Type") %>' CssClass=""></asp:Label>
                                &nbsp;
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="8%" HeaderStyle-Width="8%">
                            <HeaderTemplate>
                                <asp:Button ID="Btn_Add_Dash" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                    OnClick="Btn_Add_Fun_Click" Text="Add" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
