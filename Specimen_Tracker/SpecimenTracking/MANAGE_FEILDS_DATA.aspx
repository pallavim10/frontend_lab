<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MANAGE_FEILDS_DATA.aspx.cs" Inherits="SpecimenTracking.MANAGE_FEILDS_DATA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Specimen Tracker</title>

    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="plugins/fontawesome-free/css/all.min.css" />
    
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" />
    
    <link rel="stylesheet" href="plugins/icheck-bootstrap/icheck-bootstrap.min.css" />
    <!-- JQVMap -->
    <link rel="stylesheet" href="plugins/jqvmap/jqvmap.min.css" />
    <!-- Select2 -->
    <link rel="stylesheet" href="plugins/select2/css/select2.min.css" />
    <link rel="stylesheet" href="plugins/select2-bootstrap4-theme/select2-bootstrap4.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="dist/css/adminlte.min.css" />
    <link href="dist/css/adminlte.css" rel="stylesheet" />
    <!-- overlayScrollbars -->
    <link rel="stylesheet" href="plugins/overlayScrollbars/css/OverlayScrollbars.min.css" />
    <!-- summernote -->
    <link rel="stylesheet" href="plugins/summernote/summernote-bs4.css" />
    <!-- Google Font: Source Sans Pro -->
    <!-- Google Font: Source Sans Pro -->
    <link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700" rel="stylesheet" />

    <script src="plugins/jquery/jquery.min.js" type="text/javascript"></script>
    
    <script src="plugins/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        $.widget.bridge('uibutton', $.ui.button)
    </script>
    <!-- Bootstrap 4 -->
    <script src="plugins/bootstrap/js/bootstrap.bundle.min.js" type="text/javascript"></script>
    <!-- Select2 -->
    <script src="plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
    
    <script src="plugins/summernote/summernote-bs4.min.js" type="text/javascript"></script>
    <!-- overlayScrollbars -->
    <script src="plugins/overlayScrollbars/js/jquery.overlayScrollbars.min.js" type="text/javascript"></script>
    <!-- AdminLTE App -->
    <script src="dist/js/adminlte.js" type="text/javascript"></script>
    <!-- AdminLTE for demo purposes -->
    <script src="dist/js/demo.js" type="text/javascript"></script>
    <script src="plugins/flot/jquery.flot.js" type="text/javascript"></script>
    <!-- FLOT RESIZE PLUGIN - allows the chart to redraw when the window is resized -->
    <script src="plugins/flot/plugins/jquery.flot.resize.js" type="text/javascript"></script>
    <!-- FLOT PIE PLUGIN - also used to draw donut charts -->
    <script src="plugins/flot/plugins/jquery.flot.pie.js" type="text/javascript"></script>
    <script src="dist/js/sweetalert.min.js" type="text/javascript"></script>
    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }
    </style>
    <style type="text/css">
        .Body {
            margin-left: 7%;
        }

        .brd-1px-redimp {
            border: solid 1px !important;
            border-color: Red !important;
        }

        .Margin7 {
            margin-left: 7%;
            margin-right: 7%;
            font-size: larger;
            margin-top: 1%;
        }

        .box {
            margin-bottom: 0%;
            margin-bottom: 0px;
            width: auto;
        }

        .fontBold {
            font-weight: bold;
        }

        .color {
            color: #333333;
        }

        .colorRed {
            color: #ff0000;
        }

        .hover-end {
            padding: 0;
            margin: 0;
            font-size: 75%;
            text-align: center;
            position: absolute;
            bottom: 0;
            width: 100%;
            opacity: 0.8;
        }

        .nav-tabs-custom > .deepak > .nav-tabs > li.active {
            border-top-color: #3c8dbc;
            border-top-style: solid;
            border-top: 3px solid #3c8dbc;
            margin-bottom: -2px;
            margin-right: 5px;
            position: relative;
            list-style: none;
        }

        .pull-right {
            float: right !important;
        }

        .pull-left {
            float: left !important;
        }

        .hide {
            display: none !important;
        }

        .show {
            display: block !important;
        }

        .invisible {
            visibility: hidden;
        }

        .hidden {
            display: none !important;
        }

        .txt_center {
            text-align: center;
            padding: 1px 22px;
        }

        .text_center {
            text-align: center;
        }



        .fade-in-out {
            /*
            background-color: white;
            opacity: 0;*/
        }

            .fade-in-out.animate {
                animation: fadeInOut 0.7s forwards;
            }

        @keyframes fadeInOut {
            0% {
                opacity: 0;
                background-color: #fff;
            }

            50% {
                opacity: 0.7;
                background-color: #17a2b85e;
            }

            100% {
                opacity: 0;
                background-color: #fff;
            }
        }
    </style>
    <style type="text/css">
        .hide {
            display: none;
        }


        .brd-1px-redimp {
            border: solid 1px !important;
            border-color: Red !important;
        }

        .brd-1px-maroonimp {
            border: solid !important;
            border-color: Maroon !important;
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
            $('.select').select2();

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": true,
                stateSave: true
            });
           

            $('.select').select2();

        }

        $(document).ready(function () {
            var dropdown = $('#<%= drpControlType.ClientID %>');

            dropdown.change(function () {
                var selectedValue = $(this).val();
                var divMaxlength = $('#Max_Length');
                var txtMaxLength = $('#txtMaxLength');

                if (selectedValue === 'Textbox') {
                    divMaxlength.removeClass('d-none');
                    txtMaxLength.addClass('required');
                } else {
                    divMaxlength.addClass('d-none');
                    txtMaxLength.removeClass('required');
                }
            });
        });

        function showAuditTrail(element) {
            var ID = $(element).closest('tr').find('td').eq(0).text().trim();
            var TABLENAME = 'FIELD_OPTIONS';

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/SETUP_showAuditTrail",
                data: JSON.stringify({ TABLENAME: TABLENAME, ID: ID }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d === 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    } else {
                        $('#DivAuditTrail').html(response.d);
                        $('#modal-lg').modal('show'); // Show the modal after populating it
                    }
                },
                error: function (xhr, status, error) {
                    console.error('Error fetching audit trail:', status, error);
                    alert("An error occurred. Please contact the administrator.");
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
    <script type="text/javascript">
        
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

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <div class="card card-info">
                        <div class="card-header">
                            <h3 class="card-title">Manage Additional Properties</h3>

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
                                            <h3 class="card-title">Manage Properties</h3>
                                        </div>
                                        <div class="card-body">
                                            <div class="rows">
                                                <div class="col-md-12">
                                                    <div class="row">
                                                        <div class="col-md-12" id="divControl" runat="server" visible="false">
                                                            <div class="form-group">
                                                                <label>Control Type: &nbsp;</label>
                                                                <asp:Label ID="Label3" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                                <asp:DropDownList ID="drpControlType" runat="server" AutoPostBack="false" class="form-control drpControl w-75 required select" SelectionMode="Single">
                                                                    <asp:ListItem Value="0" Selected="True" Text="--Select--"></asp:ListItem>
                                                                    <asp:ListItem Value="Textbox" Text="Textbox"></asp:ListItem>
                                                                    <asp:ListItem Value="Dropdown" Text="Dropdown"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-12" id="divVerifyMaster" runat="server" visible="false">
                                                            <div class="form-group">
                                                                <label>Verify with Master: &nbsp;</label>
                                                                <asp:Label ID="Label6" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                                <asp:DropDownList ID="drpVerifyMaster" runat="server" AutoPostBack="false" class="form-control drpControl w-75 required select" SelectionMode="Single">
                                                                    <asp:ListItem Value="0" Selected="True" Text="--Select--"></asp:ListItem>
                                                                    <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                                    <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                        </div>
                                                        <div class="col-md-12" id="divMaxLength" runat="server" visible="false">
                                                            <div class="form-group d-none" runat="server" id="Max_Length">
                                                                <label>Maximum Length : &nbsp;</label>
                                                                <asp:Label ID="Label7" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                                <asp:TextBox ID="txtMaxLength" runat="server" CssClass="form-control w-75" MaxLength="3"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-12">
                                                            <div class="form-group" id="divverifySID" runat="server" visible="false">
                                                                <label>Verify with SID: &nbsp;</label>
                                                                <asp:Label ID="Label8" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                                <asp:DropDownList ID="drpVerifySID" runat="server" AutoPostBack="false" class="form-control drpControl w-75 required select" SelectionMode="Single">
                                                                    <asp:ListItem Value="0" Selected="True" Text="--Select--"></asp:ListItem>
                                                                    <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                                    <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <center>
                                                                <asp:LinkButton runat="server" ID="btnSubmit" Text="Submit" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnSubmit_Click"></asp:LinkButton>
                                                                &nbsp;&nbsp;&nbsp;
                                                                 <asp:LinkButton runat="server" ID="btnCancel" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm" OnClick="btnCancel_Click"></asp:LinkButton>
                                                            </center>
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
            </section>
        </div>
    </form>
    <script type="text/javascript">
        
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

    </script>
    
</body>
</html>
