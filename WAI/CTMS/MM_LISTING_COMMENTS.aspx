<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MM_LISTING_COMMENTS.aspx.cs"
    Inherits="CTMS.MM_LISTING_COMMENTS" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="Scripts/Jquery1.12.4.js" type="text/javascript"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Common-Bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />
    <link href="Styles/AdminLTE.css" rel="stylesheet" type="text/css" />
    <link href="Styles/ionicons.css" rel="stylesheet" type="text/css" />
    <link href="Styles/font-awesome.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="img/favicon.ico" type="image/x-icon">
    <link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
    <%-- <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />--%>
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <!-- for pikaday datepicker//-->
    <link href="Styles/pikaday.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/moment.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.jquery.js" type="text/javascript"></script>
    <!-- for pikaday datepicker//-->
    <!-- for jquery Datatable.1.10.15//-->
    <script src="Scripts/Datatable.1.10.15.UI.js" type="text/javascript"></script>
    <script src="Scripts/Datatable1.10.15.js" type="text/javascript"></script>
    <script type="text/javascript">

        function pageLoad() {

            $(".nav-tabs a").click(function (event) {
                event.preventDefault();
                $(this).parent().addClass("active");
                $(this).parent().siblings().removeClass("active");
                var tab = $(this).attr("href");
                $(".tab-content").not(tab).css("display", "none");
                $(tab).fadeIn();
            });
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

        $(document).on("click", ".cls-btnSave1", function () {
            var test = "0";

            $('.required1').each(function (index, element) {
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
    <style type="text/css">
        .fontBlue {
            color: Blue;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
        <div class="page">
            <asp:ScriptManager ID="script1" runat="server">
            </asp:ScriptManager>
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
            </div>
            <div id="tab" class="nav-tabs-custom ">
                <div id="tab-1" class="tab-content active">
                    <div class="box box-warning">
                        <div class="box-header">
                            <h3 class="box-title">Comments </h3>
                        </div>
                        <div class="form-group">
                            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped">
                                <Columns>
                                    <asp:TemplateField HeaderText="Comments">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCOMMENTS" runat="server" Text='<%# Eval("COMMENTS") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="align-left">
                                        <HeaderTemplate>
                                            <label>Comment Details</label><br />
                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Commented By]</label><br />
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
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="box box-warning">
                        <div class="box-header">
                            <h3 class="box-title">Add New Comment
                            </h3>
                        </div>
                        <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                            <label class="col-lg-3 width100px label">
                                Enter Comment</label>
                            <div class="col-lg-2">
                                <asp:TextBox runat="server" ID="txtComment" TextMode="MultiLine" CssClass="form-control width245px required"> 
                                </asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div runat="server" id="divClosePeer" visible="false">
                            <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                                <label class="col-lg-3 width100px label">
                                    Close Peer-Review</label>
                                <div class="col-lg-2">
                                    <asp:CheckBox ID="chkClosePeer" runat="server" />
                                </div>
                            </div>
                            <br />
                        </div>
                        <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                            <label class="col-lg-3 width100px label">
                                &nbsp;</label>
                            <div class="col-lg-2">
                                <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm cls-btnSave"
                                    OnClick="btnsubmit_Click" />
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
