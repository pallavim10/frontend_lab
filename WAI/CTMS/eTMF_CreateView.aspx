<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="eTMF_CreateView.aspx.cs" Inherits="CTMS.eTMF_CreateView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/eTMF/eTMF_ConfirmMsg.js"></script>
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <style>
        .layer1 {
            color: #0000ff;
        }

        .layer2 {
            color: #800000;
        }

        .layer3 {
            color: #008000;
        }

        .layer4 {
            color: Black;
        }

        .layerFiles {
            color: #800000;
            font-style: italic;
        }
    </style>
    <style type="text/css">
        .select2-container .select2-selection--multiple {
            min-height: 60px;
            width: 300px;
        }
    </style>
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {

            $('.select').select2();

        });

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
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Create View</h3>
        </div>
        <div class="form-group">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <div class="box-body">
        <div class="row">
            <div class="col-md-6">
                <div class="box box-primary" id="div11" runat="server">
                    <div class="box-header">
                        <h3 class="box-title">Define View</h3>
                    </div>
                    <div class="rows">
                        <div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            Enter Sequence No. :</label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:TextBox ID="txtSEQNO" runat="server" CssClass="form-control required numeric width200px" TextMode="Number" min="0"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            Enter View Name :</label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:TextBox ID="txtViewName" runat="server" CssClass="form-control required width200px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            Specific to Country :</label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:DropDownList runat="server" ID="drpCountry" AutoPostBack="true" CssClass="form-control width200px"
                                            OnSelectedIndexChanged="drpCountry_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            Specific to Site :</label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:DropDownList runat="server" ID="drpSites" CssClass="form-control width200px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            Share with :</label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:ListBox ID="lstUsers" runat="server" CssClass="width200px select" SelectionMode="Multiple"></asp:ListBox>
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
                                        <asp:Button ID="btnSubmitView" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                            OnClick="btnSubmitView_Click" />
                                        <asp:Button ID="btnUpdateView" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                            OnClick="btnUpdateView_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="btnCancelView" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                            OnClick="btnCancelView_Click" />
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="box box-primary" id="div12" runat="server">
                    <div class="box-header">
                        <h3 class="box-title">View</h3>
                    </div>
                    <div class="rows">
                        <div style="width: 100%; height: 264px; overflow: auto;">
                            <div>
                                <asp:GridView ID="grdViews" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdViews_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sequence No." ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSEQNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View Name" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%# Bind("ViewName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <label>Creation Details</label><br />
                                                <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Created By]</label><br />
                                                <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <div>
                                                        <asp:Label ID="ENTEREDBYNAME" runat="server" Text='<%# Bind("ENTEREDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="ENTERED_CAL_DAT" runat="server" Text='<%# Bind("ENTERED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="ENTERED_CAL_TZDAT" runat="server" Text='<%# Bind("ENTERED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <label>Last Update Details</label><br />
                                                <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Updated By]</label><br />
                                                <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <div>
                                                        <asp:Label ID="UPDATEDBYNAME" runat="server" Text='<%# Bind("UPDATEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="UPDATED_CAL_DAT" runat="server" Text='<%# Bind("UPDATED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="UPDATED_CAL_TZDAT" runat="server" Text='<%# Bind("UPDATED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                    CommandName="EditView" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                                <asp:LinkButton ID="lbtndelete" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                    CommandName="DeleteView" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this view : ", Eval("ViewName")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Manage Files
            </h3>
        </div>
        <div class="form-group" style="margin-bottom: 10px;">
            <div class="col-md-12">
                <div class="col-md-6">
                    <div class="col-md-3">
                        <label>
                            Select View :</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="drpView" runat="server" CssClass="form-control width300px"
                            OnSelectedIndexChanged="drpView_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6">
                    <asp:Button ID="btnAddFiles" runat="server" Text="Add Files" OnClick="btnAddFiles_Click"
                        CssClass="btn btn-primary btn-sm pull-right" Visible="false" />
                </div>
            </div>
            <br />
            <br />
            <div class="box-body">
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
                            ItemStyle-CssClass="disp-none" HeaderText="ArtifactesID">
                            <ItemTemplate>
                                <asp:Label ID="lblArtifacts" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
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
                        <asp:TemplateField HeaderText="Ref." ItemStyle-Width="10%" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="lbl_RefNo" Width="100%" ToolTip='<%# Bind("RefNo") %>' CssClass="label"
                                    runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Artifacts" ItemStyle-Width="80%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Artifact" Width="100%" ToolTip='<%# Bind("Artifact_Name") %>'
                                    CssClass="label" runat="server" Text='<%# Bind("Artifact_Name") %>'></asp:Label>
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
                                                        <asp:GridView ID="gvDocs" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                            CssClass="table table-bordered table-striped layer1" OnRowDataBound="gvDocs_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkDoc" runat="server" />
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
</asp:Content>
