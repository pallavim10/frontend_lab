<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MM_LISTING_DATA_SUBJECT.aspx.cs"
    Inherits="CTMS.MM_LISTING_DATA_SUBJECT" %>

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

    <link href="CommonStyles/MM/MM_Icons.css" rel="stylesheet" />
    <script src="CommonFunctionsJs/MM/MM_Details.js"></script>
    <script src="CommonFunctionsJs/MM/MM_Actions.js"></script>

</head>
<body>
    <form id="Form1" runat="server">
        <div class="page">
            <asp:ScriptManager ID="script1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hdnlistid" runat="server" />
                    <asp:HiddenField ID="hdnSUBJID" runat="server" />
                    <asp:HiddenField ID="hdnPREV_LISTID" runat="server" />
                    <asp:HiddenField ID="hdnPVID" runat="server" />
                    <asp:HiddenField ID="hdnRECID" runat="server" />
                    <asp:HiddenField ID="hdntranspose" runat="server" />
                    <asp:HiddenField ID="hdnPrimMODULENAME" runat="server" />
                    <asp:HiddenField ID="hdnPrimMODULEID" runat="server" />
                    <asp:HiddenField ID="hdnAutoQueryText" runat="server" />
                    <div class="box box-warning">
                        <div class="box-header">
                            <h3 class="box-title">Additional Listings
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
                                            Style="margin-bottom: 1%;" ForeColor="White"></asp:LinkButton>
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
                    <div runat="server" id="divDetails" visible="false" class="box box-warning">
                        <div class="box-header">
                            <h3 class="box-title">Details
                            </h3>
                            <div class="pull-right" style="margin-bottom: 10px; margin-right: 10px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton runat="server" ID="lbtnComments" Text="Comments" OnClientClick="return ShowComments_btn();"
                                CssClass="btn btn-primary btn-sm " ForeColor="White"></asp:LinkButton>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <div align="left" style="margin-left: 0px">
                                    <div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Repeater runat="server" ID="repeatData" OnItemDataBound="repeatData_ItemDataBound">
                                                    <ItemTemplate>
                                                        <div class="col-md-6">
                                                            <div class="col-md-6" style="padding-right: 0px;">
                                                                <label>
                                                                    <%# Eval("FIELDNAME")%>
                                                                :</label>
                                                            </div>
                                                            <div class="col-md-6" style="padding-left: 0px;">
                                                                <asp:Label runat="server" ID="lblData" Style="width: auto;" CssClass="form-control">
                                                                <%# Eval("DATA")%>
                                                                </asp:Label>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                        <br />
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
                                                    OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable table-striped notranslate"
                                                    OnRowDataBound="gridData_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Options" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                                            <ItemTemplate>
                                                                <div class="txt_center" style="display: inline-flex;">
                                                                    <asp:LinkButton ID="lbtnHistory" OnClientClick="return ShowHistory(this);" runat="server">
                                                            <i title="History" class="fa fa-clock-o" style="color:#333333;font-size:14px" aria-hidden="true"></i>
                                                                    </asp:LinkButton>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="lbtnComments" OnClientClick="return ShowComments(this);" runat="server">
                                                        <i title="Comments" id="iconComments" runat="server" class="fa fa-comment" style="color: #333333;"
                                                            aria-hidden="true"></i>
                                                    </asp:LinkButton>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:LinkButton ID="lbtnReview" OnClientClick="return MarkAsReviewed(this);" runat="server">
                                                        <i title="Review" class="fa fa-thumbs-o-up" style="color: #333333;" aria-hidden="true"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton ID="lbtnAnotherReviewed" OnClientClick="return MarkAsReviewed(this);"
                                                                        runat="server">
                                                        <i title="Reviewed from Another Listings" style="color: #90ee90;" class="fa fa-thumbs-up"   aria-hidden="true"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton ID="lbtnReviewDone_PRIM" OnClientClick="return MarkAsReviewed(this);" runat="server">
                                                        <i title="Primary Reviewed" class="fa fa-thumbs-up"  style="color: #008000;" aria-hidden="true"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:Label ID="lbtnReviewDone_SECOND" runat="server">
                                                        <i title="Secondary Reviewed" class="fa fa-thumbs-up"  style="color: #008000;" aria-hidden="true"></i>
                                                                    </asp:Label>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="lbtnAutoQuery" OnClientClick="return GenerateAutoQuery(this);"
                                                        CommandName="AutoQuery" runat="server">
                                                        <i title="Raise Auto Query" class="fa fa-question-circle" style="color: #333333;" aria-hidden="true"></i>
                                                    </asp:LinkButton>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="lbtnManualQuery" OnClientClick="return RaiseQuery(this);" runat="server">
                                                        <i title="Raise Manual Query" class="fa fa-question" style="color: #333333;" aria-hidden="true"></i>
                                                    </asp:LinkButton>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <div runat="server" id="divQueryCount" visible="false" class="circleQueryCountGreen">
                                                        <asp:LinkButton ID="lbtnQueryCount" OnClientClick="return ShowQueryList(this);" runat="server"
                                                            ForeColor="Yellow" Font-Bold="true">
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
