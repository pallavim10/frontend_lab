<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_Emails.aspx.cs" Inherits="SpecimenTracking.UMT_Emails" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        function preventSpaces(event) {
            if (event.keyCode === 32) {
                event.preventDefault();
            }
        }
    </script>
    <style>
        .lblError {
            color: red;
        }
    </style>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Define Emails</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home </a></li>
                            <li class="breadcrumb-item active"><a href="UserManagementDashboard.aspx">User Management</a></li>
                            <li class="breadcrumb-item active">UMT Setup</li>
                            <li class="breadcrumb-item active">Define Emails</li>
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
                                <h3 class="card-title">User Activation / Deactivation</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="lbtnExport" runat="server" Font-Size="14px" Style="margin-top: 3px;" CssClass="btn btn-default" OnClick="lbAssignINVUserExport_Click" ForeColor="Black">Export Emails&nbsp;<span class="fas fa-download btn-xs"></span></asp:LinkButton>
                                    &nbsp;&nbsp; 
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                                
                            </div>
                            <div class="card-body">
                                <div class="rows">
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
                                                            <textarea rows="2" cols="20" id="txtTOACEMAILIDs" runat="server" class="form-control" onkeypress="preventSpaces(event)" style="width: 100%;"></textarea>
                                                            <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs" ValidationGroup="ActEmailds"
                                                                ControlToValidate="txtTOACEMAILIDs" CssClass="lblError"
                                                                ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                                Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td style="width: 30%;">
                                                            <textarea rows="2" cols="20" id="txtCCACTEMAILIDs" runat="server" class="form-control" onkeypress="preventSpaces(event)" style="width: 100%;"></textarea>
                                                            <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ValidationGroup="ActEmailds"
                                                                ControlToValidate="txtCCACTEMAILIDs" CssClass="lblError"
                                                                ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                                Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>

                                                        </td>
                                                        <td style="width: 30%;">
                                                            <textarea rows="2" cols="20" id="txtBCCACEMAILIDs" runat="server" class="form-control" onkeypress="preventSpaces(event)" style="width: 100%;"></textarea>
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
                                    <center>
                                        <asp:Button ID="btnSubmitUserActEmails" Text="Submit" runat="server" ValidationGroup="ActEmailds" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnSubmitUserActEmails_Click" />&nbsp;&nbsp;
                                    </center>
                                </div>

                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </section>
        <br />
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">User Unlock</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="lbtUnlockExport" runat="server" Font-Size="14px" Style="margin-top: 3px;" Visible="false" CssClass="btn btn-default" OnClick="lbtUnlockExport_Click"  ForeColor="Black">Export Emails&nbsp;<span class="fas fa-download btn-xs"></span></asp:LinkButton>
                                    &nbsp;&nbsp; 
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="rows">
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
                                                        <textarea rows="2" cols="20" id="txtTOUNLOCKEMAILIDs" runat="server" class="form-control" onkeypress="preventSpaces(event)" style="width: 100%;"></textarea>
                                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator3" ValidationGroup="UnlockEmailds"
                                                            ControlToValidate="txtTOUNLOCKEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>

                                                    </td>
                                                    <td style="width: 30%;">
                                                        <textarea rows="2" cols="20" id="txtCCUNLOCKEMAILIDs" runat="server" class="form-control" onkeypress="preventSpaces(event)" style="width: 100%;"></textarea>
                                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator4" ValidationGroup="UnlockEmailds"
                                                            ControlToValidate="txtCCUNLOCKEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>

                                                    </td>
                                                    <td style="width: 30%;">
                                                        <textarea rows="2" cols="20" id="txtBCCUNLOCKEMAILIDs" runat="server" class="form-control" onkeypress="preventSpaces(event)" style="width: 100%;"></textarea>
                                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator5" ValidationGroup="UnlockEmailds"
                                                            ControlToValidate="txtBCCUNLOCKEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>

                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <center>
                                        <asp:Button ID="btnSubmitUserUnlockEmails" Text="Submit" runat="server" ValidationGroup="UnlockEmailds" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnSubmitUserUnlockEmails_Click" />&nbsp;&nbsp;
                                    </center>
                                </div>

                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </section>
        <br />
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Assign User Role</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="lbtnAssUserRolExport" runat="server" Visible="false" Font-Size="14px" Style="margin-top: 3px;" CssClass="btn btn-default" OnClick="lbtnAssUserRolExport_Click" ForeColor="Black">Export Emails&nbsp;<span class="fas fa-download btn-xs"></span></asp:LinkButton>
                                    &nbsp;&nbsp; 
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="rows">
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
                                                        <textarea rows="2" cols="20" id="txtTOROLEEMAILIDs" runat="server" class="form-control" onkeypress="preventSpaces(event)" style="width: 100%;"></textarea>
                                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator6" ValidationGroup="RoleEmailds"
                                                            ControlToValidate="txtTOROLEEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>

                                                    </td>
                                                    <td style="width: 30%;">
                                                        <textarea rows="2" cols="20" id="txtCCROLEEMAILIDs" runat="server" class="form-control" onkeypress="preventSpaces(event)" style="width: 100%;"></textarea>
                                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator7" ValidationGroup="RoleEmailds"
                                                            ControlToValidate="txtCCROLEEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>

                                                    </td>
                                                    <td style="width: 30%;">
                                                        <textarea rows="2" cols="20" id="txtBCCROLEEMAILIDs" runat="server" class="form-control" onkeypress="preventSpaces(event)" style="width: 100%;"></textarea>
                                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator8" ValidationGroup="RoleEmailds"
                                                            ControlToValidate="txtBCCROLEEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>

                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <center>
                                        <asp:Button ID="btnSubmitUserRoleEmails" Text="Submit" runat="server" ValidationGroup="RoleEmailds" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnSubmitUserRoleEmails_Click" />&nbsp;&nbsp;
                                    </center>
                                </div>

                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </section>
        <br />
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">User Role Change</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="lbtnExpRolechaange" runat="server" Font-Size="14px" Visible="false" Style="margin-top: 3px;" CssClass="btn btn-default" OnClick="lbtnExpRolechaange_Click" ForeColor="Black">Export Emails&nbsp;<span class="fas fa-download btn-xs"></span></asp:LinkButton>
                                    &nbsp;&nbsp; 
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="rows">
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
                                                        <textarea rows="2" cols="20" id="txtTOROLECHANGEEMAILIDs" runat="server" class="form-control" onkeypress="preventSpaces(event)" style="width: 100%;"></textarea>
                                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator9" ValidationGroup="RoleChangeEmailds"
                                                            ControlToValidate="txtTOROLECHANGEEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>

                                                    </td>
                                                    <td style="width: 30%;">
                                                        <textarea rows="2" cols="20" id="txtCCROLECHANGEEMAILIDs" runat="server" class="form-control" onkeypress="preventSpaces(event)" style="width: 100%;"></textarea>
                                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator10" ValidationGroup="RoleChangeEmailds"
                                                            ControlToValidate="txtCCROLECHANGEEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>

                                                    </td>
                                                    <td style="width: 30%;">
                                                        <textarea rows="2" cols="20" id="txtBCCROLECHANGEEMAILIDs" runat="server" class="form-control" onkeypress="preventSpaces(event)" style="width: 100%;"></textarea>
                                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator11" ValidationGroup="RoleChangeEmailds"
                                                            ControlToValidate="txtBCCROLECHANGEEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>

                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <center>
                                        <asp:Button ID="btnSubmitUserRoleChangeEmails" Text="Submit" runat="server" ValidationGroup="RoleChangeEmailds" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnSubmitUserRoleChangeEmails_Click" />&nbsp;&nbsp;
                                
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </section>
        <br />
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Site Activation / Deactivation</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="lbtnSiteAcDeacExport" runat="server" Font-Size="14px" Visible="false" Style="margin-top: 3px;" CssClass="btn btn-default" OnClick="lbtnSiteAcDeacExport_Click" ForeColor="Black">Export Emails&nbsp;<span class="fas fa-download btn-xs"></span></asp:LinkButton>
                                    &nbsp;&nbsp; 
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="rows">
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
                                                        <textarea rows="2" cols="20" id="txtTOSITEACTEMAILIDs" class="form-control" onkeypress="preventSpaces(event)" runat="server" style="width: 100%;"></textarea>
                                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator12" ValidationGroup="SiteACTEmailds"
                                                            ControlToValidate="txtTOSITEACTEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>

                                                    </td>
                                                    <td style="width: 30%;">
                                                        <textarea rows="2" cols="20" id="txtCCSITEACTMAILIDs" class="form-control" onkeypress="preventSpaces(event)" runat="server" style="width: 100%;"></textarea>
                                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator13" ValidationGroup="SiteACTEmailds"
                                                            ControlToValidate="txtCCSITEACTMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>

                                                    </td>
                                                    <td style="width: 30%;">
                                                        <textarea rows="2" cols="20" id="txtBCCSITEACTEMAILIDs" class="form-control" onkeypress="preventSpaces(event)" style="width: 100%;" runat="server"></textarea>
                                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator14" ValidationGroup="SiteACTEmailds"
                                                            ControlToValidate="txtBCCSITEACTEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>

                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <center>
                                        <asp:Button ID="btnSubmitSiteActEmails" Text="Submit" runat="server" ValidationGroup="SiteACTEmailds" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnSubmitSiteActEmails_Click" />&nbsp;&nbsp;
                                
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </section>
        <br />
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Re-Set Security Question</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="lbtnReSetSecExport" runat="server" Font-Size="14px" Visible="false" Style="margin-top: 3px;" CssClass="btn btn-default" OnClick="lbtnReSetSecExport_Click" ForeColor="Black">Export Emails&nbsp;<span class="fas fa-download btn-xs"></span></asp:LinkButton>
                                    &nbsp;&nbsp; 
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="rows">
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
                                                        <textarea rows="2" cols="20" id="TxtTOSECUQUESTION" class="form-control" onkeypress="preventSpaces(event)" runat="server" style="width: 100%;"> </textarea>
                                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator15" ValidationGroup="SiteResetSecQuestEmailds"
                                                            ControlToValidate="TxtTOSECUQUESTION" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>

                                                    </td>
                                                    <td style="width: 30%;">
                                                        <textarea rows="2" cols="20" id="TxtCCSECUQUESTION" class="form-control" onkeypress="preventSpaces(event)" runat="server" style="width: 100%;"></textarea>
                                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator16" ValidationGroup="SiteResetSecQuestEmailds"
                                                            ControlToValidate="TxtCCSECUQUESTION" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>

                                                    </td>
                                                    <td style="width: 30%;">
                                                        <textarea rows="2" cols="20" id="TxtBCCSECUQUESTION" class="form-control" onkeypress="preventSpaces(event)" style="width: 100%;" runat="server"></textarea>
                                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator17" ValidationGroup="SiteResetSecQuestEmailds"
                                                            ControlToValidate="TxtBCCSECUQUESTION" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>

                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <center>
                                        <asp:Button ID="btnSubSecQuestion" Text="Submit" runat="server" ValidationGroup="SiteResetSecQuestEmailds" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnSubSecQuestion_Click" />&nbsp;&nbsp;
                                
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </section>
        <br />
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Site User Approval</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="lbtnSiteAppExport" runat="server" Font-Size="14px" Visible="false" Style="margin-top: 3px;" CssClass="btn btn-default" OnClick="lbtnSiteAppExport_Click"  ForeColor="Black">Export Emails&nbsp;<span class="fas fa-download btn-xs"></span></asp:LinkButton>
                                    &nbsp;&nbsp; 
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="rows">
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
                                                        <textarea rows="2" cols="20" id="txtTOSiteUserAppMAILIDs" class="form-control" onkeypress="preventSpaces(event)" runat="server" style="width: 100%;"></textarea>
                                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator18" ValidationGroup="SiteUserAppEmailds"
                                            ControlToValidate="txtTOSiteUserAppMAILIDs" CssClass="lblError"
                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>

                                                    </td>
                                                    <td style="width: 30%;">
                                                        <textarea rows="2" cols="20" id="txtCCSiteUserAppMAILIDs" class="form-control" onkeypress="preventSpaces(event)" runat="server" style="width: 100%;"></textarea>
                                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator19" ValidationGroup="SiteUserAppEmailds"
                                            ControlToValidate="txtCCSiteUserAppMAILIDs" CssClass="lblError"
                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>

                                                    </td>
                                                    <td style="width: 30%;">
                                                        <textarea rows="2" cols="20" id="txtBCCSiteUserAppMAILIDs" class="form-control" onkeypress="preventSpaces(event)" style="width: 100%;" runat="server"></textarea>
                                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator20" ValidationGroup="SiteUserAppEmailds"
                                            ControlToValidate="txtBCCSiteUserAppMAILIDs" CssClass="lblError"
                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <center>
                                        <asp:Button ID="btnSiteUserApprovalEmails" Text="Submit" runat="server" ValidationGroup="SiteUserAppEmailds" CssClass="btn btn-primary btn-sm cls-btnSave cls-btnSave" OnClick="btnSiteUserApprovalEmails_Click" />&nbsp;&nbsp;
                                
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
