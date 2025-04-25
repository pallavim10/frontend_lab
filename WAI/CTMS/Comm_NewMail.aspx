<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Comm_NewMail.aspx.cs" Inherits="CTMS.Comm_NewMail" %>

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

            $('.txtDateNoFuture').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    format: 'DD-MMM-YYYY',
                    yearRange: [1910, 2050],
                    maxDate: new Date()
                });
            });
        }
    </script>
    <script src="js/CKEditor/ckeditor.js" type="text/javascript"></script>
    <script type="text/javascript">
        CKEDITOR.config.toolbar = [
   ['Bold', 'Italic', 'Underline', 'StrikeThrough', '-', 'Undo', 'Redo', '-', 'Cut', 'Copy', 'Paste', 'Find', 'Replace', '-', 'Outdent', 'Indent', '-', 'NumberedList', 'BulletedList', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
   ['Image', 'Table', '-', 'Link', 'Flash', 'Smiley', 'TextColor', 'BGColor'],
   '/',
   ['Styles', 'Format', 'Font', 'FontSize']
   ];
    </script>
    <style type="text/css">
        .label
        {
            margin-left: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-info">
        <div class="box-header">
            <h3 class="box-title">
                New Email</h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    <asp:HiddenField runat="server" ID="hfPWD" />
                </div>
            </div>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-1">
                            Project ID
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="Drp_Project" Width="100%" class="form-control required" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="label col-md-1">
                            Type
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="Drp_Type" Width="100%" class="form-control required" runat="server">
                                <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Internal" Value="Internal"></asp:ListItem>
                                <asp:ListItem Text="External" Value="External"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="label col-md-1">
                            Nature
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="Drp_Nature" Width="100%" class="form-control required" runat="server">
                                <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Approval" Value="Approval"></asp:ListItem>
                                <asp:ListItem Text="Decision" Value="Decision"></asp:ListItem>
                                <asp:ListItem Text="Deviation" Value="Deviation"></asp:ListItem>
                                <asp:ListItem Text="Document Review" Value="Document Review"></asp:ListItem>
                                <asp:ListItem Text="General Communication" Value="General Communication"></asp:ListItem>
                                <asp:ListItem Text="Risk" Value="Risk"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-1">
                                    Department
                                </div>
                                <div class="col-md-9">
                                    <asp:ListBox ID="list_Dept" runat="server" CssClass="select required" AutoPostBack="true"
                                        SelectionMode="Multiple" Width="100%" OnSelectedIndexChanged="list_Dept_SelectedIndexChanged">
                                    </asp:ListBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-1">
                                    Reference
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="Drp_Refer" Width="100%" class="form-control required" runat="server">
                                        <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="CRO Personnel" Value="CRO Personnel"></asp:ListItem>
                                        <asp:ListItem Text="Data Integrity" Value="Data Integrity"></asp:ListItem>
                                        <asp:ListItem Text="Data Quality" Value="Data Quality"></asp:ListItem>
                                        <asp:ListItem Text="Data Review" Value="Data Review"></asp:ListItem>
                                        <asp:ListItem Text="Document Management" Value="Document Management"></asp:ListItem>
                                        <asp:ListItem Text="Ethics Committee" Value="Ethics Committee"></asp:ListItem>
                                        <asp:ListItem Text="Medical Management" Value="Medical Management"></asp:ListItem>
                                        <asp:ListItem Text="Process Deviation" Value="Process Deviation"></asp:ListItem>
                                        <asp:ListItem Text="Protocol Deviation" Value="Protocol Deviation"></asp:ListItem>
                                        <asp:ListItem Text="Regulatory" Value="Regulatory"></asp:ListItem>
                                        <asp:ListItem Text="Risk Management" Value="Risk Management"></asp:ListItem>
                                        <asp:ListItem Text="Safety-AE" Value="Safety-AE"></asp:ListItem>
                                        <asp:ListItem Text="Safety-SAE" Value="Safety-SAE"></asp:ListItem>
                                        <asp:ListItem Text="Site Personnel" Value="Site Personnel"></asp:ListItem>
                                        <asp:ListItem Text="SOP" Value="SOP"></asp:ListItem>
                                        <asp:ListItem Text="Sponsor Decision" Value="Sponsor Decision"></asp:ListItem>
                                        <asp:ListItem Text="Training" Value="Training"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="label col-md-1">
                                    Event
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlEvent" Width="100%" class="form-control required" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-1">
                            Note
                        </div>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtNote" CssClass="form-control" Height="50px" TextMode="MultiLine"
                                Width="100%"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-1">
                            From
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtFromID" Text="anish.raut@diagnosearch.com" ReadOnly="true"
                                CssClass="form-control required" Width="100%"></asp:TextBox>
                        </div>
                        <div class="label col-md-1">
                            To
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtToID" CssClass="form-control required" Width="100%"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-1">
                            Cc
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtCcID" CssClass="form-control" Width="100%"></asp:TextBox>
                        </div>
                        <div class="label col-md-1">
                            Bcc
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtBccID" CssClass="form-control" Width="100%"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-1">
                            Subject
                        </div>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtSubject" CssClass="form-control required" Width="100%"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-1">
                            Attachments
                        </div>
                        <div class="col-md-2">
                            <asp:FileUpload ID="fileAttach" runat="server" Font-Size="X-Small" />
                        </div>
                        <div class="label col-md-1">
                            Importance
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="Drp_Importance" Width="100%" class="form-control required"
                                runat="server">
                                <asp:ListItem Text="Low" Value="Low"></asp:ListItem>
                                <asp:ListItem Selected="True" Text="Normal" Value="Normal"></asp:ListItem>
                                <asp:ListItem Text="High" Value="High"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="label col-md-1">
                            <asp:CheckBox runat="server" ID="chkFlag" />&nbsp;&nbsp; Flag
                        </div>
                        <div class="label col-md-2">
                            <asp:CheckBox runat="server" ID="chkEvent" />&nbsp;&nbsp; Event
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-1">
                            Message
                        </div>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtBody" CssClass="ckeditor" Height="50px" TextMode="MultiLine"
                                Width="100%"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div style="margin-left: 25%;">
                        <asp:LinkButton runat="server" ID="lbtnSend" Text="Send" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave"
                            OnClick="lbtnSend_Click"></asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm"
                            OnClick="lbtnCancel_Click"></asp:LinkButton>
                    </div>
                </div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
