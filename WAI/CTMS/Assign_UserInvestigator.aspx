<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Assign_UserInvestigator.aspx.cs" Inherits="CTMS.Assign_UserInvestigator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
                                        <h4 class="box-title" align="left">
                                            Assign Investigator To User
                                        </h4>
                                    </div>
                                    <div class="box-body">
                                        <div align="left" style="margin-left: 5px">
                                            <div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Select Project:</label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:DropDownList ID="Drp_Project_Name" class="form-control drpControl required width200px"
                                                                runat="server" AutoPostBack="True" OnSelectedIndexChanged="Drp_Project_Name_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Select User:</label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:DropDownList ID="Drp_User" class="form-control drpControl required width200px"
                                                                runat="server" AutoPostBack="True" Style="margin-bottom: 1px" OnSelectedIndexChanged="Drp_User_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="rows">
                                                    <div style="width: 100%; height: 300px; overflow: auto;">
                                                        <div>
                                                            <asp:GridView ID="grdINVID" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                                                                        ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="INVID" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="INVID" runat="server" Text='<%# Bind("INVID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ADDRESS" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="INVPROJECTADDRESS" runat="server" Text='<%# Bind("INVPROJECTADDRESS") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkINVID" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            &nbsp;
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:Button ID="btnSubmitINVID" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                                OnClick="btnSubmitINVID_Click" />
                                                            <asp:Button ID="btnCancelINVID" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="box box-primary" style="min-height: 300px;">
                                    <div class="box-header with-border" style="float: left;">
                                        <h4 class="box-title" align="left">
                                            Records
                                        </h4>

                                         <div id="Div2" class="dropdown" runat="server" style="display: inline-flex"><h3 class="box-title">
               <asp:LinkButton runat="server" ID="lbAssignINVUserExport" OnClick="lbAssignINVUserExport_Click" ToolTip="Export to Excel"
                 Text="" CssClass="dropdown-item dropdown-toggle glyphicon glyphicon-download-alt" Style="color: darkblue;"></asp:LinkButton>
		      </h3>
            </div>




                                    </div>
                                    <div class="box-body">
                                        <div align="left" style="margin-left: 5px">
                                            <div>
                                                <div class="rows">
                                                    <div style="width: 100%; height: 264px; overflow: auto;">
                                                        <div>
                                                            <asp:GridView ID="grdAddedINVID" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="User_ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                                                                        ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="User_ID" runat="server" Text='<%# Bind("User_ID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Project_ID" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Project_ID" runat="server" Text='<%# Bind("Project_ID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Project Name" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="PROJNAME" runat="server" Text='<%# Bind("PROJNAME") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="User Name" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="User_Dis_Name" runat="server" Text='<%# Bind("User_Dis_Name") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="INVID" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="INVID" runat="server" Text='<%# Bind("INVID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="INVNAME" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="INVNAME" runat="server" Text='<%# Bind("INVNAME") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                        <HeaderTemplate>
                                                                            <asp:LinkButton ID="lbtndeleteSection" runat="server" ToolTip="Delete" OnClick="lbtndeleteSection_Click"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkAddedINVID" />
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
            <asp:PostBackTrigger ControlID="lbAssignINVUserExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
