<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_StudyRole.aspx.cs" Inherits="CTMS.UMT_StudyRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="Styles/datatables/dataTables.bootstrap.css" />
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/UMT/UMT_ConfirmMsg.js"></script>
    <script type="text/javascript">
        function pageLoad() {

            $(".Datatable1").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                stateSave: false
            });

            $(".Datatable1").parent().parent().addClass('fixTableHead');
        }

        function showAuditTrail(element) {

            var ID = $(element).closest('tr').find('td').eq(0).text().trim();
            var TABLENAME = 'UMT_StudyRole';

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/showAuditTrail",
                data: '{TABLENAME: "' + TABLENAME + '",ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $('#DivAuditTrail').html(data.d)
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

            $("#popup_AuditTrail").dialog({
                title: "Audit Trail",
                width: 900,
                height: 450,
                modal: true,
                buttons: {
                    "Close": function () { $(this).dialog("close"); }
                }
            });

            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-primary">
        <div class="box-header">
            <div>
                <h3 class="box-title" align="left">Add Study Role</h3>
            </div>
            <div id="Div3" class="pull-right" runat="server">
                <asp:LinkButton runat="server" ID="lbStudyRoleExport" OnClick="lbStudyRoleExport_Click"
                    CssClass="btn btn-info" Style="color: white; margin-top:3px;">
                        Export Study Role &nbsp;&nbsp; <span class="glyphicon glyphicon-download 2x"></span>
                </asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp;
            </div>
            <div class="lblError">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <br />
        <div class="form-group">
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-2">
                        <label>
                            Enter Study Role :</label>
                    </div>
                    <div class="col-md-10">
                        <asp:TextBox Style="width: 250px;" ID="txtStudyRole" ValidationGroup="section" runat="server"
                            CssClass="form-control required1"></asp:TextBox>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-2">
                        <label>
                            Applicable For:</label>
                    </div>
                    <div class="col-md-1" style="width: 135px;">
                        <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkInternal" />&nbsp;&nbsp;
                        <label>Internal User</label>
                    </div>
                    <div class="col-md-1" style="width: 135px;">
                        <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkSponsor" />&nbsp;&nbsp;
                        <label>Sponsor User</label>
                    </div>
                    <div class="col-md-1" style="width: 135px;">
                        <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkSite" />&nbsp;&nbsp;
                        <label>Site User</label>
                    </div>
                    <div class="col-md-1" style="width: 135px;">
                        <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkExternal" />&nbsp;&nbsp;
                        <label>External User</label>
                    </div>
                </div>
            </div>
            <div class="row" runat="server" visible="false" style="margin-top: 7px;">
                <div class="col-md-12">
                    <div class="col-md-4">
                        <label>&nbsp;</label>
                    </div>
                    <div class="col-md-4">
                        <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkEthicsComm" />&nbsp;&nbsp;
                         <label>Ethics Committee</label>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 7px;">
                <div class="col-md-12">
                    <div class="col-md-5">
                        &nbsp;
                    </div>
                    <div class="col-md-7">
                        <asp:Button ID="btnSubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1" OnClick="btnSubmit_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btnUpdate" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1" OnClick="btnUpdate_Click" />&nbsp;&nbsp
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnCancel_Click" />&nbsp;&nbsp;
                    </div>
                </div>
            </div>
            <br />
        </div>
        <div class="box box-primary">
            <div class="box-header with-border" style="float: left;">
                <h4 class="box-title" align="left">Records
                </h4>
            </div>
            <div class="box-body">
                <div align="left" style="margin-left: 5px">
                    <div>
                        <div class="rows">
                            <div style="width: 100%; overflow: auto; height: auto">
                                <div>
                                    <asp:GridView ID="grdStudyRoles" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                        CssClass="table table-bordered table-striped Datatable1" OnPreRender="grdUserDetails_PreRender" OnRowCommand="grdStudyRoles_RowCommand" OnRowDataBound="grdStudyRoles_RowDataBound">
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
                                                        CommandName="EditStudy" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Study Role" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStudyRole" runat="server" Text='<%# Bind("StudyRole") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Internal User" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInternal" runat="server" CommandArgument='<%# Eval("Internal") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconInternal" runat="server" class="fa fa-check"></i></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Site User" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSite" runat="server" CommandArgument='<%# Eval("Site") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconSite" runat="server" class="fa fa-check"></i></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sponsor User" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSponsor" runat="server" CommandArgument='<%# Eval("Sponsor") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconSponsor" runat="server" class="fa fa-check"></i></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EthicsComm" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEthicsComm" runat="server" Text='<%# Bind("EthicsComm") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="External User" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExternal" runat="server" CommandArgument='<%# Eval("OtherExternal") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconExternal" runat="server" class="fa fa-check"></i></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail(this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtndeleteSection" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                        CommandName="DeleteStudyRole" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this study role : ", Eval("StudyRole")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
</asp:Content>
