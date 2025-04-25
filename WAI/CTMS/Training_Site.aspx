<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Training_Site.aspx.cs" Inherits="CTMS.Training_Site" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);

            if (div.style.display == "none") {
                div.style.display = "inline";
                document.getElementById('img' + divname).className = 'icon-minus-sign-alt';

            } else {
                div.style.display = "none";
                document.getElementById('img' + divname).className = 'icon-plus-sign-alt';
            }
        }

        function DownloadDoc(element) {

            var ID = $(element).closest('tr').find('td:eq(1)').find('span').html();

            var test = "Train_StudyMaterial.aspx?ID=" + ID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=520,width=900";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Training
            </h3>
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
                                <div class="label col-md-2">
                                    Select Training Plan : &nbsp;
                                    <asp:Label ID="Label4" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList Style="width: 200px;" ID="ddlPlan" runat="server" AutoPostBack="true"
                                        class="form-control drpControl required" OnSelectedIndexChanged="ddlPlan_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_ddlPlan" runat="server" ControlToValidate="ddlPlan"
                                        ValidationGroup="Grp" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="label col-md-2" style="width: auto;">
                                    Select Trainee : &nbsp;
                                    <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList Style="width: 200px;" ID="ddlTrainee" runat="server" AutoPostBack="true"
                                        class="form-control drpControl required" OnSelectedIndexChanged="ddlTrainee_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_ddlTrainee" runat="server" ControlToValidate="ddlTrainee"
                                        ValidationGroup="Grp" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <br />
                        <asp:GridView ID="gvSections" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            CssClass="table table-bordered table-striped Datatable1" OnRowDataBound="gvSections_RowDataBound">
                            <Columns>
                                <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center">
                                    <ItemTemplate>
                                        <a href="JavaScript:divexpandcollapse('_mydiv<%# Eval("Section_ID") %>');" style="color: #333333">
                                            <i id="img_mydiv<%# Eval("Section_ID") %>" class="icon-plus-sign-alt"></i></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Section ID" ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_ID" Width="100%" ToolTip='<%# Bind("Section_ID") %>' CssClass="label"
                                            runat="server" Text='<%# Bind("Section_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Section Name" ItemStyle-Width="100%">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Section" Width="100%" ToolTip='<%# Bind("Section") %>' CssClass="label"
                                            runat="server" Text='<%# Bind("Section") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDownloadDoc" Visible="false" runat="server" ToolTip="Study Material"
                                            OnClientClick="return DownloadDoc(this);">
                        <i class="fa fa-download" style="color:#333333;" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <tr>
                                            <td colspan="100%" style="padding: 2px;">
                                                <div style="float: right; font-size: 13px;">
                                                </div>
                                                <div>
                                                    <div class="rows">
                                                        <div class="col-md-12">
                                                            <div id="_mydiv<%# Eval("Section_ID") %>" style="display: none; position: relative;
                                                                overflow: auto;">
                                                                <asp:GridView ID="gvSubSection" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped Datatable1"
                                                                    OnRowDataBound="gvSubSection_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                                                            HeaderText="ID">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_Sub_Section_ID" runat="server" Text='<%# Bind("Sub_Section_ID") %>'></asp:Label>
                                                                                <asp:HiddenField ID="hf_Sub_Section_ID" runat="server" Value='<%# Bind("Sub_Section_ID") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Sub-Sections">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_SubSection_Name" Width="60%" ToolTip='<%# Bind("SubSection") %>'
                                                                                    CssClass="label" runat="server" Text='<%# Bind("SubSection") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="Chk_Sel" runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        <div class="txt_center">
                            <asp:Button ID="btnsubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                OnClick="btnsubmit_Click" />
                        </div>
                        <br />
                        <br />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
