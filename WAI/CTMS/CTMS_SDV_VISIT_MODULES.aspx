<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CTMS_SDV_VISIT_MODULES.aspx.cs" Inherits="CTMS.CTMS_SDV_VISIT_MODULES" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script runat="server"> 
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            // the following line is important 
            MasterPage master = this.Master;
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
                            <div class="col-md-6">
                                <div class="box box-primary" style="min-height: 300px;">
                                    <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                        <h4 class="box-title" align="left">
                                            Add Visit
                                        </h4>
                                    </div>
                                    <div class="box-body">
                                        <div align="left" style="margin-left: 5px">
                                            <div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Select Indication:</label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:DropDownList runat="server" ID="drpIndication" CssClass="form-control required">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Enter Visit ID:</label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:TextBox Style="width: 60px;" MaxLength="3" ID="txtVisitID" ValidationGroup="section"
                                                                runat="server" CssClass="form-control numeric required"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Enter Visit Name :</label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:TextBox Style="width: 150px;" ID="txtVisitName" ValidationGroup="section" runat="server"
                                                                CssClass="form-control required"></asp:TextBox>
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
                                                            <asp:Button ID="btnsubmitSectionVisit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                                OnClick="btnsubmitSectionVisit_Click" />
                                                            <asp:Button ID="btnupdateSectionVisit" Text="Update" Visible="false" runat="server"
                                                                CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnupdateSectionVisit_Click" />
                                                            <asp:Button ID="btncancelSectionVisit" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                                OnClick="btncancelSectionVisit_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="box box-primary">
                                    <div class="box-header with-border" style="float: left;">
                                        <h4 class="box-title" align="left">
                                            Records
                                        </h4>
                                    </div>
                                    <div class="box-body">
                                        <div align="left" style="margin-left: 5px">
                                            <div>
                                                <div class="rows">
                                                    <div style="width: 100%; height: 264px; overflow: auto;">
                                                        <div>
                                                            <asp:GridView ID="grdVisit" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdVisit_RowCommand">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="INDICATIONID" ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none"
                                                                        ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblINDICATIONID" runat="server" Text='<%# Bind("INDICATIONID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="INDICATION" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblINDICATION" runat="server" Text='<%# Bind("INDICATION") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Visit ID" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSecSEQ" runat="server" Text='<%# Bind("VISITNUM") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Visit Name" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblcategory" runat="server" Text='<%# Bind("VISIT") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("VISITNUM") %>'
                                                                                CommandName="EditVisit" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="lbtndeleteSection" runat="server" CommandArgument='<%# Bind("VISITNUM") %>'
                                                                                CommandName="DeleteVisit" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="box box-primary" style="min-height: 300px;">
                                    <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                        <h4 class="box-title" align="left">
                                            Add Module
                                        </h4>
                                    </div>
                                    <div class="box-body">
                                        <div align="left" style="margin-left: 5px">
                                            <div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Enter Module Name :</label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:TextBox Style="width: 250px;" ID="txtModuleName" ValidationGroup="section" runat="server"
                                                                CssClass="form-control required1"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Enter Seq No :</label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:TextBox Style="width: 60px;" ID="txtModuleSeqNo" ValidationGroup="section" runat="server"
                                                                CssClass="form-control numeric required1"></asp:TextBox>
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
                                                            <asp:Button ID="btnSubmitModule" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1"
                                                                OnClick="btnSubmitModule_Click" />
                                                            <asp:Button ID="btnUpdateModule" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1"
                                                                OnClick="btnUpdateModule_Click" />
                                                            <asp:Button ID="btnCancelModule" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                                OnClick="btnCancelModule_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="box box-primary">
                                    <div class="box-header with-border" style="float: left;">
                                        <h4 class="box-title" align="left">
                                            Records
                                        </h4>
                                    </div>
                                    <div class="box-body">
                                        <div align="left" style="margin-left: 5px">
                                            <div>
                                                <div class="rows">
                                                    <div style="width: 100%; height: 264px; overflow: auto;">
                                                        <div>
                                                            <asp:GridView ID="grdModule" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdModule_RowCommand">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                                                                        ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Seq No" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="SEQNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Module Name" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="MODULENAME" runat="server" Text='<%# Bind("MODULENAME") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                                CommandName="EditModule" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="lbtndeleteSection" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                                CommandName="DeleteModule" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="box box-primary" style="min-height: 300px;">
                                    <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                        <h4 class="box-title" align="left">
                                            Add Module Into Visit
                                        </h4>
                                    </div>
                                    <div class="box-body">
                                        <div align="left" style="margin-left: 5px">
                                            <div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Select Indication:</label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:DropDownList runat="server" ID="drpModuleIndication" CssClass="form-control required1"
                                                                AutoPostBack="True" OnSelectedIndexChanged="drpModuleIndication_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Select Visit ID:</label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:DropDownList Style="width: 250px;" ID="drpModuleVisit" runat="server" class="form-control drpControl"
                                                                AutoPostBack="true" OnSelectedIndexChanged="drpModuleVisit_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="rows">
                                                    <div style="width: 100%; overflow: auto;">
                                                        <div>
                                                            <asp:GridView ID="grdModuleVisit" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="MODULEID" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="MODULEID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="MODULENAME" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="MODULENAME" runat="server" Text='<%# Bind("MODULENAME") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkVisit" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
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
                                                            <asp:Button ID="btnAddModule" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1"
                                                                OnClick="btnAddModule_Click" />
                                                            <asp:Button ID="btnClearModule" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                                OnClick="btnClearModule_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="box box-primary" style="min-height: 300px;">
                                    <div class="box-header with-border" style="float: left;">
                                        <h4 class="box-title" align="left">
                                            Records
                                        </h4>
                                    </div>
                                    <div class="box-body">
                                        <div align="left" style="margin-left: 5px">
                                            <div>
                                                <div class="rows">
                                                    <div style="width: 100%; height: 264px; overflow: auto;">
                                                        <div>
                                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="GridView1_RowCommand">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="disp-none"
                                                                        HeaderStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="INDICATION" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblINDICATION" runat="server" Text='<%# Bind("INDICATION") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
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
                                                                    <asp:TemplateField HeaderText="Module Name" ItemStyle-HorizontalAlign="Center">
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
                                                                    <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtndeleteSection" runat="server" CommandName="DeleteField" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
