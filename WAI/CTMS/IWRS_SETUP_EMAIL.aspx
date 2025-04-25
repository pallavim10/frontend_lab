<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IWRS_SETUP_EMAIL.aspx.cs" Inherits="CTMS.IWRS_SETUP_EMAIL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <style type="text/css">
        .select2-container .select2-selection--multiple {
            min-height: 60px;
            width: 950px;
        }

        .fixTableHead {
            overflow-y: auto;
            max-height: 350px;
            min-height: 300px;
        }
    </style>

    <script language="javascript" type="text/javascript">

        function pageLoad() {

            $('.select').select2();

        }

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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">Define Email</h3>
                </div>
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div36" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Emails Formate</h3>
                            </div>
                            <div class="rows">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-1">
                                            <label>
                                                To Email IDs:</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtEmailFormateIDS" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                runat="server" Text='<%# Bind("EMAILIDS") %>'>
                                            </asp:TextBox>
                                            <label>Note : (Use ',' in case of multiple)</label><br />
                                            <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                ControlToValidate="txtEmailFormateIDS" CssClass="lblError"
                                                ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                Text="Invalid Email ID(s)">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                        <div class="col-md-1">
                                            <label>
                                                Cc Email IDs:</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtCCEMAILFormateIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                runat="server" Text='<%# Bind("CCEMAILIDS") %>'>
                                            </asp:TextBox>
                                            <label>Note : (Use ',' in case of multiple)</label><br />
                                            <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                ControlToValidate="txtCCEMAILFormateIDs" CssClass="lblError"
                                                ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                Text="Invalid Email ID(s)">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                        <div class="col-md-1">
                                            <label>
                                                Bcc Email IDs:</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtBCCEMAILFormateIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                runat="server" Text='<%# Bind("BCCEMAILIDS") %>'>
                                            </asp:TextBox>
                                            <label>Note : (Use ',' in case of multiple)</label><br />
                                            <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                ControlToValidate="txtBCCEMAILFormateIDs" CssClass="lblError"
                                                ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                Text="Invalid Email ID(s)">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-1">
                                            <label>Apply to :</label>
                                        </div>
                                        <div class="col-md-10">
                                            <asp:ListBox ID="lstEmailFormate" AutoPostBack="true" runat="server" ValidationGroup="grdformate" CssClass="select"
                                                SelectionMode="Multiple">

                                                <asp:ListItem Text="Setup Review" Value="SETUP_REVIEW"></asp:ListItem>
                                                <asp:ListItem Text="Setup Unreview" Value="SETUP_UNREVIEW"></asp:ListItem>

                                                <asp:ListItem Text="Unblinding (Without Treatment Arm)" Value="UNBLIND"></asp:ListItem>
                                                <asp:ListItem Text="Unblinding (With Treatment Arm)" Value="UNBLINDTREAT"></asp:ListItem>

                                                <asp:ListItem Text="DCF Email" Value="DCF"></asp:ListItem>

                                                <asp:ListItem Text="Update Expiry" Value="GENERATE_EXPIRY_UPDATE"></asp:ListItem>
                                                <asp:ListItem Text="Approval And Reject Kit Expiry" Value="REQUEST_UPDATE_EXPIRY_STATUS"></asp:ListItem>

                                                <asp:ListItem Text="Central Level" Value="KIT_CENTRAL"></asp:ListItem>
                                                <asp:ListItem Text="Central Level - Quaratine" Value="KIT_CENTRAL_QUARATINE"></asp:ListItem>
                                                <asp:ListItem Text="Central Level - Block" Value="KIT_CENTRAL_BLOCK"></asp:ListItem>
                                                <asp:ListItem Text="Central Level - Damage" Value="KIT_CENTRAL_DAMAGE"></asp:ListItem>
                                                <asp:ListItem Text="Central Level - Return" Value="KIT_CENTRAL_RETURN"></asp:ListItem>
                                                <asp:ListItem Text="Central Level - Destroy" Value="KIT_CENTRAL_DESTROY"></asp:ListItem>
                                                <asp:ListItem Text="Central Level - Reject" Value="KIT_CENTRAL_REJECT"></asp:ListItem>
                                                <asp:ListItem Text="Central Level - Retention" Value="KIT_CENTRAL_RETENTION"></asp:ListItem>
                                                <asp:ListItem Text="Central Level - Expired" Value="KIT_CENTRAL_EXPIRED"></asp:ListItem>

                                                <asp:ListItem Text="Country Level" Value="KIT_COUNTRY"></asp:ListItem>
                                                <asp:ListItem Text="Country Level-Quaratine" Value="KIT_COUNTRY_QUARATINE"></asp:ListItem>
                                                <asp:ListItem Text="Country Level-Block" Value="KIT_COUNTRY_BLOCK"></asp:ListItem>
                                                <asp:ListItem Text="Country Level - Damage" Value="KIT_COUNTRY_DAMAGE"></asp:ListItem>
                                                <asp:ListItem Text="Country Level - Return" Value="KIT_COUNTRY_RETURN"></asp:ListItem>
                                                <asp:ListItem Text="Country Level - Destroy" Value="KIT_COUNTRY_DESTROY"></asp:ListItem>
                                                <asp:ListItem Text="Country Level - Reject" Value="KIT_COUNTRY_REJECT"></asp:ListItem>
                                                <asp:ListItem Text="Country Level - Retention" Value="KIT_COUNTRY_RETENTION"></asp:ListItem>
                                                <asp:ListItem Text="Country Level - Expired" Value="KIT_COUNTRY_EXPIRED"></asp:ListItem>

                                                <asp:ListItem Text="Site Level" Value="KIT_SITE"></asp:ListItem>
                                                <asp:ListItem Text="Site Level-Quaratine" Value="KIT_SITE_QUARATINE"></asp:ListItem>
                                                <asp:ListItem Text="Site Level-Block" Value="KIT_SITE_BLOCK"></asp:ListItem>
                                                <asp:ListItem Text="Site Level - Damage" Value="KIT_SITE_DAMAGE"></asp:ListItem>
                                                <asp:ListItem Text="Site Level - Return" Value="KIT_SITE_RETURN"></asp:ListItem>
                                                <asp:ListItem Text="Site Level - Destroy" Value="KIT_SITE_DESTROY"></asp:ListItem>
                                                <asp:ListItem Text="Site Level - Reject" Value="KIT_SITE_REJECT"></asp:ListItem>
                                                <asp:ListItem Text="Site Level - Retention" Value="KIT_SITE_RETENTION"></asp:ListItem>
                                                <asp:ListItem Text="Site Level - Expired" Value="KIT_SITE_EXPIRED"></asp:ListItem>

                                                <asp:ListItem Text="Back-Up Kit" Value="BakKit"></asp:ListItem>

                                            </asp:ListBox>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                                 <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubEmailFormate" Text="Submit" runat="server" ValidationGroup="grdformate" CssClass="btn btn-primary btn-sm" OnClick="btnSubEmailFormate_Click" />
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnCalEmailFormate" Text="Cancel" ValidationGroup="grdformate" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnCalEmailFormate_Click" />
                                            </div>
                                    </div>
                                </div>
                                 <br />
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </div>

            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div32" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Email For Setup Review</h3>
                            </div>
                            <div class="rows">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvEmailSetupReview" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 100%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'>
                                                        </asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs" ValidationGroup="gvEmailSetupReview"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)">
                                                        </asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'>
                                                        </asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs" ValidationGroup="gvEmailSetupReview"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)">
                                                        </asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'>
                                                        </asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs" ValidationGroup="gvEmailSetupReview"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)">
                                                        </asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                        <asp:Button ID="btnSubmitEmailSetupReview" Text="Submit" runat="server" ValidationGroup="gvEmailSetupReview" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitEmailSetupReview_Click" />
                                    </div>
                                        </div>
                                    </div>
                              
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div33" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Email For Setup Unreview</h3>
                            </div>
                            <div class="rows">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvEmailSetupUnreview" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 100%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'>
                                                        </asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs" ValidationGroup="gvEmailSetupUnreview"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)">
                                                        </asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'>
                                                        </asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs" ValidationGroup="gvEmailSetupUnreview"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)">
                                                        </asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'>
                                                        </asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs" ValidationGroup="gvEmailSetupUnreview"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)">
                                                        </asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmitEmailSetupUnreview" Text="Submit" runat="server" ValidationGroup="gvEmailSetupUnreview" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitEmailSetupUnreview_Click" />
                                         </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div17" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Unblinding (Without Treatment Arm) Email IDs</h3>
                            </div>
                            <div class="rows">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvUnblindEmailds" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 100%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Site ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'>
                                                        </asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs" ValidationGroup="gvUnblindEmailds"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)">
                                                        </asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'>
                                                        </asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs" ValidationGroup="gvUnblindEmailds"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)">
                                                        </asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'>
                                                        </asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs" ValidationGroup="gvUnblindEmailds"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)">
                                                        </asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmitUnblindEmails" Text="Submit" runat="server" ValidationGroup="gvUnblindEmailds" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitUnblindEmails_Click" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div21" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Unblinding (With Treatment Arm) Email IDs</h3>
                            </div>
                            <div class="rows">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvUnblindTreatEmailds" AutoGenerateColumns="False"
                                            CssClass="table table-bordered table-striped" Style="width: 100%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Site ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'>
                                                        </asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs" ValidationGroup="gvUnblindTreatEmailds"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)">
                                                        </asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'>
                                                        </asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs" ValidationGroup="gvUnblindTreatEmailds"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)">
                                                        </asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'>
                                                        </asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs" ValidationGroup="gvUnblindTreatEmailds"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)">
                                                        </asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmitTreatUnblindEmails" Text="Submit" runat="server" ValidationGroup="gvUnblindTreatEmailds" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitTreatUnblindEmails_Click" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>



            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div18" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define DCF Email IDs</h3>
                            </div>
                            <div class="rows">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvDCFEmailds" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Site ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'>
                                                        </asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs" ValidationGroup="gvDCFEmailds"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)">
                                                        </asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'>
                                                        </asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs" ValidationGroup="gvDCFEmailds"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)">
                                                        </asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'>
                                                        </asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs" ValidationGroup="gvDCFEmailds"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)">
                                                        </asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmitDCFEmails" Text="Submit" runat="server" ValidationGroup="gvDCFEmailds" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitDCFEmails_Click" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div34" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Email For Update Expiry</h3>
                            </div>
                            <div class="rows">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvEmailUpdateExpiry" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 100%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'>
                                                        </asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs" ValidationGroup="gvEmailUpdateExpiry"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)">
                                                        </asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'>
                                                        </asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs" ValidationGroup="gvEmailUpdateExpiry"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)">
                                                        </asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'>
                                                        </asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs" ValidationGroup="gvEmailUpdateExpiry"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)">
                                                        </asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmitEmailUpdateExpiry" Text="Submit" runat="server" ValidationGroup="gvEmailUpdateExpiry" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitEmailUpdateExpiry_Click" />
                                         </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div35" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Email For Approval And Reject Kit Expiry</h3>
                            </div>
                            <div class="rows">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvEmailAppRejExpiry" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 100%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'>
                                                        </asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs" ValidationGroup="gvEmailAppRejExpiry"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)">
                                                        </asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'>
                                                        </asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs" ValidationGroup="gvEmailAppRejExpiry"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)">
                                                        </asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'>
                                                        </asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs" ValidationGroup="gvEmailAppRejExpiry"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)">
                                                        </asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmitEmailAppRejExpiry" Text="Submit" runat="server" ValidationGroup="gvEmailAppRejExpiry" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitEmailAppRejExpiry_Click" />
                                         </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div3" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Kit Management Email IDs At Central Level</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvEmaildsCentral" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmitEmailCentral" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitEmailCentral_Click" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div6" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Kit Management Email IDs At Central Level - Quaratine</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvEmaildsCentralQuaratine" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmitCentralQuaratine" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitCentralQuaratine_Click" />
                                         </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div7" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Kit Management Email IDs At Central Level - Block</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvEmaildsCentralBlocked" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmitCentralBlocked" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitCentralBlocked_Click" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div8" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Kit Management Email IDs At Central Level - Damage</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvEmaildsCentralDamaged" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmitCentralDamaged" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitCentralDamaged_Click" />
                                          </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div9" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Kit Management Email IDs At Central Level - Return</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvEmaildsCentralReturned" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmitCentralReturned" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitCentralReturned_Click" />
                                         </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div10" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Kit Management Email IDs At Central Level - Destroy</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvEmaildsCentralDestroyed" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmitCentralDestroyed" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitCentralDestroyed_Click" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div11" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Kit Management Email IDs At Central Level - Reject</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvEmaildsCentralRejected" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmitCentralRejected" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitCentralRejected_Click" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div25" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Kit Management Email IDs At Central Level - Retention</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvEmaildsCentralRetention" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btncentralRetentionSubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btncentralRetentionSubmit_Click" />
                                         </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div29" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Kit Management Email IDs At Central Level - Expired</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvEmaildsCentralExpired" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmitCentralExpired" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitCentralExpired_Click" />
                                         </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div4" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Kit Management Email IDs At Country Level</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvEmaildsCountry" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Country ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmitEmailCountry" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitEmailCountry_Click" />
                                         </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div12" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Kit Management Email IDs At Country Level-Quaratine</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvEmaildsCountryQuaratine" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Country ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmitEmailCountryQuaratine" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitEmailCountryQuaratine_Click" />&nbsp;&nbsp;
                                         </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div13" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Kit Management Email IDs At Country Level-Block</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvEmaildsCountryBlock" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Country ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmitEmailCountryBlock" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitEmailCountryBlock_Click" />&nbsp;&nbsp;
                                         </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div14" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Kit Management Email IDs At Country Level - Damage</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvCountryDamage" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Country ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmitCountryDamage" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitCountryDamage_Click" />&nbsp;&nbsp;
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div15" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Kit Management Email IDs At Country Level-Return</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvCountryReturn" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Country ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmitCountryReturn" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitCountryReturn_Click" />&nbsp;&nbsp;
                                         </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div16" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Kit Management Email IDs At Country Level-Destroy</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvCountryDestroy" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Country ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmitDestroy" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitDestroy_Click" />&nbsp;&nbsp;
                                         </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div1" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Kit Management Email IDs At Country Level-Reject</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvCountryReject" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Country ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmitReject" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitReject_Click" />&nbsp;&nbsp;
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div26" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Kit Management Email IDs At Country Level - Retention</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvCountryRetention" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Country ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="BtnSubmitCountryRetention" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="BtnSubmitCountryRetention_Click" />&nbsp;&nbsp;
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div30" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Kit Management Email IDs At Country Level - Expired</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvCountryExpired" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Country ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="BtnSubmitCountryExpired" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="BtnSubmitCountryExpired_Click" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div5" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Kit Management Email IDs At Site Level</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvEmaildsSite" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Site ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmitEmailSite" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                OnClick="btnSubmitEmailSite_Click" />&nbsp;&nbsp;
                                          </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div2" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Kit Management Email IDs At Site Level - Quaratine</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvEmaildsSITEQuaratine" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Site ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmitSiteQuaratine" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitSiteQuaratine_Click" />&nbsp;&nbsp;
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div20" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Kit Management Email IDs At Site Level - Block</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvEmaildsSITEBlocked" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Site ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmutSiteBlock" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmutSiteBlock_Click" />&nbsp;&nbsp;
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div19" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Kit Management Email IDs At Site Level - Damage</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvEmaildsSITEDamaged" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Site ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="BtnSubmitSiteDamage" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="BtnSubmitSiteDamage_Click" />&nbsp;&nbsp;
                                         </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div22" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Kit Management Email IDs At Site Level - Retrun</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvEmaildsSITEReturned" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Site ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="BtnSubmitSiteReturn" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="BtnSubmitSiteReturn_Click" />&nbsp;&nbsp;
                                         </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div23" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Kit Management Email IDs At Site Level - Destroy</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvEmaildsSITEDestroyed" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Site ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="BtnSubmitSiteDestroy" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="BtnSubmitSiteDestroy_Click" />&nbsp;&nbsp;
                                         </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div24" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Kit Management Email IDs At Site Level - Reject</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvEmaildsSITERejected" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Site ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="BtnSubmitSiteReject" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="BtnSubmitSiteReject_Click" />&nbsp;&nbsp;
                                         </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div27" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Kit Management Email IDs At Site Level - Retention</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvEmaildsSiteRetention" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Site ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmitEmaildsSiteRetention" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitEmaildsSiteRetention_Click" />&nbsp;&nbsp;
                                         </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div31" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Kit Management Email IDs At Site Level - Expired</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvEmaildsSiteExpired" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Site ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmitEmaildsSiteExpired" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitEmaildsSiteExpired_Click" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>



            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div28" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Back-Up Kit Email IDs</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvBakEmailds" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Site ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                                                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexCCEMAILIDs"
                                                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="regexBCCEMAILIDs"
                                                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmitEmail" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                OnClick="btnSubmitEmail_Click" />
                                          </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmitUnblindEmails" />
            <asp:PostBackTrigger ControlID="btnSubmitTreatUnblindEmails" />
            <asp:PostBackTrigger ControlID="btnSubmitDCFEmails" />
            <asp:PostBackTrigger ControlID="btnSubmitEmailCentral" />
            <asp:PostBackTrigger ControlID="btnSubmitCentralQuaratine" />
            <asp:PostBackTrigger ControlID="btnSubmitCentralBlocked" />
            <asp:PostBackTrigger ControlID="btnSubmitCentralDamaged" />
            <asp:PostBackTrigger ControlID="btnSubmitCentralReturned" />
            <asp:PostBackTrigger ControlID="btnSubmitCentralDestroyed" />
            <asp:PostBackTrigger ControlID="btnSubmitCentralRejected" />
            <asp:PostBackTrigger ControlID="btnSubmitCentralExpired" />
            <asp:PostBackTrigger ControlID="btncentralRetentionSubmit" />
            <asp:PostBackTrigger ControlID="btnSubmitEmailCountry" />
            <asp:PostBackTrigger ControlID="btnSubmitEmailSetupReview" />
            <asp:PostBackTrigger ControlID="btnSubmitEmailSetupUnreview" />
            <asp:PostBackTrigger ControlID="btnSubEmailFormate" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
