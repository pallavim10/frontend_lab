<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmPROJDETAILS.aspx.cs" Inherits="PPT.frmPROJDETAILS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Project Details
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
                <table>
                    <tr>
                        <td class="label">
                            Select Product:
                        </td>
                        <td class="requiredSign">
                            <asp:Label ID="Label2" runat="server" Text="*"></asp:Label>
                        </td>
                        <td class="Control">
                            <asp:DropDownList ID="drpProduct" runat="server" class="form-control required" AutoPostBack="True"
                                OnSelectedIndexChanged="drpProduct_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style10">
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td class="requiredSign">
                        </td>
                        <td class="Control">
                        </td>
                        <td class="style10">
                        </td>
                    </tr>
                    <div runat="server" id="divForm" visible="false">
                        <tr>
                            <td class="label">
                                Project ID:
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label1" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:TextBox ID="txt_Project_ID" runat="server" CssClass="numeric form-control txt_center"
                                    Width="100px" />
                            </td>
                            <td class="style10">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Project Name:
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label3" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:TextBox ID="txtProjName" runat="server" CssClass="numeric form-control txt_center"
                                    Width="100px" />
                            </td>
                            <td class="style10">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Sponser:
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label4" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:TextBox ID="txtSponser" runat="server" CssClass="numeric form-control txt_center"
                                    Width="100px" />
                            </td>
                            <td class="style10">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Project Start Date:
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label5" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:TextBox ID="txtStartDate" runat="server" CssClass=" txtDate form-control" Width="100px" />
                            </td>
                            <td class="style10">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Duration:
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label6" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:TextBox ID="txtDuration" runat="server" CssClass="numeric form-control txt_center"
                                    Width="100px" />
                            </td>
                            <td class="style10">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Therapeutic Area:
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label7" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:TextBox ID="txtTHERAREA" runat="server" CssClass="form-control txt_center" Width="100px" />
                            </td>
                            <td class="style10">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Indc:
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label8" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:TextBox ID="txtINDC" runat="server" CssClass="form-control txt_center" Width="100px" />
                            </td>
                            <td class="style10">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Phase:
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label9" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:TextBox ID="txtPHASE" runat="server" CssClass="form-control txt_center" Width="100px" />
                            </td>
                            <td class="style10">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Re_Screen_WP:
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label10" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:TextBox ID="txt_Re_Screen_WP" runat="server" CssClass="form-control txt_center"
                                    Width="100px" />
                            </td>
                            <td class="style10">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Rand_WP:
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label17" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:TextBox ID="txtRand_WP" runat="server" CssClass="form-control txt_center" Width="100px" />
                            </td>
                            <td class="style10">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                NoReScreen:
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label11" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:TextBox ID="txtNoReScreen" runat="server" CssClass="form-control txt_center"
                                    Width="100px" />
                            </td>
                            <td class="style10">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Re_Screen_WP_End:
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label12" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:TextBox ID="txtRe_Screen_WP_End" runat="server" CssClass="form-control txt_center"
                                    Width="100px" />
                            </td>
                            <td class="style10">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Rand_WP_End:
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label13" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:TextBox ID="txtRand_WP_End" runat="server" CssClass="form-control txt_center"
                                    Width="100px" />
                            </td>
                            <td class="style10">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Is Active:
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label14" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:CheckBox runat="server" ID="chkISACTIVE" />
                            </td>
                            <td class="style10">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Rand_No_Site_Specific:
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label15" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:CheckBox runat="server" ID="chkRand_No_Site_Specific" />
                            </td>
                            <td class="style10">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                MMA_Approval_Req:
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label16" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:CheckBox runat="server" ID="chkMMA_Approval_Req" />
                            </td>
                            <td class="style10">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                ProjectStrataYN:
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label18" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:CheckBox runat="server" ID="chkProjectStrataYN" />
                            </td>
                            <td class="style10">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                GenderStrataYN:
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label19" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:CheckBox runat="server" ID="chkGenderStrataYN" />
                            </td>
                            <td class="style10">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Button ID="bntSave" runat="server" Text="Save" Style="margin-bottom: 10px" CssClass="btn btn-primary btn-sm cls-btnSave margin-top6 margin-left10"
                                    OnClick="bntSave_Click" />
                            </td>
                            <td class="style10">
                                &nbsp;
                            </td>
                            <td class="style18">
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </div>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box">
        <asp:GridView ID="PROJDETAILS" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
            Width="700px" OnRowDataBound="PROJDETAILS_RowDataBound1" OnRowCommand="PROJDETAILS_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="Project ID" ItemStyle-CssClass="width100px">
                    <ItemTemplate>
                        <asp:TextBox ID="Project_ID" runat="server" Text='<%# Bind("Project_ID") %>' CssClass="numeric form-control txt_center"
                            Width="100px" />
                    </ItemTemplate>
                    <ItemStyle CssClass="width100px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Project Name" ItemStyle-CssClass="width100px">
                    <ItemTemplate>
                        <asp:TextBox ID="PROJNAME" runat="server" Text='<%# Bind("PROJNAME") %>' CssClass="form-control" />
                    </ItemTemplate>
                    <ItemStyle CssClass="width100px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sponsor" ItemStyle-CssClass="width100px">
                    <ItemTemplate>
                        <asp:TextBox ID="SPONSOR" runat="server" Text='<%# Bind("SPONSOR") %>' CssClass="form-control" />
                    </ItemTemplate>
                    <ItemStyle CssClass="width100px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Project Start Date" ItemStyle-CssClass="width100px">
                    <ItemTemplate>
                        <asp:TextBox ID="PROJSTDAT" runat="server" Text='<%# Bind("PROJSTDAT") %>' CssClass="txtDate form-control" />
                    </ItemTemplate>
                    <ItemStyle CssClass="width100px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Project Duration" ItemStyle-CssClass="width100px">
                    <ItemTemplate>
                        <asp:TextBox ID="PROJDUR" runat="server" Text='<%# Bind("PROJDUR") %>' CssClass="form-control" />
                    </ItemTemplate>
                    <ItemStyle CssClass="width100px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="width100px">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_SUBJID" runat="server" Text="Edit" Style="color: white;"
                            CssClass="btn btn-primary btn-sm" CommandArgument='<%#Eval("Project_ID") %>'
                            Width="60px" CommandName="EditRow"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle CssClass="width100px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="width100px">
                    <HeaderTemplate>
                        <asp:Button ID="bntAdd" runat="server" OnClick="bntAdd_Click" CssClass="btn btn-primary btn-sm"
                            Text="Add Project" />
                    </HeaderTemplate>
                    <ItemStyle CssClass="width100px"></ItemStyle>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <table>
        </table>
    </div>
</asp:Content>
