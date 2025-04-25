<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MM_QUERY_REPORTS.aspx.cs" Inherits="CTMS.MM_QUERY_REPORTS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="js/plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/DM/Grid_Queries.js"></script>
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }
    </style>
    <script type="text/jscript">

        function pageLoad() {

            $('.select').select2();

            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: false,
                fixedHeader: true
            });

            $(".nav-tabs a").click(function (event) {
                event.preventDefault();
                $(this).parent().addClass("active");
                $(this).parent().siblings().removeClass("active"); ``
                var tab = $(this).attr("href");
                $(".tab-content").not(tab).css("display", "none");
                $(tab).fadeIn();
            });
        }

        function SHOWDETAILS(element) {

            var QID = $(element).closest('tr').find('td:eq(13)').text().trim();

            var url = "MM_QUERY_DATA.aspx?QID=" + QID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=400,width=1300,resizable=no";
            window.open(url, '_blank', strWinProperty);
        }

        function SHOW_QUERY_HISTORY(element) {

            var QID = $(element).closest('tr').find('td:eq(13)').text().trim();

            Show_MM_QueryHistory(QID);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="Updatepanel1">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">MM Query Reports
                    </h3>
                </div>
                <div class="lblError">
                    <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-size: small;"></asp:Label>
                </div>
                <div class="">
                    <div runat="server" id="Div1" class="" style="display: inline-flex">
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                <asp:Label ID="lblSiteId" runat="server" CssClass="wrapperLable" Text="Select Site Id:"></asp:Label>
                            </label>
                        </div>
                        <div class="Control">
                            <asp:DropDownList ID="drpSite" CssClass="form-control required" runat="server" AutoPostBack="true"
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
                            <asp:DropDownList ID="drpModule" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpModule_SelectedIndexChanged">
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
                                <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                <asp:ListItem Text="Pushed To DM" Value="Pushed To DM"></asp:ListItem>
                                <asp:ListItem Text="Linked To DM" Value="Linked To DM"></asp:ListItem>
                                <asp:ListItem Text="Routed to MM" Value="Routed to MM"></asp:ListItem>
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
                                <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                <asp:ListItem Text="Automatic" Value="Auto"></asp:ListItem>
                                <asp:ListItem Text="Manual" Value="Manual"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="Search"
                        CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnSearch_Click" />
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm"
                        OnClick="btnCancel_Click" />
                    &nbsp;
                    <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" ToolTip="Export to Excel" CssClass="btn btn-info btn-sm"
                        ForeColor="White">Export to Excel&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                </div>
            </div>
            <div class="box-group">
                <div class="box">
                    <div align="left">
                        <div>
                            <div class="rows">
                                <div class="fixTableHead">
                                    <asp:GridView ID="grdMMQueryDetailReports" runat="server" AutoGenerateColumns="False" Width="98%"
                                        CssClass="table table-bordered table-striped Datatable" OnPreRender="grd_data_PreRender"
                                        CellPadding="4" CellSpacing="2" OnRowCommand="grdMMQueryDetailReports_RowCommand" EmptyDataText="No Data!"
                                        OnRowDataBound="grdMMQueryDetailReports_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="LISTING_ID" runat="server" Text='<%# Bind("LISTING_ID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="PVID" runat="server" Text='<%# Bind("PVID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="VISITNUM" runat="server" Text='<%# Bind("VISITNUM") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MULTIPLEYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="MULTIPLEYN" runat="server" Text='<%# Bind("MULTIPLEYN") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MODULEID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="MODULEID" runat="server" Text='<%# Bind("MODULEID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                                <HeaderTemplate>
                                                    <label>Action</label><br />
                                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Activity]</label><br />
                                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Action By]</label><br />
                                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div id="DivLinkPush" class="dropdown" runat="server" style="text-align: center;">
                                                        <a href="#" class="dropdown-toggle btn btn-danger" data-toggle="dropdown" style="color: white;">Action</a>
                                                        <ul class="dropdown-menu dropdown-menu-sm">
                                                            <li>
                                                                <asp:LinkButton runat="server" ID="lbtnPush" ToolTip="Push To DM" CommandName="Push" CommandArgument='<%# Bind("ID") %>'
                                                                    Text="Push To DM" CssClass="dropdown-item" Style="color: blue;">
                                                                </asp:LinkButton></li>
                                                            <hr style="margin: 5px;" />
                                                            <li>
                                                                <asp:LinkButton runat="server" ID="lbtnLink" ToolTip="Link To DM" CommandName="Link" CommandArgument='<%# Bind("ID") %>'
                                                                    Text="Link To DM" CssClass="dropdown-item" Style="color: blue;">
                                                                </asp:LinkButton></li>
                                                            <hr style="margin: 5px;" />
                                                        </ul>
                                                    </div>
                                                    <div id="divActivity" runat="server" visible="false">
                                                        <div>
                                                            <asp:Label ID="ACTIVITY" runat="server" ForeColor="Maroon"></asp:Label>
                                                        </div>
                                                        <div>
                                                            <asp:Label ID="BYNAME" runat="server" ForeColor="Blue"></asp:Label>
                                                        </div>
                                                        <div>
                                                            <asp:Label ID="CAL_DAT" runat="server" ForeColor="Green"></asp:Label>
                                                        </div>
                                                        <div>
                                                            <asp:Label ID="CAL_TZDAT" runat="server" ForeColor="Red"></asp:Label>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View Record" ItemStyle-CssClass="txt_center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnOpenQuery" runat="server" CommandName="OpenQuery" CommandArgument='<%# Bind("ID") %>'
                                                        OnClientClick="SHOWDETAILS(this);"><i class="fa fa-search"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Query History" ItemStyle-CssClass="txt_center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnQueryHistory" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                        OnClientClick="SHOW_QUERY_HISTORY(this);"><i class="fa fa-history"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Site ID" ItemStyle-CssClass="txt_center">
                                                <ItemTemplate>
                                                    <asp:Label ID="INVID" runat="server" Text='<%# Bind("INVID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Subject ID" ItemStyle-CssClass="txt_center">
                                                <ItemTemplate>
                                                    <asp:Label ID="SUBJID" runat="server" Text='<%# Bind("SUBJID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Record No." ItemStyle-CssClass="txt_center">
                                                <ItemTemplate>
                                                    <asp:Label ID="RECID" Font-Size="Small" Text='<%# Bind("RECID") %>' CssClass="disp-none" runat="server"></asp:Label>
                                                    <asp:Label ID="Label5" Font-Size="Small" Text='<%# Bind("RECORDNO") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Visit">
                                                <ItemTemplate>
                                                    <asp:Label ID="VISIT" runat="server" Text='<%# Bind("VISIT") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Module">
                                                <ItemTemplate>
                                                    <asp:Label ID="MODULENAME" runat="server" Text='<%# Bind("MODULENAME") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Query ID" ItemStyle-CssClass="txt_center">
                                                <ItemTemplate>
                                                    <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Query Text">
                                                <ItemTemplate>
                                                    <asp:Label ID="QUERYTEXT" runat="server" Text='<%# Bind("QUERYTEXT") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Query Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="QUERYTYPE" runat="server" Text='<%# Bind("QUERYTYPE") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Query Status" ItemStyle-CssClass="txt_center">
                                                <ItemTemplate>
                                                    <asp:Label ID="STATUS" runat="server" Text='<%# Bind("STATUS") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                                <HeaderTemplate>
                                                    <label>Generated Details</label><br />
                                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Generated By]</label><br />
                                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div>
                                                        <div>
                                                            <asp:Label ID="RAISEDBYNAME" runat="server" Text='<%# Bind("RAISEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                        </div>
                                                        <div>
                                                            <asp:Label ID="RAISED_CAL_DAT" runat="server" Text='<%# Bind("RAISED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                        </div>
                                                        <div>
                                                            <asp:Label ID="RAISED_CAL_TZDAT" runat="server" Text='<%# Bind("RAISED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                                <HeaderTemplate>
                                                    <label>Reviewed Details</label><br />
                                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Reviewed By]</label><br />
                                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div>
                                                        <div>
                                                            <asp:Label ID="MM_REVIEWBYNAME" runat="server" Text='<%# Bind("MM_REVIEWBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                        </div>
                                                        <div>
                                                            <asp:Label ID="MM_REVIEW_CAL_DAT" runat="server" Text='<%# Bind("MM_REVIEW_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                        </div>
                                                        <div>
                                                            <asp:Label ID="MM_REVIEW_CAL_TZDAT" runat="server" Text='<%# Bind("MM_REVIEW_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                        </div>
                                                    </div>
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
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
