<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Budget_ProjectTasks.aspx.cs" Inherits="CTMS.Budget_ProjectTasks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        function pageLoad() {
            $(".Datatable1").dataTable({ "bSort": false, "ordering": false,
                "bDestroy": true, stateSave: true
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Manage Project Tasks
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
                <table>
                    <tr>
                        <td class="label">
                            Select Department:
                        </td>
                        <td class="requiredSign">
                            <asp:Label ID="lbl_Section" runat="server" Text="*"></asp:Label>
                        </td>
                        <td class="Control">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddl_Dept" runat="server" Width="250px" class="form-control drpControl"
                                        ValidationGroup="View" AutoPostBack="true" OnSelectedIndexChanged="ddl_Dept_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="style10">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Select Task:
                        </td>
                        <td class="requiredSign">
                            <asp:Label ID="lbl_SubSection" runat="server" Text="*"></asp:Label>
                        </td>
                        <td class="Control">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddl_Task" runat="server" Width="250px" class="form-control drpControl"
                                        ValidationGroup="View" AutoPostBack="true" OnSelectedIndexChanged="ddl_Task_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="style10">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvTasks" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CssClass="table table-bordered table-striped Datatable1" OnPreRender="gvTasks_PreRender"
                    OnRowDataBound="gvTasks_RowDataBound">
                    <Columns>
                        <asp:TemplateField Visible="false" HeaderText="ID">
                            <ItemTemplate>
                                <asp:Label ID="lbl_TaskId" runat="server" Text='<%# Bind("Task_Id") %>'></asp:Label>
                                <asp:Label ID="lbl_SubTaskId" runat="server" Text='<%# Bind("Sub_Task_Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tasks" ItemStyle-Width="100%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_TaskName" Width="100%" ToolTip='<%# Bind("Sub_Task_Name") %>'
                                    CssClass="label" runat="server" Text='<%# Bind("Sub_Task_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                            <HeaderTemplate>
                                <asp:Button ID="Btn_Add_Fun" runat="server" OnClick="Btn_Add_Fun_Click" Text="Add"
                                    CssClass="btn btn-primary btn-sm cls-btnSave" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                            <HeaderTemplate>
                                <asp:Button ID="Btn_Remove_Fun" runat="server" OnClick="Btn_Remove_Fun_Click" Text="Remove"
                                    CssClass="btn btn-primary btn-sm cls-btnSave" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="Chk_Sel_Remove_Fun" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
