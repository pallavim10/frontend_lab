<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DB_DM_ACTIVITY_AUDIT_TRAIL.aspx.cs" Inherits="CTMS.DB_DM_ACTIVITY_AUDIT_TRAIL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="CommonFunctionsJs/btnSave_Required.js"></script>
    <script type="text/javascript" src="CommonFunctionsJS/TabIndex.js"></script>
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
            <h3 class="box-title">DB Activity Audit Trail Report
            </h3>
        </div>
        <div>
            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-2">
                    <label>
                        Select Activity:</label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList Style="width: 250px;" ID="ddlActivity" runat="server" class="form-control drpControl required1"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlActivity_SelectedIndexChanged" TabIndex="1">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="Visits" Text="Visits"></asp:ListItem>
                        <asp:ListItem Value="Visit Visibility" Text="Visit Visibility"></asp:ListItem>
                        <asp:ListItem Value="Visit Level Fields" Text="Visit Level Fields"></asp:ListItem>
                        <asp:ListItem Value="Visit Level Fields Options" Text="Visit Level Fields Options"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div id="divVisit" runat="server" visible="false">
                    <div class="col-md-1 width100px">
                        <label>
                            Select Visit:</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList Style="width: 250px;" ID="drpVisit" runat="server" class="form-control drpControl select"
                            AutoPostBack="True" OnSelectedIndexChanged="drpVisit_SelectedIndexChanged" TabIndex="2">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <div id="divModules" runat="server" visible="false">
                    <div class="col-md-2">
                        <label>
                            Select Module:</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList Style="width: 250px;" ID="drpModule" runat="server" class="form-control drpControl select"
                            AutoPostBack="True" OnSelectedIndexChanged="drpModule_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div id="divFields" runat="server" visible="false">
                    <div class="col-md-1 width100px">
                        <label>Select Field:</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList Style="width: 250px;" ID="ddlFields" runat="server" class="form-control drpControl select"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlFields_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-2">
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnShow" runat="server" Text="Show" Visible="false" CssClass="btn btn-primary btn-sm cls-btnSave1" OnClick="btnShow_Click" />
                    <asp:LinkButton ID="btnExportExcel" Visible="false" runat="server" Text="Export Reviews Logs" OnClick="btnExportExcel_Click" CssClass="btn btn-info btn-sm" ForeColor="White" Font-Bold="true"> Export To Excel&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                </div>
            </div>
        </div>
        <br />
    </div>
    <div class="box box-primary" runat="server" id="divRecord">
        <div class="box-header with-border">
            <h4 class="box-title" align="left">Records
            </h4>
        </div>
        <div class="box-body">
            <div class="rows">
                <div class="fixTableHead">
                    <asp:GridView ID="grdRecords" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered table-striped Datatable"
                        OnPreRender="grdField_PreRender" EmptyDataText="No Record Found.">
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
