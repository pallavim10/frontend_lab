<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NSAE_TYPE.aspx.cs" Inherits="CTMS.NSAE_TYPE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">SAE TYPE
                    </h3>
                    <div class="lblError">
                        &nbsp;&nbsp;&nbsp;&nbsp; 
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                </div>
            </div>


            <div class="row">
                <div class="col-md-6">
                    <div class="box box-primary">
                        
                        <%--              <div class="box-header with-border" style="float: left;">
                            <h4 class="box-title" align="left">Records
                            </h4>
                        </div>
                        <br />--%>
                  
                        <div class="box-header with-border">
                            <h4 class="box-title" align="left">Add SAE Type
                            </h4>
                        </div>
                        <br />
                        <div class="box-body">
                            <div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:Label ID="Label1" class="label" runat="server" Text="Select SAE Type :"></asp:Label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtSaeType" runat="server" Width="300px" CssClass="form-control required width200px"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:Label ID="lblModuleName" class="label" runat="server" Text="Module Name :"></asp:Label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="DrpModuleName" class="form-control width300px required" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:Label ID="dblDMModuleName" class="label" runat="server" Text="Sequence No. :"></asp:Label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtSeqno" runat="server" CssClass="form-control required width200px" Width="300px"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-3">
                                    </div>
                                    <div class="col-md-6">
                                        <center>
                                            <div class="pull-Right" style="margin-right: 15px;">
                                                <div class="pull-Right">

                                                    <asp:LinkButton ID="lbSubmit" Text="Submit" runat="server"
                                                        Style="color: white;" Visible="True" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                        OnClick="lbSubmit_Click" />
                                                    <asp:LinkButton ID="lbUpdate" Text="Update" runat="server"
                                                        Style="color: white;" Visible="True" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                        OnClick="lbUpdate_Click" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                      <asp:LinkButton ID="lbtnCancel" Text="Cancel" runat="server"
                                                          Style="color: white;" CssClass="btn btn-primary btn-sm" OnClick="lbtnCancel_Click" />
                                                </div>
                                            </div>
                                        </center>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="box box-primary">
                        <div class="box-header with-border" style="float: left;">
                            <h4 class="box-title" align="left">Records
                            </h4>
                        </div>
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div>
                                    <div class="rows">
                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="grdSAETYPE" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdSAETYPE_RowCommand"
                                                    OnRowDataBound="grdSAETYPE_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText=" " ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>' CommandName="EditSAETYPE" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SEQ NO" HeaderStyle-CssClass="disp-inline" ItemStyle-CssClass="disp-inline" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="CNTRYID" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SAE TYPE" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="COUNTRYCOD" runat="server" Text='<%# Bind("TYPE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MODULE NAME" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="COUNTRY" runat="server" Text='<%# Bind("MODULENAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtndelete" runat="server" CommandArgument='<%# Bind("ID") %>' CommandName="DeleteSAETYPE" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
            <div class="box box-primary">
                <br />
                <div class="row">
                    <div class="col-md-5">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <label>
                                        Type of SAE:</label>
                                </div>
                                <div class="col-md-7">
                                    <asp:DropDownList ID="Drp_SAEType" class="form-control drpControl width200px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Drp_SAEType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-5">
                        <div class="box box-primary" style="min-height: 300px;">
                            <div class="box-header with-border" style="float: left;">
                                <h4 class="box-title" align="left">Available Fields
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div class="row">
                                        <div style="width: 100%; height: 400px; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="gvAvFields" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped"
                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="FIELD NAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblfieldname" runat="server" Text='<%# Bind("FIELDNAME") %>'></asp:Label>
                                                                <asp:Label ID="lblfieldid" CssClass="disp-none" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                <asp:Label ID="lblVARIABLENAME" CssClass="disp-none" runat="server" Text='<%# Bind("VARIABLENAME") %>'></asp:Label>
                                                                <asp:Label ID="lblTABLENAME" CssClass="disp-none" runat="server" Text='<%# Bind("TABLENAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SEQ NO" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtSEQNO" runat="server" CssClass="form-control width100px txt_center"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
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
                    <div class="col-md-1">
                        <div class="box-body">
                            <div style="min-height: 300px;">
                                <div class="row txtCenter">
                                    <asp:LinkButton ID="lbtnAddField" ForeColor="White" Text="Add" runat="server" CssClass="btn btn-primary btn-sm"
                                        ValidationGroup="Grp" OnClick="lbtnAddField_Click" />
                                </div>
                                <div class="row txtCenter">
                                    &nbsp;
                                </div>
                                <div class="row txtCenter">
                                    <asp:LinkButton ID="lbtnRemoveField" ForeColor="White" Text="Remove" runat="server"
                                        ValidationGroup="Grp" CssClass="btn btn-primary btn-sm" OnClick="lbtnRemoveField_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-primary" style="min-height: 300px;">
                            <div class="box-header with-border" style="float: left;">
                                <h4 class="box-title" align="left">Added Fields
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div class="row">
                                        <div style="width: 100%; height: 400px; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="gvAddedFields" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped"
                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="FIELD NAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFIELDNAME" runat="server" Text='<%# Bind("FIELDNAME") %>'></asp:Label>
                                                                <asp:Label ID="lblTYPEID" CssClass="disp-none" runat="server" Text='<%# Bind("TYPEID") %>'></asp:Label>
                                                                <asp:Label ID="lblFIELDID" CssClass="disp-none" runat="server" Text='<%# Bind("FIELDID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SEQNO" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtSEQNO" runat="server" Text='<%# Bind("SEQNO") %>' CssClass="form-control width100px txt_center" style="pointer-events: none;"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
