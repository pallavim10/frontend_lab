<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DM_REFRESH_LAB_REF_RANGE.aspx.cs" Inherits="CTMS.DM_REFRESH_LAB_REF_RANGE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="CommonFunctionsJs/btnSave_Required.js"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script type="text/javascript" src="CommonFunctionsJs/TabIndex.js"></script>
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title" style="width: 100%;">
                <asp:Label ID="lblHeader" Text="Define Lab Reference Range" runat="server"></asp:Label>
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
                <div class="box box-primary">
                    <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                        <h4 class="box-title" align="left">Refresh Lab Reference Range
                        </h4>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <label>
                                        Select Site:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlSITE" Width="280px" runat="server" class="form-control drpControl required"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSITE_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label>
                                        Select Lab Name:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlLabName" Width="280px" runat="server" class="form-control drpControl"
                                        utoPostBack="true" OnSelectedIndexChanged="ddlLabName_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <label>
                                        Select Test Name:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlTestName" Width="280px" runat="server" class="form-control drpControl"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlTestName_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label>
                                        Select Module Name:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="drpModuleName" Width="280px" runat="server" class="form-control drpControl required"
                                        AutoPostBack="true" OnSelectedIndexChanged="drpModuleName_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-5">
                                    &nbsp;
                                </div>
                                <div class="col-md-7">
                                    <asp:Button ID="btnShow" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                        OnClick="btnShow_Click" />
                                    <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm"
                                        OnClick="btnCancel_Click" />
                                    <asp:Button ID="btnRefresh" Text="Refresh Data" runat="server" CssClass="btn btn-DarkGreen btn-sm cls-btnSave"
                                        Visible="false" OnClick="btnRefresh_Click" />
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
                <div class="box box-danger">
                    <div class="box-header">
                        <h4 class="box-title" align="left">Record</h4>
                        <div class="pull-right" style="margin-right: 10px;">
                            <asp:LinkButton runat="server" ID="btnExport" Visible="false" OnClick="btnExport_Click" CssClass="btn btn-info btn-sm" ForeColor="White" Style="margin-top: 3px"> Export To Excel&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                        </div>
                    </div>
                    <div class="box-body">
                        <div align="left" style="margin-left: 5px">
                            <div>
                                <div class="rows">
                                    <div class="fixTableHead">
                                        <asp:GridView ID="grd_Records" runat="server" CellPadding="3" AutoGenerateColumns="True"
                                            CssClass="table table-bordered table-striped Datatable txt_center" ShowHeader="True" EmptyDataText="No Records Available"
                                            CellSpacing="2" OnPreRender="grd_data_PreRender">
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
