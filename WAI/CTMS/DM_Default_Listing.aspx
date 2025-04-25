<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DM_Default_Listing.aspx.cs" Inherits="CTMS.DM_Default_Listing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
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
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                <asp:Label ID="lblHeader" Text="Default Listing" runat="server"></asp:Label>
            </h3>
        </div>
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700; font-size: 15px;"></asp:Label>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="row">
                    <div class="col-md-5">
                        <div style="display: inline-flex">
                            <label class="label width100px">
                                Select Module:
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="ddMODULE" runat="server" class="form-control width300px select required" AutoPostBack="true" OnSelectedIndexChanged="ddMODULE_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div style="display: inline-flex">
                            <label class="label width60px">
                                Country:
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpCountry" runat="server" class="form-control drpControl"
                                    AutoPostBack="true" OnSelectedIndexChanged="drpCountry_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div style="display: inline-flex">
                            <label class="label width60px">
                                Site ID:
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpInvID" runat="server" AutoPostBack="True" CssClass="form-control"
                                    OnSelectedIndexChanged="drpInvID_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-5">
                        <div style="display: inline-flex">
                            <label class="label width100px">
                                Subject ID:
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpSubID" runat="server" CssClass="form-control select" AutoPostBack="True" SelectionMode="Single">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div style="display: inline-flex">
                            <asp:Button ID="btngetdata" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave"
                                OnClick="btngetdata_Click" />&nbsp&nbsp&nbsp&nbsp
                        <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" ToolTip="Export to Excel" CssClass="btn btn-info btn-sm"
                            Text="Export to Excel" ForeColor="White">Export to Excel&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>&nbsp&nbsp&nbsp&nbsp
                            <asp:LinkButton runat="server" ID="btnArchival" OnClick="btnArchival_Click" ToolTip="Archival All Module Listing" CssClass="btn btn-info btn-sm"
                            Text="Archival All Module Listing" ForeColor="White">Archival All Module Listing&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                        </div>
                    </div>
                </div>
                <br />
                <div class="box-body">
                    <div class="rows">
                        <div class="fixTableHead">
                            <asp:GridView ID="gridData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable table-striped">
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
