<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NSAE_SYNC_FIELD_MAPPING.aspx.cs" Inherits="CTMS.NSAE_SYNC_FIELD_MAPPING" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">SAE MAPPING FIELD</h3>
            &nbsp&nbsp&nbsp
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="lblError">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
                <div class="box-body">
                    <div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="col-md-4" style="width: 32%">
                                    <asp:Label ID="lbltypeofSAE" class="label" runat="server" Text="Type of SAE :"></asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="DrptypeofSAE" CssClass="form-control" Style="width: 225px;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DrptypeofSAE_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-4" style="width: 32%">
                                </div>
                                <div class="col-md-8">
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <div class="col-md-4" style="width: 32%">
                                    <asp:Label ID="lblSaeModuleName" class="label" runat="server" Text="SAE Module Name :"></asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="DrpSaeModuleName" CssClass="form-control" Style="width: 225px;" runat="server"
                                        AutoPostBack="true" OnSelectedIndexChanged="DrpSaeModuleName_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-4" style="width: 32%">
                                    <asp:Label ID="dblDMModuleName" class="label" runat="server" Text="DM Module Name :"></asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="DrpDMModuleName" CssClass="form-control" Style="width: 225px;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DrpDMModuleName_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div style="overflow-x: auto;">
                            <asp:GridView ID="GrdGetData" runat="server" AutoGenerateColumns="false" OnRowDataBound="GrdGetData_RowDataBound" CssClass="table table-bordered table-striped Datatable1" Height="199px" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                                        ItemStyle-CssClass="disp-none" HeaderText="ID">
                                        <ItemTemplate>
                                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SAE Field Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsaefieldname" CssClass="form-control" Style="min-height: 25px; min-width: 300px; width: auto; height: auto" runat="server" Text='<%# Bind("FIELDNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DM Field Name">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="DrpDmField" CssClass="form-control" Style="min-height: 25px; min-width: 300px; width: auto; height: auto" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="form-group">
                        </div>
                        <div class="row">
                            <div class="row txt_center">
                                <asp:LinkButton ID="lbAdd" Text="Submit" runat="server" Style="color: white;" Visible="false" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbAdd_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="lbUpdate" Text="Update" runat="server" Style="color: white;" Visible="True" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbUpdate_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="lbtnCancel" Text="Cancel" runat="server" Style="color: white;" Visible="false" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnCancel_Click" />
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

