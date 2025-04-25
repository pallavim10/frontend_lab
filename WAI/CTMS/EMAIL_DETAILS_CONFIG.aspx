<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EMAIL_DETAILS_CONFIG.aspx.cs" Inherits="CTMS.EMAIL_DETAILS_CONFIG" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/CKEditor/ckeditor.js" type="text/javascript"></script>
    <script type="text/javascript">
        CKEDITOR.config.toolbar = [
            ['Bold', 'Italic', 'Underline', 'StrikeThrough', '-', 'Undo', 'Redo', '-', 'Outdent', 'Indent', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
            ['Styles', 'Format', 'Font', 'FontSize']
        ];

        CKEDITOR.config.height = 250;

        function CallCkedit() {

            CKEDITOR.replace("MainContent_txtEmailBody");

        }
    </script>
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

        function AddCRITs(element) {
            var ID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var test = "NIWRS_WORKFLOW_CRITs.aspx?ID=" + ID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=1300";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;
        }

        function AddCLICKs(element) {
            var ID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var test = "NIWRS_WORKFLOW_CLICKs.aspx?ID=" + ID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=1300";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;
        }

        $(document).on("click", ".cls-btnSave1", function () {
            var test = "0";

            $('.required1').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "-1" || value == null || value == "-Select-" || value == "--Select--") {
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

        $(document).on("click", ".cls-btnSave2", function () {
            var test = "0";

            $('.required2').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "-1" || value == null || value == "-Select-" || value == "--Select--") {
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

        $(document).on("click", ".cls-btnSave3", function () {
            var test = "0";

            $('.required3').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "-1" || value == "0" || value == null || value == "-Select-" || value == "--Select--") {
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

        function showDivParent() {

            var element = document.getElementById('MainContent_chkNavMenu');
            if ($(element).prop('checked') == true) {
                $('#divNAVPRENT').removeClass('disp-none');
            }
            else if ($(element).prop('checked') == false) {
                $('#divNAVPRENT').addClass('disp-none');
            }

        }

        function showDivNextVisit() {

            var element = document.getElementById('MainContent_ddlVisit');
            if ($(element).val() != "0") {
                $('#divNextVisit').removeClass('disp-none');
            }
            else if ($(element).val() == "0") {
                $('#divNextVisit').addClass('disp-none');
            }

        }

        function ShowEmailDiv() {

            var element = document.getElementById('MainContent_chkSendEmail');
            if ($(element).prop('checked') == true) {
                $('#divEmail').removeClass('disp-none');
            }
            else if ($(element).prop('checked') == false) {
                $('#divEmail').addClass('disp-none');
            }

        }

        function MngDosage(element) {
            var ID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var test = "NIWRS_WORKFLOW_DOSAGE.aspx?ID=" + ID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=1300";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;
        }

    </script>
    <script type="text/javascript">

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title"> Email IDs :</h3>
                </div>
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                </div>
                <div id="divEmail">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-12">
                                <asp:GridView runat="server" ID="gvEmailds" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                    Style="width: 100%; border-collapse: collapse;">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-CssClass="txt_center"  HeaderText="Email Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmailType" runat="server"  Text='<%# Bind("EMAILSTYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="50%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                    runat="server" Text='<%# Bind("EMAILTO") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="50%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                    runat="server" Text='<%# Bind("EMAILCC") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <br />
                <div class="form-group">
                    <div class="row txt_center">
                        <asp:LinkButton ID="lbtnsubmit" Text="Submit" runat="server" Style="color: white;"
                            CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnsubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lbtnCancel" Text="Cancel" runat="server" Style="color: white;"
                                CssClass="btn btn-primary btn-sm" OnClick="lbtnCancel_Click" />
                    </div>
                </div>
                <br />
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
