<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_LISTING_DATA.aspx.cs" Inherits="CTMS.NIWRS_LISTING_DATA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/MM/MM_DivExpand.js"></script>

    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true,
                fixedHeader: true
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                <asp:Label ID="lblHeader" runat="server"></asp:Label>
            </h3>
        </div>
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div runat="server" id="Div2" style="display: inline-flex">
                    <div style="display: inline-flex">
                        <label class="label ">
                            Country:
                        </label>
                        <div class="Control">
                            <asp:DropDownList ID="drpCountry" runat="server" class="form-control drpControl"
                                AutoPostBack="true" OnSelectedIndexChanged="drpCountry_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div runat="server" id="DivINV" style="display: inline-flex">
                    <div class="form-group" style="display: inline-flex">
                        <label class="label ">
                            Site ID:
                        </label>
                        <div class="Control">
                            <asp:DropDownList ID="drpInvID" runat="server" AutoPostBack="True" CssClass="form-control"
                                OnSelectedIndexChanged="drpInvID_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div style="display: inline-flex">
                    <label class="label ">
                        Subject ID:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpSubID" runat="server" CssClass="form-control select" SelectionMode="Single" AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                </div>
                <asp:Button ID="btngetdata" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm"
                    OnClick="btngetdata_Click" />&nbsp&nbsp&nbsp&nbsp
                <div style="display: inline-flex">
                     <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" CssClass="btn btn-info" ForeColor="White">Export Listing&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                </div>
                <div class="pull-right" style="margin-bottom: 10px; margin-right: 10px;">
                    <asp:HiddenField ID="hdnlistid" runat="server" />
                </div>
                <div class="box-body">
                    <div class="rows">
                        <div style="width: 100%; overflow: auto;">
                            <div>
                                <asp:GridView ID="gridData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                    OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable table-striped notranslate">
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
