﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage_Feilds.aspx.cs" Inherits="SpecimenTracking.Manage_Feilds" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <link href="Style/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script src="Scripts/btnSave_Required.js"></script>
    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }
    </style>
    <script type="text/javascript">
        function pageLoad() {
            //only for numeric value
            $('.numeric').keypress(function (event) {

                if (event.keyCode == 8 || event.keyCode == 9 || event.charCode == 48 || event.charCode == 49 || event.charCode == 50 || event.charCode == 51
                    || event.charCode == 52 || event.charCode == 52 || event.charCode == 53 || event.charCode == 54 || event.charCode == 55 || event.charCode == 56 || event.charCode == 57) {
                    // let it happen, don't do anything
                    return true;
                }
                else {
                    event.preventDefault();
                }
            });

            $('.select').select2();
        }

        function DATA_Changed(element) {

            $(element).closest('td').find("input[id*='btnDATA_Changed']").click();

        }

        function showAuditTrail(element) {

            var ID = $(element).closest('tr').find('td').eq(0).text().trim();
            var TABLENAME = 'FIELD_MASTER';

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/SETUP_showAuditTrail",
                data: '{TABLENAME: "' + TABLENAME + '",ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $('#DivAuditTrail').html(data.d)
                    }
                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }
                }
            });

            $("#popup_AuditTrail").dialog({
                title: "Audit Trail",
                width: 900,
                height: 450,
                modal: true,
                buttons: {
                    "Close": function () { $(this).dialog("close"); }
                }
            });

            return false;
        }

        function AddOptions(element) {
            var ID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var test = "MANAGE_ADD_OPTION.aspx?ID=" + ID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=340,width=1000";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;
        }
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Manage Fields</h1>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home</a></li>
                            <li class="breadcrumb-item active">Manage Fields</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Manage Default Fields</h3>
                                <div class="pull-right">
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="rows">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div style="width: 100%; overflow: auto;">
                                                <div>
                                                    <asp:GridView ID="grdMngOptFields" runat="server" AllowSorting="True" AutoGenerateColumns="false"
                                                        CssClass="table table-bordered table-striped" OnRowDataBound="grdMngOptFields_RowDataBound" OnRowCommand="grdMngOptFields_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none"
                                                                HeaderText="ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                    <asp:HiddenField ID="HdnISACTIVE" runat="server" Value='<%# Eval("ISACTIVE") %>' />
                                                                    <asp:HiddenField ID="HdnControlType" runat="server" Value='<%# Eval("CONTROLTYPE") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Field Name" HeaderStyle-CssClass="text-center  align-middle" ItemStyle-CssClass="text-center align-middle">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtFieldName" runat="server" onChange="DATA_Changed(this);" Text='<%# Bind("FIELDNAME") %>' CssClass="form-control w-75"></asp:TextBox>
                                                                    <asp:Button runat="server" ID="btnDATA_Changed" CssClass="d-none" OnClick="DATA_Changed"></asp:Button>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Enable/Disable" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center  align-middle" ItemStyle-CssClass="text-center align-middle">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtEnable" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                        CommandName="DISABLE" ToolTip="Diable"><i class="fa fa-toggle-on" style='color: black;font-size: 25px' ></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lbtDiable" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                        CommandName="ENABLE" ToolTip="Enable"><i class="fa fa-toggle-off" style='color: black;font-size: 25px' ></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Variable Name" HeaderStyle-CssClass="text-center  align-middle" ItemStyle-CssClass="text-center align-middle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVariable" runat="server" Text='<%# Bind("VARIABLENAME") %>' CssClass="form-control w-75"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center  align-middle" ItemStyle-CssClass="text-center align-middle">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" class="btn-info btn-sm" OnClientClick="return showAuditTrail(this);" ToolTip="Audit Trail"><i class="fa fa-history"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Add Option" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center  align-middle" ItemStyle-CssClass="text-center align-middle">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtAddOptionr" runat="server" class="btn-info btn-sm" CommandArgument='<%# Bind("ID") %>' OnClientClick="return AddOptions(this)"
                                                                        ToolTip="Add Option"><i class="fa fa-cog" ></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="content">
            <div class="container-fluid">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="card card-info">
                                            <div class="card-header">
                                                <h3 class="card-title">Create Fields</h3>
                                                <div class="pull-right">
                                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                                </div>
                                            </div>
                                            <div class="card-body">
                                                <div class="rows">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label>Enter Field Name: &nbsp;</label>
                                                                    <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                                    <asp:TextBox ID="txtFieldsName" runat="server" CssClass="form-control required w-75"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label>Enter Variable Name : &nbsp;</label>
                                                                    <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                                    <asp:TextBox ID="txtVariableName" runat="server" CssClass="form-control required w-75"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label>Enter Sequence Number : &nbsp;</label>
                                                                    <asp:Label ID="Label4" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                                    <asp:TextBox ID="txtSeqNo" runat="server" CssClass="form-control required numeric w-75"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label>Select Control Type : &nbsp;</label>
                                                                    <asp:Label ID="Label5" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                                    <asp:DropDownList ID="drpControlType" runat="server" AutoPostBack="true" class="form-control drpControl w-75 required select" SelectionMode="Single">
                                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                                        <asp:ListItem Value="Textbox" Text="Textbox"></asp:ListItem>
                                                                        <asp:ListItem Value="Multiline Textbox" Text="Multiline Textbox"></asp:ListItem>
                                                                        <asp:ListItem Value="Date" Text="Date"></asp:ListItem>
                                                                        <asp:ListItem Value="Time" Text="Time"></asp:ListItem>
                                                                        <asp:ListItem Value="Dropdown" Text="Dropdown"></asp:ListItem>
                                                                        <asp:ListItem Value="Radio Button" Text="Radio Button"></asp:ListItem>
                                                                        <asp:ListItem Value="Checkbox" Text="Checkbox"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <center>
                                                                    <asp:LinkButton runat="server" ID="lbtnSubmit" Text="Submit" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnSubmit_Click"></asp:LinkButton>
                                                                    &nbsp;&nbsp;&nbsp;
                                                                    <asp:LinkButton runat="server" ID="lbnUpdate" Text="Update" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" Visible="false" OnClick="lbnUpdate_Click"></asp:LinkButton>
                                                                    &nbsp;&nbsp;&nbsp;
                                                                    <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm" OnClick="lbtnCancel_Click"></asp:LinkButton>
                                                                </center>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="card card-info">
                                            <div class="card-header">
                                                <h3 class="card-title">Records</h3>
                                                <div class="pull-right">
                                                    <asp:LinkButton ID="lbtnExport" runat="server" Font-Size="14px" Style="margin-top: 3px;" CssClass="btn btn-default" OnClick="lbtnExport_Click" ForeColor="Black">Export Fields &nbsp;<span class="fas fa-download btn-sm"></span></asp:LinkButton>
                                                    &nbsp;&nbsp;
                                            <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                                </div>
                                            </div>
                                            <div class="card-body">
                                                <div class="rows">
                                                    <div class="col-md-12">
                                                        <div style="width: 100%; overflow: auto;">
                                                            <div>
                                                                <asp:GridView ID="GrdMngFields" runat="server" AllowSorting="True" AutoGenerateColumns="false"
                                                                    CssClass="table table-bordered table-striped" OnRowCommand="GrdMngFields_RowCommand" OnRowDataBound="GrdMngFields_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none" HeaderText="ID">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblid" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                                <asp:HiddenField ID="HdnControl" runat="server" Value='<%# Eval("CONTROLTYPE") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtEdit" runat="server" class="btn-info btn-sm" CommandArgument='<%# Bind("ID") %>'
                                                                                    CommandName="EDITED" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Seq No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSquenceNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Variable Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblVariableName" runat="server" Text='<%# Bind("VARIABLENAME") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Field Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblfieldName" runat="server" Text='<%# Bind("FIELDNAME") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Control Type">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblControltype" runat="server" Text='<%# Bind("CONTROLTYPE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnAudttrail" runat="server" class="btn-info btn-sm" OnClientClick="return showAuditTrail(this);" ToolTip="Audit Trail"><i class="fa fa-history"></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Add Option" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtAddOption" runat="server" class="btn-info btn-sm" CommandArgument='<%# Bind("ID") %>' OnClientClick="return AddOptions(this)"
                                                                                    ToolTip="Add Option"><i class="fa fa-cog" "></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtdelete" runat="server" class="btn-danger btn-sm" CommandArgument='<%# Bind("ID") %>'
                                                                                    CommandName="DELETED" OnClientClick="return confirm(event);" ToolTip="Delete"><i class="fa fa-trash"></i></asp:LinkButton>
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
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lbtnSubmit" />
                        <asp:AsyncPostBackTrigger ControlID="lbnUpdate" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </section>
    </div>
    
    <script type="text/javascript">

        function confirm(event) {
            event.preventDefault();

            swal({
                title: "Warning!",
                text: "Are you sure you want to delete this Record?",
                icon: "warning",
                buttons: true,
                dangerMode: true
            }).then(function (isConfirm) {
                if (isConfirm) {
                    var linkButton = event.target;
                    if (linkButton.tagName.toLowerCase() === 'i') {
                        linkButton = linkButton.parentElement;
                    }
                    linkButton.onclick = null;
                    linkButton.click();
                } else {
                    Response.redirect(this);
                }
            });
            return false;
        }

    </script>

</asp:Content>