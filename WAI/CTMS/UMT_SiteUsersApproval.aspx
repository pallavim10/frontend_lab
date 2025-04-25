<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_SiteUsersApproval.aspx.cs" Inherits="CTMS.UMT_SiteUsersApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: false
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Site Users for Approval
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    <asp:HiddenField ID="hdnUserID" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <div class="box box-primary">
        <div class="box-header">
            <h3 class="box-title">Site User Details
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Site Id: &nbsp;
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblSiteID" runat="server" Font-Size="Small" CssClass=" form-control" Width="90%"></asp:Label>
                        </div>
                        <div class="label col-md-2">
                            Study Role:&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblStudyRole" runat="server" Font-Size="Small" CssClass=" form-control" Width="90%"></asp:Label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            First Name: &nbsp;
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblFirstName" runat="server" Font-Size="Small" CssClass=" form-control" Width="90%"></asp:Label>
                        </div>
                        <div class="label col-md-2">
                            Last Name:&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblLastName" runat="server" Font-Size="Small" CssClass=" form-control" Width="90%"></asp:Label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Email Id: &nbsp;
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblEmailID" runat="server" Font-Size="Small" CssClass=" form-control" Width="90%"></asp:Label>
                        </div>
                        <div class="label col-md-2">
                            Contact No:&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblContactNo" runat="server" Font-Size="Small" CssClass=" form-control" Width="90%"></asp:Label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Blinded/Unblinded: &nbsp;
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblUnblined" runat="server" Font-Size="Small" CssClass="form-control" Width="90%"></asp:Label>
                        </div>
                        <div class="label col-md-2">
                            Notes:
                        </div>
                        <div class="col-md-4">
                            <asp:Label runat="server" ID="lblNotes" CssClass="form-control" Width="90%" Height="100%" TextMode="MultiLine"></asp:Label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Select Systems & Privileges:
                                <asp:Label ID="Label3" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-8">
                            <asp:UpdatePanel runat="server" ID="upnlSystems">
                                <ContentTemplate>
                                    <table class="table table-bordered table-striped">
                                        <tr>
                                            <th class="col-md-4">Systems
                                            </th>
                                            <th class="col-md-4">Privileges
                                            </th>
                                            <th class="col-md-4">Notes (If any)
                                            </th>
                                        </tr>
                                        <asp:Repeater runat="server" ID="repeatSystem" OnItemDataBound="repeatSystem_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="col-md-4">
                                                        <asp:CheckBox ID="ChkSelect" runat="server" Enabled="false" />
                                                        &nbsp;
                                                        <asp:Label ID="lblSystemName" runat="server" Text='<%# Bind("SystemName") %>'></asp:Label>
                                                        <asp:Label ID="lblSystemID" runat="server" Text='<%# Bind("SystemID") %>' Visible="false"></asp:Label>
                                                        <asp:HiddenField runat="server" ID="HiddenSubSytem" Value='<%# Eval("SubSystem") %>' />
                                                    </td>
                                                    <td class="col-md-4">
                                                        <div runat="server" id="divSubsysIWRS" visible="false">
                                                            <asp:CheckBox ID="ChkSubsysIWRS" runat="server" Enabled="false" />
                                                            &nbsp;
                                                            <asp:Label ID="lblSubsystemIWRS" runat="server" Text='Unblinding'></asp:Label>
                                                            <br />
                                                        </div>
                                                        <div runat="server" id="divSubSysPharma" visible="false">
                                                            <asp:CheckBox ID="ChkSubSysPharma" runat="server" Enabled="false" />
                                                            &nbsp;
                                                           <asp:Label ID="lblSubSysPharma" runat="server" Text='Sign-Off'></asp:Label>
                                                            <br />
                                                        </div>
                                                        <div runat="server" id="divsubsysDM" visible="false">
                                                            <asp:CheckBox ID="chksubsysDM" runat="server" Enabled="false" />
                                                            &nbsp;
                                                            <asp:Label ID="LblsubsysDM" runat="server" Text='Sign-Off'></asp:Label>
                                                            <br />
                                                        </div>
                                                        <div runat="server" id="divsubsyseSource" visible="false">
                                                            <asp:CheckBox ID="ChksubsyseSource" runat="server" Enabled="false" />
                                                            &nbsp;
                                                            <asp:Label ID="lblsubsyseSource" runat="server" Text='Sign-Off'></asp:Label>
                                                            <br />
                                                            <asp:CheckBox ID="chlReadOnlyeSource" runat="server" Enabled="false" />
                                                            &nbsp;
                                                            <asp:Label ID="LblReadOnly" runat="server" Text='Read-Only'></asp:Label>
                                                        </div>
                                                    </td>
                                                    <td class="col-md-4">
                                                        <asp:Label runat="server" ID="lblSystemNotes" CssClass="form-control" Width="100%" TextMode="MultiLine" Height="100%" Enabled="false"></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Action: &nbsp;
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList runat="server" ID="drpAction" CssClass="form-control width250px required" Width="90%">
                                <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                <asp:ListItem Value="Approve" Text="Approve"></asp:ListItem>
                                <asp:ListItem Value="Disapprove" Text="Disapprove"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="label col-md-2">
                            Comment:&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtComment" TextMode="MultiLine" CssClass="form-control required" Width="90%"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <br />
                    <div class="row txt_center">
                        <asp:LinkButton runat="server" ID="lbtnSubmit" Text="Submit" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnSubmit_Click"></asp:LinkButton>
                        <asp:LinkButton runat="server" ID="lbnUpdate" Text="Update" ForeColor="White" CssClass="btn btn-primary btn-sm" Visible="false"></asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm" OnClick="lbtnCancel_Click"></asp:LinkButton>
                    </div>
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
