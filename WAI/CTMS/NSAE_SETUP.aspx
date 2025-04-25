<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NSAE_SETUP.aspx.cs" Inherits="CTMS.NSAE_SETUP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/CKEditor/ckeditor.js" type="text/javascript"></script>
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/SAE/SAE_ConfirmMsg.js"></script>
    <script src="CommonFunctionsJs/SAE/SAE_showAuditTrail.js"></script>
    <%--<script src="CommonFunctionsJs/btnSave_Required.js"></script>--%>
    <script type="text/javascript">
        CKEDITOR.config.toolbar = [
            ['Bold', 'Italic', 'Underline', 'StrikeThrough', '-', 'Undo', 'Redo', '-', 'Outdent', 'Indent', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
            ['Styles', 'Format', 'Font', 'FontSize']
        ];

        CKEDITOR.config.height = 250;

        function CallCkedit() {

            CKEDITOR.replace("MainContent_txtEmailBody");

        }
    </script>
    <script type="text/javascript">
        $(document).on("click", ".cls-btnSave", function () {
            var test = "0";

            $('.required').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "-1" || value == null || value == "-Select-" || value == "--Select--" || value == "0") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    if (value == "") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
            });

            if (test == "1") {
                return false;
            }
            return true;
        });
    </script>
    <script>
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": false, "ordering": false,
                "bDestroy": true, stateSave: true
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title" align="left">Define Email Workflow</h3>
        </div>
        <div class="form-group">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <div class="box box-primary">
        <div class="box-header">
            <h4 class="box-title" align="left">Added Email Steps
            </h4>
        </div>
        <div class="rows">
            <asp:GridView ID="grdSteps" runat="server" Width="98%" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                OnRowCommand="grdSteps_RowCommand" OnPreRender="grdSteps_PreRender">
                <Columns>
                    <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnEdit" CommandArgument='<%# Bind("ID") %>' CommandName="EditStep"
                                runat="server" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('SAE_SETUP_EMAILS', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnDelete" CommandArgument='<%# Bind("ID") %>' CommandName="DeleteStep"
                                runat="server" ToolTip="Delete" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this Name : ", Eval("ACTIONS")) %>'>
                            <i class="fa fa-trash-o"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label ID="lblHEADER" Text='<%# Bind("ACTIONS") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Site Ids">
                        <ItemTemplate>
                            <asp:Label ID="INVIDS" Text='<%# Bind("INVIDS") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email Subject">
                        <ItemTemplate>
                            <asp:Label ID="EMAIL_SUBJECT" Text='<%# Bind("EMAIL_SUBJECT") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email Body">
                        <ItemTemplate>
                            <asp:Label ID="EMAIL_BODY" Text='<%# Bind("EMAIL_BODY") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                        <HeaderTemplate>
                            <label>Entered By Details</label><br />
                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Entered By]</label><br />
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
                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                        <HeaderTemplate>
                            <label>Last Modified Details</label><br />
                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Modified By]</label><br />
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
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div class="box box-primary">
        <div class="box-header">
            <h3 class="box-title">Add Steps</h3>
        </div>
        <div class="rows">
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-2">
                        <label>
                            Select Activity:</label>
                    </div>
                    <div class="col-md-10">
                        <asp:DropDownList ID="drpAction" runat="server" CssClass="form-control required" Style="width: 50%"
                            AutoPostBack="true" OnSelectedIndexChanged="drpAction_SelectedIndexChanged">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Text="Log New SAE">Log New SAE</asp:ListItem>
                            <asp:ListItem Text="Initial; Complete">Initial; Complete</asp:ListItem>
                            <asp:ListItem Text="Updated Initial; Complete">Updated Initial; Complete</asp:ListItem>
                            <asp:ListItem Text="Follow up">Follow up</asp:ListItem>
                            <asp:ListItem Text="Closed">Closed</asp:ListItem>
                            <asp:ListItem Text="Generate Re-Open Request">Generate Re-Open Request</asp:ListItem>
                            <asp:ListItem Text="Re-Open Request (Approve/Disapprove)">Re-Open Request (Approve/Disapprove)</asp:ListItem>
                            <asp:ListItem Text="Generate Downgrading SAE Request">Generate Downgrading SAE Request</asp:ListItem>
                            <asp:ListItem Text="Downgrading SAE Request (Approve/Disapprove)">Downgrading SAE Request (Approve/Disapprove)</asp:ListItem>
                            <asp:ListItem Text="Generate Restore SAE Request">Generate Restore SAE Request</asp:ListItem>
                            <asp:ListItem Text="Restore SAE Request (Approve/Disapprove)">Restore SAE Request (Approve/Disapprove)</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <br />
            <div id="divEmail">
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-7">
                            <label>
                                Email IDs :</label>
                            <asp:HiddenField ID="hdnStepID" runat="server" />
                            <asp:GridView runat="server" ID="gvEmailds" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                Style="border-collapse: collapse;">
                                <Columns>
                                    <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Site ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                            <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs" ValidationGroup="gvUnblindEmailds"
                                                ControlToValidate="txtEMAILIDs" CssClass="lblError"
                                                ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                            <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs2" ValidationGroup="gvUnblindEmailds"
                                                ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                                                ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                            <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs3" ValidationGroup="gvUnblindEmailds"
                                                ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                                                ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="col-md-5">
                            <div class="rows">
                                <div class="row">
                                    <label>
                                        Email Subject :</label>
                                    <asp:TextBox runat="server" ID="txtEmailSubject" Height="50px" Width="99%" TextMode="MultiLine"
                                        CssClass="form-control"> 
                                    </asp:TextBox>
                                </div>
                                <br />
                                <div class="row">
                                    <label>
                                        Email Body :</label>
                                    <asp:TextBox runat="server" ID="txtEmailBody" CssClass="ckeditor" Height="50%" TextMode="MultiLine"
                                        Width="99%"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
            </div>
            <div class="form-group">
                <div class="row txt_center">
                    <asp:LinkButton ID="lbtnsubmit" Text="Submit" runat="server" Style="color: white;"
                        CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnsubmit_Click" />
                    <asp:LinkButton ID="lbtnUpdate" Text="Update" runat="server" Style="color: white;"
                        Visible="false" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnUpdate_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lbtnCancel" Text="Cancel" runat="server" Style="color: white;"
                        CssClass="btn btn-primary btn-sm" OnClick="lbtnCancel_Click" />
                </div>
            </div>
            <br />
        </div>
    </div>
</asp:Content>
