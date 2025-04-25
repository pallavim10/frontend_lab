<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Budget_ProjectMilestone.aspx.cs" Inherits="CTMS.Budget_ProjectMilestone" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .fontRed
        {
            color: Red;
        }
        .fontGreen
        {
            color: Green;
        }
    </style>
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        var counter = 0;

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
        //For ajax function call on update data button click
        function ShowAudit(element) {

            var Task_ID = $(element).closest('tr').find('td:eq(1)').find('span').html(); //unique id for each row
            var Task = $(element).closest('tr').find('td:eq(3)').find('span').html(); //unique id for each row
            var Sub_Task_ID = $(element).closest('tr').find('td:eq(2)').find('span').html(); //unique id for each row
            var tableName = $(element).attr('Tablename');  //table name of database               
            var fieldName = $(element).attr('FieldName');  //column name of particular control
            var test4 = $(element).attr('VariableName');
            var variableName = $(element).attr('Name'); //binding control name
            CurrentObj = $(element).attr('id');
            var hdnValue = $('#hdn_Value').val().trim();
            var elementValue = $('#hdn_Value').val().trim();

            $('#<%=txt_OldValue.ClientID%>').val(hdnValue);
            $('#<%=txt_NewValue.ClientID%>').val(elementValue);
            $('#<%=txt_Task.ClientID%>').val(Task);
            $('#<%=txtTaskID.ClientID%>').val(Task_ID);
            $('#<%=txt_SubtaskID.ClientID%>').val(Sub_Task_ID);
            $('#<%=txt_TableName.ClientID%>').val(tableName);
            $('#<%=txt_FieldName.ClientID%>').val(fieldName);
            $('#<%=txt_VariableName.ClientID%>').val(test4);
            $('#<%=txt_Comments.ClientID%>').val("");
            $('#<%=drp_Reason.ClientID%>').val('0');

            var ID = $(element).closest('tr').find('td:eq(15)').find('input').val();
            $('#<%=hdn_AutoId.ClientID%>').val(ID);

            var Action = "Get_Audit_Trail";
            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/BudgetPP_Audit_Trail",
                data: '{Action:"' + Action + '",  Task_ID: "' + $('#<%=txtTaskID.ClientID%>').val() + '", Sub_Task_ID: "' + $('#<%=txt_SubtaskID.ClientID%>').val() + '" ,FieldName: "' + $('#<%=txt_FieldName.ClientID%>').val() + '",TableName: "' + $('#<%=txt_TableName.ClientID%>').val() + '",VariableName: "' + $('#<%=txt_VariableName.ClientID%>').val() + '",OldValue: "' + $('#<%=txt_OldValue.ClientID%>').val() + '",NewValue: "' + $('#<%=txt_NewValue.ClientID%>').val() + '",Reason: "' + $('#<%=drp_Reason.ClientID%>').val() + '",Comments: "' + $('#<%=txt_Comments.ClientID%>').val() + '",AutoId: "' + $('#<%=hdn_AutoId.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $('#grdQueryDetails').html(data.d)
                    }
                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }

                }
            });
            $("#popup_FieldComments2").dialog({
                title: "Audit Trail",
                width: "1000",
                height: "auto",
                modal: true,
                buttons: {
                    "Close": function () { $(this).dialog("close"); }
                }
            });

        }
        function BudgetPP_UpdateData() {
            if ($('#<%=drp_Reason.ClientID%>').val() == 0) {
                alert("Please Select Reason");
                return false;
            }
            if ($('#<%=drp_Reason.ClientID%>').val() == 'Other') {
                if ($('#<%=txt_Comments.ClientID%>').val() == '') {
                    alert("Please Enter Comments");
                    return false;
                }

            }


            var Action = "Audit_trail_BudgetPP";
            //console.log('{Task_ID: "' + $("#txt_ContId")[0].value.trim() + '",ModuleName: "' + $("#txt_ModuleName")[0].value.trim() + '" ,FieldName: "' + $("#txt_FieldName")[0].value.trim() + '",TableName: "' + $("#txt_TableName")[0].value.trim() + '",VariableName: "' + $("#txt_VariableName")[0].value.trim() + '",OldValue: "' + $("#txt_OldValue")[0].value.trim() + '",NewValue: "' + $("#txt_NewValue")[0].value.trim() + '",ModuleName: "' + $("#txt_ModuleName")[0].value.trim() + '",Reason: "' + $("#drp_Reason option:selected").text() + '",Comments: "' + $("#txt_Comments")[0].value.trim() + '",ControlType: "' + controlType + '"}');
            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/BudgetPP_Audit_Trail",
                data: '{Action:"' + Action + '",  Task_ID: "' + $('#<%=txt_SubtaskID.ClientID%>').val() + '", Sub_Task_ID: "' + $('#<%=txt_Task.ClientID%>').val() + '" ,FieldName: "' + $('#<%=txt_FieldName.ClientID%>').val() + '",TableName: "' + $('#<%=txt_TableName.ClientID%>').val() + '",VariableName: "' + $('#<%=txt_VariableName.ClientID%>').val() + '",OldValue: "' + $('#<%=txt_OldValue.ClientID%>').val() + '",NewValue: "' + $('#<%=txt_NewValue.ClientID%>').val() + '",Reason: "' + $('#<%=drp_Reason.ClientID%>').val() + '",Comments: "' + $('#<%=txt_Comments.ClientID%>').val() + '",AutoId: "' + $('#<%=hdn_AutoId.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d == 'Record Updated successfully.') {
                        alert(response.d);
                        window.location = window.location;
                    }
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert(response.d);
                    }

                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert(response.d);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }
                }
            });
        }

        function BudgetPP_CancelUpdate() {
            //            if (CurrentObjType == "checkbox") {
            //                if ($("#txt_OldValue")[0].value == "True")
            //                    $('#' + CurrentObj + '').attr('checked', true);
            //                else
            //                    $('#' + CurrentObj + '').attr('checked', false);
            //            }
            //            $('#' + CurrentObj + '').val($("#txt_OldValue")[0].value.trim()); //on cancel click set old value to control
            $("#popup_BudgetUpdateData2").dialog('close');
            window.location = window.location;
            //$(this).closest(".ui-dialog").next(".ui-widget-overlay").remove();
            return false;
        }

        function BudgetPPFocus(element) {
            var test = $(element).val().trim();
            $('#hdn_Value').val(test);
            counter++;
        }

        function BudgetPPFunction(element) {

            //if this is blank means its first data entry no audit trail required
            var statusdate = $(element).closest('td').next('td').find('input').val(); //unique id for each row

            if (statusdate == '') {
                return false;
            }


            var hdnValue = $('#hdn_Value').val().trim();
            if ($(element).hasClass("txtDate") && hdnValue == "" && counter == 0) {
                return false;
            }
            if ($(element).hasClass("txtDate") && hdnValue == "0") {
                return false;
            }
            var elementValue = $(element).val().trim();
            if (hdnValue == elementValue) {
                return false;
            }
            else {
                var Task_ID = $(element).closest('tr').find('td:eq(1)').find('span').html(); //unique id for each row
                var Task = $(element).closest('tr').find('td:eq(3)').find('span').html(); //unique id for each row
                var Sub_Task_ID = $(element).closest('tr').find('td:eq(2)').find('span').html(); //unique id for each row
                var tableName = $(element).attr('Tablename');  //table name of database               
                var fieldName = $(element).attr('FieldName');  //column name of particular control
                var test4 = $(element).attr('VariableName');
                var variableName = $(element).attr('Name'); //binding control name
                CurrentObj = $(element).attr('id');
                //alert(Task)
                $('#<%=txt_OldValue.ClientID%>').val(hdnValue);
                $('#<%=txt_NewValue.ClientID%>').val(elementValue);
                $('#<%=txt_Task.ClientID%>').val(Task);
                $('#<%=txtTaskID.ClientID%>').val(Task_ID);
                $('#<%=txt_SubtaskID.ClientID%>').val(Sub_Task_ID);
                $('#<%=txt_TableName.ClientID%>').val(tableName);
                $('#<%=txt_FieldName.ClientID%>').val(fieldName);
                $('#<%=txt_VariableName.ClientID%>').val(test4);
                $('#<%=txt_Comments.ClientID%>').val("");
                $('#<%=drp_Reason.ClientID%>').val('0');

                $('#<%=popup_BudgetUpdateData2.ClientID%>').dialog({
                    title: "Update Data",
                    width: 700,
                    height: "auto",
                    modal: true
                    //  buttons: {
                    //    "Close": function () { $(this).dialog("close"); }
                    // }
                });
                $('#<%=popup_BudgetUpdateData2.ClientID%>').dialog("open");
                return false;


            }
        }

    </script>
    <script type="text/javascript">
        function UploadDoc(element) {

            var row = element.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;

            var TaskID = $(element).closest('tr').find('td:eq(1)').find('input').val();
            var SubTaskID = $(element).closest('tr').find('td:eq(2)').find('input').val();
            var ID = $(element).closest('tr').find('td:eq(15)').find('input').val();

            //            var test = "CTMS_UploadDoc.aspx?TaskID=" + TaskID + "&SubTaskID=" + SubTaskID + "&INVID=" + INVID;
            var test = "eTMF_UploadDocument.aspx?TaskID=" + TaskID + "&SubTaskID=" + SubTaskID + "&INVID=0&ID=" + ID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500,width=1100";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function UploadDoc_Oth(element) {

            var row = element.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;

            var TaskID = $(element).closest('tr').find('td:eq(1)').find('input').val();
            var SubTaskID = $(element).closest('tr').find('td:eq(2)').find('input').val();
            var ID = $(element).closest('tr').find('td:eq(10)').find('input').val();

            var test = "eTMF_UploadDocument.aspx?TaskID=" + TaskID + "&SubTaskID=" + SubTaskID + "&INVID=0&ID=" + ID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500,width=1100";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function DownloadDoc(element) {

            var row = element.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;

            var TaskID = document.getElementById('MainContent_gvTasks_hf_TaskID_' + rowIndex).value;
            var SubTaskID = document.getElementById('MainContent_gvTasks_hf_SubTaskID_' + rowIndex).value;

            var test = "CTMS_DownloadDoc.aspx?TaskID=" + TaskID + "&SubTaskID=" + SubTaskID + "&INVID=0";

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=520,width=900";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function AddComment(element) {

            var row = element.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;

            var TaskID = $(element).closest('tr').find('td:eq(1)').find('input').val();
            var SubTaskID = $(element).closest('tr').find('td:eq(2)').find('input').val();
            var Comment = $(element).closest('tr').find('td:eq(13)').find('input').val();
            var ID = $(element).closest('tr').find('td:eq(15)').find('input').val();

            var test = "CTMS_TimeLineComment.aspx?TaskID=" + TaskID + "&SubTaskID=" + SubTaskID + "&INVID=0&Comment=" + Comment + "&ID=" + ID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=250,width=600";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function Print() {

            var ProjectId = '<%= Session["PROJECTID"] %>'
            var Action = "get_TOM_ProjectTimeline_Data";
            var test = "ReportBudgetProjectPlan.aspx?ProjectId=" + ProjectId + "&Action=" + Action;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500px,width=1500px";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function AddNewAct(element) {

            $.ajaxSetup({ async: false });

            $(element).closest('tr').find('td:eq(5)').find('a').addClass('disp-none');
            $(element).closest('tr').find('td:eq(5)').find('span').text('');
            $(element).closest('tr').find('td:eq(6)').find('input').val('');
            $(element).closest('tr').find('td:eq(7)').find('img').addClass('disp-none');
            $(element).closest('tr').find('td:eq(7)').find('input').val('');
            $(element).closest('tr').find('td:eq(8)').find('input').val('');
            $(element).closest('tr').find('td:eq(9)').find('img').addClass('disp-none');
            $(element).closest('tr').find('td:eq(9)').find('input').val('');
            $(element).closest('tr').find('td:eq(10)').find('span').text('');
            $(element).closest('tr').find('td:eq(11)').find('span').text('');
            $(element).closest('tr').find('td:eq(12)').find('span').text('');
            $(element).closest('tr').find('td:eq(13)').find('a').addClass('disp-none');
            $(element).closest('tr').find('td:eq(15)').find('span').text('0');
            $(element).closest('tr').find('td:eq(15)').find('input').val('0');

            $.ajaxSetup({ async: true });

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
                Project Milestones
                <asp:LinkButton ID="lbtnprint" runat="server" Text="Print" OnClientClick="return Print()"
                    CssClass="btn-sm">
      <span class="glyphicon glyphicon-print"></span>Print</asp:LinkButton>
            </h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="form-group" style="display: inline-flex">
            <div class="form-group" style="display: inline-flex">
                <label class="label">
                    Departments :
                </label>
                <div class="Control">
                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control required">
                    </asp:DropDownList>
                </div>
                <asp:Button ID="btnGetdata" Text="Get Data" runat="server" CssClass="btn btn-primary btn-sm"
                    OnClick="btnGetdata_Click" />
            </div>
        </div>
        <asp:HiddenField ID="hdn_Value" runat="server" Value="" />
        <asp:HiddenField ID="hdn_AutoId" runat="server" Value="" />
        <div id="popup_FieldComments2" title="Comments" class="disp-none">
            <div id="grdQueryDetails" class="">
            </div>
        </div>
        <div id="popup_BudgetUpdateData2" runat="server" title="Reason For Change" class="disp-none">
            <div class="disp-none">
                <asp:Label ID="Label7" runat="server" Text="Table Name"></asp:Label>
                <asp:TextBox ID="txt_TableName" runat="server"></asp:TextBox>
                <asp:Label ID="Label6" runat="server" Text="Cont Id"></asp:Label>
                <asp:TextBox ID="txt_ContId" runat="server"></asp:TextBox>
                <asp:Label ID="Label5" runat="server" Text="Variable Name"></asp:Label>
                <asp:TextBox ID="txt_VariableName" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtTaskID" CssClass="width245px" Enabled="false" runat="server"></asp:TextBox>
                <div class="formControl">
                    <asp:Label ID="Label10" runat="server" CssClass="wrapperLable" Text="Module Name"></asp:Label>
                    <asp:TextBox ID="txt_SubtaskID" CssClass="width245px form-control" Enabled="false"
                        runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="formControl" style="display: inline-flex">
                <asp:Label ID="Label4" runat="server" CssClass="wrapperLable" Text="Task"></asp:Label>
                <asp:TextBox ID="txt_Task" CssClass="width245px form-control" Enabled="false" runat="server"></asp:TextBox>
            </div>
            <div class="formControl" style="display: inline-flex">
                <asp:Label ID="Label8" runat="server" CssClass="wrapperLable" Text="Field Name"></asp:Label>
                <asp:TextBox ID="txt_FieldName" CssClass="width245px form-control" Enabled="false"
                    runat="server"></asp:TextBox>
            </div>
            <div class="formControl" style="display: inline-flex">
                <asp:Label ID="Label1" runat="server" CssClass="wrapperLable" Text="Old value"></asp:Label>
                <asp:TextBox ID="txt_OldValue" CssClass="width245px form-control" Enabled="false"
                    runat="server"></asp:TextBox>
            </div>
            <div class="formControl" style="display: inline-flex">
                <asp:Label ID="Label2" runat="server" CssClass="wrapperLable" Text="New value"></asp:Label>
                <asp:TextBox ID="txt_NewValue" CssClass="width245px form-control" Enabled="false"
                    runat="server"></asp:TextBox>
            </div>
            <div class="formControl" style="display: inline-flex">
                <asp:Label ID="Label3" runat="server" CssClass="wrapperLable" Text="Reason"></asp:Label>
                <asp:DropDownList ID="drp_Reason" CssClass="width245px form-control" runat="server">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                    <asp:ListItem Value="Other">Other</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="formControl" style="display: inline-flex">
                <asp:Label ID="Label9" runat="server" CssClass="wrapperLable" Text="Comments"></asp:Label>
                <asp:TextBox ID="txt_Comments" CssClass="width245px form-control" TextMode="MultiLine"
                    runat="server"></asp:TextBox>
            </div>
            <div style="margin-top: 10px">
                <asp:Button ID="btn_Update" runat="server" Style="margin-left: 107px;" CssClass="btn btn-primary btn-sm"
                    OnClientClick="BudgetPP_UpdateData()" Text="Update" />
                <asp:Button ID="btn_Cancel" runat="server" Style="margin-left: 62px" CssClass="btn btn-danger btn-sm"
                    Text="Cancel" OnClientClick="BudgetPP_CancelUpdate()" />
            </div>
        </div>
        <asp:GridView ID="gvMainTask" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="table table-bordered table-striped" OnRowDataBound="gvMainTask_RowDataBound">
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                    HeaderStyle-CssClass="txt_center">
                    <HeaderTemplate>
                        <a href="JavaScript:ManipulateAll('_Task');" id="_Folder" style="color: #333333"><i
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
                                                    CssClass="table table-bordered table-striped" OnRowDataBound="gvTasks_RowDataBound">
                                                    <%--OnPreRender="GridView_PreRender">--%>
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                                            HeaderStyle-CssClass="txt_center">
                                                            <HeaderTemplate>
                                                                <a href="JavaScript:ManipulateAll('_Site');" id="_Folder" style="color: #333333"><i
                                                                    id="img_Site" class="icon-plus-sign-alt"></i></a>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div runat="server" id="anchor">
                                                                    <a href="JavaScript:divexpandcollapse('_Site<%# String.Format("{0},{1}", DataBinder.Eval(Container.DataItem, "Task_ID"), DataBinder.Eval(Container.DataItem, "Sub_Task_ID"))%>');"
                                                                        style="color: #333333"><i id="img_Site<%# String.Format("{0},{1}", DataBinder.Eval(Container.DataItem, "Task_ID"), DataBinder.Eval(Container.DataItem, "Sub_Task_ID"))%>"
                                                                            class="icon-plus-sign-alt"></i></a>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
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
                                                        <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="4%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnAddNew" runat="server" ToolTip="Add New Activity" OnClientClick="return AddNewAct(this);"><i class="fa fa-plus" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnUploadDoc" runat="server" ToolTip="Upload Document" OnClientClick="return UploadDoc(this);"><i class="fa fa-upload" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                                <br />
                                                                <asp:Label ID="UploadCount" runat="server" Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Plan Date" ItemStyle-Width="10%" ItemStyle-CssClass="txt_center">
                                                            <ItemTemplate>
                                                                <asp:Label Visible="false" Style="text-align: center; width: 150px;" ID="lblDtPlan"
                                                                    Text='<%# Bind("DtPlan1") %>' runat="server" CssClass="txt_center"></asp:Label>
                                                                <asp:TextBox Style="text-align: center;" ID="txtDtPlan" Text='<%# Bind("DtPlan1") %>'
                                                                    runat="server" class=" txt_center form-control txtDate" onfocus="BudgetPPFocus(this)"
                                                                    onchange="BudgetPPFunction(this)" Tablename="CTMS_ProjectTimeline" FieldName="Plan Date"
                                                                    VariableName="DtPlan"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" ItemStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:Image src="Images/Audit_Trail.png" ID="AuditTrailPlanDate" Height="16" Width="16"
                                                                    runat="server" class="Raise  Comments" title="Audit Trail" Tablename="CTMS_ProjectTimeline"
                                                                    FieldName="Plan Date" VariableName="DtPlan" Visible="false" onclick="ShowAudit(this)" />
                                                                <asp:TextBox ID="txtPlan" class="disp-none" Text='<%# Bind("DtPlan1") %>' runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Actual Date" ItemStyle-Width="10%" ItemStyle-CssClass="txt_center">
                                                            <ItemTemplate>
                                                                <asp:Label Visible="false" Style="text-align: center; width: 150px;" ID="lblDtActual"
                                                                    Text='<%# Bind("DtActual1") %>' runat="server"></asp:Label>
                                                                <asp:TextBox Style="text-align: center; display: inline-flex" ID="txtDtActual" Text='<%# Bind("DtActual1") %>'
                                                                    runat="server" class=" txt_center form-control txtDate" onfocus="BudgetPPFocus(this)"
                                                                    onchange="BudgetPPFunction(this)" Tablename="CTMS_ProjectTimeline" FieldName="Actual Date"
                                                                    VariableName="DtActual"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" ItemStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:Image src="Images/Audit_Trail.png" Style="display: inline-flex" ID="AuditTrailAcualDate"
                                                                    Height="16" Width="16" runat="server" class="Raise  Comments" title="Audit Trail"
                                                                    Visible="false" Tablename="CTMS_ProjectTimeline" FieldName="Actual Date" VariableName="DtActual"
                                                                    onclick="ShowAudit(this)" />
                                                                <asp:TextBox ID="txtActual" class="disp-none" Text='<%# Bind("DtActual1") %>' runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Original Plan Date" ItemStyle-CssClass="txt_center"
                                                            ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label Style="text-align: center; width: 150px;" ID="txtDtOgPlan" Text='<%# Bind("DtOgPlan1") %>'
                                                                    runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Variance(A Vs P)" ItemStyle-CssClass="txt_center"
                                                            ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label Style="width: 150px; text-align: center;" ID="txtDifference" Text='<%# Bind("Difference1") %>'
                                                                    runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Variance(A Vs O)" ItemStyle-CssClass="txt_center"
                                                            ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label Style="width: 150px; text-align: center;" ID="txtDifference2" Text='<%# Bind("Difference2") %>'
                                                                    runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false" ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnDownloadDoc" Visible="false" runat="server" ToolTip="View Document" CssClass="btn"
                                                                    OnClientClick="return DownloadDoc(this);">
                        <i class="fa fa-download" style="color:#333333;" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Comment" ItemStyle-Width="1%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnAddComment" runat="server" ToolTip='<%# Bind("Comment") %>'
                                                                    OnClientClick="return AddComment(this);">
                        <i class="fa fa-comment" style="color:#333333;" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                                <asp:HiddenField ID="hf_Comment" runat="server" Value='<%# Bind("Comment") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                                            HeaderText="Recurring">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Recurring" runat="server" Text='<%# Bind("Recurring") %>'></asp:Label>
                                                                <asp:HiddenField ID="hf_Recurring" runat="server" Value='<%# Bind("Recurring") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                                            HeaderText="MaxID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_MaxID" runat="server" Text='<%# Bind("MaxID") %>'></asp:Label>
                                                                <asp:HiddenField ID="hf_MaxID" runat="server" Value='<%# Bind("MaxID") %>' />
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
                                                                                    <div id="_Site<%# String.Format("{0},{1}", DataBinder.Eval(Container.DataItem, "Task_ID"), DataBinder.Eval(Container.DataItem, "Sub_Task_ID"))%>"
                                                                                        style="display: none; position: relative; overflow: auto;">
                                                                                        <asp:GridView ID="gvSites" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                                            CssClass="table table-bordered table-striped" OnRowDataBound="gvSites_RowDataBound">
                                                                                            <Columns>
                                                                                                <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="4%">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="UploadCount" runat="server" Visible="false"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
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
                                                                                                <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="4%">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:LinkButton ID="lbtnUploadDoc" runat="server" ToolTip="Upload Document" OnClientClick="return UploadDoc_Oth(this);"><i class="fa fa-upload" style="color:#333333;" aria-hidden="true"></i>
                                                                                                        </asp:LinkButton>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Plan Date" ItemStyle-CssClass="txt_center" ItemStyle-Width="10%">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label Style="text-align: center; width: 150px;" ID="txtDtPlan1" Text='<%# Bind("DtPlan1") %>'
                                                                                                            runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Actual Date" ItemStyle-CssClass="txt_center" ItemStyle-Width="10%">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label Style="text-align: center; width: 150px;" ID="txtDtActual1" Text='<%# Bind("DtActual1") %>'
                                                                                                            runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Original Plan Date" ItemStyle-CssClass="txt_center"
                                                                                                    ItemStyle-Width="10%">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label Style="text-align: center; width: 150px;" ID="txtDtOgPlan1" Text='<%# Bind("DtOgPlan1") %>'
                                                                                                            runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Variance(A Vs P)" ItemStyle-CssClass="txt_center"
                                                                                                    ItemStyle-Width="10%">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label Style="width: 150px; text-align: center;" ID="txtDifference" Text='<%# Bind("Difference1") %>'
                                                                                                            runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Variance(A Vs O)" ItemStyle-CssClass="txt_center"
                                                                                                    ItemStyle-Width="10%">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label Style="width: 150px; text-align: center;" ID="txtDifference2" Text='<%# Bind("Difference2") %>'
                                                                                                            runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Comment" ItemStyle-Width="20%">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label Style="width: 150px; text-align: center;" ID="txtComment" Text='<%# Bind("Comment") %>'
                                                                                                            runat="server"></asp:Label>
                                                                                                        <asp:HiddenField ID="hf_Comment" runat="server" Value='<%# Bind("Comment") %>' />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                                                                                    HeaderText="ID">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                                                        <asp:HiddenField ID="hf_ID" runat="server" Value='<%# Bind("ID") %>' />
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
        <div class="txtCenter">
            <br />
            <asp:Button ID="btnsubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                Visible="false" ValidationGroup="section" OnClick="btnsubmit_Click" />
            <br />
        </div>
        <br />
    </div>
</asp:Content>
