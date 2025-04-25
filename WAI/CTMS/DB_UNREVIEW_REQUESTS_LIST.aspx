<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DB_UNREVIEW_REQUESTS_LIST.aspx.cs" Inherits="CTMS.DB_UNREVIEW_REQUESTS_LIST" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="js/Input%20Mask/jquery.inputmask.bundle.js"></script>
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <link href="js/plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <link href="CommonStyles/ButtonColor.css" rel="stylesheet" />
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
    <script type="text/javascript" src="CommonFunctionsJs/DM/DM_ConfirmMsg.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/btnSave_Required.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/TabIndex.js"></script>
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
                url: "DB_UNREVIEW_REQUESTS_LIST.aspx/REVIEW_HISTORY",
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
  <%--  <script type="text/javascript" src="js/MaxLength.min.js"></script>
    <script type="text/javascript" src="js/MaxLength_Limit.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning" runat="server" id="content">
        <div class="box-header">
            <h3 class="box-title">Un-Review Requests List
            </h3>
        </div>
        <div>
            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-2">
                    <label>Select System :</label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" ID="drpSystem" Style="width: 250px;" CssClass="form-control" AutoPostBack="true"
                        OnSelectedIndexChanged="drpSystem_SelectedIndexChanged" TabIndex="1">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <br />
        <div class="rows">
            <div class="fixTableHead">
                <asp:GridView ID="grdModule" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                    Style="width: 97%; border-collapse: collapse; margin-left: 20px;" OnPreRender="grdModule_PreRender">
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
                        <asp:TemplateField ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnAction" Text="Action" runat="server" ToolTip="Un-Review Action" OnClick="lbtnAction_Click">
                                </asp:LinkButton>
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
    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlReview" TargetControlID="lnkReason"
        BackgroundCssClass="Background">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlReview" runat="server" Style="display: none;" CssClass="Popup1">
        <asp:Button runat="server" ID="lnkReason" Style="display: none" />
        <h5 class="heading">Un-Review Request</h5>
        <div class="modal-body" runat="server" style="padding: 10px;">
            <div id="ModelPopup2">
                <div class="row">
                    <div style="width: 100%; height: auto; overflow: auto;">
                        <div>
                            <label id="lblReviewHeader" runat="server" style="color: blue; margin-left: 3%;"></label>
                            <asp:HiddenField ID="hdnModuleid" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hdnModulename" runat="server"></asp:HiddenField>
                        </div>
                        <asp:ListView ID="lstSystems" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <ul>
                                            <li>
                                                <asp:Label runat="server" ID="lblSystemName" Text='<%# Bind("SystemName") %>' ForeColor="Maroon"></asp:Label>
                                            </li>
                                        </ul>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtReason" placeholder="Please enter comment to approve/disapprove unreview request for above systems...." TextMode="MultiLine" MaxLength="500" CssClass="form-control required2" Style="width: 575px; height: 131px;"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-3">
                        &nbsp;
                    </div>
                    <div class="col-md-9">
                        <asp:Button ID="btnApproveUnReviewReq" runat="server" Style="height: 34px; width: 71px; font-size: 14px;" CssClass="btn btn-DarkGreen btn-sm cls-btnSave2"
                            Text="Approve" OnClick="btnApproveUnReviewReq_Click" />
                        &nbsp;
                            &nbsp;
                         <asp:Button ID="btnDisapproveUnReviewReq" runat="server" Style="height: 34px; font-size: 14px;" CssClass="btn btn-DARKORANGE btn-sm cls-btnSave2"
                             Text="Disapprove" OnClick="btnDisapproveUnReviewReq_Click" />
                        &nbsp;
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                CssClass="btn btn-danger" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnCancel_Click" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
