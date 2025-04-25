<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DM_CreateProjectMaster.aspx.cs" Inherits="CTMS.DM_CreateProjectMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="CommonFunctionsJs/DM/DM_ConfirmMsg.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/btnSave_Required.js"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <script type="text/javascript">
        function pageLoad() {

            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": false,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true,
                fixedHeader: true
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box-body">
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="box box-primary">
                                <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                    <h4 class="box-title" align="left">Add Module Into Visit
                                    </h4>
                                </div>
                                <div class="box-body">
                                    <div align="left" style="margin-left: 5px">
                                        <div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-2">
                                                        <label>
                                                            Select Visit:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList Style="width: 250px;" ID="drpModuleVisit" runat="server" class="form-control drpControl"
                                                            AutoPostBack="true" OnSelectedIndexChanged="drpModuleVisit_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="rows">
                                                <div class="fixTableHead">
                                                    <asp:GridView ID="grdModuleVisit" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                                        Style="width: 94%; border-collapse: collapse; margin-left: 20px;" OnRowDataBound="grdModuleVisit_RowDataBound" OnPreRender="grdModule_PreRender">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Module Id" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="disp-none"
                                                                HeaderStyle-CssClass="disp-none">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="MODULEID" runat="server" Text='<%# Bind("MODULEID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Module Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="MODULENAME" runat="server" Text='<%# Bind("MODULENAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="eSource" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkeSourceModule" />                                                                    
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status/Select" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkVisit" />
                                                                    <asp:Label ID="lblStatus" runat="server" ForeColor="Blue"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-5">
                                                        &nbsp;
                                                    </div>
                                                    <div class="col-md-7">
                                                        <asp:Button ID="btnSubmitModule" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1"
                                                            OnClick="btnSubmitModule_Click" />
                                                        <asp:Button ID="btnUpdateModule" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1" />
                                                        <asp:Button ID="btnCancelModule" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm"
                                                            OnClick="btnCancelModule_Click" />
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
                    <div class="row">
                        <div class="col-md-12">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h4 class="box-title" align="left">Records
                                    </h4>
                                </div>
                                <div class="box-body">
                                    <div align="left" style="margin-left: 5px">
                                        <div>
                                            <div class="rows">
                                                <div class="fixTableHead">
                                                    <div>
                                                        <asp:GridView ID="grdModule" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                                            OnRowCommand="grdModule_RowCommand"
                                                            OnRowDataBound="grdModule_RowDataBound" OnPreRender="grdModule_PreRender">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="VISITNUM" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="disp-none"
                                                                    HeaderStyle-CssClass="disp-none">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="VISITNUM" runat="server" Text='<%# Bind("VISITNUM") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Visit" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="VISIT" runat="server" Text='<%# Bind("VISIT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Module Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="MODULENAME" runat="server" Text='<%# Bind("MODULENAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="MODULE ID" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="disp-none"
                                                                    HeaderStyle-CssClass="disp-none">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="MODULEID" runat="server" Text='<%# Bind("MODULEID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="eSource" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="eSource_Module" runat="server" CommandArgument='<%# Eval("eSOURCE_MODULE") %>' Style="color: #333333; font-size: initial; font-weight: bold;">
                                                                            <i id="iconESource_Module" runat="server" class="fa fa-check"></i></asp:Label>

                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-Width="20%" HeaderStyle-CssClass="align-left" ItemStyle-Width="20%">
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
                                                                <asp:TemplateField HeaderStyle-Width="20%" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none" ItemStyle-Width="20%">
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
                                                                        <asp:LinkButton ID="lbtndeleteSection" runat="server" CommandName="DeleteModule" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this module : ", Eval("MODULENAME")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
