<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Budget_OtherDocuments.aspx.cs" Inherits="CTMS.Budget_OtherDocuments" %>

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

    </script>
    <script type="text/javascript">
        function UploadDoc(element) {

            var row = element.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;

            var TaskID = $(element).closest('tr').find('td:eq(0)').find('input').val();
            var SubTaskID = $(element).closest('tr').find('td:eq(1)').find('input').val();

            var test = "CTMS_UploadDoc.aspx?TaskID=" + TaskID + "&SubTaskID=" + SubTaskID + "&INVID=0";

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=275,width=450";
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
                Other Documents Upload</h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
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
                                                <asp:GridView ID="gvMainTask" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                    CssClass="table table-bordered table-striped" OnRowDataBound="gvMainTask_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                                            HeaderStyle-CssClass="txt_center">
                                                            <HeaderTemplate>
                                                                <a href="JavaScript:ManipulateAll('_Task');" id="_Task" style="color: #333333"><i
                                                                    id="img_Task" class="icon-plus-sign-alt"></i></a>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div runat="server" id="anchor">
                                                                    <a href="JavaScript:divexpandcollapse('_Task<%# Eval("Task_ID") %>');" style="color: #333333">
                                                                        <i id="img_Task<%# Eval("Task_ID") %>" class="icon-plus-sign-alt"></i></a>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false" HeaderText="ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TaskID" runat="server" Text='<%# Bind("Task_ID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tasks" ItemStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Task" Width="100%" ToolTip='<%# Bind("Task_Name") %>' CssClass="label"
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
                                                                                    <div id="_Task<%# Eval("Task_ID") %>" style="display: none; position: relative; overflow: auto;">
                                                                                        <asp:GridView ID="gvTasks" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                                            CssClass="table table-bordered table-striped">
                                                                                            <%--OnPreRender="GridView_PreRender">--%>
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                                                                                    HeaderText="ID">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_TaskId" runat="server" Text='<%# Bind("Task_Id") %>'></asp:Label>
                                                                                                        <asp:HiddenField ID="hf_TaskID" runat="server" Value='<%# Bind("Task_Id") %>' />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                                                                                    HeaderText="ID">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_SubTaskId" runat="server" Text='<%# Bind("Sub_Task_Id") %>'></asp:Label>
                                                                                                        <asp:HiddenField ID="hf_SubTaskID" runat="server" Value='<%# Bind("Sub_Task_Id") %>' />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Sub-Tasks">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_TaskName" Width="60%" ToolTip='<%# Bind("Sub_Task_Name") %>' CssClass="label"
                                                                                                            runat="server" Text='<%# Bind("Sub_Task_Name") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:LinkButton ID="lbtnUploadDoc" runat="server" ToolTip="Upload Document" OnClientClick="return UploadDoc(this);"><i class="fa fa-upload" style="color:#333333;" aria-hidden="true"></i>
                                                                                                        </asp:LinkButton>
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
    </div>
</asp:Content>
