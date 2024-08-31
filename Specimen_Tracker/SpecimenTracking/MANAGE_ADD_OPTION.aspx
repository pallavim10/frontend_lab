<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MANAGE_ADD_OPTION.aspx.cs" Inherits="SpecimenTracking.MANAGE_ADD_OPTION" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Specimen Tracker</title>
    
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="plugins/fontawesome-free/css/all.min.css" />
    <!-- Daterange picker -->
    <link rel="stylesheet" href="plugins/daterangepicker/daterangepicker.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" />
    <!-- Tempusdominus Bbootstrap 4 -->
    <link rel="stylesheet" href="plugins/tempusdominus-bootstrap-4/css/tempusdominus-bootstrap-4.min.css" />
    <!-- iCheck -->
    <link rel="stylesheet" href="plugins/icheck-bootstrap/icheck-bootstrap.min.css" />
    <!-- JQVMap -->
    <link rel="stylesheet" href="plugins/jqvmap/jqvmap.min.css" />
    <!-- Select2 -->
    <link rel="stylesheet" href="plugins/select2/css/select2.min.css" />
    <link rel="stylesheet" href="plugins/select2-bootstrap4-theme/select2-bootstrap4.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="dist/css/adminlte.min.css" />
    <!-- overlayScrollbars -->
    <link rel="stylesheet" href="plugins/overlayScrollbars/css/OverlayScrollbars.min.css" />
    <!-- summernote -->
    <link rel="stylesheet" href="plugins/summernote/summernote-bs4.css" />
    <!-- Google Font: Source Sans Pro -->
    <!-- Google Font: Source Sans Pro -->
    <link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700" rel="stylesheet" />

    <script src="plugins/jquery/jquery.js" type="text/javascript"></script>
    <!-- SweetAlert Message Box for Alert Box Popup -->
    <script src="dist/js/sweetalert.min.js" type="text/javascript"></script>
    <script src="plugins/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <link href="plugins/jquery-ui/jquery-ui.css" rel="stylesheet" />
     <script src="plugins/jquery/jquery.min.js" type="text/javascript"></script>
    <!-- jQuery UI 1.11.4 -->
    <script src="plugins/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }
    </style>
    <style type="text/css">
        .brd-1px-redimp {
            border: solid 1px !important;
            border-color: Red !important;
        }

        .brd-1px-maroonimp {
            border: solid !important;
            border-color: Maroon !important;
        }
    </style>
    <style type="text/css">
        .form-control-model {
            display: block;
            padding: 0px 12px;
            font-size: 13px;
            line-height: 2.428571;
            color: #555555;
            vertical-align: middle;
            border: 1px solid #cccccc;
            border-radius: 4px;
            margin-top: 3px !important;
            box-sizing: border-box;
        }
    </style>
    <script type="text/javascript">
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
    <script type="text/javascript">
        function pageLoad() {
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

            $('.select').select2();

        }

        function showAuditTrail(element) {

            var ID = $(element).closest('tr').find('td').eq(0).text().trim();
            var TABLENAME = 'FIELD_OPTIONS';

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/SETUP_showAuditTrail",
                data: '{TABLENAME: "' + TABLENAME + '",ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $('#DivAuditTrail').html(data.d)
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

            $("#popup_AuditTrail").dialog({
                title: "Audit Trail",
                width: 900,
                height: 450,
                modal: true,
                buttons: {
                    "Close": function () { $(this).dialog("close"); }
                }
            });

            return false;
        }
    </script>

    <script type="text/javascript">

        function confirm(event) {
            event.preventDefault();

            swal({
                title: "Warning!",
                text: "Are you sure you want to delete this Record?",
                icon: "warning",
                buttons: true,
                dangerMode: true
            }).then(function (isConfirm) {
                if (isConfirm) {
                    var linkButton = event.target;
                    if (linkButton.tagName.toLowerCase() === 'i') {
                        linkButton = linkButton.parentElement;
                    }
                    linkButton.onclick = null;
                    linkButton.click();
                } else {
                    Response.redirect(this);
                }
            });
            return false;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <div class="card card-info">
                        <div class="card-header">
                            <h3 class="card-title">Manage Options</h3>

                        </div>
                    </div>
                </div>
            </div>
            <section class="content">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="card card-info">
                                        <div class="card-header">
                                            <h3 class="card-title">Add Option</h3>

                                        </div>
                                        <div class="card-body">
                                            <div class="rows">
                                                <div class="col-md-12">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <label>Enter Field Name: &nbsp;</label>
                                                                <asp:Label ID="Label3" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                                <asp:TextBox ID="txtfiledNameOpt" Enabled="false" runat="server" CssClass="form-control required w-75"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <label>Enter Variable Name: &nbsp;</label>
                                                                <asp:Label ID="Label6" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                                <asp:TextBox ID="txtVariableOpt" Enabled="false" runat="server" CssClass="form-control required w-75"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <label>Enter Sequence No : &nbsp;</label>
                                                                <asp:Label ID="Label7" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                                <asp:TextBox ID="txtSquenceNoOpt" runat="server" CssClass="form-control required w-75"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <label>Enter Option: &nbsp;</label>
                                                                <asp:Label ID="Label8" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                                <asp:TextBox ID="txtoption" runat="server" CssClass="form-control required w-75"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <center>
                                                                <asp:LinkButton runat="server" ID="btnaddOption" Text="Submit" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnaddOption_Click"></asp:LinkButton>
                                                                &nbsp;&nbsp;&nbsp;
                                                                                <asp:LinkButton runat="server" ID="btnUpdateOption" Text="Update" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" Visible="false" OnClick="btnUpdateOption_Click"></asp:LinkButton>
                                                                &nbsp;&nbsp;&nbsp;
                                                                                <asp:LinkButton runat="server" ID="btnCancelOption" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm" OnClick="btnCancelOption_Click"></asp:LinkButton>
                                                            </center>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="card card-info">
                                        <div class="card-header">
                                            <h3 class="card-title">Records</h3>
                                        </div>
                                        <div class="card-body">
                                            <div class="rows">
                                                <div class="col-md-12">
                                                    <div style="width: 100%; max-height: 352px; overflow: auto;">
                                                        <div>
                                                            <asp:GridView ID="GrdOption" AutoGenerateColumns="false" runat="server" class="table table-bordered table-striped responsive grid" EmptyDataText="No Data Found!" Width="100%" OnRowCommand="GrdOption_RowCommand">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none" HeaderText="ID">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblid" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtOptEdit" runat="server" class="btn-info btn-sm" CommandArgument='<%# Bind("ID") %>'
                                                                                CommandName="EDITED" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Seq No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblOptSquenceNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Option">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblOption" runat="server" Text='<%# Bind("OPTION_VALUE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnAudttrail" runat="server" class="btn-info btn-sm" OnClientClick="return showAuditTrail(this);" ToolTip="Audit Trail"><i class="fa fa-history"></i></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtOptdelete" runat="server" class="btn-danger btn-sm" CommandArgument='<%# Bind("ID") %>'
                                                                                CommandName="DELETED" OnClientClick="return confirm(event);" ToolTip="Delete"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="popup_AuditTrail" title="Audit Trail" class="disp-none">
                                            <div id="DivAuditTrail" style="font-size: small;">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </form>
</body>
</html>
