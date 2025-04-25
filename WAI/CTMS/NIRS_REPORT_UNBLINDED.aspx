<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIRS_REPORT_UNBLINDED.aspx.cs" Inherits="CTMS.NIRS_REPORT_UNBLINDED" %>

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
            width: 180pt;
            height: 20pt;
        }
    </style>
    <script type="text/javascript" language="javascript">

        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
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
                <asp:Label runat="server" ID="lblHeader" Text="Blinded Report (Unblinding status)"></asp:Label>
            </h3>
       
            <div class="pull-right">
                <%--<asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" Font-Size="12px" Style="margin-top: 3px;" CssClass="btn btn-info" ForeColor="White">Export Report&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>--%>
                <a href="#" class="dropdown-toggle btn-info prevent-refresh-button" data-toggle="dropdown" style="color: #FFFFFF">Export Blinded Report (Unblinding status)&nbsp;<span class="glyphicon glyphicon-download"></span></a>
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
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                                    <asp:DropDownList ID="ddlSite" runat="server" AutoPostBack="True" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlSite_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
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
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
                <asp:PostBackTrigger ControlID="btnExportPDF" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
