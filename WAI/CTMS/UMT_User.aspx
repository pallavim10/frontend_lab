<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_User.aspx.cs" Inherits="CTMS.UMT_User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="CommonFunctionsJs/UMT/UMT_ConfirmMsg.js"></script>
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>

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
            $(".Datatable1").dataTable({
                "bSort": false, "ordering": false,
                "bDestroy": true, stateSave: false
            });

            $(".Datatable1").parent().parent().addClass('fixTableHead');

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

            $('.select').select2();
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Create Internal Users
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
            <h3 class="box-title">Enter User Details
            </h3>
            <div id="Div3" class="pull-right" runat="server" style="margin-top: 5px;">
                <asp:LinkButton runat="server" ID="lblUserDetailsExport" OnClick="lblUserDetailsExport_Click" CssClass="btn btn-info" Style="color: white;">
                        Export Internal User &nbsp;&nbsp; <span class="glyphicon glyphicon-download 2x"></span>
                </asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp;
            </div>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            First Name : &nbsp;
                                    <asp:Label ID="Label5" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control required width250px"></asp:TextBox>
                        </div>
                        <div class="label col-md-2">
                            Last Name :&nbsp;
                                    <asp:Label ID="Label6" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
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
                            <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ForeColor="Red" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmailid" ErrorMessage="Invalid Email"></asp:RegularExpressionValidator>
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
                            <asp:DropDownList runat="server" ID="drpUnblind" CssClass="form-control width250px required">
                                <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                <asp:ListItem Value="Blinded" Text="Blinded"></asp:ListItem>
                                <asp:ListItem Value="Unblinded" Text="Unblinded"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="label col-md-2">
                            Study Role :&nbsp;
                                    <asp:Label ID="Label14" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
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
                            TimeZone :&nbsp;
                                <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlTimeZone" runat="server" class="form-control drpControl width250px required select" SelectionMode="Single">
                            </asp:DropDownList>
                        </div>
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
                                                            <asp:CheckBox ID="chksubsysFreeze" runat="server" />
                                                            &nbsp;
                                                            <asp:Label ID="lblsubsysFreeze" runat="server" Text='Freeze'></asp:Label>
                                                            <br />
                                                            <asp:CheckBox ID="chksubsysUnFreeze" runat="server" />
                                                            &nbsp;
                                                            <asp:Label ID="lblsubsysUNFreeze" runat="server" Text='UnFreeze'></asp:Label>
                                                            <br />
                                                            <asp:CheckBox ID="chksubsysLock" runat="server" />
                                                            &nbsp;
                                                            <asp:Label ID="lblsubsysLock" runat="server" Text='Lock'></asp:Label>
                                                            <br />
                                                            <asp:CheckBox ID="chksubsysUnlock" runat="server" />
                                                            &nbsp;
                                                            <asp:Label ID="lblsubsysunLock" runat="server" Text='Unlock'></asp:Label>
                                                            <br />
                                                        </div>
                                                        <div runat="server" id="divsubsyseSource" visible="false">
                                                            <asp:CheckBox ID="ChksubsysSourceDataReview" runat="server" />
                                                            &nbsp;
                                                            <asp:Label ID="lblsubsysSourceDataReview" runat="server" Text='Source Data Review'></asp:Label>
                                                            <br />
                                                            <asp:CheckBox ID="chlReadOnlyeSource" runat="server" />
                                                            &nbsp;
                                                            <asp:Label ID="LblReadOnly" runat="server" Text='Read-Only'></asp:Label>
                                                        </div>
                                                        <div runat="server" id="divsubsysDesignbench" visible="false">
                                                            <asp:CheckBox ID="ChksubDesignbench" runat="server" />
                                                            &nbsp;
                                                            <asp:Label ID="lblDesigner" runat="server" Text='Designer'></asp:Label>
                                                            <br />
                                                            <asp:CheckBox ID="ChksubApprover" runat="server" />
                                                            &nbsp;
                                                            <asp:Label ID="lblApprover" runat="server" Text='Approver'></asp:Label>
                                                        </div>
                                                        <div runat="server" id="divsubsyseTMF" visible="false">
                                                            <asp:CheckBox ID="chkArchivist" runat="server" />
                                                            &nbsp;
                                                            <asp:Label ID="lblchkArchivist" runat="server" Text='Archivist'></asp:Label>
                                                            <br />
                                                            <asp:CheckBox ID="chkConfiSpecialist" runat="server" />
                                                            &nbsp;
                                                            <asp:Label ID="lblConfiSpecialist" runat="server" Text='Configuration Specialist'></asp:Label>
                                                            <br />
                                                            <asp:CheckBox ID="ChkDocuSpecialist" runat="server" />
                                                            &nbsp;
                                                            <asp:Label ID="lblDocuSpecialist" runat="server" Text='Document Specialist'></asp:Label>
                                                            <br />
                                                            <asp:CheckBox ID="chkContributor" runat="server" />
                                                            &nbsp;
                                                            <asp:Label ID="lblContributor" runat="server" Text='Contributor'></asp:Label>
                                                            <br />
                                                            <asp:CheckBox ID="ChkQC" runat="server" />
                                                            &nbsp;
                                                            <asp:Label ID="lblQC" runat="server" Text='QC'></asp:Label>
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
                        &nbsp;&nbsp;&nbsp;
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
                    <asp:GridView ID="grdUser" runat="server" AllowSorting="True" AutoGenerateColumns="false"
                        CssClass="table table-bordered table-striped Datatable1" OnPreRender="grdUserDetails_PreRender" OnRowCommand="grdUser_RowCommand" OnRowDataBound="grdUser_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtedituser" runat="server" CommandArgument='<%# Bind("ID") %>'
                                        CommandName="EIDIT" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserID" runat="server" Text='<%# Bind("UserID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" Name ">
                                <ItemTemplate>
                                    <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("Fname") +" "+ Eval("Lname") %>'></asp:Label>
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
                            <asp:TemplateField HeaderText="Study Role">
                                <ItemTemplate>
                                    <asp:Label ID="lblStudyRole" runat="server" Text='<%# Bind("StudyRole") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TimeZone">
                                <ItemTemplate>
                                    <asp:Label ID="lblTimeZole" runat="server" Text='<%# Bind("Timezone") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Notes">
                                <ItemTemplate>
                                    <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail(this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtdeleteuser" runat="server" CommandArgument='<%# Bind("ID") %>'
                                        CommandName="DELETED" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this User : ", Eval("Fname") +" "+ Eval("Lname") ) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
