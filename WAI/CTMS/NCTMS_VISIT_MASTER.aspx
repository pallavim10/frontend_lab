<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NCTMS_VISIT_MASTER.aspx.cs" Inherits="CTMS.NCTMS_VISIT_MASTER" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        $(document).on("click", ".cls-btnSave1", function () {
            var test = "0";

            $('.required1').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "-1" || value == null || value == "-Select-" || value == "--Select--" || value == "0") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    if (value == "") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
            });

            if (test == "1") {
                return false;
            }
            return true;
        });

        $(document).on("click", ".cls-btnSave2", function () {
            var test = "0";

            $('.required2').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "-1" || value == null || value == "-Select-" || value == "--Select--" || value == "0") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    if (value == "") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
            });

            if (test == "1") {
                return false;
            }
            return true;
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Manage Visit Type Master</h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                <asp:HiddenField ID="hdnID" runat="server" />
            </div>
        </div>
        <asp:UpdatePanel ID="uplvisit" runat="server">
            <ContentTemplate>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="box box-primary" style="min-height: 300px;">
                                    <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                        <h4 class="box-title" align="left">
                                            Add Visit Type
                                        </h4>
                                    </div>
                                    <div class="box-body">
                                        <div align="left" style="margin-left: 5px">
                                            <div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="label col-md-4">
                                                            Enter Visit Name : &nbsp;
                                                            <asp:Label ID="Label7" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtVisitName" runat="server" CssClass="form-control required"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="label col-md-4">
                                                            Enter Visit Initial :&nbsp;
                                                            <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtVisitInitial" runat="server" CssClass="form-control required"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="label col-md-4">
                                                            Enter Sequence No. :&nbsp;
                                                            <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtVisitSEQNO" runat="server" CssClass="form-control required"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="label col-md-4">
                                                            Unblind :&nbsp;
                                                            <asp:Label ID="Label3" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:DropDownList runat="server" ID="ddlUnblind" CssClass="form-control txt_center width200px required">
                                                                <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                                <asp:ListItem Value="Blinded" Text="Blinded"></asp:ListItem>
                                                                <asp:ListItem Value="Unblinded" Text="Unblinded"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="label col-md-4">
                                                        </div>
                                                        <div class="col-md-8" align="right">
                                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                                OnClick="btnSubmit_Click" />
                                                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                                OnClick="btnUpdate_Click" Visible="false" />
                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm"
                                                                OnClick="btnCancel_Click"></asp:Button>
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
                                                            <asp:GridView ID="grdVisitType" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdVisitType_RowCommand"
                                                                OnRowDataBound="grdVisitType_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Seq No." ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="SEQNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Visit Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="VISIT_NAME" runat="server" Text='<%# Bind("VISIT_NAME") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Visit Initial" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="VISIT_INITIAL" runat="server" Text='<%# Bind("VISIT_INITIAL") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Unblind" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Unblind" runat="server" Text='<%# Bind("Unblind") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                                CommandName="Edit1" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="lbtndelete" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                                CommandName="Delete1" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                <div class="box-body">
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
                                                                            <asp:Label ID="MODULEID" runat="server" Text='<%# Bind("MODULEID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="MODULENAME">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="MODULENAME" runat="server" Text='<%# Bind("MODULENAME") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Tracker" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkTracker" />
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
                                                            <asp:Button ID="btnSubmitModule" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1"
                                                                OnClick="btnSubmitModule_Click" />
                                                            <asp:Button ID="btnUpdateModule" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1" />
                                                            <asp:Button ID="btnCancelModule" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
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
                                                            <asp:GridView ID="grdModule" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdModule_RowCommand"
                                                                OnRowDataBound="grdModule_RowDataBound">
                                                                <Columns>
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
                                                                    <asp:TemplateField HeaderText="Visit">
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
                                                                    <asp:TemplateField HeaderText="INDICATIONID" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="disp-none"
                                                                        HeaderStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="INDICATIONID" runat="server" Text='<%# Bind("INDICATIONID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Tracker" ItemStyle-CssClass="txtCenter">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="chkTracker" Text='<%# Bind("TRACKER") %>' runat="server"></asp:Label>
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
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="box box-primary" id="div17" runat="server">
                                <div class="box-header">
                                    <h3 class="box-title">
                                        Define Visit Type Email IDs</h3>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <label>
                                                Select Visit ID:</label>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:DropDownList Style="width: 250px;" ID="ddlvisitID" runat="server" class="form-control drpControl required2"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlvisitID_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="rows">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:GridView runat="server" ID="gvVisitEmailds" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                Style="width: 100%; border-collapse: collapse;">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Site ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                                runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                                runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                                runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                &nbsp;
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Button ID="btnSubmitVisitEmails" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave2"
                                                    OnClick="btnSubmitVisitEmails_Click" />&nbsp;&nbsp;
                                                <asp:Button ID="btnCancelVisitEmails" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave2"
                                                    OnClick="btnCancelVisitEmails_Click" />
                                            </div>
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
    </div>
</asp:Content>
