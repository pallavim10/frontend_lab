<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DM_AddDrpDownData_Linked.aspx.cs" EnableEventValidation="false"
    Inherits="CTMS.DM_AddDrpDownData_Linked" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>WAI</title>
    <script src="Scripts/Jquery1.12.4.js" type="text/javascript"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Common-Bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />
    <link href="Styles/AdminLTE.css" rel="stylesheet" type="text/css" />
    <link href="Styles/ionicons.css" rel="stylesheet" type="text/css" />
    <link href="Styles/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="Styles/font-awesome.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="img/favicon.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
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
    <script src="CommonFunctionsJs/DB/DB_showAuditTrail.js"></script>
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .brd-1px-redimp {
            border: solid 1px !important;
            border-color: Red !important;
        }

        .brd-1px-maroonimp {
            border: solid !important;
            border-color: Maroon !important;
        }

        .overlay {
            display: none;
            position: fixed;
            width: 100%;
            height: 100%;
            top: 0;
            left: 0;
            z-index: 999;
            background: rgba(255,255,255,0.8) url("/image/loader.gif") center no-repeat;
        }
        /* Turn off scrollbar when body element has the loading class */
        body.loading {
            overflow: hidden;
        }
            /* Make spinner image visible when body element has the loading class */
            body.loading .overlay {
                display: block;
            }
    </style>
    <style type="text/css">
        .form-control-model {
            display: block;
            padding: 0px 12px;
            font-size: 13px;
            line-height: 1.428571429;
            color: #555555;
            vertical-align: middle;
            background-image: none;
            border: 1px solid #cccccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgb(0 0 0 / 8%);
            box-shadow: inset 0 1px 1px rgb(0 0 0 / 8%);
            -webkit-transition: border-color ease-in-out 0.15s, box-shadow ease-in-out 0.15s;
            transition: border-color ease-in-out 0.15s, box-shadow ease-in-out 0.15s;
            margin-top: 3px !important;
        }

        .strikeThrough {
            text-decoration: line-through;
            text-decoration-color: red;
        }
    </style>
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js"></script>
    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }
    </style>
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true,
                fixedHeader: true
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');
        }
    </script>
    <script>

        function DisableDiv() {
            var nodes = document.getElementById("divaddoption").getElementsByTagName('*');
            for (var i = 0; i < nodes.length; i++) {
                nodes[i].disabled = true;
            }

            //Get Query String Value
            const params = new Proxy(new URLSearchParams(window.location.search), {
                get: (searchParams, prop) => searchParams.get(prop),
            });

            let FREEZE = params.FREEZE;

            $("#lblStatus").text(FREEZE + ".");
        }

        $(document).on("click", ".cls-btnSave", function () {
            var test = "0";

            $('.required').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "0" || value == null) {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    if (value == "") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
            });

            if (test == "1") {
                return false;
            }
            return true;
        });

        function countCheckboxes() {
            var inputElems = $("input[type=checkbox][id*=Chk_VISIT]").toArray()

            var count = 0;

            for (var i = 0; i < inputElems.length; i++) {

                if (inputElems[i].type === "checkbox" && inputElems[i].checked === true) {
                    count++;
                }
            }

            if (count < 1) {
                alert("Select at least one visit");
                event.preventDefault();
            }
        }

    </script>
    <style type="text/css">
        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .Popup1 {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            min-width: 500px;
            max-width: 650px;
        }

        h5 {
            background-color: #007bff;
            padding: 0.4em 1em;
            margin-top: 0px;
            font-weight: bold;
            color: white;
        }

        .modal-body {
            position: relative;
            padding: 0px;
        }
    </style>

    
    <script type="text/javascript">

        function CheckStatusLogsBeforeDelete(id, fieldName, ENTEREDDAT, buttonClientID) {


            $.ajax({
                type: "POST",
                url: "DM_AddDrpDownData_Linked.aspx/CheckStatusLogs",
                data: JSON.stringify({ ENTEREDDAT: ENTEREDDAT }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d) {
                        // Data exists - show the delete popup
                        ShowDeletePopup(id, fieldName);
                    } else {
                        // No data - ask for confirmation before deletion
                        if (confirm('Are you sure you want to permanently delete this item?')) {
                            // Trigger GridView RowCommand with CommandName "DeleteModule"
                            __doPostBack('<%= grdData.UniqueID %>', 'DeleteModule$' + id);
                        }
                    }
                },
                error: function (xhr, status, error) {
                    console.error('Error: ' + error);
                }
            });
            return false; // Prevent postback until AJAX completes
        }

        function ShowDeletePopup(id, fieldName) {
            // Set the hidden field values with the ID and Variable Name
            document.getElementById('<%= hdnDeleteFieldID.ClientID %>').value = id;
            document.getElementById('<%= hdnVariableName.ClientID %>').value = fieldName;

            // Trigger the hidden button to show the modal popup
            document.getElementById('<%= btnShowDeletePopup.ClientID %>').click();
            return false; // Prevent postback
        }

        function ConfirmDeleteAction() {
            var permanentDelete = document.getElementById('<%= rdoPermanentDelete.ClientID %>').checked;
            var prospectiveDelete = document.getElementById('<%= rdoProspectiveDelete.ClientID %>').checked;

            if (permanentDelete) {
                var isConfirmed = confirm('Are you sure you want to permanently delete this item?');
                if (isConfirmed) {
                    // Trigger the postback for permanent delete
                    __doPostBack('<%= btnConfirmDelete.UniqueID %>', '');
            // Hide modal after confirmation
            document.getElementById('<%= pnlDeleteConfirmation.ClientID %>').style.display = 'none';
        }
        return false; // Prevent default postback
    } else if (prospectiveDelete) {
        // Trigger the postback for prospective delete
        __doPostBack('<%= btnConfirmDelete.UniqueID %>', '');
        // Hide modal after confirmation
        document.getElementById('<%= pnlDeleteConfirmation.ClientID %>').style.display = 'none';
                return false;
            } else {
                alert('Please select an option!');
                return false; // Prevent postback if no option is selected
            }
        }

        function CancelDelete() {
            // Close the confirmation modal without doing anything
            document.getElementById('<%= pnlDeleteConfirmation.ClientID %>').style.display = 'none';
        }


    </script>

    <style>
        /* Overlay style (blurred background) */
        .modal-overlay {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.5); /* Semi-transparent black */
            backdrop-filter: blur(5px); /* Apply blur effect */
            z-index: 1; /* Behind the modal */
        }

        /* Modal container styles */
        .modalPopup {
            padding: 20px;
            background-color: white;
            border: 1px solid #ccc;
            border-radius: 8px;
            text-align: center;
            width: 450px;
            max-width: 100%;
            margin: 0 auto;
            position: relative;
            z-index: 2;
        }

        .modal-title {
            font-size: 22px;
            margin-bottom: 15px;
            margin-top: 0;
        }

        .radio-options {
            display: flex;
            justify-content: space-evenly;
            align-items: center;
            margin-bottom: 20px;
        }

        .radio-item {
            display: flex;
            align-items: center;
            gap: 8px;
            font-size: 16px;
        }

        .button-group {
            display: flex;
            justify-content: center;
            gap: 25px;
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
         <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
        <div style="margin: 5px">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div class="row">
                <div class="lblError">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
            </div>
            <asp:HiddenField ID="hdnOldText" runat="server" />
            <asp:HiddenField ID="hdnModuleStatus" runat="server" />
            <div class="row" id="divaddoption">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                            <h4 class="box-title" align="left">Add Field Option
                            </h4>
                            <span id="lblStatus" style="font-size: 16px; color: darkorange; font-weight: bold; margin-left: 71px;"></span>
                        </div>
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <div class="col-md-4">
                                                    <label>
                                                        Field Name:</label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:Label ID="lblField" runat="server" class="form-control-model" style="color:Green;font-weight:bold; margin-right: 50px; overflow-y: auto; max-height: 100px; min-height: 21px; min-width: 250px;"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="col-md-4">
                                                    <label>
                                                        Variable Name:</label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:Label ID="lblVariable" runat="server" CssClass="form-control"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <div class="col-md-4">
                                                    <label>
                                                        Parent Field:</label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:DropDownList runat="server" ID="ddlParentField" Style="overflow-y: auto; max-height: 100px; min-height: 21px; min-width: 300px; max-width: 390px;" CssClass="form-control-model required"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlParentField_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="col-md-4">
                                                    <label>
                                                        Parent Answer :</label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:DropDownList runat="server" ID="ddlParentANS" Style="overflow-y: auto; max-height: 100px; min-height: 21px; min-width: 300px;" CssClass="form-control-model required">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <div class="col-md-4">
                                                    <label>
                                                        Seq No :</label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtSeqNo" runat="server" CssClass="form-control numeric required  text-center"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="col-md-4">
                                                    <label>
                                                        Text:</label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtText" runat="server" TextMode="MultiLine" CssClass="form-control required width300px" MaxLength="10000"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                      <div class="row" id="divPGL_Type" runat="server" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-6">
                                                        <div class="col-md-4">
                                                            <label>
                                                                PGL Type</label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:DropDownList ID="drp_PGL_Type" runat="server" class="form-control drpControl required">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Retrospective" Value="Retrospective"></asp:ListItem>
                                                                <asp:ListItem Text="Prospective" Value="Prospective"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                &nbsp;
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Button ID="btnsubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                    OnClick="btnsubmit_Click" />
                                                <asp:Button ID="btnupdate" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                    OnClick="btnupdate_Click" />
                                                <asp:Button ID="btncancel" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm"
                                                    OnClick="btncancel_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
            <div class="box box-primary" style="float: left;">
                <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                    <h4 class="box-title" align="left">Records
                    </h4>
                </div>
                <div class="box-body">
                    <div align="left" style="margin-left: 5px">
                        <div>
                            <div class="rows">
                                <div style="width: 100%; height: 390px; overflow: auto;">
                                    <div>
                                        <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                            Style="width: 96%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdData_RowCommand" OnPreRender="grdData_PreRender"
                                            OnRowDataBound="grdData_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                            CommandName="EditModule" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Parent Ans.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtParentANS" runat="server" Text='<%# Bind("ParentANS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SEQNO" ItemStyle-Width="2%" ControlStyle-Width="2%"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SEQNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TEXT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtTEXT" runat="server" Text='<%# Bind("TEXT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="PGL Type" ItemStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PGL_TYPE" runat="server" Style="text-align: left" Text='<%# Bind("PGL_TYPE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('DM_DRP_DWN_MASTER', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                    <HeaderTemplate>
                                                        <label>Entered By Details</label><br />
                                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Entered By]</label><br />
                                                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div>
                                                            <div>
                                                                <asp:Label ID="ENTEREDBYNAME" runat="server" Text='<%# Bind("ENTEREDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                            </div>
                                                            <div>
                                                                <asp:Label ID="ENTERED_CAL_DAT" runat="server" Text='<%# Bind("ENTERED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                            </div>
                                                            <div>
                                                                <asp:Label ID="ENTERED_CAL_TZDAT" runat="server" Text='<%# Bind("ENTERED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                    <HeaderTemplate>
                                                        <label>Last Modified Details</label><br />
                                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Modified By]</label><br />
                                                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div>
                                                            <div>
                                                                <asp:Label ID="UPDATEDBYNAME" runat="server" Text='<%# Bind("UPDATEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                            </div>
                                                            <div>
                                                                <asp:Label ID="UPDATED_CAL_DAT" runat="server" Text='<%# Bind("UPDATED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                            </div>
                                                            <div>
                                                                <asp:Label ID="UPDATED_CAL_TZDAT" runat="server" Text='<%# Bind("UPDATED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>

                                                        <asp:LinkButton ID="lbtndeleteSection" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="DeleteModule" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>

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
            <asp:HiddenField runat="server" ID="hdnINVID" />
        </div>
                </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
        <div id="popup_AuditTrail" title="Audit Trail" class="disp-none">
            <div id="DivAuditTrail" style="font-size: small;">
            </div>
        </div>
        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="VisitAddOption" TargetControlID="Button_Popup" BackgroundCssClass="Background"></cc1:ModalPopupExtender>
        <asp:Panel ID="VisitAddOption" runat="server" Style="display: none;" CssClass="Popup1">
            <asp:Button runat="server" ID="Button_Popup" Style="display: none" />
            <h5 class="heading">Visit Linked Add Option</h5>
            <div class="row">
                <div class="col-md-3" style="width: 121px;">
                    <asp:Label ID="visitlbl" runat="server" Style="margin-left: 10px;" ForeColor="Black" Font-Bold="true" Text=" Field Name:"></asp:Label>
                </div>
                <div class="col-md-9">
                    <asp:Label ID="LlbFieldevisit" runat="server" class="form-control-model" style="color:Green;font-weight:bold; margin-right: 50px; overflow-y: auto; max-height: 100px; min-height: 21px; min-width: 250px;" ForeColor="Green" Font-Bold="true" Text=""></asp:Label>

                </div>
            </div>
            <div class="row" style="margin-top: 7px;">
                <div class="col-md-12">
                    <asp:Label ID="Label6" runat="server" Style="margin-left: 10px; margin-right: 10px;" Font-Bold="true" Text="Do you want add this field option to below visit than select below visit?"></asp:Label>
                </div>
            </div>
            <br />
            <div class="modal-body" runat="server">
                <div id="ModelPopup">
                    <div class="row">
                        <div style="width: 95%; min-height: auto; max-height: 170px; overflow: auto;">
                            <div>
                                <asp:GridView ID="grdVisitLinkedAddOption" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" Style="width: 91%; margin-left: 25px;">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="Chk_VISIT" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="VISITNUM" runat="server" Text='<%# Bind("VISITNUM") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Visit" HeaderStyle-CssClass="align-left">
                                            <ItemTemplate>
                                                <asp:Label ID="VISIT" runat="server" Text='<%# Bind("VISIT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-4">
                        &nbsp;
                    </div>
                    <div class="col-md-6">
                        <asp:Button ID="btnSubmitLinkedAddVisitOption" runat="server" CssClass="btn btn-DarkGreen" Height="30px" Width="60px" Text="Yes" OnClick="btnSubmitLinkedAddVisitOption_Click" OnClientClick="return countCheckboxes();" />&nbsp;&nbsp;
                    <asp:Button ID="btnCancelLinkedAddVisitOption" runat="server" Text="No" CssClass="btn btn-danger" Height="30px" Width="60px" OnClick="btnCancelLinkedAddVisitOption_Click" />
                    </div>
                </div>
                <br />
            </div>
        </asp:Panel>
        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="VisitUpdateOption" TargetControlID="Button_Popup1" BackgroundCssClass="Background"></cc1:ModalPopupExtender>
        <asp:Panel ID="VisitUpdateOption" runat="server" Style="display: none;" CssClass="Popup1">
            <div class="modal-body" runat="server" style="width: 100%;">
                <div id="ModelPopup1">
                    <asp:Button runat="server" ID="Button_Popup1" Style="display: none" />
                    <h5 class="heading">Visit Linked Update Option</h5>
                    <div class="row">
                        <div class="col-md-3">
                            <asp:Label ID="Label4" runat="server" Style="margin-left: 10px;" ForeColor="Black" Font-Bold="true" Text="Field Name:"></asp:Label>
                        </div>
                        <div class="col-md-9">
                            <asp:Label ID="Label4Field" runat="server" Style="margin-left: 10px;" ForeColor="Green" Font-Bold="true"></asp:Label>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 7px;">
                        <div class="col-md-12">
                            <asp:Label ID="Label2" runat="server" Style="margin-left: 10px; margin-right: 10px;" Font-Bold="true" Text="These changes will apply to all visits where the module is present."></asp:Label>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div style="width: 95%; min-height: auto; max-height: 170px; overflow: auto;">
                            <div>
                                <asp:GridView ID="grdVisitLinkedUpdateOption" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" Style="width: 91%; margin-left: 25px;">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="VISITNUM" runat="server" Text='<%# Bind("VISITNUM") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Visit" HeaderStyle-CssClass="align-left">
                                            <ItemTemplate>
                                                <asp:Label ID="VISIT" runat="server" Text='<%# Bind("VISIT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-4">
                                &nbsp;
                            </div>
                            <div class="col-md-6">
                                <asp:Button ID="btnUpdateLinkedVisitOption" runat="server" CssClass="btn btn-DarkGreen" Height="32px" Font-Size="Larger" Width="60px" Text="Yes" OnClick="btnUpdateLinkedVisitOption_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btnCancelLinkedVisitOption" runat="server" Text="Cancel" Height="32px" Width="60px" Font-Size="Larger" CssClass="btn btn-danger" OnClick="btnCancelLinkedVisitOption_Click" />
                            </div>
                        </div>
                    </div>
                    <br />
                </div>
            </div>
        </asp:Panel>
    </ContentTemplate>
        </asp:UpdatePanel>

                <asp:UpdatePanel ID="upModalPopup" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <!-- Hidden button to trigger ModalPopupExtender -->
                <asp:Button ID="btnShowDeletePopup" runat="server" Style="display: none;" />

                <!-- Delete confirmation modal -->
                <asp:Panel ID="pnlDeleteConfirmation" runat="server" CssClass="modalPopup" Style="display: none;">
                    <h3 class="modal-title">Confirm Action</h3>

                    <!-- Radio buttons for delete type -->
                    <div class="radio-options">
                        <asp:RadioButton ID="rdoPermanentDelete" runat="server" GroupName="DeleteType" Text="Permanent Delete" CssClass="radio-item" />
                        <asp:RadioButton ID="rdoProspectiveDelete" runat="server" GroupName="DeleteType" Text="Mark As Delete" CssClass="radio-item" />
                    </div>

                    <!-- Hidden field to store ID -->
                    <asp:HiddenField ID="hdnDeleteFieldID" runat="server" />
                    <asp:HiddenField ID="hdnVariableName" runat="server" />

                    <!-- Submit and cancel buttons -->
                    <div class="button-group">
                        <asp:Button ID="btnConfirmDelete" runat="server" Text="Submit" CssClass="btn btn-success btn-sm" OnClientClick="return ConfirmDeleteAction();" OnClick="btnConfirmDelete_Click" />
                        <asp:Button ID="btnCancelDelete" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm" OnClick="btnCancelDelete_Click" />
                    </div>

                </asp:Panel>

                <!-- ModalPopupExtender -->
                <ajaxToolkit:ModalPopupExtender
                    ID="mpeDeleteConfirmation"
                    runat="server"
                    TargetControlID="btnShowDeletePopup"
                    PopupControlID="pnlDeleteConfirmation"
                    BackgroundCssClass="Background">
                </ajaxToolkit:ModalPopupExtender>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
