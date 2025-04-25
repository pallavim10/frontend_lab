<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Edit_User_Details.aspx.cs" Inherits="PPT.Edit_User_Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script type="text/javascript">
        function pageLoad() {
            $('.select').select2();
            $('.txtDate').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    format: 'DD-MMM-YYYY',
                    yearRange: [1910, 2050]
                });
            });
        }
    </script>
    <style type="text/css">
        .label {
            margin-left: 0;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Update User Details</h3>
        </div>
        <!-- /.box-header -->
        <!-- text input -->
        <div class="box-body">
            <asp:UpdatePanel runat="server" ID="UpdatePanel">
                <ContentTemplate>
                    <div class="box box-primary">
                        <div class="box-header">
                            <div class="lblError">
                                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            Select User: &nbsp;
                                            <asp:Label ID="Label8" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="Drp_User" class="form-control drpControl required width200px"
                                                runat="server" AutoPostBack="True" OnSelectedIndexChanged="Drp_User_SelectedIndexChanged"
                                                Style="margin-bottom: 1px">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="label col-md-2">
                                            Edit User Name: &nbsp;
                                            <asp:Label ID="Label9" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txt_User_Name" runat="server" class="form-control required width200px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            Edit User Display Name :&nbsp;
                                            <asp:Label ID="Label10" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txt_User_Dis_Name" runat="server" class="form-control required width200px"></asp:TextBox>
                                        </div>
                                        <div class="label col-md-2">
                                            Edit Email ID :&nbsp;
                                            <asp:Label ID="Label11" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txt_EmailID" runat="server" class="form-control required width200px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            Edit User Group: &nbsp;
                                            <asp:Label ID="Label12" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <%-- <asp:DropDownList ID="Drp_User_Group1" class="form-control drpControl width200px"
                                                runat="server" AutoPostBack="True">
                                            </asp:DropDownList>--%>
                                            <asp:ListBox ID="lstUser_Group" AutoPostBack="true" runat="server" CssClass="width300px select"
                                                SelectionMode="Multiple" Width="668px" OnSelectedIndexChanged="lstUser_Group_SelectedIndexChanged"></asp:ListBox>
                                        </div>
                                        <div class="label col-md-2" id="proj" runat="server" visible="false">
                                            Select Project: &nbsp;
                                            <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </div>
                                        <div class="col-md-4" id="projname" runat="server" visible="false">
                                            <asp:DropDownList ID="Drp_Project_Name" class="form-control drpControl required width200px"
                                                runat="server" AutoPostBack="True" OnSelectedIndexChanged="Drp_Project_Name_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row" id="divProject" runat="server">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            Select Project: &nbsp;
                                            <asp:Label ID="Label7" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:ListBox ID="lstProjects" AutoPostBack="true" runat="server" CssClass="width300px select"
                                                SelectionMode="Multiple" Width="668px"></asp:ListBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2" id="div3" runat="server">
                                            TimeZone :&nbsp;
                                            <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </div>
                                        <div class="col-md-4" id="div4" runat="server">
                                            <asp:DropDownList ID="ddlTimeZone" runat="server" class="form-control drpControl width300px required">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="label col-md-1">
                                            Unblind :&nbsp;
                                        </div>
                                        <div class="col-md-4">
                                            <asp:DropDownList runat="server" ID="ddlUnblind" CssClass="form-control txt_center width200px required">
                                                <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                <asp:ListItem Value="Blinded" Text="Blinded"></asp:ListItem>
                                                <asp:ListItem Value="Unblinded" Text="Unblinded"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            Medical Authority :&nbsp;
                                        </div>
                                        <div class="col-md-2">
                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkForm" />
                                            &nbsp;&nbsp;
                                            <label>
                                                Form
                                            </label>
                                        </div>
                                        <div class="label col-md-2">
                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkfield" />
                                            &nbsp;&nbsp;
                                            <label>
                                                Field
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            SignOff Authority :&nbsp;
                                        </div>
                                        <div class="col-md-2">
                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="Check_eSource" />
                                            &nbsp;&nbsp;
                                <label>
                                    eSource
                                </label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="Check_Safety" />
                                            &nbsp;&nbsp;
                                <label>
                                    Pharmacovigilance
                                </label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="Check_eCRF" />
                                            &nbsp;&nbsp;
                                <label>
                                    eCRF
                                </label>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            eSource ReadOnly :&nbsp;
                                        </div>
                                        <div class="col-md-2">
                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="Check_eSourceReadOnly" />
                                            &nbsp;&nbsp;                                
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 10px;">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Button ID="Btn_Update" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                OnClick="Btn_Update_Click" Text="Update" />
                                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Cancel"
                                                OnClick="btnCancel_Click" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>




</asp:Content>
