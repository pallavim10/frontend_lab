<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Code_PushRecode.aspx.cs" Inherits="CTMS.Code_PushRecode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/CD/Coding_AuditTrails.js"></script>
    <script src="CommonFunctionsJs/CD/Coding_Grid_Queries.js"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            FillAuditDetails();
            FillAnsQUERIES();
            FillOPENQUERIES();
            FillCLOSEQUERIES();
        });
    </script>
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": false, "ordering": false,
                "bDestroy": true, stateSave: true
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="content">
        <div class="box box-warning">
            <div class="box-header">
                <h3 class="box-title">Push for Recoding
                </h3>
            </div>
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info">
                    <div class="row">
                        <div class="col-md-12">
                            <br />
                            <div class="col-md-2">
                                <label>
                                    Select Form:</label>
                            </div>
                            <div class="col-md-10">
                                <asp:DropDownList Style="width: 30%;" ID="drpForm" runat="server" class="form-control drpControl required"
                                    AutoPostBack="True" OnSelectedIndexChanged="drpForm_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                </div>
                <div class="box-body">
                    <div class="box box-success" runat="server" id="SITESUBJ" visible="false">
                        <br />
                        <div class="form-group">
                            <div runat="server" id="divSIte" style="display: inline-flex" visible="false">
                                <div style="display: inline-flex">
                                    <label class="label width70px">
                                        Site ID:
                                    </label>
                                    <div class="Control">
                                        <asp:DropDownList ID="ddlSite" runat="server" AutoPostBack="True" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlSite_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div runat="server" id="divSubject" style="display: inline-flex" visible="false">
                                <div style="display: inline-flex">
                                    <label class="label width70px">
                                        Subject ID:
                                    </label>
                                    <div class="Control">
                                        <asp:DropDownList ID="ddlSUBJID" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <asp:Button ID="btngetdata" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave"
                                OnClick="btngetdata_Click" />&nbsp&nbsp&nbsp&nbsp
                        </div>
                        <br />
                        <div class="box-body">
                            <div class="rows">
                                <div style="width: 100%; overflow: auto;">
                                    <div>
                                        <asp:GridView ID="gridData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                            OnPreRender="GridView1_PreRender" OnRowCommand="gridData_RowCommand" CssClass="table table-bordered Datatable table-striped" OnRowDataBound="gridData_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Push for Recoding">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnupdate" runat="server"
                                                            ToolTip="Push for Recoding" CommandName="Recoding"><i class="fa fa-edit"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="View">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnView" runat="server" ForeColor="White"
                                                            ToolTip="View" CommandName="View"><i class="fa fa-eye" style="font-size:17px; color:blue"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Query" HeaderStyle-CssClass="width100px align-center"
                                                    ItemStyle-CssClass="width100px align-center">
                                                    <ItemTemplate>
                                                        <div style="display: inline-flex;">
                                                            <asp:LinkButton ID="lnkQUERYSTATUS" ToolTip="Open Query" OnClientClick="return ShowOpenQuery_PVID_RECID(this);" runat="server">
                                                                     <i class="fa fa-question-circle" style="font-size:17px;color:maroon;"></i></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                                   <asp:LinkButton ID="lnkQUERYANS" ToolTip="Answered Open Query" OnClientClick="return ShowAnsQuery_PVID_RECID(this);" runat="server">
                                                                     <i class="fa fa-question-circle" style="font-size:17px;color:blue;"></i></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkQUERYCLOSE" ToolTip="Closed Query" OnClientClick="return ShowClosedQuery_PVID_RECID(this);" runat="server">
                                                                     <i class="fa fa-question-circle" style="font-size:17px;color:darkgreen;"></i></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Audit Trail" HeaderStyle-CssClass="width100px align-center"
                                                    ItemStyle-CssClass="width100px align-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="AD" ToolTip="Audit trail" OnClientClick="return showAuditTrail_All(this);" class="disp-none" runat="server">
                                                            <i class="fa fa-history" id="ADICON" runat="server" style="font-size: 17px"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Coding Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCODETYPE" Text='<%# Bind("CODETYPE") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <label>Coding Details</label><br />
                                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Coded By]</label><br />
                                                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div>
                                                            <asp:Label ID="CODEBY" runat="server" Text='<%# Bind("CODEBY") %>' ForeColor="Blue"></asp:Label>
                                                        </div>
                                                        <div>
                                                            <asp:Label ID="CODEDAT_SERVER" runat="server" Text='<%# Bind("CODEDAT_SERVER") %>' ForeColor="Green"></asp:Label>
                                                        </div>
                                                        <div>
                                                            <asp:Label ID="CODEDAT_USER" runat="server" Text='<%# Bind("CODEDAT_USER") %>' ForeColor="Red"></asp:Label>
                                                        </div>
                                                        <div>
                                                            <asp:Label ID="CODEDAT_TZVAL" runat="server" Text='<%# Bind("CODEDAT_TZVAL") %>' ForeColor="Red"></asp:Label>
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
            </div>
        </div>
    </div>
</asp:Content>
