<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Budget_TaskMaster.aspx.cs"
    MasterPageFile="~/Site.Master" Inherits="CTMS.Budget_TaskMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <style>
        .select2-container .select2-selection--multiple
        {
            min-height: 20px;
        }
    </style>
    <script type="text/javascript">

        function OpenManageDocs(element) {
            var Task_ID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var Sub_Task_ID = $(element).closest('tr').find('td:eq(1)').find('span').html();
            var test = "Budget_ManageDocs.aspx?Task_ID=" + Task_ID + "&Sub_Task_ID=" + Sub_Task_ID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=530,width=1000";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;
        }

        function pageLoad() {
            $('.select').select2();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Manage Masters
            </h3>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-5">
                                    <div class="box box-primary" style="min-height: 300px;">
                                        <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                            <h4 class="box-title" align="left">
                                                Add Department
                                            </h4>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="col-md-4">
                                                                <label>
                                                                    Enter Department Name:</label>
                                                            </div>
                                                            <div class="col-md-1 requiredSign">
                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDept" ID="reqDept"
                                                                    ValidationGroup="Dept" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-md-7">
                                                                <asp:TextBox Style="width: 200px;" ID="txtDept" ValidationGroup="Dept" runat="server"
                                                                    CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12" style="margin-top: 5px;">
                                                            <div class="col-md-4">
                                                                <label>
                                                                    Site:</label>
                                                            </div>
                                                            <div class="col-md-1 requiredSign">
                                                                &nbsp;
                                                            </div>
                                                            <div class="col-md-7">
                                                                <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkSite" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="col-md-6">
                                                                &nbsp;
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:Button ID="btnsubmitDept" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                                    ValidationGroup="Dept" OnClick="btnsubmitDept_Click" />
                                                                <asp:Button ID="btnupdateDept" Text="Update" runat="server" CssClass="btn btn-primary btn-sm"
                                                                    ValidationGroup="Dept" OnClick="btnupdateDept_Click" />
                                                                <asp:Button ID="btncancelDept" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                                    OnClick="btncancelDept_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-7">
                                    <div class="box box-primary">
                                        <div class="box-header with-border" style="float: left;">
                                            <h4 class="box-title" align="left">
                                                Records
                                            </h4>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div>
                                                    <div class="rows">
                                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                                            <div>
                                                                <asp:GridView ID="gvDept" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="gvDept_RowCommand"
                                                                    OnRowDataBound="gvDept_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Department Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDept" runat="server" Text='<%# Bind("Dept_Name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Site" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblSiteCheck" Visible="false"><i class="fa fa-check-square-o"></i></asp:Label>
                                                                                <asp:Label runat="server" ID="lblSiteUnCheck" Visible="false"><i class="fa fa-square-o"></i></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnupdateDept" runat="server" CommandArgument='<%# Bind("Dept_ID") %>'
                                                                                    CommandName="Edit1" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                                                                <asp:LinkButton ID="lbtndeleteDept" runat="server" CommandArgument='<%# Bind("Dept_ID") %>'
                                                                                    CommandName="Delete1" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-5">
                                    <div class="box box-primary" style="min-height: 300px;">
                                        <div class="box-header with-border" style="float: left;">
                                            <h4 class="box-title" align="left">
                                                Add Tasks
                                            </h4>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Select Department:</label>
                                                        </div>
                                                        <div class="col-md-1 requiredSign">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDept"
                                                                ValidationGroup="Task" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:DropDownList Style="width: 200px;" ID="ddlDept" runat="server" AutoPostBack="true"
                                                                class="form-control drpControl" ValidationGroup="Task" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12" style="margin-top: 5px;">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Enter Task Name:</label>
                                                        </div>
                                                        <div class="col-md-1 requiredSign">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTask"
                                                                ValidationGroup="Task" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:TextBox ID="txtTask" Style="width: 200px;" ValidationGroup="Task" runat="server"
                                                                Text="" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <br />
                                                    <div class="col-md-12">
                                                        <div class="col-md-6">
                                                            &nbsp;
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Button ID="btnsubmitTask" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                                ValidationGroup="Task" OnClick="btnsubmitTask_Click" />
                                                            <asp:Button ID="btnupdateTask" Text="Update" runat="server" CssClass="btn btn-primary btn-sm"
                                                                ValidationGroup="Task" OnClick="btnupdateTask_Click" />
                                                            <asp:Button ID="btncancelTask" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                                OnClick="btncancelTask_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-7">
                                    <div class="box box-primary">
                                        <div class="box-header with-border" style="float: left;">
                                            <h4 class="box-title" align="left">
                                                Records
                                            </h4>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div>
                                                    <div class="rows">
                                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                                            <div>
                                                                <asp:GridView ID="gvTask" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="gvTask_RowCommand"
                                                                    OnRowDataBound="gvTask_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Task">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTask" runat="server" Text='<%# Bind("Task_Name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnupdateTask" runat="server" CommandArgument='<%# Bind("Task_ID") %>'
                                                                                    CommandName="Edit1" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                                                                <asp:LinkButton ID="lbtndeleteTask" runat="server" CommandArgument='<%# Bind("Task_ID") %>'
                                                                                    CommandName="Delete1" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-5">
                                    <div class="box box-primary" style="min-height: 300px;">
                                        <div class="box-header with-border" style="float: left;">
                                            <h4 class="box-title" align="left">
                                                Add Sub-Tasks
                                            </h4>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Select Task:</label>
                                                        </div>
                                                        <div class="col-md-1 requiredSign">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTask"
                                                                ValidationGroup="Task" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:DropDownList Style="width: 200px;" ID="ddlTask" runat="server" AutoPostBack="true"
                                                                class="form-control drpControl" OnSelectedIndexChanged="ddlTask_SelectedIndexChanged"
                                                                ValidationGroup="SubTask">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12" style="margin-top: 5px;">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Enter Sub-Task Name:</label>
                                                        </div>
                                                        <div class="col-md-1 requiredSign">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSubTask"
                                                                ValidationGroup="SubTask" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:TextBox ID="txtSubTask" Style="width: 200px;" ValidationGroup="SubTask" runat="server"
                                                                Text="" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkMilestone" />&nbsp;&nbsp;
                                                            <label>
                                                                Milestone</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkTimeline" />&nbsp;&nbsp;
                                                            <label>
                                                                Timeline</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkMultiple" />&nbsp;&nbsp;
                                                            <label>
                                                                Multiple</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkrecurring" />&nbsp;&nbsp;
                                                            <label>
                                                                Recurring</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkBudget" />&nbsp;&nbsp;
                                                            <label>
                                                                Budget</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkPassthrough" />&nbsp;&nbsp;
                                                            <label>
                                                                Passthrough</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkTOM" />&nbsp;&nbsp;
                                                            <label>
                                                                TOM</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkiTOM" />&nbsp;&nbsp;
                                                            <label>
                                                                iTOM</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkDownloadable" />&nbsp;&nbsp;
                                                            <label>
                                                                Downloadable</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkDocument" />&nbsp;&nbsp;
                                                            <label>
                                                                Document</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkEvent" />&nbsp;&nbsp;
                                                            <label>
                                                                Event</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12" style="margin-top: 5px;">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Enter Sequence Number:</label>
                                                        </div>
                                                        <div class="col-md-1 requiredSign">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSeq"
                                                                ValidationGroup="SubTask" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:TextBox Width="50px" ID="txtSeq" ValidationGroup="SubTask" runat="server" CssClass="form-control"
                                                                MaxLength="4"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <br />
                                                    <div class="col-md-12">
                                                        <div class="col-md-6">
                                                            &nbsp;
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Button ID="btnsubmitSubTask" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                                ValidationGroup="SubTask" OnClick="btnsubmitSubTask_Click" />
                                                            <asp:Button ID="btnupdateSubTask" Text="Update" runat="server" CssClass="btn btn-primary btn-sm"
                                                                ValidationGroup="SubTask" OnClick="btnupdateSubTask_Click" />
                                                            <asp:Button ID="btncancelSubTask" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                                OnClick="btncancelSubTask_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-7">
                                    <div class="box box-primary">
                                        <div class="box-header with-border" style="float: left;">
                                            <h4 class="box-title" align="left">
                                                Records
                                            </h4>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div>
                                                    <div class="rows">
                                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                                            <div>
                                                                <asp:GridView ID="gvSubTask" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="gvSubTask_RowCommand"
                                                                    OnRowDataBound="gvSubTask_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="task id" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbltaskID" runat="server" Text='<%# Bind("Task_ID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Subtask id" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSub_Task_ID" runat="server" Text='<%# Bind("Sub_Task_ID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Seq. No." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSEQNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Sub-Tasks" ItemStyle-Width="55%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSubTask" runat="server" Text='<%# Bind("Sub_Task_Name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <div>
                                                                                    <asp:Label runat="server" ID="lblMilestoneCheck" Visible="false"><i class="fa fa-check-square-o"></i></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblMilestoneUnCheck" Visible="false"><i class="fa fa-square-o"></i></asp:Label>
                                                                                    &nbsp;Milestone
                                                                                </div>
                                                                                <div>
                                                                                    <asp:Label runat="server" ID="lblTimelineCheck" Visible="false"><i class="fa fa-check-square-o"></i></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblTimelineUnCheck" Visible="false"><i class="fa fa-square-o"></i></asp:Label>
                                                                                    &nbsp;Timeline
                                                                                </div>
                                                                                <div>
                                                                                    <asp:Label runat="server" ID="lblMultipleCheck" Visible="false"><i class="fa fa-check-square-o"></i></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblMultipleUnCheck" Visible="false"><i class="fa fa-square-o"></i></asp:Label>
                                                                                    &nbsp;Multiple
                                                                                </div>
                                                                                <div>
                                                                                    <asp:Label runat="server" ID="lblRecurringCheck" Visible="false"><i class="fa fa-check-square-o"></i></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblRecurringUncheck" Visible="false"><i class="fa fa-square-o"></i></asp:Label>
                                                                                    &nbsp;Recurring
                                                                                </div>
                                                                                <div>
                                                                                    <asp:Label runat="server" ID="lblTOMCheck" Visible="false"><i class="fa fa-check-square-o"></i></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblTOMUnCheck" Visible="false"><i class="fa fa-square-o"></i></asp:Label>
                                                                                    &nbsp;TOM
                                                                                </div>
                                                                                <div>
                                                                                    <asp:Label runat="server" ID="lblEventCheck" Visible="false"><i class="fa fa-check-square-o"></i></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblEventUnCheck" Visible="false"><i class="fa fa-square-o"></i></asp:Label>
                                                                                    &nbsp;Event
                                                                                </div>
                                                                                <div>
                                                                                    <asp:Label runat="server" ID="lbliTOMCheck" Visible="false"><i class="fa fa-check-square-o"></i></asp:Label>
                                                                                    <asp:Label runat="server" ID="lbliTOMUnCheck" Visible="false"><i class="fa fa-square-o"></i></asp:Label>
                                                                                    &nbsp;iTOM
                                                                                </div>
                                                                                <div>
                                                                                    <asp:Label runat="server" ID="lblDownloadCheck" Visible="false"><i class="fa fa-check-square-o"></i></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblDownloadUnCheck" Visible="false"><i class="fa fa-square-o"></i></asp:Label>
                                                                                    &nbsp;Downloadable
                                                                                </div>
                                                                                <div>
                                                                                    <asp:Label runat="server" ID="lblDocumentCheck" Visible="false"><i class="fa fa-check-square-o"></i></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblDocumentUnCheck" Visible="false"><i class="fa fa-square-o"></i></asp:Label>
                                                                                    &nbsp;Document
                                                                                </div>
                                                                                <div>
                                                                                    <asp:Label runat="server" ID="lblBudgetCheck" Visible="false"><i class="fa fa-check-square-o"></i></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblBudgetUnCheck" Visible="false"><i class="fa fa-square-o"></i></asp:Label>
                                                                                    &nbsp;Budget
                                                                                </div>
                                                                                <div>
                                                                                    <asp:Label runat="server" ID="lblPassThroughCheck" Visible="false"><i class="fa fa-check-square-o"></i></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblPassThroughUnCheck" Visible="false"><i class="fa fa-square-o"></i></asp:Label>
                                                                                    &nbsp;PassThrough
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnupdatesubcate" runat="server" CommandArgument='<%# Bind("Sub_Task_ID") %>'
                                                                                    CommandName="Edit1" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                                                                <asp:LinkButton ID="lbtnManageDocs" runat="server" CommandArgument='<%# Bind("Sub_Task_ID") %>'
                                                                                    CommandName="Docs" ToolTip="Manage Documents" OnClientClick="return OpenManageDocs(this);"><i class="fa fa-files-o"></i></asp:LinkButton>&nbsp;&nbsp;
                                                                                <asp:LinkButton ID="lbtndeleteCheckList" runat="server" CommandArgument='<%# Bind("Sub_Task_ID") %>'
                                                                                    CommandName="Delete1" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="display: none;">
                            <div class="col-md-12">
                                <div class="col-md-5">
                                    <div class="box box-primary" style="min-height: 300px;">
                                        <div class="box-header with-border" style="float: left;">
                                            <h4 class="box-title" align="left">
                                                Add Groups
                                            </h4>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Group Name:</label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:TextBox Style="width: 200px;" ID="txtGrp" ValidationGroup="AddGrp" runat="server"
                                                                CssClass="form-control required"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:Button ID="btnAddGrp" Text="Add" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave "
                                                                ValidationGroup="AddGrp" OnClick="btnAddGrp_Click" />
                                                            <asp:Button ID="btnUpdateGrp" Text="Update" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                                ValidationGroup="AddGrp" OnClick="btnUpdateGrp_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div style="width: 100%; height: 264px; overflow: auto;">
                                                        <div>
                                                            <asp:GridView ID="gvNewSubTask" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                                <Columns>
                                                                    <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sub-Task Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSubTaskID" runat="server" Visible="false" Text='<%# Bind("Sub_Task_ID") %>'></asp:Label>
                                                                            <asp:Label ID="lblSubTask" runat="server" Text='<%# Bind("Sub_Task_Name") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="box-body">
                                        <div style="min-height: 300px;">
                                            <div class="row txtCenter">
                                                <asp:LinkButton ID="lbtnAddToGrp" ForeColor="White" Text="Add" runat="server" CssClass="btn btn-primary btn-sm"
                                                    ValidationGroup="Grp" OnClick="lbtnAddToGrp_Click" />
                                            </div>
                                            <div class="row txtCenter">
                                                &nbsp;
                                            </div>
                                            <div class="row txtCenter">
                                                <asp:LinkButton ID="lbtnRemoveFromGrp" ForeColor="White" Text="Remove" runat="server"
                                                    ValidationGroup="Grp" CssClass="btn btn-primary btn-sm" OnClick="lbtnRemoveFromGrp_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="box box-primary" style="min-height: 300px;">
                                        <div class="box-header with-border" style="float: left;">
                                            <h4 class="box-title" align="left">
                                                Manage Groups
                                            </h4>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Select Group:</label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:DropDownList Style="width: 200px;" ID="ddlGroup" runat="server" AutoPostBack="true"
                                                                ValidationGroup="Grp" class="form-control drpControl" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfv_Addto" runat="server" ControlToValidate="ddlGroup"
                                                                ValidationGroup="Grp" InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:LinkButton ID="lbtnUpdateGrp" ValidationGroup="Grp" runat="server" ToolTip="Edit"
                                                                OnClick="lbtnUpdateGrp_Click"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                                            <asp:LinkButton ID="lbtnDeleteGrp" ValidationGroup="Grp" runat="server" ToolTip="Delete"
                                                                OnClick="lbtnDeleteGrp_Click"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div style="width: 100%; height: 264px; overflow: auto;">
                                                        <div>
                                                            <asp:GridView ID="gvAddedTasks" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                                <Columns>
                                                                    <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sub-Task Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSubTaskID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                                            <asp:Label ID="lblSubTask" runat="server" Text='<%# Bind("Sub_Task_Name") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
