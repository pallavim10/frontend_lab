<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="eTMF_Group_Matrix.aspx.cs" Inherits="CTMS.eTMF_Group_Matrix" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function OpenDoc(element) {

            var SysFileName = $(element).closest('tr').find('td:eq(1)').text().trim();
            var DOCID = $(element).closest('tr').find('td:eq(0)').text().trim();

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/INSERT_DOC_OPEN_LOGS",
                data: '{DOCID: "' + DOCID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                }
            });

            var test = "eTMF_Docs/" + SysFileName + "#toolbar=0";

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=700,width=1200";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        

    </script>
    <style>
        .layer1
        {
            color: #0000ff;
        }
        .layer2
        {
            color: #800000;
        }
        .layer3
        {
            color: #008000;
        }
        .layer4
        {
            color: Black;
        }
        .layerFiles
        {
            color: #800000;
            font-style: italic;
        }
    </style>
    <style>
        .label
        {
            max-width: 100%;
            margin-bottom: -2px;
            font-weight: bold;
            font-size: 13px;
            margin-left: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header" style="display: inline-flex; width: 100%;">
            <h3 class="box-title" style="width: 100%">
                <asp:Label runat="server" ID="lblHeader" Text="View Documents" />
            </h3>
            <div class="pull-right" style="display: inline-flex;">
                <asp:ImageButton ID="btnRefresh" runat="server" Style="height: 27px;" ImageUrl="img/Sync.png" OnClick="btnRefresh_Click"
                     ToolTip="Refresh"></asp:ImageButton>&nbsp;&nbsp;&nbsp;
            </div>
        </div>
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
        </div>
        <div class="row">
            <div class="col-md-12">
               <div  class="col-md-6">
                    <div class="col-md-4">
                        <asp:Label ID="lblgroup" runat="server" Font-Bold="true">
                             Select Group :
                        </asp:Label>
                    </div>
                    <div class="col-md-8">
                    <asp:DropDownList ID="ddlGroup" Width="250px" runat="server" class="form-control drpControl"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
               </div>
               <div class="col-md-6">
                   <div class="col-md-4">
                       <asp:Label ID="lblZones" runat="server" Font-Bold="true">
                           Select Group Zones :
                       </asp:Label>
                  </div>
                   <div class="col-md-8">
                       <asp:DropDownList ID="ddlZone" Width="250px" runat="server" class="form-control drpControl"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged">
                        </asp:DropDownList>
                   </div>
               </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-6">
                    <div class="col-md-4">
                        <asp:Label ID="lblSection" runat="server" Font-Bold="true">
                           Select Group Sections :
                        </asp:Label>
                    </div>
                    <div class="col-md-8">
                        <asp:DropDownList ID="ddlSections" Width="250px" runat="server" class="form-control drpControl"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlSections_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="col-md-4">
                        <asp:Label ID="lblDocumentCount" runat="server" Font-Bold="true">
                            Total Documents:
                        </asp:Label>
                    </div>
                    <div class=" col-md-8">
                        <asp:Label ID="lblCount" CssClass="form-control width50px label txt_center" ForeColor="Blue"
                            Text="0" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <br />
    </div>
    <div id ="DATAVIEW" runat="server">
    <asp:GridView ID="gvArtifact" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped layer3" OnRowDataBound="gvArtifact_RowDataBound">
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                HeaderStyle-CssClass="txt_center">
                <HeaderTemplate>
                    <a href="JavaScript:ManipulateAll('_Docs');" id="_Folder" style="color: #333333"><i
                        id="img_Docs" class="icon-plus-sign-alt"></i></a>
                </HeaderTemplate>
                <ItemTemplate>
                    <div runat="server" id="anchor">
                        <a href="JavaScript:divexpandcollapse('_Docs<%# Eval("ID") %>');" style="color: #333333">
                            <i id="img_Docs<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                ItemStyle-CssClass="disp-none" HeaderText="MainRefNo">
                <ItemTemplate>
                    <asp:Label ID="MainRefNo" runat="server" Text='<%# Bind("MainRefNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                ItemStyle-CssClass="disp-none" HeaderText="DocTypeId">
                <ItemTemplate>
                    <asp:Label ID="DocTypeId" runat="server" Text='<%# Bind("DocTypeId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ref." ItemStyle-Width="8%" ItemStyle-CssClass="txt_center">
                <ItemTemplate>
                    <asp:Label ID="lbl_RefNo" Width="100%" ToolTip='<%# Bind("RefNo") %>' CssClass="label"
                        runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Artifacts" ItemStyle-Width="82%">
                <ItemTemplate>
                    <asp:Label ID="lbl_Artifact" Width="100%" ToolTip='<%# Bind("Artifact_Name") %>'
                        CssClass="label" runat="server" Text='<%# Bind("Artifact_Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total Documents" ItemStyle-Width="10%" ItemStyle-CssClass="txt_center">
                <ItemTemplate>
                    <asp:Label ID="lblCount" Text="0" CssClass="label" runat="server"></asp:Label>
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
                                        <div id="_Docs<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                                            <asp:GridView ID="gvDocs" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-bordered table-striped layer1">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                                        HeaderStyle-CssClass="txt_center">
                                                        <HeaderTemplate>
                                                            <a href="JavaScript:ManipulateAll('_Files');" id="_Folder" style="color: #333333"><i
                                                                id="img_Files" class="icon-plus-sign-alt"></i></a>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <div runat="server" id="anchor">
                                                                <a href="JavaScript:divexpandcollapse('_Files<%# Eval("ID") %>');" style="color: #333333">
                                                                    <i id="img_Files<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                                                        ItemStyle-CssClass="disp-none" HeaderText="ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ref." ItemStyle-Width="15%" ItemStyle-CssClass="txt_center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_RefNo" Width="100%" ToolTip='<%# Bind("RefNo") %>' CssClass="label"
                                                                runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unique Ref." ItemStyle-Width="15%" ItemStyle-CssClass="txt_center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_UniqueRefNo" Width="100%" ToolTip='<%# Bind("UniqueRefNo") %>'
                                                                CssClass="label" runat="server" Text='<%# Bind("UniqueRefNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Document" ItemStyle-Width="65%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="DocName" Width="100%" ToolTip='<%# Bind("DocName") %>' CssClass="label"
                                                                runat="server" Text='<%# Bind("DocName") %>'></asp:Label>
                                                            <div id="divcomment" runat="server">
                                                                <asp:Label ID="lblComment" Text='<%# Bind("Comment") %>' Width="100%" CssClass="label"
                                                                    runat="server" ForeColor="Red"></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Documents" ItemStyle-Width="10%" ItemStyle-CssClass="txt_center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCount" Text="0" CssClass="label" runat="server"></asp:Label>
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
</asp:Content>