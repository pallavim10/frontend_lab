<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DM_AUDITTRAIL_REPORT.aspx.cs" Inherits="CTMS.DM_AUDITTRAIL_REPORT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <script type="text/jscript">

        $(document).ready(function () {
            var count = 0;
            $("#MainContent_grdAuditTrail tr").each(function () {
                if (count > 0) {
                    var tableName = 'grd_Data';
                    var REVIEW = $(this).find('td:eq(1)').find('span').text();

                    $('#' + $(this).closest('tr').find('td:eq(7)').find("a[id*='lbtnReview']").attr('id')).addClass("disp-none");
                    $('#' + $(this).closest('tr').find('td:eq(7)').find("a[id*='lbtnReviewDone']").attr('id')).addClass("disp-none");

                    if (REVIEW == "True") {

                        $('#' + $(this).closest('tr').find('td:eq(7)').find("a[id*='lbtnReview']").attr('id')).addClass("disp-none");
                        $('#' + $(this).closest('tr').find('td:eq(7)').find("a[id*='lbtnReviewDone']").attr('id')).removeClass("disp-none");

                    }
                    else {

                        $('#' + $(this).closest('tr').find('td:eq(7)').find("a[id*='lbtnReview']").attr('id')).removeClass("disp-none");
                        $('#' + $(this).closest('tr').find('td:eq(7)').find("a[id*='lbtnReviewDone']").attr('id')).addClass("disp-none");

                    }
                }
                count++;
            });

        });

        function MarkAsReview(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var PVID = $(element).closest('tr').find('td:eq(2)').find('span').html();
            var RECID = $(element).closest('tr').find('td:eq(3)').find('span').html();

            $.ajax({
                type: "POST",
                url: "AjaxFunction_DM.aspx/MarkAsReview",
                data: '{ID:"' + ID + '",PVID:"' + PVID + '",RECID:"' + RECID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {

                    var SDVSTATUS = res.d;

                    $('#' + $(element).closest('tr').find('td:eq(7)').find("a[id*='lbtnReview']").attr('id')).addClass("disp-none");
                    $('#' + $(element).closest('tr').find('td:eq(7)').find("a[id*='lbtnReviewDone']").attr('id')).addClass("disp-none");

                    if (SDVSTATUS == 'True') {

                        $('#' + $(element).closest('tr').find('td:eq(7)').find("a[id*='lbtnReviewDone']").attr('id')).removeClass("disp-none");

                    }
                    else if (SDVSTATUS == 'False') {

                        $('#' + $(element).closest('tr').find('td:eq(7)').find("a[id*='lbtnReview']").attr('id')).removeClass("disp-none");

                    }
                    else {

                        alert("This data can not be Review.");

                    }

                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }
                }
            });

            return false;
        }

        function UnMarkAsReviewed(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var PVID = $(element).closest('tr').find('td:eq(2)').find('span').html();
            var RECID = $(element).closest('tr').find('td:eq(3)').find('span').html();

            $.ajax({
                type: "POST",
                url: "AjaxFunction_DM.aspx/UnMarkAsReviewed",
                data: '{ID:"' + ID + '",PVID:"' + PVID + '",RECID:"' + RECID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {

                    var SDVSTATUS = res.d;

                    $('#' + $(element).closest('tr').find('td:eq(7)').find("a[id*='lbtnReview']").attr('id')).addClass("disp-none");
                    $('#' + $(element).closest('tr').find('td:eq(7)').find("a[id*='lbtnReviewDone']").attr('id')).addClass("disp-none");

                    if (SDVSTATUS == 'True') {

                        $('#' + $(element).closest('tr').find('td:eq(7)').find("a[id*='lbtnReviewDone']").attr('id')).removeClass("disp-none");

                    }
                    else if (SDVSTATUS == 'False') {

                        $('#' + $(element).closest('tr').find('td:eq(7)').find("a[id*='lbtnReview']").attr('id')).removeClass("disp-none");

                    }
                    else {

                        alert("This data can not be Review.");

                    }

                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }
                }
            });

            return false;
        }

        function ViewData(element) {

            var RECID = $(element).closest('tr').find('td:eq(3)').find('span').html();
            var VISITNUM = $(element).closest('tr').find('td:eq(5)').find('span').html();
            var VISIT = $(element).closest('tr').find('td:eq(11)').find('span').html();
            var MODULEID = $(element).closest('tr').find('td:eq(4)').find('span').html();
            var MODULENAME = $(element).closest('tr').find('td:eq(12)').find('span').html();
            var SUBJID = $(element).closest('tr').find('td:eq(10)').find('span').html();
            var INVID = $(element).closest('tr').find('td:eq(9)').find('span').html();
            var MULTIPLEYN = $(element).closest('tr').find('td:eq(6)').find('span').html();

            var test;
            if (MULTIPLEYN == "True") {

                test = "DM_DataEntry_MultipleData_ReadOnly.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITNUM + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&RECID=" + RECID + "&VIEWDATA=1";

            }
            else {

                test = "DM_DataEntry_ReadOnly.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITNUM + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&RECID=" + RECID + "&VIEWDATA=1";

            }
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=450,resizable=no";
            window.open(test);
            return false;
        }
    </script>
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>

    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }
    </style>
    <script type="text/javascript">
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Audit Trail Reports
            </h3>
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
                    <asp:DropDownList ID="ddlSiteId" CssClass="form-control required" runat="server"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlSiteId_SelectedIndexChanged">
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
                    <asp:DropDownList ID="ddlSubjectId" runat="server" CssClass="form-control select" SelectionMode="Single" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlSubjectId_SelectedIndexChanged">
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
                    <asp:DropDownList ID="ddlVisitId" runat="server" CssClass="form-control" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlVisitId_SelectedIndexChanged">
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
                    <asp:DropDownList ID="ddlModuleId" runat="server" CssClass="form-control" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlModuleId_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div runat="server" id="Div6" class="form-group" style="display: inline-flex">
                <div class="form-group" style="display: inline-flex">
                    <label class="label">
                        <asp:Label ID="lblField" runat="server" CssClass="wrapperLable" Text="Select Field:"></asp:Label>
                    </label>
                </div>
                <div class="Control">
                    <asp:DropDownList ID="ddlFieldId" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFieldId_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div runat="server" id="Div7" class="form-group" style="display: inline-flex">
                <div class="form-group" style="display: inline-flex">
                    <label class="label">
                        <asp:Label ID="Label1" runat="server" CssClass="wrapperLable" Text="Select Reason:"></asp:Label>
                    </label>
                </div>
                <div class="Control">
                    <asp:DropDownList ID="ddlReason" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlReason_SelectedIndexChanged">
                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Updated data available" Value="Updated data available"></asp:ListItem>
                        <asp:ListItem Text="Data entry error" Value="Data entry error"></asp:ListItem>
                        <asp:ListItem Text="Initial Entry" Value="Initial Entry"></asp:ListItem>
                        <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div runat="server" id="Div8" class="form-group" style="display: inline-flex">
                <div class="form-group" style="display: inline-flex">
                    <label class="label">
                        <asp:Label ID="Label2" runat="server" CssClass="wrapperLable" Text="Select Transaction:"></asp:Label>
                    </label>
                </div>
                <div class="Control">
                    <asp:DropDownList ID="ddlTransact" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlTransact_SelectedIndexChanged">
                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                        <asp:ListItem Text="New Entry" Value="New Entry"></asp:ListItem>
                        <asp:ListItem Text="Changed Data" Value="Changed Data" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="Search"
                CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnSearch_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" ToolTip="Export to Excel" CssClass="btn btn-info btn-sm"
                Text="Export to Excel" ForeColor="White">Export to Excel&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
        </div>
        <div class="box-group">
            <div class="fixTableHead">
                <asp:GridView ID="grdAuditTrail" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped Datatable"
                    Width="100%" OnPreRender="grd_data_PreRender" CellPadding="4" CellSpacing="2">
                    <Columns>
                        <asp:TemplateField ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="txt_center disp-none" HeaderStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="REVIEW" runat="server" Text='<%# Bind("REVIEW") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="PVID" runat="server" Text='<%# Bind("PVID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="RECID" runat="server" Text='<%# Bind("RECID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="MODULEID" runat="server" Text='<%# Bind("MODULEID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="VISITNUM" runat="server" Text='<%# Bind("VISITNUM") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="MULTIPLEYN" runat="server" Text='<%# Bind("MULTIPLEYN") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnReview" runat="server" ToolTip="Review" OnClientClick="return MarkAsReview(this)">
                                    <i id="iconReview" runat="server" class="disp-none fa fa-square-o"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbtnReviewDone" runat="server" ToolTip="Reviewed" OnClientClick="return UnMarkAsReviewed(this)">
                                    <i id="iconReviewed" runat="server" class="disp-none fa fa-check-square-o"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="align-center" ItemStyle-CssClass="align-center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkPAGENUM" runat="server" ToolTip="View" OnClientClick="return ViewData(this)"><i class="fa fa-search"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Site Id" ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="INVID" runat="server" Text='<%# Bind("INVID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject Id" ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="SUBJID" runat="server" Text='<%# Bind("SUBJID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Record No." HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="Label5" Font-Size="Small" Text='<%# Bind("RECORDNO") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Visit" ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="VISIT" runat="server" Text='<%# Bind("VISIT") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Module Name">
                            <ItemTemplate>
                                <asp:Label ID="MODULENAME" runat="server" Text='<%# Bind("MODULENAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Field Name">
                            <ItemTemplate>
                                <asp:Label ID="FIELDNAME" runat="server" Text='<%# Bind("FIELDNAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Old Value">
                            <ItemTemplate>
                                <asp:Label ID="OLDVALUE" runat="server" Text='<%# Bind("OLDVALUE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="New Value">
                            <ItemTemplate>
                                <asp:Label ID="NEWVALUE" runat="server" Text='<%# Bind("NEWVALUE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reason">
                            <ItemTemplate>
                                <asp:Label ID="REASON" runat="server" Text='<%# Bind("REASON") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comment">
                            <ItemTemplate>
                                <asp:Label ID="COMMENTS" runat="server" Text='<%# Bind("COMMENTS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Transaction">
                            <ItemTemplate>
                                <asp:Label ID="TRANSACT" runat="server" Text='<%# Bind("TRANSACT") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Source" ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="SOURCE" runat="server" Text='<%# Bind("SOURCE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="20%" HeaderStyle-CssClass="align-left" ItemStyle-Width="20%">
                            <HeaderTemplate>
                                <label>Changed Details</label><br />
                                <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Changed By]</label><br />
                                <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div>
                                    <div>
                                        <asp:Label ID="ENTEREDBYNAME" runat="server" Text='<%# Bind("ENTEREDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="ENTERED_CAL_DAT" runat="server" Text='<%# Bind("ENTERED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="ENTERED_CAL_TZDAT" runat="server" Text='<%# Bind("ENTERED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="align-left" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                            <HeaderTemplate>
                                <label>Reviewed Details</label><br />
                                <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Reviewed By]</label><br />
                                <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div>
                                    <div>
                                        <asp:Label ID="REVIEWBYNAME" runat="server" Text='<%# Bind("REVIEWBYNAME") %>' ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="REVIEW_CAL_DAT" runat="server" Text='<%# Bind("REVIEW_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="REVIEW_CAL_TZDAT" runat="server" Text='<%# Bind("REVIEW_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
</asp:Content>
