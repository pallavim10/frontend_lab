<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Remove_User_Group_Rights.aspx.cs" Inherits="PPT.Remove_User_Group_Rights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
            Remove User Group Rights
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
                <td class="label">
                    Select Project:
                </td>
                <td class="requiredSign">
                    <asp:Label ID="Lbl_Sel_Dept" runat="server" Text="*"></asp:Label>
                </td>
                <td class="Control">
                    <asp:DropDownList ID="Drp_Project_Name" runat="server" class="form-control drpControl required"
                        OnSelectedIndexChanged="Drp_Project_Name_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    Select User Group:
                </td>
                <td class="requiredSign">
                    <asp:Label ID="Lbl_Sel_UserGroup" runat="server" Text="*"></asp:Label>
                </td>
                <td class="Control">
                    <asp:DropDownList ID="Drp_User_Group" runat="server" class="form-control drpControl">
                    </asp:DropDownList>
                </td>
                <td class="label">
                </td>
                <td class="requiredSign">
                </td>
                <td class="label" colspan="5">
                    <asp:Button ID="Btn_Get_Fun" runat="server" OnClick="Btn_Get_Fun_Click" Text="Get Functions"
                        CssClass="btn btn-primary btn-sm cls-btnSave" />
                </td>
                <td>
                    &nbsp;
                </td>
                </tr>
                <tr>
                    <td class="label" colspan="5">
                </tr>
            </table>
            <%--<table>
                <tr>
                    <td class="label">
                        Select Project:
                    </td>
                    <td class="requiredSign">
                        <asp:Label ID="Label2" runat="server" Text="*"></asp:Label>
                    </td>
                    <td class="Control">
                       
                    </td>
                    <td class="label">
                        Select User Group:
                    </td>
                    <td class="requiredSign">
                        <asp:Label ID="Label1" runat="server" Text="*"></asp:Label>
                    </td>
                    <td class="Control">
                       
                        <asp:CompareValidator ID="Comp_Val_Sel_UG" runat="server" ControlToValidate="Drp_User_Group"
                            ErrorMessage="Required" Font-Size="X-Small" ForeColor="#FF3300" Operator="NotEqual"
                            ValueToCompare="0"></asp:CompareValidator>
                    </td>
                    <td class="label">
                        
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        &nbsp;
                    </td>
                    <td class="style12">
                        &nbsp;
                    </td>
                    <td class="style6">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>--%>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable txt_center"
                OnPreRender="GridView1_PreRender" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                <Columns>
                    <asp:TemplateField HeaderText="Function ID" ItemStyle-CssClass="width20px">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_Fun_ID" runat="server" Text='<%# Bind("FunctionID") %>' Enabled="False"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Parent Function" ItemStyle-CssClass="width30px">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_Parent" runat="server" Text='<%# Bind("Parent") %>' BorderStyle="None"
                                Enabled="False" CssClass="form-control txt_center" Visible="false"></asp:TextBox>
                            <asp:Label ID="lblSerious" runat="server" Text='<%# Eval("Parent") %>' CssClass=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Function Name" ItemStyle-CssClass="width30px">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_Fun_Name" runat="server" Text='<%# Bind("FunctionName") %>'
                                BorderStyle="None" Visible="false" CssClass="form-control txt_center" Enabled="False"
                                Font-Bold="True"></asp:TextBox>
                            <asp:Label ID="FunctionName" runat="server" Text='<%# Eval("FunctionName") %>' CssClass=""></asp:Label>
                            &nbsp;
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="width20px">
                        <HeaderTemplate>
                            <asp:Button ID="Btn_Delete" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                Text="Delete" OnClick="Btn_Delete_Click" />
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
