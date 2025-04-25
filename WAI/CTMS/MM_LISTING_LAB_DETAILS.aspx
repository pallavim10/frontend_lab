<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MM_LISTING_LAB_DETAILS.aspx.cs"
    Inherits="CTMS.MM_LISTING_LAB_DETAILS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="Scripts/Jquery1.12.4.js" type="text/javascript"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Common-Bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />
    <link href="Styles/AdminLTE.css" rel="stylesheet" type="text/css" />
    <link href="Styles/ionicons.css" rel="stylesheet" type="text/css" />
    <link href="Styles/font-awesome.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="img/favicon.ico" type="image/x-icon">
    <link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="js/plugins/datatables/dataTables.bootstrap.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="Styles/datatables/dataTables.bootstrap.css" />
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": false,
                "bDestroy": false,
                stateSave: true
            });
        }

        function ViewEventDetails_Other(element, LISTING_ID, PREV_LISTID) {
            var VALUE = $('#hdnValue').val();
            var SUBJID = $('#hdnSUBJID').val();
            //            var PREV_LISTID = $('#hdnPREV_LISTID').val();
            var FIELDID = $('#hdnFIELDID').val();

            var test = "MM_LISTING_DATA_DETAILS.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&SUBJID=" + SUBJID + "&PREV_LISTID=" + PREV_LISTID + "&FIELDID=" + FIELDID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=445,width=1300";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ViewSubjectDetails_Other(element, LISTING_ID, PREV_LISTID) {
            var VALUE = $('#hdnValue').val();
            var SUBJID = $('#hdnSUBJID').val();

            var test = "MM_LISTING_DATA_SUBJECT.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&PREV_LISTID=" + PREV_LISTID + "&SUBJID=" + SUBJID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=445,width=1300";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ViewEventDetails(element, LISTING_ID, FIELDID, PREV_LISTID) {
            var VALUE = $(element).text();
            var SUBJID = $(element).closest('tr').find('td:eq(2)').text();
            var PREV_LISTID = $('#hdnlistid').val();

            if ($(element).text().trim() != '') {
                var test = "MM_LISTING_DATA_DETAILS.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&SUBJID=" + SUBJID + "&PREV_LISTID=" + PREV_LISTID + "&FIELDID=" + FIELDID;
                var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=445,width=1300";
                window.open(test, '_blank', strWinProperty);
                return false;
            }
        }

        function ViewSubjectDetails(element, LISTING_ID, PREV_LISTID) {
            var VALUE = $(element).text();
            var SUBJID = $(element).closest('tr').find('td:eq(2)').text();

            var test = "MM_LISTING_DATA_SUBJECT.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&PREV_LISTID=" + PREV_LISTID + "&SUBJID=" + SUBJID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=445,width=1300";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ViewVisitDetails(element, LISTING_ID, PREV_LISTID) {
            var VALUE = $(element).text();
            var SUBJID = $(element).closest('tr').find('td:eq(2)').text();

            var test = "MM_LISTING_DATA_VISIT.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&PREV_LISTID=" + PREV_LISTID + "&SUBJID=" + SUBJID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=620,width=1300";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ShowComments_PEER() {

            var Subject = $('#hdnSUBJID').val();
            var Source = "Medical";
            var PVID = $('#hdnPVID').val();
            var RECID = $('#hdnRECID').val();
            var LISTING_ID = $('#hdnPREV_LISTID').val();

            var test = "MM_LISTING_COMMENTS.aspx?SUBJID=" + Subject + "&PVID=" + PVID + "&RECID=" + RECID + "&LISTING_ID=" + LISTING_ID + "&TYPE=PEER_REVIEW"

            //var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=370,width=550,resizable=no,left=850";
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=450,resizable=no";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ShowComments() {

            var Subject = $('#hdnSUBJID').val();
            var Source = "Medical";
            var PVID = $('#hdnPVID').val();
            var RECID = $('#hdnRECID').val();
            var LISTING_ID = $('#hdnPREV_LISTID').val();

            var test = "MM_LISTING_COMMENTS.aspx?SUBJID=" + Subject + "&PVID=" + PVID + "&RECID=" + RECID + "&LISTING_ID=" + LISTING_ID;

            //var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=370,width=550,resizable=no,left=850";
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=450,resizable=no";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ReCateg(type) {

            var Subject = $('#hdnSUBJID').val();
            var Test = $('#hfTest').val();

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
        label
        {
            margin-bottom: 10px;
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
    <div class="page">
        <asp:ScriptManager ID="script1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hdnValue" runat="server" />
                <asp:HiddenField ID="hdnlistid" runat="server" />
                <asp:HiddenField ID="hdnSUBJID" runat="server" />
                <asp:HiddenField ID="hdnPREV_LISTID" runat="server" />
                <asp:HiddenField ID="hdnFIELDID" runat="server" />
                <asp:HiddenField ID="hdnPVID" runat="server" />
                <asp:HiddenField ID="hdnRECID" runat="server" />
                <asp:HiddenField ID="hfTest" runat="server" />
                <div class="box box-warning">
                    <div class="box-header">
                        <h3 class="box-title">
                            Additional Listings
                        </h3>
                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <div class="rows">
                                <asp:Repeater runat="server" ID="repeatOtherListings">
                                    <ItemTemplate>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton runat="server" ID="lbtnOtherListings" Text='<%# Bind("ListingName") %>'
                                            OnClientClick='<%# Bind("OnClickEvent") %>' CssClass="btn btn-primary btn-sm cls-btnSave"
                                            ForeColor="White"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div runat="server" id="divDetails" class="box box-warning">
                    <div class="box-header">
                        <h3 class="box-title">
                            Details
                        </h3>
                        <div class="pull-right" style="margin-bottom: 10px; margin-right: 10px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton runat="server" ID="lbtnComments" Text="Comments" OnClientClick="return ShowComments();"
                                CssClass="btn btn-primary btn-sm " ForeColor="White"></asp:LinkButton>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton runat="server" ID="lbtnPeer_Review" Text="Peer-Review" OnClientClick="return ShowComments_PEER();"
                                CssClass="btn btn-primary btn-sm " ForeColor="White"></asp:LinkButton>
                        </div>
                    </div>
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">
                                Screening Details
                            </h3>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <div align="left" style="margin-left: 0px">
                                    <div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <div class="col-md-4" style="padding-right: 0px;">
                                                        <label>
                                                            Subject Id :</label>
                                                    </div>
                                                    <div class="col-md-8" style="padding-left: 0px;">
                                                        <asp:Label runat="server" ID="lblSUBJID" Style="width: auto;" CssClass="form-control">
                                                        </asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="col-md-4" style="padding-right: 0px;">
                                                        <label>
                                                            Test Name :</label>
                                                    </div>
                                                    <div class="col-md-8" style="padding-left: 0px;">
                                                        <asp:Label runat="server" ID="lblTest" Style="width: auto;" CssClass="form-control">
                                                        </asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="col-md-4" style="padding-right: 0px;">
                                                        <label>
                                                            Result :</label>
                                                    </div>
                                                    <div class="col-md-8" style="padding-left: 0px;">
                                                        <asp:Label runat="server" ID="lblScr_Res" Style="width: auto;" CssClass="form-control">
                                                        </asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="col-md-4" style="padding-right: 0px;">
                                                        <label>
                                                            Flag :</label>
                                                    </div>
                                                    <div class="col-md-8" style="padding-left: 0px;">
                                                        <asp:Label runat="server" ID="lblFlag" Style="width: auto;" CssClass="form-control">
                                                        </asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="col-md-4" style="padding-right: 0px;">
                                                        <label>
                                                            LLN :</label>
                                                    </div>
                                                    <div class="col-md-8" style="padding-left: 0px;">
                                                        <asp:Label runat="server" ID="lblLLN" Style="width: auto;" CssClass="form-control">
                                                        </asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="col-md-4" style="padding-right: 0px;">
                                                        <label>
                                                            ULN :</label>
                                                    </div>
                                                    <div class="col-md-8" style="padding-left: 0px;">
                                                        <asp:Label runat="server" ID="lblULN" Style="width: auto;" CssClass="form-control">
                                                        </asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="box box-primary">
                                <div class="box-header">
                                    <h3 class="box-title">
                                        Most Divergent Value-Low
                                    </h3>
                                    <div class="pull-right" style="margin-bottom: 10px; margin-right: 10px;">
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton runat="server" ID="LinkButton2" Text="Re-Categorize" OnClientClick="return ReCateg('Low');"
                                            CssClass="btn btn-primary btn-sm " ForeColor="White"></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="form-group">
                                        <div align="left" style="margin-left: 0px">
                                            <div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="col-md-6" style="padding-right: 0px;">
                                                            <label>
                                                                Visit :</label>
                                                        </div>
                                                        <div class="col-md-6" style="padding-left: 0px;">
                                                            <asp:Label runat="server" ID="lblLMDV_Visit" Style="width: auto;" CssClass="form-control">
                                                            </asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        &nbsp;
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="col-md-6" style="padding-right: 0px;">
                                                            <label>
                                                                Value :</label>
                                                        </div>
                                                        <div class="col-md-6" style="padding-left: 0px;">
                                                            <asp:Label runat="server" ID="lblLMDV" Style="width: auto;" CssClass="form-control">
                                                            </asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="col-md-6" style="padding-right: 0px;">
                                                            <label>
                                                                Flag :</label>
                                                        </div>
                                                        <div class="col-md-6" style="padding-left: 0px;">
                                                            <asp:Label runat="server" ID="lblLMDV_Flag" Style="width: auto;" CssClass="form-control">
                                                            </asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="col-md-6" style="padding-right: 0px;">
                                                            <label>
                                                                % Change of Baseline :</label>
                                                        </div>
                                                        <div class="col-md-6" style="padding-left: 0px;">
                                                            <asp:Label runat="server" ID="lblLMDV_Change_Base" Style="width: auto;" CssClass="form-control">
                                                            </asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="col-md-6" style="padding-right: 0px;">
                                                            <label>
                                                                % Change from LLN :</label>
                                                        </div>
                                                        <div class="col-md-6" style="padding-left: 0px;">
                                                            <asp:Label runat="server" ID="lblLMDV_Change_LLN" Style="width: auto;" CssClass="form-control">
                                                            </asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="col-md-6" style="padding-right: 0px;">
                                                            <label>
                                                                Category :</label>
                                                        </div>
                                                        <div class="col-md-6" style="padding-left: 0px;">
                                                            <asp:Label runat="server" ID="lblLMDV_Cat" Style="width: auto;" CssClass="form-control">
                                                            </asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="col-md-6" style="padding-right: 0px;">
                                                            <label>
                                                                Manual Category :</label>
                                                        </div>
                                                        <div class="col-md-6" style="padding-left: 0px;">
                                                            <asp:Label runat="server" ID="lblLMDV_ManCat" Style="width: auto;" CssClass="form-control">
                                                            </asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="box box-danger">
                                <div class="box-header">
                                    <h3 class="box-title">
                                        Most Divergent Value-High
                                    </h3>
                                    <div class="pull-right" style="margin-bottom: 10px; margin-right: 10px;">
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton runat="server" ID="LinkButton1" Text="Re-Categorize" OnClientClick="return ReCateg('High');"
                                            CssClass="btn btn-primary btn-sm " ForeColor="White"></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="form-group">
                                        <div align="left" style="margin-left: 0px">
                                            <div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="col-md-6" style="padding-right: 0px;">
                                                            <label>
                                                                Visit :</label>
                                                        </div>
                                                        <div class="col-md-6" style="padding-left: 0px;">
                                                            <asp:Label runat="server" ID="lblHMDV_Visit" Style="width: auto;" CssClass="form-control">
                                                            </asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        &nbsp;
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="col-md-6" style="padding-right: 0px;">
                                                            <label>
                                                                Value :</label>
                                                        </div>
                                                        <div class="col-md-6" style="padding-left: 0px;">
                                                            <asp:Label runat="server" ID="lblHMDV" Style="width: auto;" CssClass="form-control">
                                                            </asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="col-md-6" style="padding-right: 0px;">
                                                            <label>
                                                                Flag :</label>
                                                        </div>
                                                        <div class="col-md-6" style="padding-left: 0px;">
                                                            <asp:Label runat="server" ID="lblHMDV_Flag" Style="width: auto;" CssClass="form-control">
                                                            </asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="col-md-6" style="padding-right: 0px;">
                                                            <label>
                                                                % Change of Baseline :</label>
                                                        </div>
                                                        <div class="col-md-6" style="padding-left: 0px;">
                                                            <asp:Label runat="server" ID="lblHMDV_Change_Base" Style="width: auto;" CssClass="form-control">
                                                            </asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="col-md-6" style="padding-right: 0px;">
                                                            <label>
                                                                % Change from ULN :</label>
                                                        </div>
                                                        <div class="col-md-6" style="padding-left: 0px;">
                                                            <asp:Label runat="server" ID="lblHMDV_Change_LLN" Style="width: auto;" CssClass="form-control">
                                                            </asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="col-md-6" style="padding-right: 0px;">
                                                            <label>
                                                                Category :</label>
                                                        </div>
                                                        <div class="col-md-6" style="padding-left: 0px;">
                                                            <asp:Label runat="server" ID="lblHMDV_Cat" Style="width: auto;" CssClass="form-control">
                                                            </asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="col-md-6" style="padding-right: 0px;">
                                                            <label>
                                                                Manual Category :</label>
                                                        </div>
                                                        <div class="col-md-6" style="padding-left: 0px;">
                                                            <asp:Label runat="server" ID="lblHMDV_ManCat" Style="width: auto;" CssClass="form-control">
                                                            </asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="box box-warning">
            <div class="box-header">
                <h3 class="box-title">
                    <asp:Label ID="lblHeader" runat="server"></asp:Label>
                </h3>
                &nbsp;&nbsp;&nbsp;&nbsp;
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
                </div>
            </div>
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
            </div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="box-body">
                        <div class="form-group">
                            <div class="box-body">
                                <div class="rows">
                                    <div style="width: 100%; overflow: auto;">
                                        <div>
                                            <asp:GridView ID="gridData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                                OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable table-striped"
                                                OnRowDataBound="gridData_RowDataBound">
                                                <Columns>
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
                                            <asp:GridView ID="gridData_Tran" Visible="false" HeaderStyle-CssClass="txt_center"
                                                runat="server" AutoGenerateColumns="true" OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable table-striped">
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
    </div>
    </form>
</body>
</html>
