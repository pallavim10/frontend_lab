<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CTMS_BudgetiTOM.aspx.cs" Inherits="CTMS.CTMS_BudgetiTOM" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);

            if (div.style.display == "none") {
                div.style.display = "inline";
                document.getElementById('img' + divname).className = 'icon-minus-sign-alt';

            } else {
                div.style.display = "none";
                document.getElementById('img' + divname).className = 'icon-plus-sign-alt';
            }
        }

        function ManipulateAll(ID) {
            var img = document.getElementById('img' + ID);

            if (img.className == 'icon-plus-sign-alt') {
                img.className = 'icon-minus-sign-alt'
                $("div[id*='" + ID + "']").css("display", "inline");
                $("i[id*='" + ID + "']").removeClass('icon-plus-sign-alt');
                $("i[id*='" + ID + "']").addClass('icon-minus-sign-alt');
            } else {
                img.className = 'icon-plus-sign-alt'
                $("div[id*='" + ID + "']").css("display", "none");
                $("i[id*='" + ID + "']").removeClass('icon-minus-sign-alt');
                $("i[id*='" + ID + "']").addClass('icon-plus-sign-alt');
            }
        }

        function Print() {

            var ProjectId = '<%= Session["PROJECTID"] %>'
            var Action = "get_TOM_ProjectTaks_Data";
            var test = "ReportBudgetTOM.aspx?ProjectId=" + ProjectId + "&Action=" + Action;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500px,width=1500px";
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
                Task Ownership Matrix
                <asp:LinkButton ID="lbtnprint" runat="server" Text="Print" OnClientClick="return Print()"
                    CssClass="btn-sm">
      <span class="glyphicon glyphicon-print">Print</span></asp:LinkButton>
            </h3>
        </div>
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
        </div>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvDept" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CssClass="table table-bordered table-striped" OnRowDataBound="gvDept_RowDataBound">
                    <Columns>
                        <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                            HeaderStyle-CssClass="txt_center">
                            <HeaderTemplate>
                                <a href="JavaScript:ManipulateAll('_Dept');" id="_Folder" style="color: #333333"><i
                                    id="img_Dept" class="icon-plus-sign-alt"></i></a>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div runat="server" id="anchor">
                                    <a href="JavaScript:divexpandcollapse('_Dept<%# Eval("Dept_ID") %>');" style="color: #333333">
                                        <i id="img_Dept<%# Eval("Dept_ID") %>" class="icon-plus-sign-alt"></i></a>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false" HeaderText="ID">
                            <ItemTemplate>
                                <asp:Label ID="lbl_DeptID" runat="server" Text='<%# Bind("Dept_ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Departments" ItemStyle-Width="100%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Dept" Width="100%" ToolTip='<%# Bind("Dept_Name") %>' CssClass="label"
                                    runat="server" Text='<%# Bind("Dept_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <tr>
                                    <td colspan="100%" style="padding: 2px;">
                                        <div style="float: right; font-size: 13px;">
                                        </div>
                                        <div>
                                            <div class="rows">
                                                <div class="col-md-12">
                                                    <div id="_Dept<%# Eval("Dept_ID") %>" style="display: none; position: relative; overflow: auto;">
                                                        <asp:GridView ID="gvMain" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                            CssClass="table table-bordered table-striped" OnRowDataBound="gvMain_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                                                    HeaderStyle-CssClass="txt_center">
                                                                    <HeaderTemplate>
                                                                        <a href="JavaScript:ManipulateAll('_mydiv');" id="_Folder" style="color: #333333"><i
                                                                            id="img_mydiv" class="icon-plus-sign-alt"></i></a>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <div runat="server" id="anchor">
                                                                            <a href="JavaScript:divexpandcollapse('_mydiv<%# Eval("Task_Id") %>');" style="color: #333333">
                                                                                <i id="img_mydiv<%# Eval("Task_Id") %>" class="icon-plus-sign-alt"></i></a>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false" HeaderText="ID">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_TaskId" runat="server" Text='<%# Bind("Task_Id") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tasks" ItemStyle-Width="100%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_TaskName" Width="100%" ToolTip='<%# Bind("Task_Name") %>' CssClass="label"
                                                                            runat="server" Text='<%# Bind("Task_Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td colspan="100%" style="padding: 2px;">
                                                                                <div style="float: right; font-size: 13px;">
                                                                                </div>
                                                                                <div>
                                                                                    <div class="rows">
                                                                                        <div class="col-md-12">
                                                                                            <div id="_mydiv<%# Eval("Task_Id") %>" style="display: none; position: relative;
                                                                                                overflow: auto;">
                                                                                                <asp:GridView ID="gvTasks" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                                                    CssClass="ChildGrid table table-bordered table-striped" OnRowCommand="gvTasks_RowCommand"
                                                                                                    OnRowDataBound="gvTasks_RowDataBound">
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                                                                                            HeaderStyle-CssClass="txt_center">
                                                                                                            <HeaderTemplate>
                                                                                                                <a href="JavaScript:ManipulateAll('_SubTask');" id="_Folder" style="color: #333333">
                                                                                                                    <i id="img_SubTask" class="icon-plus-sign-alt"></i></a>
                                                                                                            </HeaderTemplate>
                                                                                                            <ItemTemplate>
                                                                                                                <div runat="server" id="anchor">
                                                                                                                    <a href="JavaScript:divexpandcollapse('_SubTask<%# Eval("ID") %>');" style="color: #333333">
                                                                                                                        <i id="img_SubTask<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                                                                                                                </div>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField Visible="false" HeaderText="ID">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lbl_Id" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
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
                                                                                                            HeaderText="Training">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:CheckBox runat="server" ID="chkTrain" />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter" ControlStyle-CssClass="txt_center"
                                                                                                            HeaderText="Sponsor">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label runat="server" ID="lblSponsorCheck" Visible="false"><i class="fa fa-check-square-o"></i></asp:Label>
                                                                                                                <asp:Label runat="server" ID="lblSponsorUnCheck" Visible="false"><i class="fa fa-square-o"></i></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter" ControlStyle-CssClass="txt_center"
                                                                                                            HeaderText="DiagnoSearch">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label runat="server" ID="lblDSCheck" Visible="false"><i class="fa fa-check-square-o"></i></asp:Label>
                                                                                                                <asp:Label runat="server" ID="lblDSUnCheck" Visible="false"><i class="fa fa-square-o"></i></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter" ControlStyle-CssClass="txt_center"
                                                                                                            HeaderText="Site">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label runat="server" ID="lblSiteCheck" Visible="false"><i class="fa fa-check-square-o"></i></asp:Label>
                                                                                                                <asp:Label runat="server" ID="lblSiteUnCheck" Visible="false"><i class="fa fa-square-o"></i></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter" ControlStyle-CssClass="txt_center"
                                                                                                            HeaderText="Others">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label runat="server" ID="lblOthersCheck" Visible="false"><i class="fa fa-check-square-o"></i></asp:Label>
                                                                                                                <asp:Label runat="server" ID="lblOthersUnCheck" Visible="false"><i class="fa fa-square-o"></i></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField>
                                                                                                            <ItemTemplate>
                                                                                                                <tr>
                                                                                                                    <td colspan="100%" style="padding: 2px;">
                                                                                                                        <div style="float: right; font-size: 13px;">
                                                                                                                        </div>
                                                                                                                        <div>
                                                                                                                            <div class="rows">
                                                                                                                                <div class="col-md-12">
                                                                                                                                    <div id="_SubTask<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                                                                                                                                        <asp:GridView ID="gvRoles" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid table table-bordered table-striped">
                                                                                                                                            <Columns>
                                                                                                                                                <asp:TemplateField Visible="false" HeaderText="ID">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:Label ID="lbl_TaskId" runat="server" Text='<%# Bind("Task_Id") %>'></asp:Label>
                                                                                                                                                        <asp:Label ID="lbl_SubTaskId" runat="server" Text='<%# Bind("Sub_Task_Id") %>'></asp:Label>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField HeaderText="Roles" ItemStyle-Width="60%">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:Label ID="lbl_Role" Width="100%" ToolTip='<%# Bind("Role") %>' CssClass="label"
                                                                                                                                                            runat="server" Text='<%# Bind("Role") %>'></asp:Label>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField HeaderText="Hours" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:Label ID="lbl_Hours" Width="100%" ToolTip='<%# Bind("Hrs") %>' CssClass="label"
                                                                                                                                                            runat="server" Text='<%# Bind("Hrs") %>'></asp:Label>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                            </Columns>
                                                                                                                                            <RowStyle ForeColor="Blue" />
                                                                                                                                            <HeaderStyle ForeColor="Blue" />
                                                                                                                                        </asp:GridView>
                                                                                                                                    </div>
                                                                                                                                </div>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <RowStyle ForeColor="Maroon" />
                                                            <HeaderStyle ForeColor="Maroon" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div class="txt_center">
                    <br />
                    <asp:Button ID="btnsubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                        OnClick="btnsubmit_Click" />
                    <br />
                    <br />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
