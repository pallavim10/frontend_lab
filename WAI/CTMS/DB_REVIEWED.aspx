<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DB_REVIEWED.aspx.cs" Inherits="CTMS.DB_REVIEWED" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="js/Input%20Mask/jquery.inputmask.bundle.js"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <link href="js/plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <link href="CommonStyles/ButtonColor.css" rel="stylesheet" />
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
    <script type="text/javascript" src="CommonFunctionsJs/DM/DM_ConfirmMsg.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/btnSave_Required.js"></script>
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
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
            $('.select').select2();

            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true,
                fixedHeader: true
            });
        }

        function REVIEW_HISTORY(element) {

            var MODULEID = $(element).closest('tr').find('td:eq(0)').find('span').text();

            $.ajax({
                type: "POST",
                url: "DB_REVIEWED.aspx/REVIEW_HISTORY",
                data: '{MODULEID: "' + MODULEID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $('#DivReviewLogs').html(data.d)
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

            $("#popup_ReviewLogs").dialog({
                title: "Review Logs",
                width: 1100,
                height: 450,
                modal: true,
                buttons: {
                    "Close": function () { $(this).dialog("close"); }
                }
            });

            return false;
        }

        function View_Map(element) {

            var MODULEID = $(element).closest('tr').find('td:eq(0)').find('span').html();

            var MODULENAME = $(element).closest('tr').find('td:eq(1)').find('span').html();

            if (MODULEID != "0") {
                var test = "DM_Mappings.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VIEWMAPING=1" + "&SYSTEM=" + $("#MainContent_drpSystem option:selected").val();;
                var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                window.open(test, '_blank');
                return false;
            }
        }
    </script>
    <%--<script type="text/javascript" src="js/MaxLength.min.js"></script>
    <script type="text/javascript" src="js/MaxLength_Limit.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning" runat="server" id="content">
        <div class="box-header">
            <h3 class="box-title">List Of Reviewed Modules
            </h3>
        </div>
        <div>
            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <div class="rows">
            <div class="fixTableHead">
                <asp:GridView ID="grdModule" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                    Style="width: 97%; border-collapse: collapse; margin-left: 20px;" OnPreRender="grdModule_PreRender" OnRowDataBound="grd_Data_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                            ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Module Name">
                            <ItemTemplate>
                                <asp:Label ID="MODULENAME" runat="server" Text='<%# Bind("MODULENAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View Annotated Module Mapping" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnViewMappings" runat="server" ToolTip="View Annotated Module Mapping" OnClientClick="return View_Map(this);">
                                        <i class="fa fa-eye" style="font-size:large;" aria-hidden="true"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Freeze" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="lblFrrezeStatus" runat="server" ForeColor="Maroon" Visible="false"></asp:Label>
                                <asp:LinkButton ID="lbtnFreeze" runat="server" ToolTip="Freeze Module" OnClick="lbtnFreeze_Click" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to Freeze this module : ", Eval("MODULENAME")) %>'>
                                        <img src="Images/FREEZED.png" /></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Review Logs" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnReviewHistory" runat="server" ToolTip="Review Logs" OnClientClick="return REVIEW_HISTORY(this);">
                                        <i class="fa fa-history" style="font-size:large;" aria-hidden="true"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnModuleid" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hdnModulename" runat="server"></asp:HiddenField>
</asp:Content>
