<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_Emails.aspx.cs" Inherits="CTMS.UMT_Emails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
            <h3 class="box-title">Define Emails</h3>
            <div class="pull-right">
                <asp:LinkButton runat="server" ID="lbAssignINVUserExport" ToolTip="Export to Excel" OnClick="lbAssignINVUserExport_Click" Font-Size="12px" CssClass="btn btn-info" Style="margin-top: 3px; margin-bottom: 3px;" ForeColor="White">Export Emails&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>&nbsp;&nbsp;
            </div>
        </div>
        <div class="form-group">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <div class="box-body">
        <div class="rows">
            <div class="box box-primary" id="div11" runat="server">
                <div class="box-header">
                    <h3 class="box-title">User Activation / Deactivation</h3>
                    <div id="Div29" class="dropdown" runat="server" style="display: inline-flex">
                        <h3 class="box-title"></h3>
                    </div>
                </div>
                <div class="col-md-12">
                    <div>
                        <table class="table table-bordered table-striped" id="ActEmailds" style="border-collapse: collapse; width: 99%;">
                            <tbody>
                                <tr>
                                    <th scope="col">To Email IDs<br>
                                        (Use ',' in case of multiple)</th>
                                    <th scope="col">Cc Email IDs<br>
                                        (Use ',' in case of multiple)</th>
                                    <th scope="col">Bcc Email IDs<br>
                                        (Use ',' in case of multiple)</th>
                                </tr>
                                <tr>
                                    <td style="width: 30%;">
                                        <textarea rows="2" cols="20" id="txtTOACEMAILIDs" runat="server" class="form-control noSpace" style="width: 100%;"></textarea>
                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs" ValidationGroup="ActEmailds"
                                            ControlToValidate="txtTOACEMAILIDs" CssClass="lblError"
                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 30%;">
                                        <textarea rows="2" cols="20" id="txtCCACTEMAILIDs" runat="server" class="form-control noSpace" style="width: 100%;"></textarea>
                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ValidationGroup="ActEmailds"
                                            ControlToValidate="txtCCACTEMAILIDs" CssClass="lblError"
                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 30%;">
                                        <textarea rows="2" cols="20" id="txtBCCACEMAILIDs" runat="server" class="form-control noSpace" style="width: 100%;"></textarea>
                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2" ValidationGroup="ActEmailds"
                                            ControlToValidate="txtBCCACEMAILIDs" CssClass="lblError"
                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="row">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                            <div class="col-md-12">
                                <div class="col-md-5">
                                    &nbsp;  &nbsp;
                                     <br />
                                    <br />
                                </div>
                                <div class="col-md-7">
                                    <asp:Button ID="btnSubmitUserActEmails" Text="Submit" runat="server" ValidationGroup="ActEmailds" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitUserActEmails_Click" />&nbsp;&nbsp;
                                </div>
                            </div>
                </div>
            </div>
        </div>
        <br />
    </div>
    <div class="box-body">
        <div class="rows">
            <div class="box box-primary" id="div1" runat="server">
                <div class="box-header">
                    <h3 class="box-title">User Unlock</h3>
                </div>
                <div class="col-md-12">
                    <div>
                        <table class="table table-bordered table-striped" cellspacing="0" rules="all" border="1" id="UnlockEmailds" style="border-collapse: collapse; width: 99%; border-collapse: collapse;">
                            <tbody>
                                <tr>
                                    <th scope="col">To Email IDs<br>
                                        (Use ',' in case of multiple)</th>
                                    <th scope="col">Cc Email IDs<br>
                                        (Use ',' in case of multiple)</th>
                                    <th scope="col">Bcc Email IDs<br>
                                        (Use ',' in case of multiple)</th>
                                </tr>
                                <tr>
                                    <td style="width: 30%;">
                                        <textarea rows="2" cols="20" id="txtTOUNLOCKEMAILIDs" runat="server" class="form-control noSpace" style="width: 100%;"></textarea>
                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator3" ValidationGroup="UnlockEmailds"
                                            ControlToValidate="txtTOUNLOCKEMAILIDs" CssClass="lblError"
                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 30%;">
                                        <textarea rows="2" cols="20" id="txtCCUNLOCKEMAILIDs" runat="server" class="form-control noSpace" style="width: 100%;"></textarea>
                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator4" ValidationGroup="UnlockEmailds"
                                            ControlToValidate="txtCCUNLOCKEMAILIDs" CssClass="lblError"
                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 30%;">
                                        <textarea rows="2" cols="20" id="txtBCCUNLOCKEMAILIDs" runat="server" class="form-control noSpace" style="width: 100%;"></textarea>
                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator5" ValidationGroup="UnlockEmailds"
                                            ControlToValidate="txtBCCUNLOCKEMAILIDs" CssClass="lblError"
                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="row">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                            <div class="col-md-12">
                                <div class="col-md-5">
                                    &nbsp;  &nbsp;
                                     <br />
                                    <br />
                                </div>
                                <div class="col-md-7">
                                    <asp:Button ID="btnSubmitUserUnlockEmails" Text="Submit" runat="server" ValidationGroup="UnlockEmailds" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitUserUnlockEmails_Click" />&nbsp;&nbsp;
                                </div>
                            </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div class="box-body">
        <div class="rows">
            <div class="box box-primary" id="div2" runat="server">
                <div class="box-header">
                    <h3 class="box-title">Assign User Role</h3>
                </div>
                <div class="col-md-12">
                    <div>
                        <table class="table table-bordered table-striped" cellspacing="0" rules="all" border="1" id="RoleEmailds" style="border-collapse: collapse; width: 99%; border-collapse: collapse;">
                            <tbody>
                                <tr>
                                    <th scope="col">To Email IDs<br>
                                        (Use ',' in case of multiple)</th>
                                    <th scope="col">Cc Email IDs<br>
                                        (Use ',' in case of multiple)</th>
                                    <th scope="col">Bcc Email IDs<br>
                                        (Use ',' in case of multiple)</th>
                                </tr>
                                <tr>
                                    <td style="width: 30%;">
                                        <textarea rows="2" cols="20" id="txtTOROLEEMAILIDs" runat="server" class="form-control noSpace" style="width: 100%;"></textarea>
                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator6" ValidationGroup="RoleEmailds"
                                            ControlToValidate="txtTOROLEEMAILIDs" CssClass="lblError"
                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 30%;">
                                        <textarea rows="2" cols="20" id="txtCCROLEEMAILIDs" runat="server" class="form-control noSpace" style="width: 100%;"></textarea>
                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator7" ValidationGroup="RoleEmailds"
                                            ControlToValidate="txtCCROLEEMAILIDs" CssClass="lblError"
                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 30%;">
                                        <textarea rows="2" cols="20" id="txtBCCROLEEMAILIDs" runat="server" class="form-control noSpace" style="width: 100%;"></textarea>
                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator8" ValidationGroup="RoleEmailds"
                                            ControlToValidate="txtBCCROLEEMAILIDs" CssClass="lblError"
                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="row">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                            <div class="col-md-12">
                                <div class="col-md-5">
                                    &nbsp;  &nbsp;
                                     <br />
                                    <br />
                                </div>
                                <div class="col-md-7">
                                    <asp:Button ID="btnSubmitUserRoleEmails" Text="Submit" runat="server" ValidationGroup="RoleEmailds" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitUserRoleEmails_Click" />&nbsp;&nbsp;
                                </div>
                            </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div class="box-body">
        <div class="rows">
            <div class="box box-primary" id="div3" runat="server">
                <div class="box-header">
                    <h3 class="box-title">User Role Change</h3>
                </div>
                <div class="col-md-12">
                    <div>
                        <table class="table table-bordered table-striped" cellspacing="0" rules="all" border="1" id="RoleChangeEmailds" style="border-collapse: collapse; width: 99%; border-collapse: collapse;">
                            <tbody>
                                <tr>
                                    <th scope="col">To Email IDs<br>
                                        (Use ',' in case of multiple)</th>
                                    <th scope="col">Cc Email IDs<br>
                                        (Use ',' in case of multiple)</th>
                                    <th scope="col">Bcc Email IDs<br>
                                        (Use ',' in case of multiple)</th>
                                </tr>
                                <tr>
                                    <td style="width: 30%;">
                                        <textarea rows="2" cols="20" id="txtTOROLECHANGEEMAILIDs" runat="server" class="form-control noSpace" style="width: 100%;"></textarea>
                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator9" ValidationGroup="RoleChangeEmailds"
                                            ControlToValidate="txtTOROLECHANGEEMAILIDs" CssClass="lblError"
                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 30%;">
                                        <textarea rows="2" cols="20" id="txtCCROLECHANGEEMAILIDs" runat="server" class="form-control noSpace" style="width: 100%;"></textarea>
                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator10" ValidationGroup="RoleChangeEmailds"
                                            ControlToValidate="txtCCROLECHANGEEMAILIDs" CssClass="lblError"
                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 30%;">
                                        <textarea rows="2" cols="20" id="txtBCCROLECHANGEEMAILIDs" runat="server" class="form-control noSpace" style="width: 100%;"></textarea>
                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator11" ValidationGroup="RoleChangeEmailds"
                                            ControlToValidate="txtBCCROLECHANGEEMAILIDs" CssClass="lblError"
                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="row">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                            <div class="col-md-12">
                                <div class="col-md-5">
                                    &nbsp;  &nbsp;
                                     <br />
                                    <br />
                                </div>
                                <div class="col-md-7">
                                    <asp:Button ID="btnSubmitUserRoleChangeEmails" Text="Submit" runat="server" ValidationGroup="RoleChangeEmailds" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitUserRoleChangeEmails_Click" />&nbsp;&nbsp;
                                </div>
                            </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div class="box-body">
        <div class="rows">
            <div class="box box-primary" id="div5" runat="server">
                <div class="box-header">
                    <h3 class="box-title">Site Activation / Deactivation</h3>
                </div>
                <div class="col-md-12">
                    <div>
                        <table class="table table-bordered table-striped" cellspacing="0" rules="all" border="1" id="SiteACTEmailds" style="border-collapse: collapse; width: 99%; border-collapse: collapse;">
                            <tbody>
                                <tr>
                                    <th scope="col">To Email IDs<br>
                                        (Use ',' in case of multiple)</th>
                                    <th scope="col">Cc Email IDs<br>
                                        (Use ',' in case of multiple)</th>
                                    <th scope="col">Bcc Email IDs<br>
                                        (Use ',' in case of multiple)</th>
                                </tr>
                                <tr>
                                    <td style="width: 30%;">
                                        <textarea rows="2" cols="20" id="txtTOSITEACTEMAILIDs" class="form-control noSpace" runat="server" style="width: 100%;"></textarea>
                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator12" ValidationGroup="SiteACTEmailds"
                                            ControlToValidate="txtTOSITEACTEMAILIDs" CssClass="lblError"
                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 30%;">
                                        <textarea rows="2" cols="20" id="txtCCSITEACTMAILIDs" class="form-control noSpace" runat="server" style="width: 100%;"></textarea>
                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator13" ValidationGroup="SiteACTEmailds"
                                            ControlToValidate="txtCCSITEACTMAILIDs" CssClass="lblError"
                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 30%;">
                                        <textarea rows="2" cols="20" id="txtBCCSITEACTEMAILIDs" class="form-control noSpace" style="width: 100%;" runat="server"></textarea>
                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator14" ValidationGroup="SiteACTEmailds"
                                            ControlToValidate="txtBCCSITEACTEMAILIDs" CssClass="lblError"
                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="row">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                            <div class="col-md-12">
                                <div class="col-md-5">
                                    &nbsp;  &nbsp;
                                     <br />
                                    <br />
                                </div>
                                <div class="col-md-7">
                                    <asp:Button ID="btnSubmitSiteActEmails" Text="Submit" runat="server" ValidationGroup="SiteACTEmailds" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitSiteActEmails_Click" />&nbsp;&nbsp;
                    
                                </div>
                            </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div class="box-body">
        <div class="rows">
            <div class="box box-primary" id="div7" runat="server">
                <div class="box-header">
                    <h3 class="box-title">Re-Set Security Question</h3>
                </div>
                <div class="col-md-12">
                    <div>
                        <table class="table table-bordered table-striped" cellspacing="0" rules="all" border="1" id="SiteResetSecQuestEmailds" style="border-collapse: collapse; width: 99%; border-collapse: collapse;">
                            <tbody>
                                <tr>
                                    <th scope="col">To Email IDs<br>
                                        (Use ',' in case of multiple)</th>
                                    <th scope="col">Cc Email IDs<br>
                                        (Use ',' in case of multiple)</th>
                                    <th scope="col">Bcc Email IDs<br>
                                        (Use ',' in case of multiple)</th>
                                </tr>
                                <tr>
                                    <td style="width: 30%;">
                                        <textarea rows="2" cols="20" id="TxtTOSECUQUESTION" class="form-control noSpace" runat="server" style="width: 100%;"> </textarea>
                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator15" ValidationGroup="SiteResetSecQuestEmailds"
                                            ControlToValidate="TxtTOSECUQUESTION" CssClass="lblError"
                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 30%;">
                                        <textarea rows="2" cols="20" id="TxtCCSECUQUESTION" class="form-control noSpace" runat="server" style="width: 100%;"></textarea>
                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator16" ValidationGroup="SiteResetSecQuestEmailds"
                                            ControlToValidate="TxtCCSECUQUESTION" CssClass="lblError"
                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 30%;">
                                        <textarea rows="2" cols="20" id="TxtBCCSECUQUESTION" class="form-control noSpace" style="width: 100%;" runat="server"></textarea>
                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator17" ValidationGroup="SiteResetSecQuestEmailds"
                                            ControlToValidate="TxtBCCSECUQUESTION" CssClass="lblError"
                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="row">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                            <div class="col-md-12">
                                <div class="col-md-5">
                                    &nbsp;  &nbsp;
                                     <br />
                                    <br />
                                </div>
                                <div class="col-md-7">
                                    <asp:Button ID="btnSubSecQuestion" Text="Submit" runat="server" ValidationGroup="SiteResetSecQuestEmailds" CssClass="btn btn-primary btn-sm" OnClick="btnSubSecQuestion_Click" />&nbsp;&nbsp;
                                    
                                </div>
                            </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div class="box-body">
        <div class="rows">
            <div class="box box-primary" id="div4" runat="server">
                <div class="box-header">
                    <h3 class="box-title">Site User Approval </h3>
                </div>
                <div class="col-md-12">
                    <div>
                        <table class="table table-bordered table-striped" cellspacing="0" rules="all" border="1" id="SiteUserAppEmailds" style="border-collapse: collapse; width: 99%; border-collapse: collapse;">
                            <tbody>
                                <tr>
                                    <th scope="col">To Email IDs<br>
                                        (Use ',' in case of multiple)</th>
                                    <th scope="col">Cc Email IDs<br>
                                        (Use ',' in case of multiple)</th>
                                    <th scope="col">Bcc Email IDs<br>
                                        (Use ',' in case of multiple)</th>
                                </tr>
                                <tr>
                                    <td style="width: 30%;">
                                        <textarea rows="2" cols="20" id="txtTOSiteUserAppMAILIDs" class="form-control noSpace" runat="server" style="width: 100%;"></textarea>
                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator18" ValidationGroup="SiteUserAppEmailds"
                                            ControlToValidate="txtTOSiteUserAppMAILIDs" CssClass="lblError"
                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 30%;">
                                        <textarea rows="2" cols="20" id="txtCCSiteUserAppMAILIDs" class="form-control noSpace" runat="server" style="width: 100%;"></textarea>
                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator19" ValidationGroup="SiteUserAppEmailds"
                                            ControlToValidate="txtCCSiteUserAppMAILIDs" CssClass="lblError"
                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 30%;">
                                        <textarea rows="2" cols="20" id="txtBCCSiteUserAppMAILIDs" class="form-control noSpace" style="width: 100%;" runat="server"></textarea>
                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator20" ValidationGroup="SiteUserAppEmailds"
                                            ControlToValidate="txtBCCSiteUserAppMAILIDs" CssClass="lblError"
                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="row">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                            <div class="col-md-12">
                                <div class="col-md-5">
                                    &nbsp;  &nbsp;
                                     <br />
                                    <br />
                                </div>
                                <div class="col-md-7">
                                    <asp:Button ID="btnSiteUserApprovalEmails" Text="Submit" runat="server" ValidationGroup="SiteUserAppEmailds" CssClass="btn btn-primary btn-sm" OnClick="btnSiteUserApprovalEmails_Click" />&nbsp;&nbsp;
                                </div>
                            </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
