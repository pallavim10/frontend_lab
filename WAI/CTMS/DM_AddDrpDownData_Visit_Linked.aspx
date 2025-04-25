<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="DM_AddDrpDownData_Visit_Linked.aspx.cs" Inherits="CTMS.DM_AddDrpDownData_Visit_Linked" %>

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

            $(".numeric").on("keypress keyup blur", function (event) {
                $(this).val($(this).val().replace(/[^\d].+/, ""));
                if ((event.which < 48 || event.which > 57)) {
                    event.preventDefault();
                }
            });

            //only for numeric value
            $('.numeric').keypress(function (event) {

                if (event.keyCode == 8 || event.keyCode == 9 || event.charCode == 48 || event.charCode == 49 || event.charCode == 50 || event.charCode == 51
                    || event.charCode == 52 || event.charCode == 52 || event.charCode == 53 || event.charCode == 54 || event.charCode == 55 || event.charCode == 56 || event.charCode == 57) {
                    // let it happen, don't do anything
                    return true;
                }
                else {
                    event.preventDefault();
                }
            });
        }
    </script>
    <script>

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

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 5px">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div class="row">
                <div class="lblError">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                            <h4 class="box-title" align="left">Add Field Option
                            </h4>
                        </div>
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px; margin-right: 5px">
                                <div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <div class="col-md-4">
                                                    <label>
                                                        Visit Name :</label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:Label ID="lblVisit" runat="server" Style="overflow-y: auto; max-height: 100px; min-height: 21px; min-width: 300px;" CssClass="form-control-model"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                &nbsp;
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <div class="col-md-4">
                                                    <label>
                                                        Field Name :</label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:Label ID="lblField" runat="server" Style="overflow-y: auto; max-height: 100px; min-height: 21px; min-width: 300px;" CssClass="form-control-model"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="col-md-4">
                                                    <label>
                                                        Variable Name :</label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:Label ID="lblVariable" runat="server" Style="overflow-y: auto; max-height: 100px; min-height: 21px; min-width: 300px;" CssClass="form-control-model"></asp:Label>
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
                                                        Parent Field :</label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:DropDownList runat="server" ID="ddlParentField" Style="overflow-y: auto; max-height: 100px; min-height: 21px; min-width: 300px;" CssClass="form-control-model required"
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
                                                    <asp:TextBox ID="txtSeqNo" runat="server" ValidationGroup="section" CssClass="form-control-model numeric required "></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="col-md-4">
                                                    <label>
                                                        Text:</label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtText" runat="server" TextMode="MultiLine" MaxLength="10000" CssClass="form-control required width300px"></asp:TextBox>
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
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-primary" style="min-height: 437px;">
                <div class="box-header with-border" style="float: left;">
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
                                            Style="width: 96%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdData_RowCommand" OnPreRender="grdData_PreRender">
                                            <Columns>
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
                                                <asp:TemplateField HeaderStyle-CssClass="align-left">
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
                                                <asp:TemplateField HeaderStyle-CssClass="align-left">
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
                                                        <asp:LinkButton ID="lbtndeleteSection" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                            CommandName="DeleteModule" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
    </form>
</body>
</html>

