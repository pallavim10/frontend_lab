<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_EVENT_HISTORY.aspx.cs" Inherits="CTMS.NIWRS_EVENT_HISTORY" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    
    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
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
            width: 100pt;
            height: 20pt;
        }
    </style>
    <script>
        function pageLoad() {
            $('.select').select2();
            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: false,
                fixedHeader: true
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');

            //var table = $(".Datatable").DataTable(); 
            //table.search('').draw(); 
            //$('.dataTables_filter input').val('');

            $(document).on("click", ".cls-btnSave1", function () {
                var test = "0";

                $('.required').each(function (index, element) {
                    var value = $(this).val();
                    var ctrl = $(this).prop('type');

                    if (ctrl == "select-one") {
                        if (value == "-1" || value == null || value == "-Select-" || value == "--Select--" || value == "0") {
                            $(this).addClass("brd-1px-redimp");
                            test = "1";
                        }
                    }
                });

                if (test == "1") {
                    return false;
                }
                return true;
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                <asp:Label runat="server" ID="lblHeader" Text="Event History"></asp:Label>
            </h3>
            <div class="pull-right">
                <a href="#" class="dropdown-toggle btn-info prevent-refresh-button" data-toggle="dropdown" style="color: #FFFFFF">Export Event History&nbsp;<span class="glyphicon glyphicon-download"></span></a>
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
            </div>

        </div>
        <div class="form-group">
            <div class="has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
            <div class="rows">
                <div style="display: inline-flex">
                    <div style="display: inline-flex">
                        <label class="label width60px">
                            Site ID:
                        </label>
                        <div class="Control">
                            <asp:DropDownList runat="server" ID="ddlSite" CssClass="form-control required width200px"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlSite_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div style="display: inline-flex">
                    <div style="display: inline-flex">
                        <label class="label width70px">
                            <asp:Label ID="lbSubject" runat="server" text=""></asp:Label> :
                        </label>
                        <div class="Control">
                            <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control  select" SelectionMode="Single">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div style="display: inline-flex">
                    <label class="label ">
                        From Date :
                    </label>
                    <div class="Control" style="display: inline-flex">
                        <asp:TextBox ID="txtDateFrom" CssClass="form-control txtDate" runat="server" autocomplete="off"
                            Width="120px"></asp:TextBox>
                    </div>
                </div>
                <div runat="server" id="Div1" style="display: inline-flex">
                    <div style="display: inline-flex">
                        <label class="label ">
                            To Date :
                        </label>
                        <div class="Control" style="display: inline-flex">
                            <asp:TextBox ID="txtDateTo" CssClass="form-control txtDate" runat="server" autocomplete="off"
                                Width="120px"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div runat="server" id="Div2" style="display: inline-flex">
                    <div style="display: inline-flex">
                        <asp:Button ID="btnGet" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave"
                            OnClick="btnGet_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <%--<asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" CssClass="btn btn-info btn-sm" ForeColor="White">Export Event History&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>--%>
                    </div>
                </div>

                <br />
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div style="width: 100%; overflow: auto;">
                            <div>
                                <asp:GridView ID="grdData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                    Width="100%" OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable table-striped">
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
