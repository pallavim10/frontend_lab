﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_SiteUser.aspx.cs" Inherits="CTMS.UMT_SiteUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="CommonFunctionsJs/UMT/UMT_ConfirmMsg.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">

    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": true,
                stateSave: false
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');

            $(".numeric").on("keypress keyup blur", function (event) {
                $(this).val($(this).val().replace(/[^\d].+/, ""));
                if ((event.which < 48 || event.which > 57)) {
                    event.preventDefault();
                }
            });

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
        }

        function showAuditTrail(element) {

            var ID = $(element).closest('tr').find('td').eq(0).text().trim();
            var TABLENAME = 'UMT_Users';

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
    <script type="text/javascript">
        $(document).ready(function () {
            $('.noSpace').keypress(function (e) {
                if (e.which === 32) {
                    return false;
                }
            });
        });
    </script>
    <script type="text/javascript" src="CommonFunctionsJs/UMT/UMT_Txt_Numeric.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Create Site User
            </h3>

        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    <asp:HiddenField ID="hdnID" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <div class="box box-primary">
        <div class="box-header">
            <h3 class="box-title">Site Users Details
            </h3>
            <div id="Div3" class="pull-right" runat="server" style="margin-top: 5px;">
                <asp:LinkButton runat="server" ID="lbSiteUsersDetailsExport" OnClick="lbSiteUsersDetailsExport_Click" CssClass="btn btn-info" Style="color: white;">
			        Export Site Users &nbsp;&nbsp; <span class="glyphicon glyphicon-download 2x"></span>
                </asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp;
            </div>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Site Id : &nbsp;
                             <asp:Label ID="Label5" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="drpSiteID" runat="server" AutoPostBack="true" CssClass="form-control required width250px" OnSelectedIndexChanged="drpSiteID_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="label col-md-2">
                            Study Role :&nbsp;
                            <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="drpStudyRole" runat="server" AutoPostBack="true" CssClass="form-control required width250px">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            First Name : &nbsp;
                            <asp:Label ID="Label6" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control required width250px"></asp:TextBox>
                        </div>
                        <div class="label col-md-2">
                            Last Name :&nbsp;
                            <asp:Label ID="Label4" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control required width250px"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Email Id : &nbsp;
                            <asp:Label ID="Label9" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtEmailid" CssClass="form-control noSpace required width250px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="regexEmailValid" ForeColor="Red" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmailid" ErrorMessage="Invalid Email"></asp:RegularExpressionValidator>
                        </div>
                        <div class="label col-md-2">
                            Contact No :&nbsp;
                            <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtContactNo" CssClass="form-control required numeric width250px"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Blinded/Unblinded : &nbsp;
                            <asp:Label ID="Label7" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList runat="server" ID="drpUnblind" CssClass="form-control width250px required" AutoPostBack="true" OnSelectedIndexChanged="drpUnblind_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                <asp:ListItem Value="Blinded" Text="Blinded"></asp:ListItem>
                                <asp:ListItem Value="Unblinded" Text="Unblinded"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="label col-md-2">
                            Is this Principal Investigator:&nbsp;
                            <asp:Label ID="Label8" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList runat="server" ID="drpSITEPI" CssClass="form-control width250px required">
                                <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                <asp:ListItem Value="No" Text="No"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Notes (If any):
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtNotes" CssClass="form-control width250px" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Select Systems & Privileges:
                                <asp:Label ID="Label3" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-8">
                            <asp:UpdatePanel runat="server" ID="upnlSystems">
                                <ContentTemplate>
                                    <table class="table table-bordered table-striped">
                                        <tr>
                                            <th class="col-md-4">Systems
                                            </th>
                                            <th class="col-md-4">Privileges
                                            </th>
                                            <th class="col-md-4">Notes (If any)
                                            </th>
                                        </tr>
                                        <asp:Repeater runat="server" ID="repeatSystem" OnItemDataBound="repeatSystem_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="col-md-4">
                                                        <asp:CheckBox ID="ChkSelect" runat="server" OnCheckedChanged="ChkSelect_CheckedChanged" AutoPostBack="true" />
                                                        &nbsp;
                                                        <asp:Label ID="lblSystemName" runat="server" Text='<%# Bind("SystemName") %>'></asp:Label>
                                                        <asp:Label ID="lblSystemID" runat="server" Text='<%# Bind("SystemID") %>' Visible="false"></asp:Label>
                                                        <asp:HiddenField runat="server" ID="HiddenSubSytem" Value='<%# Eval("SubSystem") %>' />
                                                    </td>
                                                    <td class="col-md-4">
                                                        <div runat="server" id="divSubsysIWRS" visible="false">
                                                            <asp:CheckBox ID="ChkSubsysIWRS" runat="server" />
                                                            &nbsp;
                                                            <asp:Label ID="lblSubsystemIWRS" runat="server" Text='Unblinding'></asp:Label>
                                                            <br />
                                                        </div>
                                                        <div runat="server" id="divSubSysPharma" visible="false">
                                                            <asp:CheckBox ID="ChkSubSysPharma" runat="server" />
                                                            &nbsp;
                                                           <asp:Label ID="lblSubSysPharma" runat="server" Text='Sign-Off'></asp:Label>
                                                            <br />
                                                        </div>
                                                        <div runat="server" id="divsubsysDM" visible="false">
                                                            <asp:CheckBox ID="chksubsysDM" runat="server" />
                                                            &nbsp;
                                                            <asp:Label ID="LblsubsysDM" runat="server" Text='Sign-Off'></asp:Label>
                                                            <br />
                                                        </div>
                                                        <div runat="server" id="divsubsyseSource" visible="false">
                                                            <asp:CheckBox ID="ChksubsyseSource" runat="server" />
                                                            &nbsp;
                                                            <asp:Label ID="lblsubsyseSource" runat="server" Text='Sign-Off'></asp:Label>
                                                            <br />
                                                            <asp:CheckBox ID="chlReadOnlyeSource" runat="server" />
                                                            &nbsp;
                                                            <asp:Label ID="LblReadOnly" runat="server" Text='Read-Only'></asp:Label>
                                                        </div>
                                                    </td>
                                                    <td class="col-md-4">
                                                        <asp:TextBox runat="server" ID="txtSystemNotes" CssClass="form-control" Width="100%" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <br />
                <div class="form-group">
                    <br />
                    <div class="row txt_center">
                        <asp:LinkButton runat="server" ID="lbtnSubmit" Text="Submit" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnSubmit_Click"></asp:LinkButton>
                        &nbsp;&nbsp;
                                <asp:LinkButton runat="server" ID="lbnUpdate" Text="Update" ForeColor="White" CssClass="btn btn-primary btn-sm" OnClick="lbnUpdate_Click" Visible="false"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm" OnClick="lbtnCancel_Click"></asp:LinkButton>
                    </div>
                    <br />
                </div>
            </div>
        </div>
        <div class="rows">
            <div style="width: 100%; overflow: auto;">
                <div>
                    <asp:GridView ID="grdSiteUser" runat="server" AllowSorting="True" AutoGenerateColumns="false"
                        CssClass="table table-bordered table-striped Datatable" OnPreRender="grdUserDetails_PreRender" OnRowCommand="grdUser_RowCommand" OnRowDataBound="grdSiteUser_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none" HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtedituser" runat="server" CommandArgument='<%# Bind("ID") %>'
                                        CommandName="EIDIT" ToolTip="Edit"><i class="fa fa-edit" style="font-size:15px"></i></asp:LinkButton>&nbsp;&nbsp;
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserID" runat="server" Text='<%# Bind("UserID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Site ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SiteID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Study Role">
                                <ItemTemplate>
                                    <asp:Label ID="lblStudyRole" runat="server" Text='<%# Bind("StudyRole") %>'></asp:Label>
                                    <asp:HiddenField ID="HiddenActive" runat="server" Value='<%# Eval("ACTIVE") %>' />
                                    <asp:HiddenField ID="HiddenLOCK" runat="server" Value='<%# Eval("LOCK") %>' />
                                    <asp:HiddenField ID="HiddenSECURITY" runat="server" Value='<%# Eval("SECURITY") %>' />
                                    <asp:HiddenField ID="ACTIVATION_Pending" runat="server" Value='<%# Eval("ACTIVATION_Pending") %>' />
                                    <asp:HiddenField ID="DEACTIVATION_Pending" runat="server" Value='<%# Eval("DEACTIVATION_Pending") %>' />
                                    <asp:HiddenField ID="UNLOCK_Pending" runat="server" Value='<%# Eval("UNLOCK_Pending") %>' />
                                    <asp:HiddenField ID="RESET_PASSWORD_Pending" runat="server" Value='<%# Eval("RESET_PASSWORD_Pending") %>' />
                                    <asp:HiddenField ID="SECURITY_QUESTION_Pending" runat="server" Value='<%# Eval("SECURITY_QUESTION_Pending") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("User_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmailID" runat="server" Text='<%# Bind("EmailID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contact No">
                                <ItemTemplate>
                                    <asp:Label ID="lblContactNo" runat="server" Text='<%# Bind("ContactNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Blinded/Unblinded">
                                <ItemTemplate>
                                    <asp:Label ID="lblBlinded" runat="server" Text='<%# Bind("Blind") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Principal Investigator">
                                <ItemTemplate>
                                    <asp:Label ID="lblSITE_PI" runat="server" Text='<%# Bind("SITE_PI") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Notes">
                                <ItemTemplate>
                                    <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Review Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblReviewStatus" runat="server" Text='<%# string.IsNullOrEmpty(Eval("ReviewAct").ToString()) ? "Pending" : Eval("ReviewAct") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Review Comment">
                                <ItemTemplate>
                                    <asp:Label ID="lblReviewComment" runat="server" Text='<%# Bind("REVIEWCOMM") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Activation/Deactivation" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtActive" runat="server" CommandArgument='<%# Bind("ID") %>'
                                        CommandName="DEACTIVATION" ToolTip="Deactivate"><i class="fa fa-toggle-on" style='color: #333;font-size: 20px' ></i></asp:LinkButton>
                                    <asp:LinkButton ID="lbtDeactive" runat="server" CommandArgument='<%# Bind("ID") %>'
                                        CommandName="ACTIVATION" ToolTip="Activate"><i class="fa fa-toggle-off" style='color: #333;font-size: 20px' ></i></asp:LinkButton>

                                    <asp:LinkButton ID="lbtnReqforActi" runat="server" Enabled="false"
                                        ToolTip="Request For Activation"><i class="fa fa-toggle-on" style='color: blue;font-size: 20px' ></i></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnReqForDactive" runat="server" Enabled="false"
                                        ToolTip="Request For Deactivation"><i class="fa fa-toggle-off" style='color: blue;font-size: 20px' ></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lock/Unlock" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtLock" runat="server" CommandArgument='<%# Bind("ID") %>'
                                        CommandName="LOCK" ToolTip="Request for Unlock"><i class="fa fa-lock" style='color: #333;font-size: 20px' ></i></asp:LinkButton>
                                    <asp:LinkButton ID="lbtUnlock" runat="server" Enabled="false" ToolTip="Request Generated"><i class="fa fa-unlock" style='color: blue;font-size: 20px'></i></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Resend Password" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbresendPassword" runat="server" CommandArgument='<%# Bind("ID") %>'
                                        CommandName="Resend_Password" ToolTip="Request for Password"><i class="fa fa-envelope" style='color: #333;font-size: 20px' ></i></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnGenReqPass" runat="server" Enabled="false"
                                        ToolTip="Request Generated for Password"><i class="fa fa-envelope" style='color: blue;font-size: 20px' ></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reset Security Question" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblResetQuestion" runat="server" CommandArgument='<%# Bind("ID") %>'
                                        CommandName="ReSet_Question" ToolTip="Request to reset security question"><i class="fa fa-question-circle" style='color: #333;font-size: 20px' ></i></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnGenReqSecQues" runat="server" Enabled="false"
                                        ToolTip="Request for Security Question"><i class="fa fa-question-circle" style='color: blue;font-size: 20px' ></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail(this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size: 20px"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtdeleteuser" runat="server" CommandArgument='<%# Bind("ID") %>'
                                        CommandName="DELETED" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this site user  : ", Eval("Fname") +" "+ Eval("Lname")) %>' ToolTip="Delete"><i class="fa fa-trash-o" style="font-size: 20px"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

