<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MM_StudyReview.aspx.cs" Inherits="CTMS.MM_StudyReview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="CommonStyles/MM/MM_Icons.css" rel="stylesheet" />
    <script src="CommonFunctionsJs/MM/MM_DivExpand.js"></script>
    <script src="CommonFunctionsJs/MM/MM_Actions.js"></script>
    <script src="CommonFunctionsJs/MM/MM_Details.js"></script>
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": false, "ordering": false,
                "bDestroy": true, stateSave: true
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">Study Specific Review</h3>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                        <div runat="server" id="DivINV" class="form-group" style="display: inline-flex">
                            <div class="form-group" style="display: inline-flex">
                                <label class="label">
                                    Site ID:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="drpInvID" runat="server" OnSelectedIndexChanged="drpInvID_SelectedIndexChanged"
                                        AutoPostBack="True" CssClass="form-control required">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                Subject ID:
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpSubID" runat="server" CssClass="form-control required">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <asp:Button ID="btngetdata" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave"
                            OnClick="btngetdata_Click" />&nbsp&nbsp&nbsp <a href="JavaScript:ManipulateAll('_Pat_');"
                                id="_Folder" style="color: #333333; font-size: x-large; margin-top: 5px;"><i id="img_Pat_"
                                    class="icon-plus-sign-alt"></i></a>
                    </div>
                </div>
            </div>
            <asp:Repeater runat="server" OnItemDataBound="repeatData_ItemDataBound" ID="repeatData">
                <ItemTemplate>
                    <div class="box box-primary">
                        <div class="box-header">
                            <div runat="server" style="display: inline-flex; padding: 0px; margin: 4px 0px 0px 10px;"
                                id="anchor">
                                <a href="JavaScript:divexpandcollapse('_Pat_<%# Eval("ID") %>');" style="color: #333333">
                                    <i id="img_Pat_<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                                <h3 class="box-title" style="padding-top: 0px;">
                                    <asp:Label ID="lblHeader" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                                </h3>
                            </div>
                        </div>
                        <div id="_Pat_<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                            <div class="box-body">
                                <div class="rows">
                                    <div style="width: 100%; overflow: auto;">
                                        <div>
                                            <asp:HiddenField ID="hfLISTID" runat="server" Value='<%# Bind("ID") %>'></asp:HiddenField>
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
                                                        <i title="Primary Reviewed" class="fa fa-thumbs-up"  style="color: #FFBF00;" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                                <asp:Label ID="lbtnReviewDone_SECOND" runat="server">
                                                        <i title="Secondary Reviewed" class="fa fa-thumbs-up"  style="color: #008000;" aria-hidden="true"></i>
                                                                </asp:Label>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="lbtnAutoQuery" OnClientClick="return GenerateAutoQuery(this);" CommandName="AutoQuery" runat="server">
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
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                </ItemTemplate>
            </asp:Repeater>
            <asp:HiddenField ID="hdntranspose" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
