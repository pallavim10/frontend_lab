<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MM_QUERY_REPORT.aspx.cs" Inherits="CTMS.MM_QUERY_REPORT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js" type="text/javascript"></script>
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />

    <script src="CommonFunctionsJs/MM/MM_Details.js"></script>

    <link href="js/plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <script type="text/jscript">

        function pageLoad() {
            //    $(".Datatable").dataTable();
            $(".Datatable1").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                stateSave: true
            });

            $(".Datatable1").parent().parent().addClass('fixTableHead');

            $(".nav-tabs a").click(function (event) {
                event.preventDefault();
                $(this).parent().addClass("active");
                $(this).parent().siblings().removeClass("active");
                var tab = $(this).attr("href");
                $(".tab-content").not(tab).css("display", "none");
                $(tab).fadeIn();
            });
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updPanel">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">Query Report</h3>
                </div>
                <div class="lblError">
                    <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700; font-size: small;"></asp:Label>
                </div>
                <div class="">
                    <br />
                    <div runat="server" id="Div1" class="" style="display: inline-flex">
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                <asp:Label ID="lblSiteId" runat="server" CssClass="wrapperLable" Text="Select Site Id:"></asp:Label>
                            </label>
                        </div>
                        <div class="Control">
                            <asp:DropDownList ID="drpSite" CssClass="form-control" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="drpSite_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div runat="server" id="Div2" class="form-group" style="display: inline-flex">
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                <asp:Label ID="lblPatientId" runat="server" CssClass="wrapperLable" Text="Select Subject Id:"></asp:Label>
                            </label>
                        </div>
                        <div class="Control">
                            <asp:DropDownList ID="drpPatient" runat="server" CssClass="form-control" AutoPostBack="True"
                                OnSelectedIndexChanged="drpPatient_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div runat="server" id="Div3" class="form-group" style="display: inline-flex">
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                <asp:Label ID="lblVisitId" runat="server" CssClass="wrapperLable" Text="Select Visit:"></asp:Label>
                            </label>
                        </div>
                        <div class="Control">
                            <asp:DropDownList ID="drpVisit" runat="server" CssClass="form-control" AutoPostBack="True"
                                OnSelectedIndexChanged="drpVisit_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div runat="server" id="Div5" class="form-group" style="display: inline-flex">
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                <asp:Label ID="lblModule" runat="server" CssClass="wrapperLable" Text="Select Module:"></asp:Label>
                            </label>
                        </div>
                        <div class="Control">
                            <asp:DropDownList ID="drpModule" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div runat="server" id="Div7" class="form-group" style="display: inline-flex">
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                <asp:Label ID="lblQueryStatus" runat="server" CssClass="wrapperLable" Text="Query Status:"></asp:Label>
                            </label>
                        </div>
                        <div class="Control">
                            <asp:DropDownList ID="drpQueryStatus" runat="server" CssClass="form-control">
                                <asp:ListItem Text="--All--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Pending for Review/Post" Value="Pending for Review/Post"></asp:ListItem>
                                <asp:ListItem Text="Reviewed/Posted" Value="Reviewed/Posted"></asp:ListItem>
                                <asp:ListItem Text="Pushed/Linked" Value="Pushed/Linked"></asp:ListItem>
                                <asp:ListItem Text="Closed" Value="Closed"></asp:ListItem>
                                <asp:ListItem Text="Deleted" Value="Deleted"></asp:ListItem>
                                <asp:ListItem Text="Data Deleted" Value="Data Deleted"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div runat="server" id="Div8" class="form-group" style="display: inline-flex">
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                <asp:Label ID="Label1" runat="server" CssClass="wrapperLable" Text="Query Type:"></asp:Label>
                            </label>
                        </div>
                        <div class="Control">
                            <asp:DropDownList ID="drpQueryType" runat="server" CssClass="form-control">
                                <asp:ListItem Text="--All--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Auto" Value="Auto"></asp:ListItem>
                                <asp:ListItem Text="Manual" Value="Manual"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="Search"
                        CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnSearch_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm"
                        OnClick="btnCancel_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <div id="Div6" class="dropdown" runat="server" style="display: inline-flex">
                        <a href="#" class="dropdown-toggle glyphicon glyphicon-download-alt" data-toggle="dropdown"
                            style="color: #333333" title="Export"></a>
                        <ul class="dropdown-menu dropdown-menu-sm">
                            <li>
                                <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" ToolTip="Export to Excel"
                                    Text="Export to Excel" CssClass="dropdown-item" Style="color: #333333;">
                                </asp:LinkButton></li>
                            <hr style="margin: 5px;" />
                        </ul>
                    </div>
                </div>
                <div class="box-group">
                    <div class="form-group">
                        <div style="overflow: auto;">
                            <asp:GridView ID="grdQUERY" runat="server" AutoGenerateColumns="False"
                                CssClass="table table-bordered table-striped Datatable1" OnPreRender="grd_data_PreRender"
                                CellPadding="4" CellSpacing="2"
                                OnRowDataBound="grdQUERY_RowDataBound" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Query Id" ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="QueryId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Show Comments" ItemStyle-CssClass="txt_center">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="lbtnComments" OnClientClick="ShowComments_QUERY_Popup(this);">
                                                <i class="fa fa-comment" style="color:#333333;font-size:14px" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Show History" ItemStyle-CssClass="txt_center">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="lbtnHistory" OnClientClick="ShowHistory_QUERY_Popup(this);">
                                                <i class="fa fa-clock-o" style="color:#333333;font-size:14px" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Query Id" ItemStyle-CssClass="txt_center">
                                        <ItemTemplate>
                                            <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Site ID" ItemStyle-CssClass="txt_center">
                                        <ItemTemplate>
                                            <asp:Label ID="SITEID" runat="server" Text='<%# Eval("SITEID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Subject ID" ItemStyle-CssClass="txt_center">
                                        <ItemTemplate>
                                            <asp:Label ID="SUBJID" runat="server" Text='<%# Eval("SUBJID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Visit" ItemStyle-CssClass="txt_center">
                                        <ItemTemplate>
                                            <asp:Label ID="VISIT" runat="server" Text='<%# Eval("VISIT") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Listing" ItemStyle-CssClass="txt_center">
                                        <ItemTemplate>
                                            <asp:Label ID="SOURCE" runat="server" Text='<%# Eval("SOURCE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Module Name" ItemStyle-CssClass="txt_center">
                                        <ItemTemplate>
                                            <asp:Label ID="MODULENAME" runat="server" Text='<%# Eval("MODULENAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Query Text">
                                        <ItemTemplate>
                                            <asp:Label ID="QUERYTEXT" runat="server" Text='<%# Eval("QUERYTEXT") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Query Type" ItemStyle-CssClass="txt_center">
                                        <ItemTemplate>
                                            <asp:Label ID="QUERYTYPE" runat="server" Text='<%# Eval("QUERYTYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="txt_center">
                                        <ItemTemplate>
                                            <asp:Label ID="STATUS" runat="server" Text='<%# Eval("STATUS") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="align-left">
                                        <HeaderTemplate>
                                            <label>Raised details</label><br />
                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Raised By]</label><br />
                                            <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                            <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="RAISEDBYNAME" runat="server" Text='<%# Bind("RAISEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="RAISED_CAL_DAT" runat="server" Text='<%# Bind("RAISED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="RAISED_CAL_TZDAT" runat="server" Text='<%# Eval("RAISED_CAL_TZDAT") +" "+ Eval("RAISED_TZVAL") %>' ForeColor="Red"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="align-left">
                                        <HeaderTemplate>
                                            <label>Review</label><br />
                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Reviewed By]</label><br />
                                            <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                            <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div runat="server" id="divREVIEW">
                                                <div>
                                                    <asp:Label ID="MM_REVIEWBYNAME" runat="server" Text='<%# Bind("MM_REVIEWBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="MM_REVIEW_CAL_DAT" runat="server" Text='<%# Bind("MM_REVIEW_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="MM_REVIEW_CAL_TZDAT" runat="server" Text='<%# Eval("MM_REVIEW_CAL_TZDAT")+" "+ Eval("MM_REVIEW_TZVAL") %>' ForeColor="Red"></asp:Label>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="align-left">
                                        <HeaderTemplate>
                                            <label>Pushed/Linked Details</label><br />
                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Pushed/Linked By]</label><br />
                                            <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                            <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="LINKED_PUSHED_BYNAME" runat="server" Text='<%# Bind("LINKED_PUSHED_BYNAME") %>' ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="LINKED_PUSHED_CAL_DAT" runat="server" Text='<%# Bind("LINKED_PUSHED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="LINKED_PUSHED_CAL_TZDAT" runat="server" Text='<%# Eval("LINKED_PUSHED_CAL_TZDAT") +" "+ Eval("LINKED_PUSHED_TZVAL") %>' ForeColor="Red"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="align-left">
                                        <HeaderTemplate>
                                            <label>Closed Details</label><br />
                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Closed By]</label><br />
                                            <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                            <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="RESOLVEDBYNAME" runat="server" Text='<%# Bind("RESOLVEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="RESOLVED_CAL_DAT" runat="server" Text='<%# Bind("RESOLVED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="RESOLVED_CAL_TZDAT" runat="server" Text='<%# Eval("RESOLVED_CAL_TZDAT")+" "+ Eval("RESOLVED_TZVAL") %>' ForeColor="Red"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
