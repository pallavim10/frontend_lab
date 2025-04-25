<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MM_LISTING_DATA.aspx.cs" Inherits="CTMS.MM_LISTING_DATA" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="//translate.google.com/translate_a/element.js?cb=googleTranslateElementInit"></script>
    <link href="js/plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <link href="CommonStyles/MM/MM_Icons.css" rel="stylesheet" />
    <script src="CommonFunctionsJs/MM/MM_Details.js"></script>
    <script src="CommonFunctionsJs/MM/MM_Actions.js"></script>
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": true, "ordering": true,
                "bDestroy": true, stateSave: false,
                fixedHeader: true
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');
        }
    </script>
    <style type="text/css">
        .Body {
            margin-left: 7%;
        }

        .Margin7 {
            margin-left: 7%;
            margin-right: 7%;
            font-size: larger;
            margin-top: 1%;
        }

        .box {
            margin-bottom: 0%;
            margin-bottom: 0px;
            width: auto;
        }

        .fontBold {
            font-weight: bold;
        }

        .color {
            color: #333333;
        }

        .colorRed {
            color: #ff0000;
        }

        .hover-end {
            padding: 0;
            margin: 0;
            font-size: 75%;
            position: absolute;
            bottom: 0;
            width: 100%;
            opacity: 0.8;
        }

        .Popup1 > h3 {
            background-color: #007bff;
            padding: 0.4em 1em;
            margin-top: 0px;
            font-weight: bold;
            color: white;
        }
    </style>
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
    <%--<link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />--%>
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
   
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
                        <label class="label width65px">
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
                        <label class="label width65px">
                            Site ID :
                        </label>
                        <div class="Control">
                            <asp:DropDownList ID="drpInvID" runat="server" AutoPostBack="True" CssClass="form-control"
                                OnSelectedIndexChanged="drpInvID_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div style="display: inline-flex">
                    <label class="label width65px">
                        Subject ID:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpSubID" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <div style="display: inline-flex">
                    <label class="label width65px">
                        Status:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control">
                            <asp:ListItem Selected="True" Text="--ALL--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Unreviewed Records" Value="Unreviewed Records"></asp:ListItem>
                            <asp:ListItem Text="Queried Records" Value="Queried Records"></asp:ListItem>
                            <asp:ListItem Text="Records with Unresolved Queries" Value="Records with Unresolved Queries"></asp:ListItem>
                            <asp:ListItem Text="Records Reviewed at least once by one reviewer" Value="Records Reviewed at least once by one reviewer"></asp:ListItem>
                            <asp:ListItem Text="Reviewed from Another Listings" Value="Reviewed from Another Listings"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <asp:Button ID="btngetdata" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave"
                    OnClick="btngetdata_Click" />
                &nbsp&nbsp&nbsp&nbsp
                <div id="Div1" class="dropdown" runat="server" style="display: inline-flex">
                    <a href="#" class="dropdown-toggle glyphicon glyphicon-download-alt" data-toggle="dropdown"
                        style="color: #333333" title="Export"></a>
                    <ul class="dropdown-menu dropdown-menu-sm">
                        <li>
                            <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" ToolTip="Export to Excel"
                                Text="Export to Excel" CssClass="dropdown-item" Style="color: #333333;">
                            </asp:LinkButton>
                        </li>
                        <hr style="margin: 5px;" />
                    </ul>
                </div>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <div id="Div3" class="dropdown" runat="server" style="display: inline-flex">
                    <asp:LinkButton ID="lbtExportQueries" OnClick="lbtExportQueries_Click" runat="server">
                        <i title="Export Query Report" class="glyphicon glyphicon-download-alt" style="color: Red;" aria-hidden="true"></i>
                    </asp:LinkButton>
                </div>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <div id="Div4" class="dropdown" runat="server" style="display: inline-flex">
                    <asp:LinkButton ID="lbtExportComments" OnClick="lbtExportComments_Click" runat="server">
                        <i title="Export Comments Report" class="glyphicon glyphicon-download-alt" style="color: Blue;" aria-hidden="true"></i>
                    </asp:LinkButton>
                </div>
                <div class="pull-right" style="margin-bottom: 10px; margin-right: 10px;">
                    <asp:HiddenField ID="hdntranspose" runat="server" />
                    <asp:HiddenField ID="hdnlistid" runat="server" />
                    <asp:HiddenField ID="hdnPrimMODULENAME" runat="server" />
                    <asp:HiddenField ID="hdnPrimMODULEID" runat="server" />
                    <asp:HiddenField ID="hdnAutoQueryText" runat="server" />
                </div>
                <div class="pull-right" style="margin-bottom: 10px; margin-right: 10px;">
                    <asp:LinkButton runat="server" CssClass="btn btn-info" ForeColor="White" ID="lbtnPivot"><i class="fa fa-table"></i>&nbsp;&nbsp;Pivot Table Options</asp:LinkButton>
                </div>
                <div class="box-body">
                    <div class="rows">
                        <div style="width: 100%; overflow: auto;">
                            <div>
                                <asp:GridView ID="gridData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                    OnPreRender="grd_data_PreRender" CssClass="table table-bordered  table-striped notranslate Datatable"
                                    OnRowDataBound="gridData_RowDataBound" OnRowCommand="gridData_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Options" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <div class="txt_center" style="display: inline-flex;">
                                                    <asp:LinkButton ID="lbtnHistory" OnClientClick="return ShowHistory(this);" runat="server">
                                                        <i title="History" class="fa fa-clock-o" style="color: #333333; font-size: 14px" aria-hidden="true"></i>
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
                                                        <i title="Reviewed from Another Listings" style="color: #90ee90;" class="fa fa-thumbs-up" aria-hidden="true"></i>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnReviewDone_PRIM" OnClientClick="return MarkAsReviewed(this);" runat="server">
                                                        <i title="Primary Reviewed" class="fa fa-thumbs-up" style="color: #FFBF00;" aria-hidden="true"></i>
                                                    </asp:LinkButton>
                                                    <asp:Label ID="lbtnReviewDone_SECOND" runat="server">
                                                        <i title="Secondary Reviewed" class="fa fa-thumbs-up" style="color: #008000;" aria-hidden="true"></i>
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
                                                    <div runat="server" id="divQueryCount" visible="false" class="badge">
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
    </div>
    <cc1:ModalPopupExtender ID="modalPivot" runat="server" BehaviorID="mpe" PopupControlID="pnlPivot" TargetControlID="lbtnPivot"
        CancelControlID="lbtnPivotCancel" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPivot" runat="server" CssClass="Popup1" align="center" Style="border: 5px solid #ccc; display: none;">
        <h3 class="heading">Set Pivot Table Options</h3>
        <div class="modal-body" runat="server">
            <div id="ModelPopup">
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-4" style="text-align: left;">
                            Select Row Field:
                        </div>
                        <div class="col-md-8">
                            <asp:DropDownList runat="server" ID="ddlRowField" CssClass="form-control required1" Style="max-width: 370px; width: 100%">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-4" style="text-align: left;">
                            Select Column Field:
                        </div>
                        <div class="col-md-8">
                            <asp:DropDownList runat="server" ID="ddlColField" CssClass="form-control required1" Style="max-width: 370px; width: 100%">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-4" style="text-align: left;">
                            Select Data Field:
                        </div>
                        <div class="col-md-8">
                            <asp:DropDownList runat="server" ID="ddlDataField" CssClass="form-control required1" Style="max-width: 370px; width: 100%">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-4" style="text-align: left;">
                            Summarize Data By:
                        </div>
                        <div class="col-md-8">
                            <asp:DropDownList runat="server" ID="ddlSummarize" CssClass="form-control" Style="max-width: 370px; width: 100%">
                                <asp:ListItem Selected="True" Text="None" Value="None"></asp:ListItem>
                                <asp:ListItem Text="Average" Value="Average"></asp:ListItem>
                                <asp:ListItem Text="Count" Value="Count"></asp:ListItem>
                                <asp:ListItem Text="First" Value="First"></asp:ListItem>
                                <asp:ListItem Text="Last" Value="Last"></asp:ListItem>
                                <asp:ListItem Text="Min" Value="Min"></asp:ListItem>
                                <asp:ListItem Text="Max" Value="Max"></asp:ListItem>
                                <asp:ListItem Text="Sum" Value="Sum"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12 txtCenter">
                        <div class="col-md-4">
                            &nbsp;
                        </div>
                        <div class="col-md-8">
                            <asp:Button ID="lbtnPivotSubmit" runat="server" Text="Submit" CssClass="btn btn-DarkGreen cls-btnSave1" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="lbtnPivotSubmit_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="lbtnPivotCancel" runat="server" Text="Close" CssClass="btn btn-danger" Style="height: 34px; width: 71px; font-size: 14px;" />
                        </div>
                    </div>
                </div>
            </div>
            <br />
        </div>
    </asp:Panel>
</asp:Content>
