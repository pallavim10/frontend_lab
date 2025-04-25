<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NIWRS_REPORT_KIT_DISP_SUM_UNBLIND.aspx.cs" Inherits="CTMS.NIWRS_REPORT_KIT_DISP_SUM_UNBLIND" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .fontBlue {
            color: Blue;
            cursor: pointer;
        }

        .btn-info {
            background-repeat: repeat-x;
            border-color: #28a4c9;
            /*background-image: linear-gradient(to bottom, #5bc0de 0%, #2aabd2 100%);*/
        }
        
        .prevent-refresh-button {
            display: inline-block;
            padding: 5px 5px;
            margin-bottom: 0;
            font-size: 12px;
            font-weight: normal;
            line-height: 1.428571429;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            cursor: pointer;
            border: 1px solid transparent;
            border-radius: 4px;
            width: 140pt;
            height: 20pt;
        }
    </style>
    <script type="text/javascript" language="javascript">

        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: false,
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
                <asp:Label runat="server" ID="lblHeader" Text="Dosing Summary Unblind"></asp:Label>
            </h3>

            <div class="pull-right">
                <%--<asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" Font-Size="12px" Style="margin-top: 3px;" CssClass="btn btn-info" ForeColor="White">Export Dosing Summary Unlind&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>--%>
                <a href="#" class="dropdown-toggle btn-info prevent-refresh-button" data-toggle="dropdown" style="color: #FFFFFF">Export Dosing Summary Unlind&nbsp;<span class="glyphicon glyphicon-download"></span></a>
                        <ul class="dropdown-menu dropdown-menu-sm">
                            <li>
                                <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" CommandName="Excel" ToolTip="Excel"
                                    Text="Excel" CssClass="dropdown-item" Style="color: #333333;">
                                </asp:LinkButton></li>
                            <hr style="margin: 5px;" />
                            <li>
                                <asp:LinkButton runat="server" ID="btnExportPDF" OnClick="btnExportPDF_Click" CssClass="dropdown-item"
                                    ToolTip="PDF" Text="PDF" Style="color: #333333;">
                                </asp:LinkButton>
                            </li>
                        </ul>
                &nbsp;&nbsp;
            </div>
        </div>
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
            <br />
        </div>
            <ContentTemplate>
                <div class="box-body">
                    <div class="form-group">
                        <div style="display: inline-flex">
                            <div style="display: inline-flex">
                                <label class="label width70px">
                                    Country :
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div style="display: inline-flex">
                            <div style="display: inline-flex">
                                <label class="label width70px">
                                    Site ID:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="ddlSite" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                         <div style="display: inline-flex; margin-bottom: 10px;">
                        <label class="label" for="txtDateFrom">
                            From Date :
                        </label>
                        <div class="Control">
                            <asp:TextBox ID="txtDateFrom" CssClass="form-control txtDate" runat="server" autocomplete="off" Width="120px"></asp:TextBox>
                        </div>
                    </div>
                    <div style="display: inline-flex; margin-bottom: 10px;">
                        <label class="label" for="txtDateTo">
                            To Date :
                        </label>
                        <div class="Control">
                            <asp:TextBox ID="txtDateTo" CssClass="form-control txtDate" runat="server" autocomplete="off" Width="120px"></asp:TextBox>
                        </div>
                    </div>
                    <div style="display: inline-flex;">
                        <asp:Button ID="btnGetdata" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm" OnClick="btnGetdata_Click" />
                    </div>                 
                    <br />
                        <div style="display: none">
                            <label class="label width70px">
                                Sub-Site ID:
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="ddlSubSite" runat="server" AutoPostBack="True" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlSubSite_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="box-body">
                            <div class="rows">
                                <div style="width: 100%; overflow: auto;">
                                    <div>
                                        <asp:GridView ID="gridData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                            OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable table-striped notranslate txt_center">
                                            <Columns>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
    </div>
</asp:Content>
