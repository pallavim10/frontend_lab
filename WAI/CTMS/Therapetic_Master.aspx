<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Therapetic_Master.aspx.cs" Inherits="CTMS.Therapetic_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>
   <div class="box box-warning">
        <div class="box-header">
            <div>
                <h3 class="box-title">Manage Therapeutic Masters
                </h3>
            </div>

            <div id="Div1" class="dropdown" runat="server" style="display: inline-flex">
                <h3 class="box-title">
                    <asp:LinkButton runat="server" ID="btnTharaMasterExport" OnClick="btnTharaMasterExport_Click" ToolTip=" Export to Excel"
                        Text="" CssClass="dropdown-item dropdown-toggle glyphicon glyphicon-download-alt" Style="color: darkblue;"></asp:LinkButton>

                </h3>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="box box-primary" style="min-height: 300px;">
                                        <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                            <div>
                                                <h3 class="box-title">Add Therapeutic Class
                                                </h3>
                                            </div>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="col-md-4">
                                                                <label>
                                                                    Enter Therapeutic Class :</label>
                                                            </div>
                                                            <div class="col-md-1 requiredSign">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtClass"
                                                                    ValidationGroup="Class" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-md-7">
                                                                <asp:TextBox Style="width: 250px;" ID="txtClass" ValidationGroup="Class" runat="server"
                                                                    CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="col-md-6">
                                                                &nbsp;
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:Button ID="btnSubmitClass" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                                    ValidationGroup="Class" OnClick="btnSubmitClass_Click" />
                                                                <asp:Button ID="btnUpdateClass" Text="Update" runat="server" CssClass="btn btn-primary btn-sm"
                                                                    ValidationGroup="Class" OnClick="btnUpdateClass_Click" />
                                                                <asp:Button ID="btnCancelClass" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                                    OnClick="btnCancelClass_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="box box-primary">
                                        <div class="box-header with-border" style="float: left;">
                                            <h4 class="box-title" align="left">Records
                                            </h4>

                                            <div id="Div2" class="dropdown" runat="server" style="display: inline-flex">
                                                <h3 class="box-title">

                                                    <asp:LinkButton runat="server" ID="btnTharaExport" OnClick="btnTharaExport_Click" ToolTip="Export to Excel"
                                                        Text=" " CssClass="dropdown-item dropdown-toggle glyphicon glyphicon-download-alt" Style="color: darkblue;"></asp:LinkButton>

                                                </h3>
                                            </div>

                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div>
                                                    <div class="rows">
                                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                                            <div>
                                                                <asp:GridView ID="gvClass" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="gvClass_RowCommand"
                                                                    OnRowDataBound="gvClass_RowDataBound">
                                                                    <Columns>

                                                                        <asp:TemplateField HeaderText=" " ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnupdateClass" runat="server" CommandArgument='<%# Bind("PRODUCTID") %>'
                                                                                    CommandName="Edit1" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Therapeutic Class" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblClass" runat="server" Text='<%# Bind("PRODUCTNAM") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtndeleteClass" runat="server" CommandArgument='<%# Bind("PRODUCTID") %>'
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
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="box box-primary" style="min-height: 300px;">
                                        <div class="box-header with-border" style="float: left;">
                                            <h4 class="box-title" align="left">Add Therapeutic Sub-Class
                                            </h4>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Select Therapeutic Class:</label>
                                                        </div>
                                                        <div class="col-md-1 requiredSign">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlClass"
                                                                ValidationGroup="SubClass" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:DropDownList Style="width: 250px;" ID="ddlClass" runat="server" AutoPostBack="true"
                                                                class="form-control drpControl" ValidationGroup="SubClass" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12" style="margin-top: 5px;">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Enter Therapeutic Sub-Class:</label>
                                                        </div>
                                                        <div class="col-md-1 requiredSign">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSubClass"
                                                                ValidationGroup="SubClass" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:TextBox ID="txtSubClass" Style="width: 250px;" ValidationGroup="SubClass" runat="server"
                                                                Text="" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-6">
                                                            &nbsp;
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Button ID="btnSubmitSubClass" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                                ValidationGroup="SubClass" OnClick="btnSubmitSubClass_Click" />
                                                            <asp:Button ID="btnUpdateSubClass" Text="Update" runat="server" CssClass="btn btn-primary btn-sm"
                                                                ValidationGroup="SubClass" OnClick="btnUpdateSubClass_Click" />
                                                            <asp:Button ID="btnCancelSubClass" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                                OnClick="btnCancelSubClass_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="box box-primary">
                                        <div class="box-header with-border" style="float: left;">
                                            <h4 class="box-title" align="left">Records
                                            </h4>
                                            <div id="Div3" class="dropdown" runat="server" style="display: inline-flex">
                                                <h4 class="box-title">

                                                    <asp:LinkButton ID="btnTharaSubExport" runat="server" OnClick="btnTharaSubExport_Click" ToolTip="Export to Excel"
                                                        Text="" CssClass="dropdown-item dropdown-toggle glyphicon glyphicon-download-alt" Style="color: darkblue;"></asp:LinkButton>

                                                </h4>
                                            </div>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div>
                                                    <div class="rows">
                                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                                            <div>
                                                                <asp:GridView ID="gvSubClass" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="gvSubClass_RowCommand"
                                                                    OnRowDataBound="gvSubClass_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText=" " ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnupdateSubClass" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                                    CommandName="Edit1" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>

                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Therapeutic Sub-Class" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblsubcategory" runat="server" Text='<%# Bind("Therapetic_SubClass") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtndeleteSubClass" runat="server" CommandArgument='<%# Bind("ID") %>'
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
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="box box-primary" style="min-height: 300px;">
                                        <div class="box-header with-border" style="float: left;">
                                            <h4 class="box-title" align="left">Add Indications
                                            </h4>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Select Therapeutic Sub-Class:</label>
                                                        </div>
                                                        <div class="col-md-1 requiredSign">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSubClass"
                                                                ValidationGroup="Indic" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:DropDownList Style="width: 250px;" ID="ddlSubClass" runat="server" AutoPostBack="true"
                                                                class="form-control drpControl" ValidationGroup="Indic" OnSelectedIndexChanged="ddlSubClass_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12" style="margin-top: 5px;">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Enter Indications:</label>
                                                        </div>
                                                        <div class="col-md-1 requiredSign">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtIndic"
                                                                ValidationGroup="Indic" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:TextBox ID="txtIndic" Style="width: 250px;" ValidationGroup="Indic" runat="server"
                                                                Text="" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-6">
                                                            &nbsp;
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Button ID="btnSubmitIndic" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                                ValidationGroup="Indic" OnClick="btnSubmitIndic_Click" />
                                                            <asp:Button ID="btnUpdateIndic" Text="Update" runat="server" CssClass="btn btn-primary btn-sm"
                                                                ValidationGroup="Indic" OnClick="btnUpdateIndic_Click" />
                                                            <asp:Button ID="btnCancelIndic" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                                OnClick="btnCancelIndic_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="box box-primary">
                                        <div class="box-header with-border" style="float: left;">
                                            <h4 class="box-title" align="left">Records
                                            </h4>
                                            <div id="Div4" class="dropdown" runat="server" style="display: inline-flex">
                                                <h4 class="box-title">

                                                    <asp:LinkButton runat="server" ID="btnTharaIndicExport" OnClick="btnTharaIndicExport_Click" ToolTip="Export to Excel"
                                                        Text="" CssClass="dropdown-item dropdown-toggle glyphicon glyphicon-download-alt" Style="color: darkblue;"></asp:LinkButton>

                                                </h4>
                                            </div>

                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div>
                                                    <div class="rows">
                                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                                            <div>
                                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="GridView1_RowCommand"
                                                                    OnRowDataBound="GridView1_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText=" " ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnupdateSubSection" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                                    CommandName="Edit1" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>

                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Indications" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblsubcategory" runat="server" Text='<%# Bind("INDICATION") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtndeleteSubSection" runat="server" CommandArgument='<%# Bind("ID") %>'
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
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnTharaExport" />
                <asp:PostBackTrigger ControlID="btnTharaSubExport" />
                <asp:PostBackTrigger ControlID="btnTharaIndicExport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
