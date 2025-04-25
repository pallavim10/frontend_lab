<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Sponsor_Team.aspx.cs" Inherits="CTMS.Sponsor_Team" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        function pageLoad() {
            $(".Datatable1").dataTable({ "bSort": false, "ordering": false,
                "bDestroy": true, stateSave: false
            });
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="box box-primary">
                <div class="box-header">
                    <div>
                    <h3 class="box-title">
                        Add Team Member
                    </h3>
                        </div>
                     <div id="Div2" class="dropdown" runat="server" style="display: inline-flex"><h3 class="box-title">
                        <asp:LinkButton runat="server" ID="btnSponsorTeamMemberExport" OnClick="btnSponsorTeamMemberExport_Click" ToolTip="Export to Excel Sponsor Team Member"
                               Text="" CssClass="dropdown-item dropdown-toggle glyphicon glyphicon-download-alt" Style="color: darkblue;"></asp:LinkButton>
				             </h3>
                        </div>

                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Select Sponsor : &nbsp;
                                    <asp:Label ID="Label7" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlSponsor" runat="server" Width="250px" class="form-control drpControl required"
                                        ValidationGroup="View" AutoPostBack="true" OnSelectedIndexChanged="ddlSponsor_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    First Name : &nbsp;
                                    <asp:Label ID="Label5" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control required"></asp:TextBox>
                                </div>
                                <div class="label col-md-2">
                                    Last Name :&nbsp;
                                    <asp:Label ID="Label6" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control required"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Email Address : &nbsp;
                                    <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtEmailID" CssClass="form-control required"></asp:TextBox>
                                </div>
                                <div class="label col-md-2">
                                    Mobile Phone :&nbsp;
                                    <asp:Label ID="Label3" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" ID="txtMobilePhone" CssClass="form-control required"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Department : &nbsp;
                                    <asp:Label ID="Label9" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtDepartment" CssClass="form-control required"></asp:TextBox>
                                </div>
                                <div class="label col-md-2">
                                    Designation :&nbsp;
                                    <asp:Label ID="Label12" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" ID="txtDesignation" CssClass="form-control required"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Country : &nbsp;
                                    <asp:Label ID="Label13" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                     <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" CssClass="form-control required"
                                        OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="label col-md-2">
                                    State :&nbsp;
                                    <asp:Label ID="Label14" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlstate" runat="server" AutoPostBack="true" CssClass="form-control required"
                                        OnSelectedIndexChanged="ddlstate_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    City : &nbsp;
                                    <asp:Label ID="Label4" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                     <asp:DropDownList ID="ddlcity" runat="server" AutoPostBack="true" CssClass="form-control required">
                                    </asp:DropDownList>
                                </div>
                                <div class="label col-md-2">
                                    ZIP/Postal Code :&nbsp;
                                    <asp:Label ID="Label11" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" ID="txtPostal" CssClass="form-control required"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Address :&nbsp;
                                    <asp:Label ID="Label10" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtAddress" TextMode="MultiLine" CssClass="form-control required"
                                        Style="width: 250px;"></asp:TextBox>
                                </div>
                                <div class="label col-md-2">
                                    Notes :&nbsp;
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" ID="txtNotes" TextMode="MultiLine" CssClass="form-control "
                                        Style="width: 250px;"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div style="margin-left: 25%;">
                                <asp:LinkButton runat="server" ID="lbtnUpdate" Text="Update" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave"
                                    OnClick="lbtnUpdate_Click"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="lbtnSubmit" Text="Submit" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave"
                                    OnClick="lbtnSubmit_Click"></asp:LinkButton>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm"
                                    OnClick="lbtnCancel_Click"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <asp:GridView runat="server" ID="gvEmp" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable1"
                        OnRowCommand="gvEmp_RowCommand" OnRowDataBound="gvEmp_RowDataBound" AllowSorting="True"
                        OnPreRender="gvEmp_PreRender">
                        <Columns>
                              <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                              <asp:LinkButton ID="lbtnupdateEmp" runat="server" CommandArgument='<%# Bind("ID") %>'
                                        CommandName="Edit1" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                </ItemTemplate>
                                  </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_EMPID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email ID" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("EmailID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contact No." ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblCont" runat="server" Text='<%# Bind("Mobile") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Department" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblCompany" runat="server" Text='<%# Bind("Department") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblJob" runat="server" Text='<%# Bind("Designation") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtndeleteEmp" runat="server" CommandArgument='<%# Bind("ID") %>'
                                        CommandName="Delete1" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="width30px txtCenter disp-none" HeaderStyle-CssClass="disp-none">
                                <HeaderTemplate>
                                    <asp:Button ID="Btn_Add_Fun" runat="server" OnClick="Btn_Add_Fun_Click" Text="Add"
                                        CssClass="btn btn-primary btn-sm " />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="width30px txtCenter disp-none" HeaderStyle-CssClass="disp-none">
                                <HeaderTemplate>
                                    <asp:Button ID="Btn_Remove_Fun" runat="server" OnClick="Btn_Remove_Fun_Click" Text="Remove"
                                        CssClass="btn btn-primary btn-sm " />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="Chk_Sel_Remove_Fun" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSponsorTeamMemberExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
