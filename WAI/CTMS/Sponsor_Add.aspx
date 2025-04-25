<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Sponsor_Add.aspx.cs" Inherits="CTMS.Sponsor_Add" %>

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
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">
                        Sponsor Details
                    </h3>
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
                                <div class="label col-md-2" style="width: 140px;">
                                    Company Name : &nbsp;
                                    <asp:Label ID="Label11" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-7">
                                    <asp:TextBox runat="server" ID="txtCompanyName" CssClass="form-control required"
                                        Width="100%"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2" style="width: 140px;">
                                    Contact No :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtContactNo" CssClass="form-control required"></asp:TextBox>
                                </div>
                                <div class="label col-md-2" style="width: 140px;">
                                    Website :&nbsp;
                                    <asp:Label ID="Label18" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" ID="txtWebsite" CssClass="form-control required"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2" style="width: 140px;">
                                    Country : &nbsp;
                                    <asp:Label ID="Label13" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" CssClass="form-control required"
                                        OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="label col-md-2" style="width: 140px;">
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
                                <div class="label col-md-2" style="width: 140px;">
                                    City : &nbsp;
                                    <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                      <asp:DropDownList ID="ddlcity" runat="server" AutoPostBack="true" CssClass="form-control required">
                                    </asp:DropDownList>
                                </div>
                                <div class="label col-md-2" style="width: 140px;">
                                    Address :&nbsp;
                                    <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" ID="txtAddress" TextMode="MultiLine" CssClass="form-control required"
                                        Style="width: 250px;"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div style="margin-left: 15%;">
                                <asp:LinkButton runat="server" ID="lbtnUpdate" Text="Update" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave"
                                    OnClick="lbtnUpdate_Click"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="lbtnSubmit" Text="Submit" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave"
                                    OnClick="lbtnSubmit_Click"></asp:LinkButton>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm"
                                    OnClick="lbtnCancel_Click"></asp:LinkButton>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
            <div class="box box-warning">
                <div class="box-header">
                   <div> <h3 class="box-title">
                        Sponsor List
                    </h3>
                       </div>
                     <div id="Div2" class="dropdown" runat="server" style="display: inline-flex"><h3 class="box-title">
                        <asp:LinkButton runat="server" ID="btnAddSponsorExport" OnClick="btnAddSponsorExport_Click" ToolTip="Export to Excel Sponsor Details"
                               Text="" CssClass="dropdown-item dropdown-toggle glyphicon glyphicon-download-alt" Style="color: darkblue;"></asp:LinkButton>
				             </h3>
                        </div>

                </div>
                <asp:GridView runat="server" ID="gvSponsor" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable1"
                    OnRowCommand="gvSponsor_RowCommand" OnRowDataBound="gvSponsor_RowDataBound" AllowSorting="True"
                    OnPreRender="gvSponsor_PreRender">
                    <Columns>
                         <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                 <asp:LinkButton ID="lbtnupdateSposnor" runat="server" CommandArgument='<%# Bind("ID") %>'
                                    CommandName="Edit1" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                            HeaderText="ID">
                            <ItemTemplate>
                                <asp:Label ID="lbl_SponsorID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Contact No." HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="lblCont" runat="server" Text='<%# Bind("ContactNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Website" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="lblWeb" runat="server" Text='<%# Bind("Website") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtndeleteSposnor" runat="server" CommandArgument='<%# Bind("ID") %>'
                                    CommandName="Delete1" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAddSponsorExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
