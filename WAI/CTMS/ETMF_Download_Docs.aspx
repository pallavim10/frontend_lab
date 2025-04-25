<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ETMF_Download_Docs.aspx.cs" Inherits="CTMS.ETMF_Download_Docs" %>

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

            var ID = $(element).closest('tr').find('td:eq(0)').find('input').val();

            var test = "CTMS_DownloadDoc.aspx?ID=" + ID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=520,width=900";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ManipulateAll(ID) {
            var img = document.getElementById('img' + ID);

            if (img.className == 'icon-plus-sign-alt') {
                img.className = 'icon-minus-sign-alt'
                $("div[id*='" + ID + "']").css("display", "inline");
                $("i[id*='" + ID + "']").removeClass('icon-plus-sign-alt');
                $("i[id*='" + ID + "']").addClass('icon-minus-sign-alt');
            } else {
                img.className = 'icon-plus-sign-alt'
                $("div[id*='" + ID + "']").css("display", "none");
                $("i[id*='" + ID + "']").removeClass('icon-minus-sign-alt');
                $("i[id*='" + ID + "']").addClass('icon-plus-sign-alt');
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Download Documents
            </h3>
        </div>
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:GridView ID="gvFolder" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CssClass="table table-bordered table-striped" OnRowDataBound="gvFolder_RowDataBound">
                <Columns>
                    <asp:TemplateField ItemStyle-CssClass="txtCenter" HeaderStyle-CssClass="txt_center"
                        ControlStyle-CssClass="txt_center">
                        <HeaderTemplate>
                            <a href="JavaScript:ManipulateAll('_Folder');" id="_Folder" style="color: #333333"><i
                                id="img_Folder" class="icon-plus-sign-alt"></i></a>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div runat="server" id="anchor">
                                <a href="JavaScript:divexpandcollapse('_Folder<%# Eval("ID") %>');" style="color: #333333">
                                    <i id="img_Folder<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="ID">
                        <ItemTemplate>
                            <asp:Label ID="lbl_TaskId" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Folder" ItemStyle-Width="95%">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Folder" Width="100%" ToolTip='<%# Bind("Folder") %>' CssClass="label"
                                runat="server" Text='<%# Bind("Folder") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Documents" ItemStyle-Width="5%" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="lblCount" CssClass="label" runat="server"></asp:Label>
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
                                                <div id="_Folder<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                                                    <asp:GridView ID="gvSubFolder" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                        CssClass="table table-bordered table-striped" OnRowDataBound="gvSubFolder_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                                                HeaderStyle-CssClass="txt_center">
                                                                <HeaderTemplate>
                                                                    <a href="JavaScript:ManipulateAll('_SubFolder');" id="_Folder" style="color: #333333">
                                                                        <i id="img_SubFolder" class="icon-plus-sign-alt"></i></a>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <div runat="server" id="anchor">
                                                                        <a href="JavaScript:divexpandcollapse('_SubFolder<%# Eval("SubFolder_ID") %>');"
                                                                            style="color: #333333"><i id="img_SubFolder<%# Eval("SubFolder_ID") %>" class="icon-plus-sign-alt">
                                                                            </i></a>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false" HeaderText="ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_FolderID" runat="server" Text='<%# Bind("Folder_ID") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_SubFolderID" runat="server" Text='<%# Bind("SubFolder_ID") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sub-Folder" ItemStyle-Width="95%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_SubFolder" Width="100%" ToolTip='<%# Bind("Sub_Folder_Name") %>'
                                                                        CssClass="label" runat="server" Text='<%# Bind("Sub_Folder_Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Documents" ItemStyle-Width="5%" ItemStyle-CssClass="txt_center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCount" CssClass="label" runat="server"></asp:Label>
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
                                                                                        <div id="_SubFolder<%# Eval("SubFolder_ID") %>" style="display: none; position: relative;
                                                                                            overflow: auto;">
                                                                                            <asp:GridView ID="gvDocs" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                                                CssClass="table table-bordered table-striped Datatable" OnRowCommand="gvDocs_RowCommand">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                                                                                        HeaderText="ID">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:HiddenField ID="hf_ID" runat="server" Value='<%# Bind("ID") %>' />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                                                                                        HeaderText="ID">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:HiddenField ID="hf_TaskID" runat="server" Value='<%# Bind("Task_ID") %>' />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                                                                                        HeaderText="ID">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:HiddenField ID="hf_SubTaskID" runat="server" Value='<%# Bind("Sub_Task_ID") %>' />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Site ID" ItemStyle-Width="5%" ItemStyle-CssClass="txt_center">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lbl_Site" ToolTip='<%# Bind("INVID") %>' CssClass="label" runat="server"
                                                                                                                Text='<%# Bind("INVID") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="File Name">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lbl_FileName" Width="55%" ToolTip='<%# Bind("FileName") %>' CssClass="label"
                                                                                                                runat="server" Text='<%# Bind("FileName") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Version" ItemStyle-Width="2%" ItemStyle-CssClass="txt_center">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lbl_Version" ToolTip='<%# Bind("Version_ID") %>' CssClass="label"
                                                                                                                runat="server" Text='<%# Bind("Version_ID") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Uploaded By" ItemStyle-CssClass="txt_center">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lbl_User_Name" Width="60%" ToolTip='<%# Bind("User_Name") %>' CssClass="label"
                                                                                                                runat="server" Text='<%# Bind("User_Name") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Uploaded Date-Time" ItemStyle-CssClass="txt_center">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lbl_Datetime" Width="60%" ToolTip='<%# Bind("Datetime") %>' CssClass="label"
                                                                                                                runat="server" Text='<%# Bind("Datetime") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lbtnDownloadDoc" runat="server" ToolTip="Download" CommandArgument='<%# Bind("ID") %>' CssClass="btn"><i class="fa fa-download" style="color:#333333;" aria-hidden="true"></i>
                                                                                                            </asp:LinkButton>
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="gvFolder" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
