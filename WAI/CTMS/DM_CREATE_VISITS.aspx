<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DM_CREATE_VISITS.aspx.cs" Inherits="CTMS.DM_CREATE_VISITS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="CommonFunctionsJs/DM/DM_ConfirmMsg.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/DB/DB_showAuditTrail.js"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true,
                fixedHeader: true,
            });
        }
    </script>
    <script type="text/jscript">

        function SetVisitCriteria(element) {

            var VISITNUM = $(element).closest('tr').find('td:eq(3)').find('span').html();
            var VISITNAME = $(element).closest('tr').find('td:eq(4)').find('span').html();

            var test = "DM_SET_VISIT_CRITERIA.aspx?VISITNUM=" + VISITNUM.trim() + "&VISITNAME=" + VISITNAME;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500,width=1200";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;

        }

    </script>
    <script src="js/CKEditor/ckeditor.js" type="text/javascript"></script>
    <script type="text/javascript" src="CommonFunctionsJs/ControlJS.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div class="box box-primary">
        <div class="box-header">
            <h3 class="box-title">Add Visit
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700; font-size: 15px;"></asp:Label>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-4">
                            <div class="col-md-4" style="width: 150px;">
                                <label>Enter Visit ID:</label>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox Style="width: 123px;" MaxLength="3" ID="txtVisitID" ValidationGroup="section"
                                    runat="server" CssClass="form-control numeric required"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="col-md-4" style="width: 150px;">
                                <label>Enter Visit Name:</label>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox Style="width: 420px;" ID="txtVisitName" ValidationGroup="section" runat="server"
                                    CssClass="form-control required"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            &nbsp;
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-4">
                            <div class="col-md-4" style="width: 150px;">
                                <label>Unscheduled Visit:</label>
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkUnschedule" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="col-md-12">
                                <label>Repeat :</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkRepeat" />
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="col-md-12">
                                <label>Applicable to Progression Tracker :</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkProgTracker" />
                            </div>
                        </div>
                        <div class="col-md-6" runat="server" visible="false">
                            <div class="col-md-4" style="width: 150px;">
                                <label>
                                    Publish To DM:
                                </label>
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkPublish_DM" />
                            </div>
                            <div class="col-md-4" style="width: 150px;">
                                <label>Publish To eSource :</label>
                            </div>
                            <div class="col-md-1">
                                <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkPublish_eSource" />
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12" >
                        <div class="col-md-4">
                            <div class="col-md-4" style="width: 150px;">
                                <label>Select Dependency:</label>
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList ID="drpVisit" runat="server" CssClass="form-control width200px"
                                    AutoPostBack="true" OnSelectedIndexChanged="drpVisit_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-5">
                                <asp:DropDownList ID="drpModule" runat="server" CssClass="form-control width200px"
                                    AutoPostBack="true" OnSelectedIndexChanged="drpModule_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList ID="drpField" runat="server" CssClass="form-control width200px">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12" >
                        <div class="col-md-4">
                            <div class="col-md-4" style="width: 150px;">
                                <label>Select Progression Tracker Field:</label>
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList ID="drpProgTrackerVisits" runat="server" CssClass="form-control width200px"
                                    AutoPostBack="true" OnSelectedIndexChanged="drpProgVisits_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-5">
                                <asp:DropDownList ID="drpProgTrackerModule" runat="server" CssClass="form-control width200px"
                                    AutoPostBack="true" OnSelectedIndexChanged="drpProgModule_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList ID="drpProgTrackerField" runat="server" CssClass="form-control width200px">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-4">
                            <div class="col-md-4" style="width: 150px;">
                                <label>Window Period:</label>
                            </div>
                            <div class="col-md-6" style="display: inline-block;" runat="server" id="divtxtWindow">
                                <asp:TextBox Style="width: 123px;" MaxLength="5" ID="txtWindow" runat="server" CssClass="form-control numeric " Placeholder="In Days"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-7">
                            <div class="col-md-4" style="width: 150px;">
                                <label>Early Window Period:</label>
                            </div>
                            <div class="col-md-3" runat="server" id="divtxtearlyWindow">
                                <asp:TextBox Style="width: 123px;" MaxLength="5" ID="txtEarly" runat="server" CssClass="form-control numeric " Placeholder="In Days"></asp:TextBox>
                            </div>
                            <div class="col-md-4" style="width: 150px;">
                                <label>Late Window Period:</label>
                            </div>
                            <div class="col-md-3"  id="divtxtlateWindow" runat="server">
                                <asp:TextBox Style="width: 123px;" MaxLength="5" ID="txtLate" runat="server" CssClass="form-control numeric " Placeholder="In Days"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-5">
                            &nbsp;
                        </div>
                        <div class="col-md-7">
                            <asp:Button ID="btnsubmitSectionVisit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                OnClick="btnsubmitSectionVisit_Click" />
                            <asp:Button ID="btnupdateSectionVisit" Text="Update" Visible="false" runat="server"
                                CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnupdateSectionVisit_Click" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btncancelSectionVisit" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm"
                                OnClick="btncancelSectionVisit_Click" />
                        </div>
                    </div>
                </div>
                <br />
            </div>
        </div>
    </div>
    <div class="box box-primary">
        <div class="box-header with-border">
            <h4 class="box-title" align="left">Records
            </h4>
            <div class="pull-right" style="padding-top: 4px; margin-right: 10px;">
                <asp:LinkButton runat="server" ID="btnExportExcel" OnClick="btnExportExcel_Click" Text="Export Visits" CssClass="btn btn-info btn-sm" ForeColor="White" Font-Bold="true"> Export Visits&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
            </div>
        </div>
        <div class="box-body">
            <div align="left" style="margin-left: 5px">
                <div>
                    <div class="rows">
                        <div class="fixTableHead">
                            <asp:GridView ID="grdVisit" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                Style="width: 96%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdVisit_RowCommand" OnRowDataBound="grdVisit_RowDataBound" OnPreRender="grdVisit_PreRender">
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                                        ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                CommandName="EditVisit" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Set Criteria">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnSetCriteria" runat="server" CommandArgument='<%# Bind("VISITNUM") %>'
                                                ToolTip="Set Criteria" OnClientClick="return SetVisitCriteria(this)">  <i class="fa fa-cog"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Visit ID" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSecSEQ" runat="server" Text='<%# Bind("VISITNUM") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Visit Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcategory" runat="server" Text='<%# Bind("VISIT") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unscheduled Visit" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblunscheduled" runat="server" CommandArgument='<%# Eval("Unscheduled") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconunscheduled" runat="server" class="fa fa-check"></i></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Applicable to Progression Tracker" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsubjidprog" runat="server" CommandArgument='<%# Eval("SUBJID_PROG") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconsubjidprog" runat="server" class="fa fa-check"></i></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Early Window Period" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblearlywindowperiod" runat="server" Text='<%# Bind("EARLY") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Late Window Period" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbllatewindowperiod" runat="server" Text='<%# Bind("LATE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('DM_VISITDETAILS', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <HeaderTemplate>
                                            <label>Entered By Details</label><br />
                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Entered By]</label><br />
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
                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <HeaderTemplate>
                                            <label>Last Modified Details</label><br />
                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Modified By]</label><br />
                                            <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                            <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <div>
                                                    <asp:Label ID="UPDATEDBYNAME" runat="server" Text='<%# Bind("UPDATEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="UPDATED_CAL_DAT" runat="server" Text='<%# Bind("UPDATED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="UPDATED_CAL_TZDAT" runat="server" Text='<%# Bind("UPDATED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtndeleteSection" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                CommandName="DeleteVisit" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this visit : ", Eval("VISIT")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
           <asp:PostBackTrigger ControlID="btnsubmitSectionVisit" />
           <asp:PostBackTrigger ControlID="btnupdateSectionVisit" />
           <asp:PostBackTrigger ControlID="btncancelSectionVisit" />
            <asp:PostBackTrigger  ControlID="btnExportExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
