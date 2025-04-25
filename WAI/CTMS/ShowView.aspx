<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ShowView.aspx.cs" Inherits="CTMS.ShowView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .fontBlue
        {
            color: Blue;
            cursor: pointer;
        }
    </style>
    <script type="text/javascript" language="javascript">

        function pageLoad() {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": true,
                stateSave: false,
                aaSorting: [[1, 'asc']]
            });

        }

        $(document).ready(function () {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": true,
                stateSave: false,
                aaSorting: [[1, 'asc']]
            });

        });
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title" style="width: 100%;">
                <asp:Label ID="lblHeader" runat="server" Font-Size="12px" Font-Bold="true" Font-Names="Arial"
                    Text=""></asp:Label>
            </h3>
        </div>
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="box-body">
                    <div class="form-group">
                        <div runat="server" id="divSIte" style="display: inline-flex">
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
                        <div runat="server" id="divSubject" style="display: inline-flex">
                            <div style="display: inline-flex">
                                <label class="label width70px">
                                    Subject ID:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="ddlSUBJID" runat="server" AutoPostBack="True" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlSUBJID_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div id="divDownload" class="dropdown" runat="server" style="display: inline-flex">
                            <a href="#" class="dropdown-toggle glyphicon glyphicon-download-alt" data-toggle="dropdown"
                                style="color: #333333" title="Export"></a>
                            <ul class="dropdown-menu dropdown-menu-sm">
                                <li>
                                    <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" ToolTip="Export to Excel"
                                        Text="Export to Excel" CssClass="dropdown-item" Style="color: #333333;">
                                    </asp:LinkButton></li>
                                <hr style="margin: 5px;" />
                                <li>
                                    <asp:LinkButton runat="server" CssClass="dropdown-item" ID="btnPDF" OnClick="btnPDF_Click"
                                        ToolTip="Export to PDF" Text="Export to PDF" Style="color: #333333;">
                                    </asp:LinkButton></li>
                            </ul>
                        </div>
                        <br />
                        <br />
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
                <br />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
                <asp:PostBackTrigger ControlID="btnPDF" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
