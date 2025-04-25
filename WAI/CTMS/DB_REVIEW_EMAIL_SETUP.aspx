<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DB_REVIEW_EMAIL_SETUP.aspx.cs" Inherits="CTMS.DB_REVIEW_EMAIL_SETUP" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/CKEditor/ckeditor.js" type="text/javascript"></script>
    <script type="text/javascript" src="CommonFunctionsJs/btnSave_Required.js"></script>
       <script type="text/javascript" src="CommonFunctionsJS/TabIndex.js"></script>
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
  <%--  <script type="text/javascript" src="js/MaxLength.min.js"></script>
    <script type="text/javascript" src="js/MaxLength_Limit.js"></script>--%>
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true,
                fixedHeader: true
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title" align="left">Define Review Emails</h3>
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
        <div class="box">
            <div align="left" style="margin-left: 5px">
                <div>
                    <div class="rows">
                        <div class="fixTableHead">
                            <asp:GridView ID="grdSteps" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                OnRowCommand="grdSteps_RowCommand" OnPreRender="grdSteps_PreRender">
                                <Columns>
                                    <asp:TemplateField HeaderText="Edit" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnEdit" CommandArgument='<%# Bind("ID") %>' CommandName="EditStep"
                                                runat="server" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Activity">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHEADER" Text='<%# Bind("ACTIVITY") %>' runat="server"></asp:Label>
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
                                    <asp:TemplateField HeaderStyle-Width="20%" HeaderStyle-CssClass="align-left" ItemStyle-Width="20%">
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
                                    <asp:TemplateField HeaderStyle-Width="20%" HeaderStyle-CssClass="align-left" ItemStyle-Width="20%">
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
                                    <asp:TemplateField HeaderText="Delete" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnDelete" CommandArgument='<%# Bind("ID") %>' CommandName="DeleteStep"
                                                runat="server" ToolTip="Delete" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this Name : ", Eval("ACTIVITY")) %>'><i class="fa fa-trash-o"></i></asp:LinkButton>
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
    <div class="box box-primary">
        <div class="box-header">
            <h3 class="box-title">Add Steps</h3>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-2">
                    <label style="color: blue;">
                        Select Activity:</label>
                </div>
                <div class="col-md-10">
                    <asp:DropDownList ID="drpAction" runat="server" CssClass="form-control required" Style="width: 50%" AutoPostBack="true"
                        OnSelectedIndexChanged="drpAction_SelectedIndexChanged" TabIndex="1">
                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Send For Review">Send For Review</asp:ListItem>
                        <asp:ListItem Text="Open For Edit From Designer">Open For Edit From Designer</asp:ListItem>
                        <asp:ListItem Text="Send Back To Designer From Reviewer">Send Back To Designer From Reviewer</asp:ListItem>
                        <asp:ListItem Text="Reviewed">Reviewed</asp:ListItem>
                        <asp:ListItem Text="Send Back To Designer After Reviewed">Send Back To Designer After Reviewed</asp:ListItem>
                        <asp:ListItem Text="Frozen">Frozen</asp:ListItem>
                        <asp:ListItem Text="Generate Unfreeze Request">Generate Unfreeze Request</asp:ListItem>
                        <asp:ListItem Text="Approved Unfreeze Request">Approved Unfreeze Request</asp:ListItem>
                        <asp:ListItem Text="Disapproved Unfreeze Request">Disapproved Unfreeze Request</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <br />
        <div id="divEmail">
            <div class="row disp-none">
                <div class="col-md-12">
                    <div class="col-md-2">
                        <label style="color: blue;">
                            To Email IDs (Use ',' in case of multiple) :</label>
                    </div>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                            runat="server" Text='<%# Bind("EMAILIDS") %>' TabIndex="2"></asp:TextBox>
                        <asp:RegularExpressionValidator runat="server" ID="regexEMAILIDs"
                            ControlToValidate="txtEMAILIDs" CssClass="lblError"
                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-2">
                        <label style="color: blue;">
                            Cc Email IDs (Use ',' in case of multiple) :</label>
                    </div>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                            runat="server" Text='<%# Bind("CCEMAILIDS") %>' TabIndex="3"></asp:TextBox>
                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1"
                            ControlToValidate="txtCCEMAILIDs" CssClass="lblError"
                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-2">
                        <label style="color: blue;">
                            Bcc Email IDs (Use ',' in case of multiple) :</label>
                    </div>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                            runat="server" Text='<%# Bind("BCCEMAILIDS") %>' TabIndex="3"></asp:TextBox>
                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2"
                            ControlToValidate="txtBCCEMAILIDs" CssClass="lblError"
                            ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                            Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-2">
                        <label style="color: blue;">
                            Email Subject :</label>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox runat="server" ID="txtEmailSubject" Width="895px" CssClass="form-control required" TabIndex="4">                             
                        </asp:TextBox>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-2">
                        <label style="color: blue;">
                            Email Body :</label>
                    </div>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="txtEmailBody" CssClass="ckeditor required" MaxLength="1000" Height="50%" TextMode="MultiLine"
                            Width="99%" TabIndex="5"></asp:TextBox>
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
                        CssClass="btn btn-danger btn-sm" OnClick="lbtnCancel_Click" />
            </div>
        </div>
        <br />
    </div>
</asp:Content>
