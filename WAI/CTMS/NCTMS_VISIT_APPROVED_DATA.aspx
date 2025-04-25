<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NCTMS_VISIT_APPROVED_DATA.aspx.cs"
    Inherits="CTMS.NCTMS_VISIT_APPROVED_DATA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8">
    <title>WAI</title>
    <script src="Scripts/Jquery1.12.4.js" type="text/javascript"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Common-Bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />
    <link href="Styles/AdminLTE.css" rel="stylesheet" type="text/css" />
    <link href="Styles/ionicons.css" rel="stylesheet" type="text/css" />
    <link href="Styles/font-awesome.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="img/favicon.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="Scripts/ClientValidation.js" type="text/javascript"></script>
    <!-- for Jquery Popup//-->
    <script src="https://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <link href="Styles/Jquery.ui.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/CommonFunction.js" type="text/javascript"></script>
    <!-- for Jquery Popup//-->
    <!-- for pikaday datepicker//-->
    <link href="Styles/pikaday.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/moment.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.jquery.js" type="text/javascript"></script>
    <!-- for pikaday datepicker//-->
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="js/plugins/datatables/dataTables.bootstrap.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="Styles/datatables/dataTables.bootstrap.css" />
    <script src="js/plugins/moment/moment.min.js" type="text/javascript"></script>
    <script src="js/plugins/moment/datetime-moment.js" type="text/javascript"></script>
    <link href="js/plugins/datatables/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <!-- Bootstrap -->
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <!-- Morris.js charts -->
    <script src="//cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js"></script>
    <%--  <script src="js/plugins/morris/morris.min.js" type="text/javascript"></script>--%>
    <!-- Sparkline -->
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <!-- jvectormap -->
    <script src="js/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js" type="text/javascript"></script>
    <script src="js/plugins/jvectormap/jquery-jvectormap-world-mill-en.js" type="text/javascript"></script>
    <!-- jQuery Knob Chart -->
    <script src="js/plugins/jqueryKnob/jquery.knob.js" type="text/javascript"></script>
    <!-- daterangepicker -->
    <script src="js/plugins/daterangepicker/daterangepicker.js" type="text/javascript"></script>
    <!-- Bootstrap WYSIHTML5 -->
    <script src="js/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js" type="text/javascript"></script>
    <!-- iCheck -->
    <script src="js/plugins/iCheck/icheck.min.js" type="text/javascript"></script>
    <!-- AdminLTE App -->
    <script src="js/AdminLTE/app.js" type="text/javascript"></script>
    <!-- AdminLTE dashboard demo (This is only for demo purposes) -->
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <link href="Styles/graph.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .form-control
        {
            width: 100%;
            min-width: 100px;
        }
        .brd-1px-redimp
        {
            border: 2px solid !important;
            border-color: Red !important;
        }
        .brd-1px-maroonimp
        {
            border: 2px solid !important;
            border-color: Maroon !important;
        }
        .style1
        {
            width: 100%;
            margin-right: 142px;
        }
        .strikeThrough
        {
            text-decoration: line-through;
        }
    </style>
    <script>
        function pageLoad() {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: false,
                fixedHeader: true
            });

            FillAuditDetails();
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

        //For ajax function call on update data button click
        function ShowComments(element) {
            var dataFreezeLockStatus = "1";

            if (dataFreezeLockStatus == '1') {

                var INVID = $("#hdnSITEID").val();
                var PROJECTID = '<%= Session["PROJECTID"] %>';
                var SVID = $("#hdnSVID").val();
                var VISITID = $("#hdnVISITID").val();
                var VISIT = $("#hdnVISIT").val();
                var Section = $("#<%=drpModule.ClientID%>").find("option:selected").text();
                var SectionID = $("#<%=drpModule.ClientID%>").find("option:selected").val();
                var CHECKLISTID = $(element).closest('tr').find('td:eq(1)').find('span').html();
                var variableName = $(element).attr('variablename');
                var ID = $(element).closest('tr').find('td:eq(0)').find('span').html();

                var test = "NCTMS_AddChecklistComments_PM.aspx?Section=" + VISIT + "&SubSectionText=" + Section + "&ProjectId=" + PROJECTID + "&INVID=" + INVID + "&SVID=" + SVID + "&SectionID=" + VISITID + "&SubSectionID=" + SectionID + "&CHECKLISTID=" + CHECKLISTID + "&Action=Field_Added_Comment" + "&ID=" + ID;
                var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=550,width=1200";
                window.open(test, '_blank', strWinProperty);
                return false;

            }
        }

        function Add_Field_Comments(element) {
            var dataFreezeLockStatus = "1";

            if (dataFreezeLockStatus == '1') {

                var INVID = $("#hdnSITEID").val();
                var PROJECTID = '<%= Session["PROJECTID"] %>';
                var SVID = $("#hdnSVID").val();
                var VISITID = $("#hdnVISITID").val();
                var VISIT = $("#hdnVISIT").val();
                var Section = $("#<%=drpModule.ClientID%>").find("option:selected").text();
                var SectionID = $("#<%=drpModule.ClientID%>").find("option:selected").val();
                var CHECKLISTID = $(element).closest('tr').find('td:eq(1)').find('span').html();
                var variableName = $(element).attr('variablename');

                var test = "NCTMS_AddChecklistComments_PM.aspx?Section=" + VISIT + "&SubSectionText=" + Section + "&ProjectId=" + PROJECTID + "&INVID=" + INVID + "&SVID=" + SVID + "&SectionID=" + VISITID + "&SubSectionID=" + SectionID + "&CHECKLISTID=" + CHECKLISTID + "&Action=FieldComment";
                var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=550,width=1200";
                window.open(test, '_blank', strWinProperty);
                return false;

            }
        }

        //show audit details icon of field 
        function FillAuditDetails() {
            var count = 0;
            $("#grdAUDITTRAILDETAILS tr").each(function () {
                if (count > 0) {
                    var contId = $(this).find('td:eq(2)').find('input').val().trim();
                    var tableName = 'grd_Data';
                    var variableName = $(this).find('td:eq(1)').find('input').val().trim();
                    var REASON = $(this).find('td:eq(3)').find('input').val().trim();

                    if (REASON == "Initial Entry") {

                        var element = $("img[id*='_AD_" + variableName + "_']").attr('id');
                        var element1 = $("img[id*='_ADINITIAL_" + variableName + "_']").attr('id');
                        $("#" + element).addClass("disp-none");
                        $("#" + element1).removeClass("disp-none");

                    }
                    else {

                        var element = $("img[id*='_AD_" + variableName + "_']").attr('id');
                        var element1 = $("img[id*='_ADINITIAL_" + variableName + "_']").attr('id');
                        $("#" + element).removeClass("disp-none");
                        $("#" + element1).addClass("disp-none");

                    }
                }
                count++;
            });

        }

        //For ajax function call on update data button click
        function showAuditTrail(element) {
            var dataFreezeLockStatus = "1";

            if (dataFreezeLockStatus == '1') {
                var variableName = $(element).attr('variablename'); //bindin  
                var contId = $(element).closest('tr').find('td:first').find('input').val(); //unique id for each row
                var CTMS_SVID = $("#hdnSVID").val();
                var CTMS_RECID = $("#hdnRECID").val();

                $.ajax({
                    type: "POST",
                    url: "AjaxFunction.aspx/CTMS_GetAuditTrail",
                    data: '{VariableName: "' + variableName + '",ContId: "' + contId + '",CTMS_SVID: "' + CTMS_SVID + '",CTMS_RECID: "' + CTMS_RECID + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d == 'Object reference not set to an instance of an object.') {
                            alert("Session Expired");
                            var url = "SessionExpired.aspx";
                            $(location).attr('href', url);
                        }
                        else {
                            $('#grdAuditTrail').html(data.d)
                            $("#popup_AuditTrail").dialog({
                                title: "Audit Trail",
                                width: 560,
                                height: 300,
                                modal: true,
                                buttons: {
                                    "Close": function () { $(this).dialog("close"); }
                                }
                            });
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
            }
        }
    </script>
</head>
<body>
    <form id="form1" method="post" class="content" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div id="popup_AuditTrail" title="Audit Trail" class="disp-none">
        <div id="grdAuditTrail">
        </div>
    </div>
    <asp:GridView ID="grdAUDITTRAILDETAILS" runat="server" AutoGenerateColumns="False"
        CellPadding="4" font-family="Arial" Font-Size="X-Small" ForeColor="#333333" GridLines="None"
        Style="text-align: center" Width="228px" CssClass="table table-bordered table-striped disp-none">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="TABLE NAME">
                <ItemTemplate>
                    <asp:TextBox ID="TABLENAME" runat="server" font-family="Arial" Font-Size="X-Small"
                        Text='<%# Bind("TABLENAME") %>' Width="100px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="VARIABLE NAME">
                <ItemTemplate>
                    <asp:TextBox ID="VARIABLENAME" runat="server" font-family="Arial" Font-Size="X-Small"
                        Text='<%# Bind("VARIABLENAME") %>' Width="100px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CONTID">
                <ItemTemplate>
                    <asp:TextBox ID="CONTID" runat="server" font-family="Arial" Font-Size="X-Small" Text='<%# Bind("CONTID") %>'
                        Width="100px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="REASON">
                <ItemTemplate>
                    <asp:TextBox ID="REASON" runat="server" font-family="Arial" Font-Size="X-Small" Text='<%# Bind("REASON") %>'
                        Width="100px" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div class="box box-warning">
        <div class="box-header" style="display: inline-flex; width: 100%;">
            <h3 class="box-title" style="width: 100%;">
                Add Review Comments &nbsp;||&nbsp;
                <asp:Label runat="server" ID="lblSiteId" />&nbsp;||&nbsp;<asp:Label runat="server"
                    ID="lblSubjectId" />&nbsp;||&nbsp;<asp:Label runat="server" ID="lblVisit" />&nbsp;||&nbsp;<asp:Label
                        runat="server" ID="lblVisitID" />&nbsp;
            </h3>
        </div>
        <div class="row">
            <div class="lblError">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <asp:HiddenField runat="server" ID="hfTablename" />
        <asp:HiddenField ID="hdnSourceValue" runat="server" />
        <asp:HiddenField runat="server" ID="hdnVISITID" />
        <asp:HiddenField runat="server" ID="hdnSITEID" />
        <asp:HiddenField runat="server" ID="hdnVISIT" />
        <asp:HiddenField runat="server" ID="hdnRECID" Value="0" />
        <asp:HiddenField runat="server" ID="hdnMODULEID" />
        <asp:HiddenField runat="server" ID="hdnMODULENAME" />
        <asp:HiddenField runat="server" ID="hdnVISIT_NOM" />
        <asp:HiddenField runat="server" ID="hdnSVID" />
        <asp:HiddenField runat="server" ID="hdn_CTMS_Value" />
        <asp:HiddenField runat="server" ID="hdnPageStatus" />
        <div style="display: inline-flex;">
            <div runat="server" id="divDDLS">
                <div class="form-group" style="display: inline-flex">
                    <label class="label" style="color: Maroon;">
                        Select Section:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpModule" runat="server" ForeColor="Blue" AutoPostBack="True"
                            CssClass="form-control " Style="width: 100%" OnSelectedIndexChanged="drpModule_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <table class="style1">
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblModuleName" runat="server" Text="" Font-Size="12px" Font-Bold="true"
                        Font-Names="Arial" CssClass="list-group-item"></asp:Label>
                </td>
            </tr>
            <tr>
                <asp:GridView ID="grdField" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CssClass="table table-bordered table-striped layer1" OnRowDataBound="grdField_RowDataBound">
                    <Columns>
                        <asp:TemplateField ItemStyle-CssClass="txtCenter" HeaderStyle-CssClass="txt_center"
                            ControlStyle-CssClass="txt_center">
                            <HeaderTemplate>
                                <a href="JavaScript:ManipulateAll('_Comment');" id="_Folder" style="color: #333333">
                                    <i id="img_Comment" class="icon-plus-sign-alt"></i></a>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div runat="server" id="anchor">
                                    <a href="JavaScript:divexpandcollapse('_Comment<%# Eval("ID") %>');" style="color: #333333">
                                        <i id="img_Comment<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                            HeaderText="ID">
                            <ItemTemplate>
                                <asp:Label ID="ID" runat="server" Text='<%# Bind("FIELD_ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblVARIABLENAME" runat="server" Text='<%# Bind("VARIABLENAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Questions" HeaderStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="lblFieldName" runat="server" ToolTip='<%# Bind("Descrip") %>' Text='<%# Bind("FIELDNAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Answers" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="lblANS" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                            <ItemTemplate>
                                <asp:Label ID="CM" CssClass="btn btn-warning btn-sm" runat="server" ToolTip="Add Comments">
                                    <asp:Label class="badge badge-info right" ID="lblAllCommentCount" runat="server"
                                        Text="0" Style="margin-left: 5px;" Font-Bold="true"></asp:Label>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                            <ItemTemplate>
                                <img src="Images/Audit_Trail.png" id="AD" height="20" width="25" runat="server" title="Audit trail"
                                    onclick="showAuditTrail(this)" class="disp-none" />
                                <img src="Images/Audit_Trail1.png" id="ADINITIAL" height="20" width="25" runat="server"
                                    title="Audit trail" onclick="showAuditTrail(this)" class="disp-none" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <tr>
                                    <td colspan="100%" style="padding: 2px;">
                                        <div>
                                            <div class="rows">
                                                <div class="col-md-12">
                                                    <div id="_Comment<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                                                        <asp:GridView ID="grdComments" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                            ForeColor="Blue" CssClass="table table-bordered table-striped Datatable" Caption=""
                                                            OnRowDataBound="grdComments_RowDataBound" OnPreRender="gvEmp_PreRender">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CheckListRow_ID" HeaderStyle-CssClass="disp-none"
                                                                    ItemStyle-CssClass="disp-none">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="CheckListRow_ID" runat="server" CommandArgument='<%#Eval("CheckListRow_ID") %>'
                                                                            Text='<%#Eval("CheckListRow_ID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Subject ID" ItemStyle-CssClass="txt_center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSUBJID" runat="server" CommandArgument='<%#Eval("SUBJID") %>' Text='<%# Eval("SUBJID") %>'
                                                                            CommandName="SUBJID"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CRA Comment">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Comments" runat="server" CommandName="Comments" Text='<%# Eval("Comments") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CRA Comment By">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ENTEREDBY" runat="server" Text='<%# Eval("ENTEREDBY") %>' CommandName="ENTEREDBY"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CRA Comment Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ENTEREDAT" runat="server" Text='<%# Eval("DTENTERED") %>' CommandName="ENTEREDBY"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Issue" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="CHK_Issue" Enabled="false" runat="server" CssClass="txt_center"
                                                                            ToolTip="Issue" font-family="Arial"></asp:CheckBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="For Internal Use" HeaderStyle-CssClass="txt_center"
                                                                    ItemStyle-CssClass="txt_center">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="CHK_Internal" Enabled="false" runat="server" CssClass="txt_center"
                                                                            ToolTip="Internal" font-family="Arial"></asp:CheckBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="In Report" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="CHK_REPORT" Enabled="false" runat="server" CssClass="txt_center"
                                                                            ToolTip="Issue" font-family="Arial"></asp:CheckBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Followup" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="CHK_Followup" Enabled="false" runat="server" CssClass="txt_center"
                                                                            ToolTip="Followup" font-family="Arial"></asp:CheckBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Protocol Deviation" HeaderStyle-CssClass="txt_center"
                                                                    ItemStyle-CssClass="txt_center">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="CHK_PD" Enabled="false" runat="server" CssClass="txt_center" ToolTip="Protocol Deviation"
                                                                            font-family="Arial"></asp:CheckBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PM Comment">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="PM_COMMENTS" runat="server" CommandName="Comments" Text='<%# Eval("PM_COMMENTS") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PM Comment By">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="PM_COMMENTS_BY" runat="server" Text='<%# Eval("PM_COMMENTS_BY") %>'
                                                                            CommandName="ENTEREDBY"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PM Comment Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="PM_COMMENTS_DAT" runat="server" Text='<%# Eval("PM_COMMENTS_DAT") %>'
                                                                            CommandName="ENTEREDBY"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sponsor Comment">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="SPONSOR_COMMENTS" runat="server" CommandName="Comments" Text='<%# Eval("SPONSOR_COMMENTS") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sponsor Comment By">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="SPONSOR_COMMENTS_BY" runat="server" Text='<%# Eval("SPONSOR_COMMENTS_BY") %>'
                                                                            CommandName="ENTEREDBY"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sponsor Comment Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="SPONSOR_COMMENTS_DAT" runat="server" Text='<%# Eval("SPONSOR_COMMENTS_DAT") %>'
                                                                            CommandName="ENTEREDBY"></asp:Label>
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
                </asp:GridView>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <div class="box box-danger">
                        <div class="box-header">
                            <h3 class="box-title" style="width: 100%;">
                                Tracker Data &nbsp&nbsp&nbsp <a href="JavaScript:ManipulateAll('_Pat_');" id="_Folder"
                                    style="color: #333333; font-size: larger; margin-top: 5px;"><i id="img_Pat_" class="icon-plus-sign-alt">
                                    </i></a>
                            </h3>
                        </div>
                        <asp:Repeater runat="server" OnItemDataBound="repeatData_ItemDataBound" ID="repeatData">
                            <ItemTemplate>
                                <div class="box box-primary">
                                    <div class="box-header">
                                        <div runat="server" style="display: inline-flex; padding: 0px; margin: 4px 0px 0px 10px;"
                                            id="anchor">
                                            <a href="JavaScript:divexpandcollapse('_Pat_<%# Eval("MODULEID") %>');" style="color: #333333">
                                                <i id="img_Pat_<%# Eval("MODULEID") %>" class="icon-plus-sign-alt"></i></a>
                                            <h3 class="box-title" style="padding-top: 0px;">
                                                <asp:Label ID="lblHeader" runat="server" Text='<%# Bind("MODULENAME") %>'></asp:Label>
                                            </h3>
                                        </div>
                                    </div>
                                    <div id="_Pat_<%# Eval("MODULEID") %>" style="display: none; position: relative;
                                        overflow: auto;">
                                        <div class="box-body">
                                            <div class="rows">
                                                <div style="width: 100%; overflow: auto;">
                                                    <div>
                                                        <asp:GridView ID="grd_Records" runat="server" CellPadding="3" AutoGenerateColumns="True"
                                                            ForeColor="Blue" CssClass="table table-bordered table-striped Datatable" ShowHeader="True"
                                                            EmptyDataText="No Records Available" CellSpacing="2" OnPreRender="gvEmp_PreRender"
                                                            OnRowDataBound="grd_Records_RowDataBound">
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
