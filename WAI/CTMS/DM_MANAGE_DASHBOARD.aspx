<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DM_MANAGE_DASHBOARD.aspx.cs" Inherits="CTMS.DM_MANAGE_DASHBOARD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="CommonFunctionsJs/DM/DM_ConfirmMsg.js"></script>
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script type="text/javascript" src="CommonFunctionsJs/ControlJS.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/btnSave_Required.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/DB/DB_showAuditTrail.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/TabIndex.js"></script>
    <script type="text/javascript">
        function pageLoad() {

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
    <script type="text/javascript">
        window.history.replaceState(null, "", window.location.href);
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <contenttemplate>
        <div class="box box-primary">
            <div class="box-header">
                <h3 class="box-title">Manage Dashboard
                </h3>

            </div>

            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    <asp:HiddenField runat="server" ID="hfValues" />
                </div>
            </div>
            <div class="box-body">
                
                <div class="row">
                    
                    <div class="col-md-12">
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div class="row">                                   
                                    <div class="col-md-6">
                                        <div class="box box-primary" style="min-height: 300px;">
                                            <div class="box-header with-border">
                                                <h4 class="box-title" align="left">Dashboard Sequence
                                                </h4>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <asp:Label CssClass="col-form-label" Font-Bold="true" runat="server">Type :</asp:Label>
                                                    </div>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList ID="Dashboardtype" runat="server" CssClass="form-control required"  OnSelectedIndexChanged="Dashboard_ValChange" AutoPostBack="true" TabIndex="1">
                                                            <asp:ListItem Value="0" Text="--SELECT--"></asp:ListItem>
                                                            <asp:ListItem Value="Query Details" Text="Query Details"></asp:ListItem>
                                                            <asp:ListItem Value="Subject Module Status Details" Text="Subject Module Status Details"></asp:ListItem>
                                                            <asp:ListItem Value="Listing Tile" Text="Listing Tile"></asp:ListItem>
                                                            <asp:ListItem Value="Listing Graph" Text="Listing Graph"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row ">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <asp:Label CssClass="col-form-label" Font-Bold="true" runat="server">Sequence Number :</asp:Label>
                                                    </div>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txtSeqNo" runat="server"
                                                            CssClass="form-control numeric required" TabIndex="2"></asp:TextBox>
                                                    </div>
                                                    <asp:HiddenField ID="hdnID" runat="server" />
                                                    <asp:HiddenField ID="hdnTYPE" runat="server" />
                                                    <asp:HiddenField ID="hdnSeqNo" runat="server" />
                                                </div>
                                            </div>
                                            <br />

                                            <div class="row ">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        &nbsp;
                                                    </div>
                                                    <div class="col-md-8">
                                                        <asp:Button ID="btnSubmitClick" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnSubmit_Click" />
                                                        <asp:Button ID="btnUpdateClick" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnUpdate_Click" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btncancel" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm" OnClick="btncancel_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="box box-primary" style="min-height: 300px;">
                                            <div class="box-header with-border">
                                                <h4 class="box-title" align="left">Records
                                                </h4>
                                            </div>
                                            <div class="box-body">
                                                <div align="left" style="margin-left: 0px">
                                                    <div class="rows">
                                                        <div style="width: 100%; min-height: 300px; overflow: auto;">
                                                            <asp:GridView ID="grdManageDashboard" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                                                Style="border-collapse: collapse; width: 90%;" OnRowCommand="grdManageDashboard_RowCommand" OnPreRender="grdManageDashboard_PreRender">
                                                                <%--   OnRowDataBound="grdManageDashboard_RowDataBound"--%>
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                                CommandName="EditDashboardSeqno" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Type" HeaderStyle-CssClass="width200px" ItemStyle-CssClass="width200px" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblType" runat="server" Text='<%# Eval("TYPE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sequence No" HeaderStyle-CssClass="width100px" ItemStyle-CssClass="width80px;" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSEQNO" runat="server" Text='<%# Eval("SEQUENCENO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnAudttrail" runat="server" ToolTip="Audit Trail" OnClientClick="return showAuditTrail('DM_DASHBOARD_SEQNO', this);"><i class="fa fa-clock-o" style="color:blue; font-size:15px;"></i></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtndeletesequence" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                                CommandName="DeleteDashboardSeqno" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this Sequence for Dashboard : ", Eval("TYPE")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>&nbsp;&nbsp;
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
                <br />
            </div>
        
    </contenttemplate>
</asp:Content>
