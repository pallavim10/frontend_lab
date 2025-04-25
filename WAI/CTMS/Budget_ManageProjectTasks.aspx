<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Budget_ManageProjectTasks.aspx.cs" Inherits="CTMS.Budget_ManageProjectTasks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        function pageLoad() {
            $(".Datatable1").dataTable({ "bSort": false, "ordering": false,
                "bDestroy": true, stateSave: true
            });
        }

        function Print() {
            if ($("#<%=ddl_Dept.ClientID%>").val() == "0") {
                $("#<%=ddl_Dept.ClientID%>").addClass("brd-1px-redimp");
                return false;
            }

            if ($("#<%=ddl_Task.ClientID%>").val() == "0") {
                $("#<%=ddl_Task.ClientID%>").addClass("brd-1px-redimp");
                return false;
            }

            var ProjectId = '<%= Session["PROJECTID"] %>'
            var Task_Id = $("#<%=ddl_Task.ClientID%>").val();
            var test = "ReportBudget_ManageProjectTasks.aspx?ProjectId=" + ProjectId + "&Task_Id=" + Task_Id;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500px,width=1000px";
            window.open(test, '_blank', strWinProperty);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Manage Tasks
                                        <asp:LinkButton ID="lbtnprint" runat="server" Text="Print" OnClientClick="return Print()" CssClass="btn-sm">
      <span class="glyphicon glyphicon-print"></span>Print</asp:LinkButton>
            </h3>
        </div>
        <table>
            <tr>
                <td class="label">
                    Select Department:
                </td>
                <td class="requiredSign">
                    <asp:Label ID="Label1" runat="server" Text="*"></asp:Label>
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
        </table>
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gvTasks" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        CssClass="table table-bordered table-striped" OnRowDataBound="gvTasks_RowDataBound">
                        <Columns>
                            <asp:TemplateField Visible="false" HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_TaskId" runat="server" Text='<%# Bind("Task_Id") %>'></asp:Label>
                                    <asp:Label ID="lbl_SubTaskId" runat="server" Text='<%# Bind("Sub_Task_Id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sub-Tasks" ItemStyle-Width="100%">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_TaskName" Width="100%" ToolTip='<%# Bind("Sub_Task_Name") %>'
                                        CssClass="label" runat="server" Text='<%# Bind("Sub_Task_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="width30px txtCenter" ControlStyle-CssClass="txt_center"
                                HeaderText="Sponsor">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSponsor" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="width30px txtCenter" ControlStyle-CssClass="txt_center"
                                HeaderText="DiagnoSearch">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelf" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="width30px txtCenter" ControlStyle-CssClass="txt_center"
                                HeaderText="Site">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSite" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="width30px txtCenter" ControlStyle-CssClass="txt_center"
                                HeaderText="Others">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkOthers" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <br />
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <asp:LinkButton runat="server" ID="btnSubmit" Text="Submit" ForeColor="White" Style="margin-left: 120px"
                        CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnSubmit_Click"></asp:LinkButton>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <br />
    </div>
</asp:Content>
