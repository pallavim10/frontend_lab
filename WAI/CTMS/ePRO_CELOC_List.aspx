<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ePRO_CELOC_List.aspx.cs" Inherits="CTMS.ePRO_CELOC_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">

        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);

            if (div.style.display == "none") {
                div.style.display = "inline";
                document.getElementById('img' + divname).className = 'icon-minus-sign-alt';

            } else {
                div.style.display = "none";
                document.getElementById('img' + divname).className = 'icon-plus-sign-alt';
            }
        }

        function ManipulateAll(ID) {
            var img = document.getElementById('img' + ID);

            if (img.className == 'icon-plus-sign-alt') {
                img.className = 'icon-minus-sign-alt'
                $("div[id*='" + ID + "']").css("display", "inline");
                $("i[id*='" + ID + "']").removeClass('icon-plus-sign-alt');
                $("i[id*='" + ID + "']").addClass('icon-minus-sign-alt');
            } else {
                img.className = 'icon-plus-sign-alt'
                $("div[id*='" + ID + "']").css("display", "none");
                $("i[id*='" + ID + "']").removeClass('icon-minus-sign-alt');
                $("i[id*='" + ID + "']").addClass('icon-plus-sign-alt');
            }
        }
        
    </script>
    <script>
        function ViewEventDetails(element, URL) {
            var VALUE = $(element).text();

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=620,width=1300";
            window.location.href = URL;
            return false;

        }
    </script>
    <style type="text/css">
        .fontBlue
        {
            color: Blue;
            cursor: pointer;
            text-align: center;
        }
        .fontGreen
        {
            color: Green;
        }
        .fontGreen:hover
        {
            color: Green !important;
        }
         .fontBlack
        {
            color: Black;
            text-align: center;
        }
        .fontBlack:hover
        {
            color: #333333 !important;
        }
    </style>
    <script type="text/javascript" language="javascript">

        function pageLoad() {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: true
            });

        }
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title" style="width: 100%;">
                <asp:Label ID="lblHeader" runat="server" Font-Size="12px" Font-Bold="true" Font-Names="Arial"
                    Text="Recorded Local Solicited Events"></asp:Label>
            </h3>
        </div>
        <asp:HiddenField ID="hdnLASTDOSE" runat="server" />
        <asp:HiddenField ID="hdnDOSEDdays" runat="server" />
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
        </div>
        <div class="box-body">
            <div class="form-group">
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
                <div style="display: inline-flex">
                    <div style="display: inline-flex">
                        <label class="label width70px">
                            Subject ID:
                        </label>
                        <div class="Control">
                            <asp:DropDownList ID="ddlSubject" runat="server" AutoPostBack="True" CssClass="form-control"
                                OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                &nbsp&nbsp&nbsp&nbsp
                <div id="Div1" class="dropdown" runat="server" style="display: inline-flex">
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
                        <hr style="margin: 5px;" />
                    </ul>
                </div>
            </div>
        </div>
        <br />
    </div>
    <div class="box box-info">
        <div class="box-header">
            <h3 class="box-title" style="width: 100%;">
                <asp:Label ID="Label2" runat="server" Font-Size="12px" Font-Bold="true" Font-Names="Arial"
                    Text="Summary"></asp:Label>
            </h3>
        </div>
        <asp:GridView ID="gridData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
            EmptyDataText="No Record Available" OnPreRender="grd_data_PreRender" CssClass="table table-bordered table-striped table-striped Datatable"
            HeaderStyle-ForeColor="Black" OnRowDataBound="gridData_RowDataBound">
        </asp:GridView>
    </div>
</asp:Content>
