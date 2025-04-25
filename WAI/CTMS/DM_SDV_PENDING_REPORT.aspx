    <%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DM_SDV_PENDING_REPORT.aspx.cs" Inherits="CTMS.DM_SDV_PENDING_REPORT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <style type="text/css">
        .fontBlue {
            color: Blue;
            cursor: pointer;
        }
    </style>
    <link href="CommonStyles/Searchable_DropDown.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript">

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

        $(document).ready(function () {

            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true,
                fixedHeader: true
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
                    Text="SDV Pending"></asp:Label>
            </h3>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700; font-size: 15px;"></asp:Label>
                <div class="box-body">
                    <div class="form-group">
                        <div runat="server" id="divSIte" style="display: inline-flex">
                            <div style="display: inline-flex">
                                <label class="label width70px">
                                    Site ID:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="drpInvID" runat="server" AutoPostBack="True" CssClass="form-control "
                                        OnSelectedIndexChanged="drpInvID_SelectedIndexChanged">
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
                                    <asp:DropDownList ID="drpSubID" runat="server" CssClass="form-control select" AutoPostBack="true" OnSelectedIndexChanged="drpSubID_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <asp:Button ID="btngetdata" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave"
                            OnClick="btngetdata_Click" />&nbsp&nbsp&nbsp&nbsp
                        <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" ToolTip="Export to Excel" CssClass="btn btn-info btn-sm"
                            Text="Export to Excel" ForeColor="White">Export to Excel&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                        <br />
                        <br />
                        <div class="box-body">
                            <div class="rows">
                                <div class="fixTableHead">
                                    <asp:GridView ID="gridData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                        OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable table-striped notranslate">
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
