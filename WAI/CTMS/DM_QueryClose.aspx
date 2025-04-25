<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DM_QueryClose.aspx.cs" Inherits="CTMS.DM_QueryClose" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 <script>
     $(document).ready(function () {
         $("#grdQUERYDETAILS").addClass("disp-none")
     });
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                RESLOVE QUERY MANUALLY
            </h3>
        </div>
   
    <div class="lblError">
        <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;
            font-size: small;"></asp:Label>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           
                <br />

                <div runat="server" id="Div1" class="form-group" style="display: inline-flex">
                    <div class="form-group" style="display: inline-flex">
                        <label class="label">
                            <asp:Label ID="lblSiteId" runat="server" CssClass="wrapperLable" Text="Select Site Id:"></asp:Label>
                        </label>
                    </div>
                    <div class="Control">
                        <asp:DropDownList ID="drpSite" CssClass="width150px form-control" runat="server"
                            AutoPostBack="true" OnSelectedIndexChanged="drpSite_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpSite"
                            ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div runat="server" id="Div2" class="form-group" style="display: inline-flex">
                    <div class="form-group" style="display: inline-flex">
                        <label class="label">
                            <asp:Label ID="lblPatientId" runat="server" CssClass="wrapperLable" Text="Select Subject Id:"></asp:Label>
                        </label>
                    </div>
                    <div class="Control">
                        <asp:DropDownList ID="drpPatient" runat="server" CssClass="width150px form-control"
                            AutoPostBack="True" OnSelectedIndexChanged="drpPatient_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div runat="server" id="Div3" class="form-group" style="display: inline-flex">
                    <div class="form-group" style="display: inline-flex">
                        <label class="label">
                            <asp:Label ID="lblVisitId" runat="server" CssClass="wrapperLable" Text="Select Visit:"></asp:Label>
                        </label>
                    </div>
                    <div class="Control">
                        <asp:DropDownList ID="drpVisit" runat="server" CssClass="width150px form-control"
                            AutoPostBack="True" OnSelectedIndexChanged="drpVisit_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>

                <div runat="server" id="Div5" class="form-group" style="display: inline-flex">
                    <div class="form-group" style="display: inline-flex">
                        <label class="label">
                            <asp:Label ID="lblModule" runat="server" CssClass="wrapperLable" Text="Select Module:"></asp:Label>
                        </label>
                    </div>
                    <div class="Control">
                        <asp:DropDownList ID="drpModule" runat="server" CssClass="width150px form-control"
                            AutoPostBack="True" OnSelectedIndexChanged="drpModule_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div runat="server" id="Div6" class="form-group" style="display: inline-flex">
                    <div class="form-group" style="display: inline-flex">
                        <label class="label">
                            <asp:Label ID="lblField" runat="server" CssClass="wrapperLable" Text="Select Field:"></asp:Label>
                        </label>
                    </div>
                    <div class="Control">
                        <asp:DropDownList ID="drpField" runat="server" CssClass="width150px form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="formControl disp-none">
                    <div runat="server" id="Div7" class="form-group" style="display: inline-flex">
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                <asp:Label ID="lblQueryStatus" runat="server" CssClass="wrapperLable" Text="Query Status:"></asp:Label>
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpQueryStatus" runat="server" CssClass="width150px">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary btn-sm cls-btnSave"
                    OnClick="btnSearch_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm"
                    OnClick="btnCancel_Click" />
           
        </ContentTemplate>
    </asp:UpdatePanel>
     </div>
    <div class="box-body">
        <div class="form-group">
            <label class="label">
                SELECT ALL:
            </label>
            <asp:CheckBox ID="Chk_Select_All" runat="server" AutoPostBack="True" OnCheckedChanged="Chk_Select_All_CheckedChanged"
                Style="font-size: x-small" />
        </div>
    </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
    <asp:GridView ID="grdQueryDetailReports" runat="server" AutoGenerateColumns="False"
        CellPadding="3" EmptyDataText="No records found"  Width="1100px"
        CellSpacing="2" CssClass="table table-bordered table-striped">
        <Columns>
            <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                <ItemTemplate>
                    <asp:TextBox ID="Txt_ID" runat="server" font-family="Arial" Font-Size="X-Small" Text='<%# Bind("ID") %>'
                        Width="20px" />
                </ItemTemplate>
                <HeaderStyle CssClass="disp-none"></HeaderStyle>
                <ItemStyle CssClass="disp-none"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="QUERY TEXT">
                <ItemTemplate>
                    <asp:TextBox ID="Txt_QUERYTEXT" runat="server" Text='<%# Bind("QUERYTEXT") %>' Width="650px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MODULE NAME">
                <ItemTemplate>
                    <asp:TextBox ID="Txt_MODULENAME" runat="server" Text='<%# Bind("MODULENAME") %>'
                        Width="100px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="FIELD NAME">
                <ItemTemplate>
                    <asp:TextBox ID="Txt_FIELDNAME" runat="server" Text='<%# Bind("FIELDNAME") %>' Width="100px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Resolve Y/N">
                <ItemTemplate>
                    <asp:CheckBox ID="Chk_CloseYN" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Update">
                <HeaderTemplate>
                    <asp:Button ID="Btn_Update" runat="server" OnClick="Btn_Update_Click" Style="font-size: x-small"
                        Text="Update" />
                </HeaderTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
         </ContentTemplate>
    </asp:UpdatePanel>                               
</asp:Content>

