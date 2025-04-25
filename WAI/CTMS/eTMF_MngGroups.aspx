<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="eTMF_MngGroups.aspx.cs" Inherits="CTMS.eTMF_MngGroups" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="CommonFunctionsJs/eTMF/eTMF_ConfirmMsg.js"></script>
    <script src="CommonFunctionsJs/eTMF/eTMF_Show_AuditTrail.js"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <style type="text/css">
        .fixTableHead {
            overflow-y: auto;
            max-height: 300px;
            min-height: 300px;
            width: 100%;
        }
    </style>
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").parent().parent().addClass('fixTableHead');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager11" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">Create Groups</h3>
                </div>
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-5">
                        <div class="box box-primary" id="div11" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Groups</h3>
                            </div>
                            <div class="rows">
                                <div style="height: 317px;">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Group Name :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox ID="txtGroup" runat="server" CssClass="form-control required width200px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Types :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkEVENT" />&nbsp;&nbsp;
                                                <label>
                                                    Event</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkMILESTONE" />&nbsp;&nbsp;
                                                <label>
                                                    Milestone</label>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                &nbsp;
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Button ID="btnSubmitGroups" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnSubmitGroups_Click" />
                                                <asp:Button ID="btnUpdateGroups" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnUpdateGroups_Click" />&nbsp;&nbsp;
                                                <asp:Button ID="btnCancelGroups" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnCancelGroups_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-7">
                        <div class="box box-primary" id="div12" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Groups </h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="lbnGroupsExport" runat="server" CssClass="btn btn-info" ForeColor="White" OnClick="lbnGroupsExport_Click" Style="margin-top: 3px; margin-right: 3px;">Export Group&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                                </div>
                            </div>
                            <br />
                            <div class="rows">
                                <asp:GridView ID="grdGroups" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                    Style="width: 95%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdGroups_RowCommand" OnRowDataBound="grdGroups_RowDataBound" OnPreRender="grdGroups_PreRender">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none" HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                    CommandName="EditGroup" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Group Name" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGroup" runat="server" Text='<%# Bind("Group_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Event" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEvent" runat="server" CommandArgument='<%# Eval("Type_Event") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconEvent" runat="server" class="fa fa-check"></i></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Milestone" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMilestone" runat="server" CommandArgument='<%# Eval("Type_Milestone") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconMilestone" runat="server" class="fa fa-check"></i></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('eTMF_Group', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtndelete" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                    CommandName="DeleteGroup" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this Group : ", Eval("Group_Name")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
            <div class="box box-warning">
                <div class="box-body">
                    <div class="box-header">
                        <h3 class="box-title">Manage Groups
                        </h3>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="col-md-3">
                                    <label>
                                        Select Group :</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="drpGroup" runat="server" CssClass="form-control" Width="100%" OnSelectedIndexChanged="drpGroup_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                &nbsp;
                            </div>
                        </div>
                    </div>
                    <br />
                </div>
            </div>
            <div runat="server" id="divOthers" visible="false">
                <div class="box-body row">
                    <div class="col-md-5">
                        <div class="box box-danger">
                            <div class="box-header ">
                                <h4 class="box-title" align="left">Add Zones
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px; height:300px;">
                                    <div class="row">
                                        <asp:GridView ID="gvNewZones" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                            Style="width: 95%; border-collapse: collapse; margin-left: 20px;" OnPreRender="grdGroups_PreRender">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ref. No." ItemStyle-CssClass="txtCenter">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRefNo" runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Zones">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                        <asp:Label ID="lblZone" runat="server" Text='<%# Bind("Zone") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-1" style="padding-top: 10%; padding-bottom: 10%;">
                        <div class="box-body">
                            <div class="row txtCenter">
                                <asp:LinkButton ID="lbtnAddZones" ForeColor="White" Text="Add" runat="server" CssClass="btn btn-primary btn-sm"
                                    ValidationGroup="Grp" OnClick="lbtnAddZones_Click" />
                            </div>
                            <div class="row txtCenter">
                                &nbsp;
                            </div>
                            <div class="row txtCenter">
                                <asp:LinkButton ID="lbtnRemoveZones" ForeColor="White" Text="Remove" runat="server"
                                    ValidationGroup="Grp" CssClass="btn btn-primary btn-sm" OnClick="lbtnRemoveZones_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-danger">
                            <div class="box-header ">
                                <h4 class="box-title" align="left">Added Zones
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px; height:300px;">
                                    <div class="row">
                                        <asp:GridView ID="gvAddedZones" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                            Style="width: 95%; border-collapse: collapse; margin-left: 20px;" OnPreRender="grdGroups_PreRender">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ref. No." ItemStyle-CssClass="txtCenter">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRefNo" runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Zones">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                        <asp:Label ID="lblZone" runat="server" Text='<%# Bind("Zone") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-left">
                                                    <HeaderTemplate>
                                                        <label>Added Details</label><br />
                                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Added By]</label><br />
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
                                                                <asp:Label ID="ENTERED_CAL_TZDAT" runat="server" Text='<%# Eval("ENTERED_CAL_TZDAT")+", "+"("+ Eval("ENTERED_TZVAL") +")" %>' ForeColor="Red"></asp:Label>
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
                <br />
                <div class="box-body row">
                    <div class="col-md-5">
                        <div class="box box-danger">
                            <div class="box-header ">
                                <h4 class="box-title" align="left">Add Sections
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px; height:300px;">
                                    <div class="row">
                                        <asp:GridView ID="gvNewSections" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                            Style="width: 95%; border-collapse: collapse; margin-left: 20px;" OnPreRender="grdGroups_PreRender">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ref. No." ItemStyle-CssClass="txtCenter">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRefNo" runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sections">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                        <asp:Label ID="lblZoneID" runat="server" Visible="false" Text='<%# Bind("ZoneID") %>'></asp:Label>
                                                        <asp:Label ID="lblSection" runat="server" Text='<%# Bind("Section") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-1" style="padding-top: 10%; padding-bottom: 10%;">
                        <div class="box-body">
                            <div class="row txtCenter">
                                <asp:LinkButton ID="lbtnAddSections" ForeColor="White" Text="Add" runat="server"
                                    CssClass="btn btn-primary btn-sm" ValidationGroup="Grp" OnClick="lbtnAddSections_Click" />
                            </div>
                            <div class="row txtCenter">
                                &nbsp;
                            </div>
                            <div class="row txtCenter">
                                <asp:LinkButton ID="lbtnRemoveSections" ForeColor="White" Text="Remove" runat="server"
                                    ValidationGroup="Grp" CssClass="btn btn-primary btn-sm" OnClick="lbtnRemoveSections_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-danger">
                            <div class="box-header ">
                                <h4 class="box-title" align="left">Added Sections
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px; height:300px;">
                                    <div class="row">
                                        <asp:GridView ID="gvAddedSections" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                            Style="width: 95%; border-collapse: collapse; margin-left: 20px;" OnPreRender="grdGroups_PreRender">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ref. No." ItemStyle-CssClass="txtCenter">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRefNo" runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sections">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                        <asp:Label ID="lblZoneID" runat="server" Visible="false" Text='<%# Bind("ZoneID") %>'></asp:Label>
                                                        <asp:Label ID="lblSection" runat="server" Text='<%# Bind("Section") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-left">
                                                    <HeaderTemplate>
                                                        <label>Added Details</label><br />
                                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Added By]</label><br />
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
                                                                <asp:Label ID="ENTERED_CAL_TZDAT" runat="server" Text='<%# Eval("ENTERED_CAL_TZDAT")+", "+"("+ Eval("ENTERED_TZVAL") +")" %>' ForeColor="Red"></asp:Label>
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
                <br />
                <div class="box-body row">
                    <div class="col-md-5">
                        <div class="box box-danger">
                            <div class="box-header ">
                                <h4 class="box-title" align="left">Add Artifacts
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px; height:300px;">
                                    <div class="row">
                                        <asp:GridView ID="gvNewArtifacts" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                            Style="width: 95%; border-collapse: collapse; margin-left: 20px;" OnPreRender="grdGroups_PreRender">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ref. No." ItemStyle-CssClass="txtCenter">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRefNo" runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Artifacts">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                        <asp:Label ID="lblZoneID" runat="server" Visible="false" Text='<%# Bind("ZoneID") %>'></asp:Label>
                                                        <asp:Label ID="lblSectionID" runat="server" Visible="false" Text='<%# Bind("SectionID") %>'></asp:Label>
                                                        <asp:Label ID="lblArtifact" runat="server" Text='<%# Bind("Artifact") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-1" style="padding-top: 10%; padding-bottom: 10%;">
                        <div class="box-body">
                            <div class="row txtCenter">
                                <asp:LinkButton ID="lbtnAddArtifacts" ForeColor="White" Text="Add" runat="server"
                                    CssClass="btn btn-primary btn-sm" ValidationGroup="Grp" OnClick="lbtnAddArtifacts_Click" />
                            </div>
                            <div class="row txtCenter">
                                &nbsp;
                            </div>
                            <div class="row txtCenter">
                                <asp:LinkButton ID="lbtnRemoveArtifacts" ForeColor="White" Text="Remove" runat="server"
                                    ValidationGroup="Grp" CssClass="btn btn-primary btn-sm" OnClick="lbtnRemoveArtifacts_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-danger">
                            <div class="box-header ">
                                <h4 class="box-title" align="left">Added Artifacts
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px; height:300px;">
                                    <div class="row">
                                        <asp:GridView ID="gvAddedArtifacts" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                            Style="width: 95%; border-collapse: collapse; margin-left: 20px;" OnPreRender="grdGroups_PreRender">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ref. No." ItemStyle-CssClass="txtCenter">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRefNo" runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Artifacts">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                        <asp:Label ID="lblZoneID" runat="server" Visible="false" Text='<%# Bind("ZoneID") %>'></asp:Label>
                                                        <asp:Label ID="lblSectionID" runat="server" Visible="false" Text='<%# Bind("SectionID") %>'></asp:Label>
                                                        <asp:Label ID="lblArtifacts" runat="server" Text='<%# Bind("Artifact") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-left">
                                                    <HeaderTemplate>
                                                        <label>Added Details</label><br />
                                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Added By]</label><br />
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
                                                                <asp:Label ID="ENTERED_CAL_TZDAT" runat="server" Text='<%# Eval("ENTERED_CAL_TZDAT")+", "+"("+ Eval("ENTERED_TZVAL") +")" %>' ForeColor="Red"></asp:Label>
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
                <br />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmitGroups" />
            <asp:PostBackTrigger ControlID="btnUpdateGroups" />
            <asp:PostBackTrigger ControlID="lbnGroupsExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
