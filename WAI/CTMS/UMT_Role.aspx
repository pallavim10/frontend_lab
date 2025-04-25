<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_Role.aspx.cs" Inherits="CTMS.UMT_Role" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/UMT/UMT_ConfirmMsg.js"></script>
    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);

            if (div.style.display == "none") {
                div.style.display = "inline";
                document.getElementById('img' + divname).className = 'ion-minus-round';

            } else {
                div.style.display = "none";
                document.getElementById('img' + divname).className = 'ion-plus-circled';
            }
        }
    </script>
    <script src="Scripts/Select2.js"></script>

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

            $(".Datatable1").dataTable({
                "bSort": false, "ordering": false,
                "bDestroy": true, stateSave: false
            });

            $(".Datatable1").parent().parent().addClass('fixTableHead');
        }

        function showAuditTrail(element) {

            var ID = $(element).closest('tr').find('td').eq(0).text().trim();
            var TABLENAME = 'UMT_Roles';

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/showAuditTrail",
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-primary">
        <div class="box-header">
            <div>
                <h3 class="box-title">User Roles</h3>
            </div>
            <div id="Div3" class="pull-right" runat="server">
                <asp:LinkButton runat="server" ID="lbUserDetailsExport" OnClick="lbUserDetailsExport_Click" CssClass="btn btn-info" Style="color: white; margin-top: 3px;">
                        Export User Roles &nbsp;&nbsp; <span class="glyphicon glyphicon-download 2x"></span>
                </asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp;
            </div>
            <div class="lblError">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <br />
        <div class="form-group">
            <div class="row">
                <div class="col-md-12">
                    <div class="label col-md-2">
                        Select System: &nbsp;
                                <asp:Label ID="Label8" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="drpSystem" runat="server" AutoPostBack="true" class="form-control drpControl required width200px" OnSelectedIndexChanged="drpSystem_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div id="DivROLE" runat="server" visible="false">
                        <div class="label col-md-2">
                            Enter Role Name: &nbsp;
                                    <asp:Label ID="Label9" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtRoleName" CssClass="form-control required width200px"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 7px;">
                <div class="col-md-12">
                    <div id="divBlinded" runat="server" visible="false">
                        <div class="label col-md-2">
                            Blinded/Unblinded :&nbsp;
                            <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList runat="server" ID="ddlUnblind" CssClass="form-control width200px required">
                                <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                <asp:ListItem Value="Blinded" Text="Blinded"></asp:ListItem>
                                <asp:ListItem Value="Unblinded" Text="Unblinded"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div id="DiveSourceReadonly" runat="server" visible="false">
                        <div class="label col-md-2">
                            eSource :&nbsp;
                        </div>
                        <div class="col-md-4" style="display: inline-flex;">
                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" AutoPostBack="true" GroupName="eSource" ID="Check_CRA" OnCheckedChanged="Check_eSourceReadOnly_CheckedChanged" />
                            &nbsp;&nbsp;    
                            <label>SDR</label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" AutoPostBack="true" GroupName="eSource" ID="Check_eSourceReadOnly" OnCheckedChanged="Check_eSourceReadOnly_CheckedChanged" />
                            &nbsp;&nbsp;    
                            <label>ReadOnly</label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" GroupName="eSource" ID="Check_eSourceAdmin" />
                            &nbsp;&nbsp;    
                            <label>Admin</label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 7px;">
                <div class="col-md-12">
                    <div id="DivMedicalAuth" runat="server" visible="false">
                        <div class="label col-md-2">
                            Medical Opinion :&nbsp;
                        </div>
                        <div class="col-md-2" style="width: 147px;">
                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkForm" />
                            &nbsp;&nbsp;
                                <label>Module </label>
                        </div>
                        <div class="col-md-2">
                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkfield" />
                            &nbsp;&nbsp;
                                <label>Field </label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 7px;">
                <div class="col-md-12">
                    <div>
                        <div class="label col-md-2" id="lblSignoff" runat="server" visible="false">
                            SignOff Authority :&nbsp;
                        </div>
                        <div class="col-md-2" id="divesource" runat="server" visible="false">
                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="Check_eSource" />
                            &nbsp;&nbsp;
                                <label>
                                    eSource
                                </label>
                        </div>
                        <div class="col-md-2" id="divPharmacovigilance" runat="server" visible="false">
                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="Check_Safety" />
                            &nbsp;&nbsp;
                                <label>
                                    Pharmacovigilance
                                </label>
                        </div>
                        <div class="col-md-2" id="diveCRF" runat="server" visible="false">
                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="Check_eCRF" />
                            &nbsp;&nbsp;
                                <label>
                                    eCRF
                                </label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 7px;">
                <div class="col-md-12">
                    <div class="col-md-5">
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btnSUBMITRoles" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                            Text="Submit" OnClick="btnSUBMITRoles_Click" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnUpdateRole" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                            Text="Update" OnClick="btnUpdateRole_Click" Visible="false" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancelROles" runat="server" CssClass="btn btn-primary btn-sm" Text="Cancel" OnClick="btnCancelROles_Click" />&nbsp;&nbsp;&nbsp;

                    </div>
                </div>
            </div>
        </div>
        <div class="rows">
            <div style="width: 100%; overflow: auto; height: auto">
                <div>
                    <asp:GridView ID="grdRoles" runat="server" AllowSorting="True" AutoGenerateColumns="false"
                        CssClass="table table-bordered table-striped Datatable1" OnPreRender="grdUserDetails_PreRender" OnRowCommand="grdRoles_RowCommand" OnRowDataBound="grdRoles_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none" HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbteditRole" runat="server" CommandArgument='<%# Bind("ID") %>'
                                        CommandName="EIDIT" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblsystemID" runat="server" Text='<%# Bind("SystemID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="System Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblSystemName" runat="server" Text='<%# Bind("SystemName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Role Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblRoleName" runat="server" Text='<%# Bind("RoleName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Blinded/Unblinded">
                                <ItemTemplate>
                                    <asp:Label ID="lblBlined" runat="server" Text='<%# Bind("Blind") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="eSource SDR" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSDRSource" runat="server" CommandArgument='<%# Eval("CRA_eSource") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconSDR_eSource" runat="server" class="fa fa-check"></i></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="eSource ReadOnly" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblReadonlyeSource" runat="server" CommandArgument='<%# Eval("ReadOnly_eSource") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconReadOnly_eSource" runat="server" class="fa fa-check"></i></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="eSource Admin" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblAdmineSource" runat="server" CommandArgument='<%# Eval("Admin_eSource") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconAdmin_eSource" runat="server" class="fa fa-check"></i></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Medical Opinion Module" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblMedForm" runat="server" CommandArgument='<%# Eval("Med_FORM") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconMed_FORM" runat="server" class="fa fa-check"></i></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Medical Opinion Field" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblMedField" runat="server" CommandArgument='<%# Eval("Med_FIELD") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconMed_FIELD" runat="server" class="fa fa-check"></i></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SignOff Authority eSource" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSignofeSource" runat="server" CommandArgument='<%# Eval("Sign_eSource") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconSign_eSource" runat="server" class="fa fa-check"></i></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SignOff Authority Pharmacovigilance" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblsignofSaftey" runat="server" CommandArgument='<%# Eval("Sign_PV") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconSign_PV" runat="server" class="fa fa-check"></i></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SignOff Authority eCRF" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblsignofeCRF" runat="server" CommandArgument='<%# Eval("Sign_DM") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconSign_DM" runat="server" class="fa fa-check"></i></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail(this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtdeleteRole" runat="server" CommandArgument='<%# Bind("ID") %>'
                                        CommandName="DELETED" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this user role : ", Eval("RoleName")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
