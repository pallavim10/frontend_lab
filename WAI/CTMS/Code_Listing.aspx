<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Code_Listing.aspx.cs" Inherits="CTMS.Code_Listing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
                <h3 class="box-title">Coded Listing
                </h3>
            </div>
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info">
                    <br />
                    <div style="display: inline-flex">
                        <div style="display: inline-flex">
                            <label class="label">
                                Select Dictionary :
                            </label>
                            <div class="Control">
                                <asp:DropDownList runat="server" ID="drpdictionary" CssClass="form-control required">
                                    <asp:ListItem Text="--Select--" Value="0">
                                    </asp:ListItem>
                                    <asp:ListItem Text="MedDRA" Value="MedDRA">
                                    </asp:ListItem>
                                    <asp:ListItem Text="WHODD" Value="WHODD">
                                    </asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div style="display: inline-flex">
                        <div style="display: inline-flex">
                            <label class="label">
                                Select Coding Type :
                            </label>
                            <div class="Control">
                                <asp:DropDownList runat="server" ID="drpCodeType" CssClass="form-control required">
                                    <asp:ListItem Text="--Select--" Value="0">
                                    </asp:ListItem>
                                    <asp:ListItem Text="Auto" Value="Auto">
                                    </asp:ListItem>
                                    <asp:ListItem Text="Manual" Value="Manual">
                                    </asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div style="display: inline-flex">
                        <div style="display: inline-flex">
                            <div class="Control">
                                <asp:Button ID="btngetdata" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btngetdata_Click" />&nbsp&nbsp&nbsp&nbsp
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                    </div>
                    <br />
                    <div class="box-body">
                        <div class="rows">
                            <div style="width: 100%; overflow: auto;">
                                <div runat="server" id="divManual">
                                    <asp:GridView ID="grdData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                        OnPreRender="gridData_PreRender" CssClass="table table-bordered Datatable table-striped" OnRowDataBound="grdData_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <label>Coding Details</label><br />
                                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Coded By]</label><br />
                                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="RequestByName" runat="server" Text='<%# Bind("RequestByName") %>' ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="Request_CAL_DAT" runat="server" Text='<%# Bind("Request_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="Request_CAL_TZDAT" runat="server" Text='<%# Bind("Request_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="Request_TZVAL" runat="server" Text='<%# Bind("Request_TZVAL") %>' ForeColor="Red"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <label>Approved Details</label><br />
                                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Approve By]</label><br />
                                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="ApprovalByName" runat="server" Text='<%# Bind("ApprovalByName") %>' ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="Approve_CAL_DAT" runat="server" Text='<%# Bind("Approve_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="Approve_CAL_TZDAT" runat="server" Text='<%# Bind("Approve_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="Approve_TZVAL" runat="server" Text='<%# Bind("Approve_TZVAL") %>' ForeColor="Red"></asp:Label>
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
</asp:Content>
