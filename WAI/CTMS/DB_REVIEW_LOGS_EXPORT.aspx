<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DB_REVIEW_LOGS_EXPORT.aspx.cs" Inherits="CTMS.DB_REVIEW_LOGS_EXPORT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script type="text/javascript" src="CommonFunctionsJs/TabIndex.js"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }

        .Mandatory {
            border: solid 1px !important;
            border-color: Red !important;
        }

        .REQUIRED {
            background-color: yellow;
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning" runat="server" id="content">
        <div class="box-header">
            <h3 class="box-title">Module Reviews Logs
            </h3>
        </div>
        <div>
            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-2">
                    <label>
                        Select Module:</label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList Style="width: 250px;" ID="drpModule" runat="server" class="form-control drpControl required1 select"
                        AutoPostBack="True" OnSelectedIndexChanged="drpModule_SelectedIndexChanged" TabIndex="1">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnShowReview" runat="server" Text="Show" CssClass="btn btn-primary btn-sm cls-btnSave1" OnClick="btnShowReview_Click" TabIndex="2" />
                </div>
            </div>
        </div>
        <br />
    </div>
    <div class="box box-primary" runat="server" id="divRecord">
        <div class="box-header with-border">
            <h4 class="box-title" align="left">Records
            </h4>
            <div class="pull-right" style="padding-top: 4px; margin-right: 10px;">
                <asp:LinkButton ID="btnExportExcel" runat="server" Text="Export Reviews Logs" OnClick="btnExportExcel_Click" CssClass="btn btn-info btn-sm" ForeColor="White" Font-Bold="true" Visible="false"> Export Reviews Logs&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
            </div>
        </div>
        <div class="box-body">
            <div class="rows">
                <div class="fixTableHead">
                    <asp:GridView ID="grdReviewLogs" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered table-striped Datatable"
                        OnPreRender="grdField_PreRender" EmptyDataText="No Record Found.">
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
