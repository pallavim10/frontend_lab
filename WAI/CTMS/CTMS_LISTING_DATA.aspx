<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CTMS_LISTING_DATA.aspx.cs" Inherits="CTMS.CTMS_LISTING_DATA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

        function pageLoad() {
            var transpose = $('#MainContent_hdntranspose').val();

            if (transpose == 'FieldNameVise') {

                $(".Datatable").dataTable({
                    "bSort": true,
                    "ordering": true,
                    "bDestroy": false,
                    stateSave: true,
                    aaSorting: [[1, 'asc']]
                });

            }
            else {

                $(".Datatable").dataTable({
                    "bSort": true,
                    "ordering": true,
                    "bDestroy": false,
                    stateSave: true
                });

            }

        }

        function ViewEventDetails(element, LISTING_ID, FIELDID) {
            var VALUE = $(element).text();
            var SUBJID = $(element).closest('tr').find('td:eq(3)').text();
            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();
            var PREV_LISTID = $('#MainContent_hdnlistid').val();

            if ($(element).text().trim() != '') {
                var test = "MM_LISTING_DATA_DETAILS.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&SUBJID=" + SUBJID + "&PREV_LISTID=" + PREV_LISTID + "&FIELDID=" + FIELDID + "&PVID=" + PVID + "&RECID=" + RECID;
                var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=620,width=1300";
                window.open(test, '_blank', strWinProperty);
                return false;
            }
        }

        function ViewLabDetails(element, LISTING_ID, FIELDID) {
            var VALUE = $(element).text();
            var TEST = $(element).closest('tr').find('td:eq(5)').text().trim();
            var SUBJID = $(element).closest('tr').find('td:eq(3)').text();
            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();
            var PREV_LISTID = $('#MainContent_hdnlistid').val();

            if ($(element).text().trim() != '') {
                var test = "MM_LISTING_LAB_DETAILS.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&SUBJID=" + SUBJID + "&PREV_LISTID=" + PREV_LISTID + "&FIELDID=" + FIELDID + "&PVID=" + PVID + "&RECID=" + RECID + "&TEST=" + TEST;
                var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=620,width=1300";
                window.open(test, '_blank', strWinProperty);
                return false;
            }
        }

        function ViewSubjectDetails(element, LISTING_ID) {
            var VALUE = $(element).text();
            var SUBJID = $(element).closest('tr').find('td:eq(3)').text();
            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();
            var PREV_LISTID = $('#MainContent_hdnlistid').val();

            var test = "MM_LISTING_DATA_SUBJECT.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&PREV_LISTID=" + PREV_LISTID + "&SUBJID=" + SUBJID + "&PVID=" + PVID + "&RECID=" + RECID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=620,width=1300";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ViewVisitDetails(element, LISTING_ID) {
            var VALUE = $(element).text();
            var SUBJID = $(element).closest('tr').find('td:eq(3)').text();
            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();
            var PREV_LISTID = $('#MainContent_hdnlistid').val();

            var test = "MM_LISTING_DATA_VISIT.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&PREV_LISTID=" + PREV_LISTID + "&SUBJID=" + SUBJID + "&PVID=" + PVID + "&RECID=" + RECID; ;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=620,width=1300";
            window.open(test, '_blank', strWinProperty);
            return false;
        }


        function RaiseQuery(element) {

            var Subject = $(element).closest('tr').find('td:eq(3)').text().trim();
            var EventCode = $(element).closest('tr').find('td:eq(5)').text().trim();

            var Department = "Medical"
            var Rule = $('#MainContent_lblHeader').text().trim();
            var Source = $('#MainContent_hdnPrimMODULENAME').val().trim();

            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();

            if (Source == '' || Source == undefined) {
                Source = Rule;
            }

            var LISTING_ID = $('#MainContent_hdnlistid').val();

            var test = "NewQueryPopup.aspx?Subject=" + Subject + "&Department=" + Department + "&Source=" + Source + "&EventCode=" + EventCode + "&Rule=" + Rule + "&PVID=" + PVID + "&RECID=" + RECID + "&LISTING_ID=" + LISTING_ID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=250,width=500,resizable=no";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ShowComments(element) {

            var Subject = $(element).closest('tr').find('td:eq(3)').text().trim();
            var Source = $('#MainContent_hdnPrimMODULENAME').val().trim();
            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();

            var LISTING_ID = $('#MainContent_hdnlistid').val();

            var test = "MM_LISTING_COMMENTS.aspx?SUBJID=" + Subject + "&PVID=" + PVID + "&RECID=" + RECID + "&LISTING_ID=" + LISTING_ID;

            //var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=370,width=550,resizable=no,left=850";
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=450,resizable=no";
            window.open(test, '_blank', strWinProperty);
            return false;
        }
    </script>
    <style type="text/css">
        .fontBlue
        {
            color: Blue;
            cursor: pointer;
        }
        .circleQueryCountRed
        {
            width: 15px;
            height: 15px;
            border-radius: 50%;
            font-size: 10px;
            color: Yellow;
            text-align: center;
            background: Red;
        }
        .circleQueryCountGreen
        {
            width: 15px;
            height: 15px;
            border-radius: 50%;
            font-size: 10px;
            color: Yellow;
            text-align: center;
            background: Green;
        }
        .YellowIcon
        {
            color: Yellow;
        }
        .GreenIcon
        {
            color: Green;
        }
    </style>
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
            <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700; font-size: 15px;"></asp:Label>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ViewStateMode="Enabled">
            <ContentTemplate>
                <div class="box-body">
                    <div class="form-group">
                        <div runat="server" id="Div2" style="display: inline-flex">
                            <div style="display: inline-flex">
                                <label class="label">
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
                            <div style="display: inline-flex">
                                <label class="label">
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
                            <label class="label">
                                Select Indication:
                            </label>
                            <div class="Control">
                                <asp:DropDownList runat="server" ID="drpIndication" CssClass="form-control" AutoPostBack="True"
                                    OnSelectedIndexChanged="drpIndication_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div style="display: inline-flex">
                            <label class="label">
                                Subject ID:
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpSubID" runat="server" CssClass="form-control" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <asp:Button ID="btngetdata" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave"
                            OnClick="btngetdata_Click" />&nbsp&nbsp&nbsp&nbsp
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
                                <li>
                                    <asp:LinkButton runat="server" CssClass="dropdown-item" ID="btnRTF" OnClick="btnRTF_Click"
                                        ToolTip="Export to RTF" Text="Export to RTF" Style="color: #333333;">
                                    </asp:LinkButton></li>
                            </ul>
                        </div>
                        <div class="pull-right" style="margin-bottom: 10px; margin-right: 10px;">
                            <asp:ImageButton ID="lnktranspose" runat="server" ImageUrl="Images/transpose.png"
                                ToolTip="Transpose" OnClick="btngetTransposData_Click" Visible="false" />
                            <asp:HiddenField ID="hdntranspose" runat="server" />
                            <asp:HiddenField ID="hdnlistid" runat="server" />
                            <asp:HiddenField ID="hdnPrimMODULENAME" runat="server" />
                        </div>
                        <div class="box-body">
                            <div class="rows">
                                <div style="width: 100%; overflow: auto;">
                                    <div>
                                        <asp:GridView ID="gridData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                            OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable table-striped notranslate"
                                            OnRowDataBound="gridData_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Options" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                                    <ItemTemplate>
                                                        <div class="txt_center" style="display: inline-flex;">
                                                            <asp:LinkButton ID="lbtnComments" OnClientClick="return ShowComments(this);" runat="server">
                                                            <i title="Comments" class="fa fa-comment" style="color:#333333;" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:LinkButton ID="lbtnManualQuery" OnClientClick="return RaiseQuery(this);" runat="server">
                                                        <i title="Raise Manual Query" class="fa fa-question" style="color: #333333;" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPVID" Font-Size="Small" Text='<%# Bind("PVID") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRECID" Font-Size="Small" Text='<%# Bind("RECID") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
                <asp:PostBackTrigger ControlID="btnPDF" />
                <asp:PostBackTrigger ControlID="btnRTF" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
