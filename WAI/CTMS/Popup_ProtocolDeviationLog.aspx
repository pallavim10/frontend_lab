<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Popup_ProtocolDeviationLog.aspx.cs"
    Inherits="CTMS.Popup_ProtocolDeviationLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Diagonsearch</title>
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
    <script src="Scripts/ClientValidation.js" type="text/javascript"></script>
    <!-- for pikaday datepicker//-->
    <link href="Styles/pikaday.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/moment.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.jquery.js" type="text/javascript"></script>
    <!-- for pikaday datepicker//-->
    <link href="Styles/jquery.alerts.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery.alerts.js" type="text/javascript"></script>
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script type="text/javascript">

        function DecodeUrl(url) {

            var dec = decodeURI(url);

            window.location.href = dec;
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


        // js prototype
        if (typeof (Number.prototype.isBetween) === "undefined") {
            Number.prototype.isBetween = function (min, max, notBoundaries) {
                var between = false;
                if (notBoundaries) {
                    if ((this < max) && (this > min)) between = true;
                } else {
                    if ((this <= max) && (this >= min)) between = true;
                }
                return between;
            }
        }


        $(document).ready(function () {
            $(".nav-tabs a").click(function (event) {
                event.preventDefault();
                $(this).parent().addClass("active");
                $(this).parent().siblings().removeClass("active");
                var tab = $(this).attr("href");
                $(".tab-content").not(tab).css("display", "none");
                $(tab).fadeIn();
            });
        });
        function pageLoad() {
            //CalculateRPN();
            $('.select').select2();
            $('.txtDate').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    // trigger: $(element).closest('div').find('.datepicker-button').get(0), // <<<<
                    // firstDay: 1,
                    //position: 'top right',
                    // minDate: new Date('2000-01-01'),
                    // maxDate: new Date('9999-12-31'),
                    format: 'DD-MMM-YYYY',
                    //  defaultDate: new Date(''),
                    //setDefaultDate: false,
                    yearRange: [1910, 2050]
                });
            });
        }
    </script>
    <script language="javascript" type="text/javascript">
        function PDPopup(element) {


            var PROTVOIL_ID = $(element).closest('tr').find('td:eq(0)').find('span').attr('commandargument');

            var ISSUEID = $(element).closest('tr').find('td:eq(1)').find('span').attr('commandargument');

            var test = "ProtDev.aspx?ISSUEID=" + ISSUEID + "&PROTVOIL_ID=" + PROTVOIL_ID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=700,width=900,resizable=no";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function NewPDPopup() {
            var test = "PDList_AddNew.aspx";
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=700,width=700,resizable=no";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="box box-warning">
            <div class="box-header">
                <h3 class="box-title">
                    Protocol Deviation
                </h3>
            </div>
            <div class="box-body">
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="box box-warning">
            <div class="box-body">
                <asp:Panel ID="Panel4" runat="server" ScrollBars="Both" Height="450" Width="100%">
                    <asp:GridView ID="grdProtVoil" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        CssClass="table table-bordered table-striped Datatable">
                        <Columns>
                            <asp:TemplateField HeaderText="No." HeaderStyle-Width="10px" ItemStyle-Width="10px"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblPROTVOIL_ID" runat="server" Text='<%# Eval("PROTVOIL_ID") %>' CssClass="txt_center disp-none"
                                        CommandArgument='<%#Eval("PROTVOIL_ID") %>' CommandName="PROTVOIL_ID"></asp:Label>
                                    <asp:LinkButton ID="bnt_PROTVOIL_ID" runat="server" Text='<%# Eval("PROTVOIL_ID") %>'
                                        CssClass="txt_center" CommandArgument='<%#Eval("PROTVOIL_ID") %>' CommandName="PROTVOIL_ID"
                                        OnClientClick="return PDPopup(this)">
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="10px" />
                                <ItemStyle Width="10px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Issue ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                                ItemStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:Label ID="lblISSUES_ID" runat="server" Text='<%# Eval("ISSUES_ID") %>' CommandArgument='<%#Eval("ISSUES_ID") %>'
                                        CommandName="ISSUES_ID"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--
                                <asp:TemplateField HeaderText="SUBJID"  HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSUBJID" runat="server" CssClass="color-RedLinkimp" Text='<%# Eval("SUBJID") %>'
                                            CommandArgument='<%#Eval("SUBJID") %>' CommandName="SUBJID"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Department" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                                ItemStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:Label ID="lblDepartment" runat="server" Text='<%# Eval("Department") %>' CommandArgument='<%#Eval("Department") %>'
                                        CommandName="Department"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MVID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:Label ID="lblMVID" runat="server" Text='<%# Eval("MVID") %>' CommandArgument='<%#Eval("MVID") %>'
                                        CommandName="MVID"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="disp-none" />
                                <ItemStyle CssClass="disp-none" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' CommandArgument='<%#Eval("Description") %>'
                                        CommandName="Description"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Summary">
                                <ItemTemplate>
                                    <asp:Label ID="lblSummary" runat="server" Text='<%# Eval("Summary") %>' CommandArgument='<%#Eval("Summary") %>'
                                        CommandName="Summary"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblNature" runat="server" Text='<%# Eval("Nature") %>' CommandArgument='<%#Eval("Nature") %>'
                                        CommandName="MVID"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sub Category">
                                <ItemTemplate>
                                    <asp:Label ID="lblPDCODE1" runat="server" Text='<%# Eval("PDCODE1") %>' CommandArgument='<%#Eval("PDCODE1") %>'
                                        CommandName="PDCODE1"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Factors">
                                <ItemTemplate>
                                    <asp:Label ID="lblPDCODE2" runat="server" Text='<%# Eval("PDCODE2") %>' CommandArgument='<%#Eval("PDCODE2") %>'
                                        CommandName="PDCODE2"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
